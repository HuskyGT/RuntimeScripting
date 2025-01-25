﻿using BepInEx;
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
        bool autoReload = false;
        void Awake()
        {
            if (!File.Exists("BepInEx/Lua Scripting"))
            {
                Directory.CreateDirectory("BepInEx/Lua Scripting");
            }
            if (!File.Exists("BepInEx/Lua Scripting/script.lua"))
            {
                File.CreateText("BepInEx/Lua Scripting/script.lua");
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

            /*if (GUILayout.Button("Autoreload: " + (autoReload ? "Enabled" : "Disabled")))
            {
                autoReload = !autoReload;
            }*/

        }

        void ReloadScript()
        {
            Debug.Log("Reloading Map Script...");
            var script = File.ReadAllText("BepInEx/Lua Scripting/script.lua") ?? "";
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