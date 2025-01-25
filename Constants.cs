using System.IO;

namespace RuntimeScripting
{
    internal class Constants
    {
        public const string scriptingFolderPath = "BepInEx/Lua Scripting";
        public static readonly string scriptPath = Path.Combine(scriptingFolderPath, "script.lua");
        public const string GUID = "Husky.RuntimeScripting";
        public const string Name = "RuntimeScripting";
        public const string Version = "1.0.0";
    }
}
