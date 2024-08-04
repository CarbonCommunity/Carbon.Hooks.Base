using System;
using API.Events;
using API.Hooks;

namespace Carbon.Hooks;

#pragma warning disable IDE0051

public partial class Category_Static
{
	public partial class Static_ServerMgr
	{
		[HookAttribute.Patch("OnServerInitialized", "OnServerInitialized", typeof(ServerMgr), "OpenConnection", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.IgnoreChecksum)]

		[MetadataAttribute.Info("Called after the server startup has been completed and is awaiting connections.")]
		[MetadataAttribute.Info("Also called for plugins that are hotloaded while the server is already started running.")]
		[MetadataAttribute.Parameter("initialLoad", typeof(bool), true)]

		public class IOnServerInitialized : Patch
		{
			public static void Postfix()
			{
				if (!ConVar.Server.autoUploadMap)
				{
					Community.Runtime.MarkServerInitialized(true);
				}

				Events.Trigger(CarbonEvent.OnServerInitialized, EventArgs.Empty);
			}
		}
	}
}
