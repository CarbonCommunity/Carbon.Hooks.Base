using API.Hooks;
using Carbon.Core;

/*
 *
 * Copyright (c) 2022-2024 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Hooks;
#pragma warning disable IDE0051

public partial class Category_Item
{
	public partial class Item_OnLoseCondition
	{
		[HookAttribute.Patch("OnLoseCondition", "OnLoseCondition", typeof(CorePlugin), nameof(CorePlugin.IOnLoseCondition), null)]
		[HookAttribute.Options(HookFlags.MetadataOnly)]

		[MetadataAttribute.Info("Called right before the condition of the item is modified.")]
		[MetadataAttribute.Parameter("item", typeof(Item))]
		[MetadataAttribute.Parameter("amount", typeof(float))]

		public class OnLoseCondition : Patch;
	}
}
