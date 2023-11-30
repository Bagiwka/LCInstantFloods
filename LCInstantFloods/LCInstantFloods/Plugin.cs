using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LCInstantFloods.Patches;

namespace LCInstantFloods
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class LCInfiniteHealthModBase : BaseUnityPlugin
    {

        private const string modGUID = "Bagiwka.LCInstantFloods";
        private const string modName = "LCInstantFloods";
        private const string modVersion = "1.0.0.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static LCInfiniteHealthModBase Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Time to drown!");

            harmony.PatchAll(typeof(LCInfiniteHealthModBase));
            harmony.PatchAll(typeof(FloodWeatherPatch));


        }

    }
}
