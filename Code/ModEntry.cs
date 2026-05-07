using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;

namespace TestSubjectEnrageBalance;

[ModInitializer("Init")]
public static class ModEntry
{
    private static Harmony? _harmony;

    public static void Init()
    {
        Log.Info("[TestSubjectEnrageBalance] Initializing...");

        _harmony = new Harmony("com.kziz3988.testsubjectenragebalance");
        _harmony.PatchAll();

        Log.Info("[TestSubjectEnrageBalance] Loaded successfully.");
    }
}
