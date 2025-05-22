using HarmonyLib;
using ProjectM;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;

namespace KinThrone;

/*
[HarmonyPatch]
internal static class BuffSystemSpawnPatch
{
    static EntityManager EntityManager => Core.EntityManager;

    [HarmonyPatch(typeof(BuffSystem_Spawn_Server), nameof(BuffSystem_Spawn_Server.OnUpdate))]
    [HarmonyPrefix]
    static void OnUpdatePrefix(BuffSystem_Spawn_Server __instance)
    {
        if (!Core._initialized) return;

        NativeArray<Entity> entities = __instance._Query.ToEntityArray(Allocator.Temp);

        try
        {
            for (int i = 0; i < entities.Length; i++)
            {
                Entity buffEntity = entities[i];
                PrefabGUID prefabGuid = buffEntity.TryGetComponent(out prefabGuid) ? prefabGuid : PrefabGUID.Empty;
            }
        }
        catch (Exception e)
        {
            Core.Log.LogWarning($"[BuffSystem_Spawn_Server] - Exception: {e}");
        }
        finally
        {
            entities.Dispose();
        }
    }
}
*/
