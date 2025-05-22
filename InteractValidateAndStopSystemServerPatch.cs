using HarmonyLib;
using ProjectM;
using ProjectM.Gameplay.Systems;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;

namespace KinThrone;

/*
[HarmonyPatch]
internal static class InteractValidateAndStopSystemServerPatch
{
    static readonly PrefabGUID _interactThroneDraculaTravel = new(559608494);

    [HarmonyPatch(typeof(InteractValidateAndStopSystemServer), nameof(InteractValidateAndStopSystemServer.OnUpdate))]
    [HarmonyPrefix]
    static void OnUpdatePrefix(InteractValidateAndStopSystemServer __instance)
    {
        if (!Core._initialized) return;

        NativeArray<Entity> entities = __instance.__query_195794971_3.ToEntityArray(Allocator.Temp);
        ComponentLookup<PlayerCharacter> playerCharacterLookup = __instance.GetComponentLookup<PlayerCharacter>();

        try
        {
            for (int i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                if (!entity.TryGetComponent(out Buff buff)) continue;

                PrefabGUID prefabGuid = entity.TryGetComponent(out prefabGuid) ? prefabGuid : PrefabGUID.Empty;
                bool isPlayerTarget = playerCharacterLookup.HasComponent(buff.Target);

                if (prefabGuid.Equals(_interactThroneDraculaTravel) && isPlayerTarget)
                {

                }
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
