﻿using System;
using BannerlordCheats.Extensions;
using BannerlordCheats.Settings;
using HarmonyLib;
using JetBrains.Annotations;
using SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace BannerlordCheats.Patches.Combat
{
    public static class OneHitKill
    {
        public static void CalculateDamage(
            ref AttackInformation attackInformation,
            ref AttackCollisionData collisionData,
            ref WeaponComponentData weapon,
            ref float __result)
        {
            try
            {
                if (attackInformation.IsAttackerPlayer
                    && !attackInformation.IsFriendlyFire
                    && BannerlordCheatsSettings.Instance?.OneHitKill == true)
                {
                    __result = 10000;
                }
            }
            catch (Exception e)
            {
                SubModule.LogError(e, typeof(OneHitKill));
            }
        }
    }

    [HarmonyPatch(typeof(DefaultAgentApplyDamageModel), nameof(DefaultAgentApplyDamageModel.CalculateDamage))]
    public static class OneHitKill_Default
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void CalculateDamage(
            ref AttackInformation attackInformation,
            ref AttackCollisionData collisionData,
            ref WeaponComponentData weapon,
            ref float __result)
            => OneHitKill.CalculateDamage(
                ref attackInformation,
                ref collisionData,
                ref weapon,
                ref __result);
    }

    [HarmonyPatch(typeof(SandboxAgentApplyDamageModel), nameof(SandboxAgentApplyDamageModel.CalculateDamage))]
    public static class OneHitKill_Sandbox
    {
        [UsedImplicitly]
        [HarmonyPostfix]
        public static void CalculateDamage(
            ref AttackInformation attackInformation,
            ref AttackCollisionData collisionData,
            ref WeaponComponentData weapon,
            ref float __result)
            => OneHitKill.CalculateDamage(
                ref attackInformation,
                ref collisionData,
                ref weapon,
                ref __result);
    }
}
