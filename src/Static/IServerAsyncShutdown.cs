using System;
using System.Linq;
using System.Threading.Tasks;
using API.Hooks;
using Carbon.Base;
using Carbon.Core;
using ConVar;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Hooks;
#pragma warning disable IDE0051
#pragma warning disable CS4014

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

				foreach (var module in Community.Runtime.ModuleProcessor.Modules)
				{
					if (module is BaseModule baseModule && baseModule.GetEnabled())
					{
						await HandleHookable(module);
					}
				}

				foreach (var plugin in ModLoader.LoadedPackages.SelectMany(package => package.Plugins))
				{
					await HandleHookable(plugin);
				}

				async ValueTask HandleHookable(BaseHookable hookable)
				{
					try
					{
						await hookable.OnAsyncServerShutdown();
					}
					catch (Exception ex)
					{
						Logger.Error($"[{hookable.Name}] Failed asynchronous shutdown", ex);
					}
				}

				_allowNative = true;
				ConsoleSystem.Run(ConsoleSystem.Option.Server, "quit");
			}
		}
	}
}
