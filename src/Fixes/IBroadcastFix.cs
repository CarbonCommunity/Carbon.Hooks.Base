using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using API.Hooks;
using Facepunch;
using HarmonyLib;
using UnityEngine;
using Patch = API.Hooks.Patch;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Hooks;
#pragma warning disable IDE0051

public partial class Category_Fixes
{
	public partial class Fixes_BroadcastFix
	{
		[HookAttribute.Patch("IBroadcastFix", "IBroadcastFix", typeof(ConVar.Chat), "Broadcast", new System.Type[] { typeof(string), typeof(string), typeof(string), typeof(ulong) })]
		[HookAttribute.Options(HookFlags.Hidden | HookFlags.Static | HookFlags.IgnoreChecksum)]

		public class IBroadcastFix : Patch
		{
			public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> Instructions, ILGenerator Generator, MethodBase Method)
			{
				int x = 0;
				const int target = 13;
				foreach (CodeInstruction instruction in Instructions)
				{
					var index = x++;

					if (index == target + 1)
					{
						yield return new CodeInstruction(OpCodes.Box, typeof(ulong));
						continue;
					}

					if (index != target)
					{
						yield return instruction;
						continue;
					}

					yield return new CodeInstruction(OpCodes.Ldarg_3);
				}
			}
		}
	}
}
