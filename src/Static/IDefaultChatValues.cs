﻿using System.Linq;
using API.Hooks;
using Carbon.Components;
using Carbon.Core;
using UnityEngine;

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
#if !MINIMAL
	public partial class Static_Debug
	{
		[HookAttribute.Patch("IDefaultChatValues", "IDefaultChatValues", typeof(ConVar.Chat), "Broadcast", new System.Type[] { typeof(string), typeof(string), typeof(string), typeof(ulong) })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.Hidden | HookFlags.IgnoreChecksum)]

		public class IDefaultChatValues : Patch
		{
			public static void Prefix(string message, ref string username, ref string color, ref ulong userid)
			{
				const string defaultValue = "-1";

				if(username == "SERVER" && color == "#eee" && userid == 0ul && Community.Runtime.CorePlugin is CorePlugin core)
				{
					if (core.DefaultServerChatName != defaultValue)
					{
						username = core.DefaultServerChatName;
					}
					if (core.DefaultServerChatColor != defaultValue)
					{
						color = core.DefaultServerChatColor;
					}
					if (core.DefaultServerChatId != -1)
					{
						userid = (ulong)core.DefaultServerChatId;
					}
				}
			}
		}
	}
#endif
}
