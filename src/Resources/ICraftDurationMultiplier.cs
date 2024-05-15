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
		[HookAttribute.Patch("ICraftDurationMultiplier", "ICraftDurationMultiplier", typeof(ItemCrafter),
			"GetScaledDuration", new System.Type[] { typeof(ItemBlueprint), typeof(float), typeof(bool) })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class ICraftDurationMultiplier : Patch
		{
			public static void Postfix(ItemBlueprint bp, float workbenchLevel, bool isInTutorial, ref float __result)
			{
				if (Community.Runtime.Core.ICraftDurationMultiplier(bp, workbenchLevel, isInTutorial) is not float value) return;

				__result *= value;
			}
		}
	}
}

#endif
