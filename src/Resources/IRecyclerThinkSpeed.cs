using API.Hooks;
using UnityEngine;

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
	public partial class Fixes_Recycler
	{
		[HookAttribute.Patch("IRecyclerThinkSpeed", "IRecyclerThinkSpeed", typeof(Recycler), "GetRecycleThinkDuration", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IRecyclerThinkSpeed : Patch
		{
			private static bool Prefix(Recycler __instance, ref float __result)
			{
				var hook = HookCaller.CallStaticHook(880503512, __instance);

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
