﻿using API.Hooks;
using Carbon.Core;

namespace Carbon.Hooks;

#pragma warning disable IDE0051

public partial class Category_Plugin
{
	public partial class Plugin_Loaded
	{
		[HookAttribute.Patch("Loaded", "Loaded [Instance]", typeof(ModLoader), nameof(ModLoader.InitializePlugin), null)]
		[HookAttribute.Options(HookFlags.MetadataOnly)]

		[MetadataAttribute.Category("Plugin")]
		[MetadataAttribute.Info("Gets called when the plugin executes the Load method on the plugin.")]

		public class Loaded : Patch
		{

		}
	}
}
