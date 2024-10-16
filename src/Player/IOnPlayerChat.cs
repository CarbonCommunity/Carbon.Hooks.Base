﻿using System;
using System.Threading.Tasks;
using API.Hooks;
using Carbon.Core;
using ConVar;
using static ConVar.Chat;

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
			public static bool IsValidCommand(string input, BasePlayer player, out API.Commands.Command.Prefix prefix)
			{
				prefix = null;

				if (string.IsNullOrEmpty(input))
				{
					return false;
				}

				if (API.Commands.Command.Prefixes == null)
				{
					Logger.Error("This is really bad. Let the devs know ASAP, unless some plugin broke this. (Prefixes == null)");
					return false;
				}

				if (!API.Commands.Command.HasPrefix(input[..1], out prefix)) return false;

				if (prefix.PrintToChat)
				{
					player.ChatMessage($"<color=orange>Command:</color> {input}");
				}

				if (prefix.PrintToConsole)
				{
					ServerConsole.PrintColoured(ConsoleColor.DarkYellow, $"[{player.Connection}]: ", ConsoleColor.DarkGreen, input);
				}

				return true;

			}

			public static bool Prefix(ChatChannel targetChannel, ulong userId, string username, string message, BasePlayer player, ref ValueTask<bool> __result)
			{
				if (string.IsNullOrEmpty(message)) return true;

				if (IsValidCommand(message, player, out var prefix) && CorePlugin.IOnPlayerCommand(player, message, prefix) is bool hookValue1)
				{
					__result = new ValueTask<bool>(hookValue1);
					return false;
				}

				if (CorePlugin.IOnPlayerChat(userId, username, message, targetChannel, player) is not bool hookValue2)
					return true;

				__result = new ValueTask<bool>(hookValue2);
				return false;

			}
		}
	}
}
