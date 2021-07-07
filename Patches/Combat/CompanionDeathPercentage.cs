﻿using BannerlordCheats.Extensions;
using BannerlordCheats.Settings;
using HarmonyLib;
using SandBox;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.Combat
{
    [HarmonyPatch(typeof(DefaultAgentDecideKilledOrUnconsciousModel), nameof(DefaultAgentDecideKilledOrUnconsciousModel.GetAgentStateProbability))]
    public static class CompanionDeathPercentage
    {
        [HarmonyPostfix]
        public static void GetAgentStateProbability(Agent affectorAgent, Agent effectedAgent, DamageTypes damageType, float useSurgeryProbability, ref float __result)
        {
            if (effectedAgent.IsPlayerCompanion()
                && BannerlordCheatsSettings.Instance?.CompanionDeathPercentage < 100f)
            {
                var factor = BannerlordCheatsSettings.Instance.CompanionDeathPercentage / 100f;

                __result *= factor;
            }
        }
    }
}
