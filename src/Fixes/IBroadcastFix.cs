using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using API.Hooks;
using HarmonyLib;
using Patch = API.Hooks.Patch;

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
