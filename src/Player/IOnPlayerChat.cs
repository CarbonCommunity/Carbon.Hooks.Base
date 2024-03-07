using System.Linq;
using System.Threading.Tasks;
using API.Hooks;
using Carbon.Core;
using ConVar;
using Oxide.Core;
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

public partial class Category_Player
{
	public partial class BasePlayer_Player
	{
		[HookAttribute.Patch("IOnPlayerChat", "IOnPlayerChat", typeof(ConVar.Chat), "sayAs", new System.Type[] { typeof(Chat.ChatChannel), typeof(ulong), typeof(string), typeof(string), typeof(BasePlayer) })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.Hidden)]

		public class IOnPlayerChat : Patch
		{
			public static bool IsValidCommand(string source)
			{
				if (API.Commands.Command.Prefixes == null)
				{
					Logger.Error("This is really bad. Let the devs know ASAP, unless some plugin broke this. (IOnPlayerChat.IsValidCommand -> Prefixes == null");
					return false;
				}

				if (string.IsNullOrEmpty(source))
				{
					return false;
				}

				return API.Commands.Command.Prefixes.Contains(source[..1]);
			}

			public static bool Prefix(ChatChannel targetChannel, ulong userId, string username, string message, BasePlayer player, ref ValueTask<bool> __result)
			{
				if (string.IsNullOrEmpty(message)) return true;

				if (IsValidCommand(message) && CorePlugin.IOnPlayerCommand(player, message) is bool hookValue1)
				{
					__result = new ValueTask<bool>(hookValue1);
					return false;
				}

				if (CorePlugin.IOnPlayerChat(userId, username, message, targetChannel, player) is bool hookValue2)
				{
					__result = new ValueTask<bool>(hookValue2);
					return false;
				}

				return true;
			}
		}
	}
}
