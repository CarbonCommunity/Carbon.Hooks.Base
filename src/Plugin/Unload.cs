using System.Threading.Tasks;
using API.Hooks;
using Carbon.Core;
using ConVar;
using Oxide.Core;
using Oxide.Core.Plugins;
using UnityEngine;
using static ConVar.Chat;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Hooks;
#pragma warning disable IDE0051

public partial class Category_Plugin
{
	public partial class Plugin_Unload
	{
		[HookAttribute.Patch("Unload", "Unload [Instance]", typeof(ModLoader), nameof(ModLoader.InitializePlugin), null)]
		[HookAttribute.Options(HookFlags.MetadataOnly)]

		[MetadataAttribute.Category("Plugin")]
		[MetadataAttribute.Info("Gets called when the plugin has fully shut down and disposed.")]

		public class Unload : Patch
		{

		}
	}
}
