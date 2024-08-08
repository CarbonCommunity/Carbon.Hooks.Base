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
	public partial class Fixes_IHABBumpFix
	{
		[HookAttribute.Patch("IHABBumpFix", "IHABBumpFix", typeof(HotAirBalloon), "DecayTick", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Hidden | HookFlags.Static | HookFlags.IgnoreChecksum)]

		public class IHABBumpFix : Patch
		{
			public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> Instructions, ILGenerator Generator, MethodBase Method)
			{
				var x = 0;
				foreach (CodeInstruction instruction in Instructions)
				{
					if (x is < 61 or > 62)
					{
						yield return instruction;
					}

					x++;
				}
			}
		}
	}
}
