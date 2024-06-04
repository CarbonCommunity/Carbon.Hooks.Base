#if !MINIMAL

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
	public partial class Fixes_ItemCrafter
	{
		[HookAttribute.Patch("IOvenSmeltSpeedMultiplier", "IOvenSmeltSpeedMultiplier", typeof(BaseOven), nameof(BaseOven.StartCooking), new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IOvenSmeltSpeedMultiplier : Patch
		{
			public static bool Prefix(BaseOven __instance)
			{
				if (__instance.FindBurnable() == null && !__instance.CanRunWithNoFuel)
					return true;

				if (Community.Runtime.Core.IOvenSmeltSpeedMultiplier(__instance) is not float speedMultiplier) return true;

				var newBurnTime = 0.5f * speedMultiplier;
				__instance.inventory.temperature = __instance.cookingTemperature;
				__instance.UpdateAttachmentTemperature();
				__instance.InvokeRepeating(__instance.Cook, newBurnTime, newBurnTime);
				__instance.SetFlag(BaseEntity.Flags.On, true);
				return false;
			}
		}
	}
}

#endif
