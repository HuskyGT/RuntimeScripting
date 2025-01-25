using BepInEx;
using Photon.Pun;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RuntimeScripting
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool reloadButtonVisible = true;

        void Awake()
        {
            if (!File.Exists(Constants.scriptingFolderPath))
            {
                Directory.CreateDirectory(Constants.scriptingFolderPath);
            }
            if (!File.Exists(Constants.scriptPath))
            {
                File.WriteAllText(Constants.scriptPath, "print(\"Hello, World!\")");
            }
        }

        void Update()
        {
            if (Keyboard.current.gKey.wasPressedThisFrame)
            {
                reloadButtonVisible = !reloadButtonVisible;
            }
        }

        void OnGUI()
        {
            if (!reloadButtonVisible)
                return;

            if (!PhotonNetwork.InRoom || PhotonNetwork.PlayerListOthers?.Length != 0)
                return;

            if (!GUILayout.Button("Reload Map Script\nPress G To Hide This"))
                return;

            ReloadScript();
        }

        void ReloadScript()
        {
            Debug.Log("Reloading Map Script...");
            var script = File.ReadAllText(Constants.scriptPath) ?? "";
            if (script == "")
                return;

            if (!CustomGameMode.GameModeInitialized)
                return;

            if (CustomGameMode.gameScriptRunner != null)
            {
                CustomGameMode.StopScript();
            }
            CustomGameMode.LuaScript = script;
            CustomGameMode.LuaStart();
        }
    }
}
