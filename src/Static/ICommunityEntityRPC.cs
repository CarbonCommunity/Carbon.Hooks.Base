using API.Hooks;
using Network;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Hooks;
#pragma warning disable IDE0051

public partial class Category_Static
{
	public partial class Static_CommunityEntity
	{
		[HookAttribute.Patch("ICommunityEntityRPC", "ICommunityEntityRPC", typeof(CommunityEntity), "OnRpcMessage", new System.Type[] { typeof(BasePlayer), typeof(uint), typeof(Message) })]
		[HookAttribute.Options(HookFlags.Hidden | HookFlags.Static | HookFlags.IgnoreChecksum)]

		public class ICommunityEntityRPC : API.Hooks.Patch
		{
			private static bool Prefix(BasePlayer player, uint rpc, Message msg, CommunityEntity __instance, ref bool __result)
			{
				if (Carbon.Client.RPC.HandleRPCMessage(player, rpc, msg) is bool value)
				{
					__result = value;
					return false;
				}

				return true;
			}
		}
	}
}
