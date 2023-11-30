using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace LCInstantFloods.Patches
{
    [HarmonyPatch(typeof(FloodWeather))]
    internal class FloodWeatherPatch
    {
        private static FieldInfo floodLevelOffsetField = typeof(FloodWeather).GetField("floodLevelOffset", BindingFlags.NonPublic | BindingFlags.Instance);

        [HarmonyPatch("OnGlobalTimeSync")]
        [HarmonyPrefix]
        static void Prefix(FloodWeather __instance)
        {
            // Access floodLevelOffset using reflection
            float currentFloodLevelOffset = (float)floodLevelOffsetField.GetValue(__instance);

            // Modify floodLevelOffset with a multiplier to make it change faster
            float multiplier = 10.0f; // Adjust this multiplier as needed
            float modifiedFloodLevelOffset = Mathf.Clamp(TimeOfDay.Instance.globalTime / 1f, 0f, 100f) * multiplier * (float)TimeOfDay.Instance.currentWeatherVariable2;

            floodLevelOffsetField.SetValue(__instance, modifiedFloodLevelOffset);
        }
    }
}
