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
	public partial class Fixes_VendingMachine
	{
		[HookAttribute.Patch("IVendingBuyDuration", "IVendingBuyDuration", typeof(VendingMachine), "GetBuyDuration", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IVendingBuyDuration1 : Patch
		{
			public static bool Prefix(VendingMachine __instance, ref float __result)
			{
				var hook = HookCaller.CallStaticHook(2959446098, __instance);

				if (hook is float value)
				{
					__result = value;
					return false;
				}

				return true;
			}
		}

		[HookAttribute.Patch("IVendingBuyDuration", "IVendingBuyDuration", typeof(InvisibleVendingMachine), "GetBuyDuration", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IVendingBuyDuration2 : Patch
		{
			public static bool Prefix(InvisibleVendingMachine __instance, ref float __result)
			{
				var hook = HookCaller.CallStaticHook(2959446098, __instance);

				if (hook is float value)
				{
					__result = value;
					return false;
				}

				return true;
			}
		}
	}
}
