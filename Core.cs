using BepInEx.Logging;
using Il2CppInterop.Runtime;
using ProjectM;
using Stunlock.Core;
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
            var buffer = EntityManager.GetBuffer<ApplyBuffOnGameplayEvent>(prefab);

            ApplyBuffOnGameplayEvent applyBuffOnGameplayEvent = new()
            {
                BuffTarget = ApplyBuffTarget.Owner,
                SpellTarget = SetSpellTarget.Default,
                EntityOwner = SetEntityOwner.Default,
                OverrideDuration = new(900f),
                Stacks = 1,
                Buff0 = new(-1703886455),   // AB_Interact_UseRelic_Behemoth_Buff
                Buff1 = new(-1161197991),   // AB_Interact_UseRelic_Paladin_Buff
                Buff2 = new(-238197495),    // AB_Interact_UseRelic_Manticore_Buff
                Buff3 = new(1068709119),    // AB_Interact_UseRelic_Monster_Buff
            };

            buffer.Add(applyBuffOnGameplayEvent);
        }
    }
    public static T Read<T>(this Entity entity) where T : struct
    {
        return EntityManager.GetComponentData<T>(entity);
    }
    public static bool Has<T>(this Entity entity) where T : struct
    {
        return EntityManager.HasComponent(entity, new(Il2CppType.Of<T>()));
    }
    public static bool TryGetComponent<T>(this Entity entity, out T componentData) where T : struct
    {
        componentData = default;

        if (entity.Has<T>())
        {
            componentData = entity.Read<T>();
            return true;
        }

        return false;
    }
}