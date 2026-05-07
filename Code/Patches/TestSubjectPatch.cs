using Godot;
using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using TestSubjectEnrageBalance;
using System.Collections.Generic;
using System.Reflection;
using System;
namespace TestSubjectEnrageBalance.Patches;

[HarmonyPatch(typeof(TestSubject))]
public static class TestSubjectPatch
{
	[HarmonyPrefix]
	[HarmonyPatch(nameof(TestSubject.AfterAddedToRoom))]
	static bool AfterAddedToRoomPatch(TestSubject __instance, ref Task __result)
	{
		__result = AfterAddedToRoomAsync(__instance);
		return false;
	}

    static Task BaseAfterAddedToRoom()
    {
        return Task.CompletedTask;
    }

	static async Task AfterAddedToRoomAsync(TestSubject testSubject)
	{
		await BaseAfterAddedToRoom();
		await PowerCmd.Apply<AdaptablePower>(testSubject.Creature, 1m, testSubject.Creature, null);
		await PowerCmd.Apply<Code.Powers.NewEnragePower>(testSubject.Creature, 3m, testSubject.Creature, null);
		MethodInfo afterApplied = typeof(TestSubject).GetMethod("AfterPowerApplied", BindingFlags.NonPublic | BindingFlags.Instance);
    	MethodInfo afterRemoved = typeof(TestSubject).GetMethod("AfterPowerRemoved", BindingFlags.NonPublic | BindingFlags.Instance);
    	testSubject.Creature.PowerApplied += (Action<PowerModel>)Delegate.CreateDelegate(typeof(Action<PowerModel>), testSubject, afterApplied);
    	testSubject.Creature.PowerRemoved += (Action<PowerModel>)Delegate.CreateDelegate(typeof(Action<PowerModel>), testSubject, afterRemoved);
	}
}
