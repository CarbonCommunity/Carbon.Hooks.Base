using System;
using API.Hooks;

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
	public partial class Fixes_MixingTable
	{

		[HookAttribute.Patch("IMixingSpeedMultiplier", "IMixingSpeedMultiplier", typeof(MixingTable), nameof(MixingTable.StartMixing), new Type[] { typeof(BasePlayer) })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IMixingSpeedMultiplier : Patch
		{
			public static void Postfix(BasePlayer player, ref MixingTable __instance)
			{
				var value = __instance.RemainingMixTime;

				if (value > 0f && Community.Runtime.Core.IMixingSpeedMultiplier(__instance, value) is float overridenValue)
				{
					__instance.RemainingMixTime = __instance.TotalMixTime /= overridenValue;
					__instance.SendNetworkUpdateImmediate();
				}
			}
		}
	}
}
