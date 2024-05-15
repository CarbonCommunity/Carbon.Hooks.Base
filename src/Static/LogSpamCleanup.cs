using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using API.Hooks;
using HarmonyLib;
using Patch = API.Hooks.Patch;

/*
 *
 * Copyright (c) 2022-2024 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Hooks;
#pragma warning disable IDE0051

public partial class Category_Static
{
	public partial class Cleanup
	{
		[HookAttribute.Patch("IAiInformationLogRemoval", "IAiInformationLogRemoval", typeof(AIInformationZone), "MarkDirty", new System.Type[] { typeof(bool) })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.Hidden | HookFlags.IgnoreChecksum)]

		public class IAiInformationLogRemoval : Patch
		{
			public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> Instructions, ILGenerator Generator, MethodBase Method)
			{
				var x = 0;
				foreach (CodeInstruction instruction in Instructions)
				{
					if (x is < 17 or > 18)
					{
						yield return instruction;
					}

					x++;
				}
			}
		}
	}
}
