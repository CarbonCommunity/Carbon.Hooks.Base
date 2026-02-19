#if !MINIMAL

using API.Hooks;
using UnityEngine;

namespace Carbon.Hooks;

#pragma warning disable IDE0051

public partial class Category_Fixes
{
	public partial class Fixes_ItemCrafter
	{
		[HookAttribute.Patch("IOvenSmeltSpeedMultiplier", "IOvenSmeltSpeedMultiplier", typeof(BaseOven.BaseOvenWorkQueue), nameof(BaseOven.BaseOvenWorkQueue.RunJob), [typeof(BaseOven)])]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IOvenSmeltSpeedMultiplier : Patch
		{
			public static bool Prefix(BaseOven oven)
			{
				if (Community.Runtime.Core.IOvenSmeltSpeedMultiplier(oven) is not float speedMultiplier)
				{
					return true;
				}
				var delta = BaseOven.UpdateRate * speedMultiplier;
				if (oven.lastCookUpdate > delta)
				{
					var num = Mathf.Floor(oven.lastCookUpdate / delta);
					oven.lastCookUpdate -= num * delta;
					oven.Cook(delta * num);
				}
				if (oven.visualFood && oven.lastCookVisualsUpdate > BaseOven.VisualUpdateRate)
				{
					oven.lastCookVisualsUpdate = 0f;
					oven.CookVisuals();
				}

				return false;
			}
		}
	}
}

#endif
