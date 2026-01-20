using BepInEx;
using HarmonyLib;
using System.Collections.Generic;

[BepInPlugin("com.you.MagicCraft.MonsterHpRatio", "怪物血量翻倍模组", "1.0.0")]
public class MonsterHpX2Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Harmony harmony = new Harmony("MonsterHpRatio");
        harmony.PatchAll();
        Logger.LogInfo("✅ MonsterHpRatio 加载成功！");
    }
}

[HarmonyPatch(typeof(UnitConfig.Initializer), "ApplyResult")]
public class Patch_UnitConfig_Initializer_ApplyResult
{
    private static bool hasApplied = false;
    [HarmonyPrefix]
    public static void Prefix(ref List<UnitConfig> result)
    {
        if (hasApplied)
        {
            return;
        }
        for (int i = 0; i < result.Count; i++)
        {
            UnitConfig unitConfig = result[i];
            if (unitConfig.unitType == UnitType.Monster || unitConfig.unitType == UnitType.Elite || unitConfig.unitType == UnitType.Boss || unitConfig.unitType == UnitType.WillAttack)
            {
                unitConfig.maxHP *= 10f;
                unitConfig.currentHP = unitConfig.maxHP;
                unitConfig.moveSpeed *= 2f;
                result[i] = unitConfig;
            }
        }
        hasApplied = true;
    }
}
