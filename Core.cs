using BepInEx.Logging;
using ProjectM;
using Stunlock.Core;
using Unity.Entities;

namespace KinThrone;
internal static class Core
{
    public static World Server { get; } = GetServerWorld() ?? throw new Exception("Couldn't find Server world!");
    public static EntityManager EntityManager { get; } = Server.EntityManager;
    public static PrefabCollectionSystem PrefabCollectionSystem { get; set; }
    public static ManualLogSource Log => Plugin.LogInstance;

    public static bool _initialized;

    public static void Initialize()
    {
        if (_initialized) return;

        PrefabCollectionSystem = Server.GetExistingSystemManaged<PrefabCollectionSystem>();
        AddAncientRelicBuffs();

        _initialized = true;
    }

    static World GetServerWorld()
    {
        return World.s_AllWorlds.ToArray().FirstOrDefault(world => world.Name == "Server");
    }

    static void AddAncientRelicBuffs()
    {
        PrefabGUID AB_Interact_Throne_Dracula_Travel = new(559608494);

        if (PrefabCollectionSystem._PrefabGuidToEntityMap.TryGetValue(AB_Interact_Throne_Dracula_Travel, out Entity prefab))
        {
            var applyBuffBuffer = EntityManager.GetBuffer<ApplyBuffOnGameplayEvent>(prefab);

            ApplyBuffOnGameplayEvent applyBuffOnGameplayEvent = applyBuffBuffer[1];
            applyBuffOnGameplayEvent.Buff1 = new(-1703886455);   // AB_Interact_UseRelic_Behemoth_Buff
            applyBuffOnGameplayEvent.Buff2 = new(-1161197991);   // AB_Interact_UseRelic_Paladin_Buff
            applyBuffOnGameplayEvent.Buff3 = new(-238197495);    // AB_Interact_UseRelic_Manticore_Buff

            applyBuffBuffer[1] = applyBuffOnGameplayEvent;
        }
    }
}