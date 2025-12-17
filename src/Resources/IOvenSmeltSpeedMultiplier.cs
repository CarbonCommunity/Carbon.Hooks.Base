#if !MINIMAL

using API.Hooks;

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
				if (oven.lastCookUpdate > BaseOven.UpdateRate * speedMultiplier)
				{
					oven.Cook(oven.lastCookUpdate);
					oven.lastCookUpdate = 0.0f;
				}
				if (!oven.visualFood || oven.lastCookVisualsUpdate <= 0.05f)
				{
					return false;
				}
				oven.lastCookVisualsUpdate = 0.0f;
				oven.CookVisuals();
				return false;
			}
		}
	}
}

#endif
