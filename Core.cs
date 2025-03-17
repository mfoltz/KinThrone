using BepInEx.Logging;
using Il2CppInterop.Runtime;
using ProjectM;
using ProjectM.Scripting;
using Stunlock.Core;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Entities;

namespace KinThrone;
internal static class Core
{
    public static World Server { get; } = GetServerWorld() ?? throw new Exception("Couldn't find Server world!");
    public static EntityManager EntityManager { get; } = Server.EntityManager;
    public static PrefabCollectionSystem PrefabCollectionSystem { get; internal set; }
    public static ManualLogSource Log => Plugin.LogInstance;

    public static bool _initialized;
    public static void Initialize()
    {
        if (_initialized) return;

        PrefabCollectionSystem = Server.GetExistingSystemManaged<PrefabCollectionSystem>();
        ReplaceThroneBuff();

        _initialized = true;
    }
    static World GetServerWorld()
    {
        return World.s_AllWorlds.ToArray().FirstOrDefault(world => world.Name == "Server");
    }
    static void ReplaceThroneBuff()
    {
        PrefabGUID draculaThroneTravelBuff = new(559608494);
        PrefabGUID interactThroneSitBuff = new(211319841);

        if (PrefabCollectionSystem._PrefabGuidToEntityMap.TryGetValue(draculaThroneTravelBuff, out Entity prefab))
        {
            var buffer = EntityManager.GetBuffer<ApplyBuffOnGameplayEvent>(prefab);

            ApplyBuffOnGameplayEvent applyBuffOnGameplayEvent = buffer[1];
            applyBuffOnGameplayEvent.Buff0 = interactThroneSitBuff;

            buffer[1] = applyBuffOnGameplayEvent;
        }
    }
}