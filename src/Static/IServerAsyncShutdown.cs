using System;
using System.Linq;
using System.Threading.Tasks;
using API.Abstracts;
using API.Hooks;
using Carbon.Base;
using Carbon.Core;
using Carbon.Extensions;
using Carbon.Plugins;
using ConVar;

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
	public partial class Static_ServerMgr
	{
		[HookAttribute.Patch("IServerAsyncShutdown", "IServerAsyncShutdown", typeof(Global), "quit", new System.Type[] { typeof(ConsoleSystem.Arg) })]
		[HookAttribute.Identifier("adcd48cf175e5002b4c64bb0d49150f4")]
		[HookAttribute.Options(HookFlags.Static | HookFlags.Hidden | HookFlags.IgnoreChecksum)]

		public class Static_ServerMgr_adcd48cf175e5002b4c64bb0d49150f4 : Patch
		{
			internal static bool _isQuitting;
			internal static bool _allowNative;

			public static bool Prefix(ConsoleSystem.Arg args)
			{
				if (_isQuitting)
				{
					return _allowNative;
				}

				Shutdown();

				return false;
			}

			public static async ValueTask Shutdown()
			{
				_isQuitting = true;

				foreach (var package in ModLoader.LoadedPackages)
				{
					foreach (var plugin in package.Plugins)
					{
						if (plugin is CarbonPlugin carbonPlugin)
						{
							try
							{
								await carbonPlugin.OnAsyncServerShutdown();
							}
							catch (Exception ex)
							{
								carbonPlugin.LogError("Failed asynchronous shutdown", ex);
							}
						}
					}
				}

				_allowNative = true;
				ConsoleSystem.Run(ConsoleSystem.Option.Server, "quit");
			}
		}
	}
}
