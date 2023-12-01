using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace LCInstantFloods.Patches
{
    [HarmonyPatch(typeof(FloodWeather))]
    internal class FloodWeatherPatch
    {
        private static FieldInfo floodLevelOffsetField = typeof(FloodWeather).GetField("floodLevelOffset", BindingFlags.NonPublic | BindingFlags.Instance);

        [HarmonyPatch("Update")]
        private static void Postfix(FloodWeather __instance)
        {
            // Access floodLevelOffset using reflection
            float floodLevelOffset = (float)floodLevelOffsetField.GetValue(__instance);

            // Increase the rate at which floodLevelOffset changes
            float rateMultiplier = 2.0f; // Adjust this multiplier as needed
            float modifiedFloodLevelOffset = floodLevelOffset + (rateMultiplier * Time.deltaTime);

            // Set the modified floodLevelOffset
            floodLevelOffsetField.SetValue(__instance, modifiedFloodLevelOffset);

            // Modify the position using the updated floodLevelOffset
            Vector3 newPosition = new Vector3(0f, TimeOfDay.Instance.currentWeatherVariable, 0f) + Vector3.up * modifiedFloodLevelOffset;
            __instance.gameObject.transform.position = Vector3.MoveTowards(__instance.gameObject.transform.position, newPosition, 0.5f * Time.deltaTime);
        }
    }
}
