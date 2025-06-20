using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace KinThrone;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    private Harmony _harmony;
    internal static Plugin Instance { get; set; }
    public static Harmony Harmony => Instance._harmony;
    public static ManualLogSource LogInstance => Instance.Log;

    public static readonly string ConfigPath = Path.Combine(Paths.ConfigPath, MyPluginInfo.PLUGIN_NAME);

    public override void Load()
    {
        Instance = this;

        if (Application.productName != "VRisingServer")
        {
            LogInstance.LogInfo($"{MyPluginInfo.PLUGIN_NAME} is a server mod and won't load on clients!");
            return;
        }

        _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

        Core.Log.LogInfo($"{MyPluginInfo.PLUGIN_NAME}[{MyPluginInfo.PLUGIN_VERSION}] loaded!");
    }

    public override bool Unload()
    {
        Config.Clear();
        _harmony.UnpatchSelf();

        return true;
    }
}
