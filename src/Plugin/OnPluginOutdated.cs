﻿using System.Threading.Tasks;
using API.Hooks;
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
	public partial class Plugin_Outdated
	{
		[HookAttribute.Patch("OnPluginOutdated", "OnPluginOutdated", null, null, null)]
		[HookAttribute.Identifier("171cea552ddf5cc4841171c221719e21")]
		[HookAttribute.Options(HookFlags.MetadataOnly)]

		[MetadataAttribute.Info("Gets called whenever a plugin from the Admin module -> Plugins tab is marked as 'auto-updateable' and is outdated.")]
		[MetadataAttribute.Parameter("pluginName", typeof(string))]
		[MetadataAttribute.Parameter("currentVersion", typeof(VersionNumber))]
		[MetadataAttribute.Parameter("newVersion", typeof(VersionNumber))]
		[MetadataAttribute.Parameter("plugin", typeof(Plugin))]
		[MetadataAttribute.Parameter("vendorName", typeof(string))]

		public class Plugin_Outdated_171cea552ddf5cc4841171c221719e21 : Patch
		{

		}
	}
}
