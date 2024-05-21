using System.Threading.Tasks;
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

public partial class Category_Entity
{
	public partial class Entity_Outdated
	{
		[HookAttribute.Patch("OnEntitySaved", "OnEntitySaved", typeof(BaseNetworkable), nameof(BaseNetworkable.ToStream), null)]
		[HookAttribute.Options(HookFlags.MetadataOnly)]

		[MetadataAttribute.Info("Gets called whenever the entity is about to be saved for streaming.")]
		[MetadataAttribute.Parameter("networkable", typeof(BaseNetworkable))]
		[MetadataAttribute.Parameter("info", typeof(BaseNetworkable.SaveInfo))]

		public class OnEntitySaved : Patch;
	}
}
