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
				if (Community.Runtime.Core.IVendingBuyDuration() is not float value) return true;
				__result = value;
				return false;

			}
		}

		[HookAttribute.Patch("IVendingBuyDuration", "IVendingBuyDuration", typeof(InvisibleVendingMachine), "GetBuyDuration", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IVendingBuyDuration2 : Patch
		{
			public static bool Prefix(InvisibleVendingMachine __instance, ref float __result)
			{
				if (Community.Runtime.Core.IVendingBuyDuration() is not float value) return true;
				__result = value;
				return false;

			}
		}
	}
}
