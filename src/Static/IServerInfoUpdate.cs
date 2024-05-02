using System;
using System.Linq;
using API.Abstracts;
using API.Hooks;
using Carbon.Base;
using Carbon.Extensions;
using CarbonClient = Carbon.Client.Client;

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
		[HookAttribute.Patch("IServerInfoUpdate", "IServerInfoUpdate", typeof(ServerMgr), "UpdateServerInformation", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.Hidden | HookFlags.IgnoreChecksum)]

		public class IServerInfoUpdate : Patch
		{
			public static bool ForceModded =>
#if !MINIMAL
				CarbonAuto.Singleton.IsChanged() ||
#endif
			                                  Community.Runtime.ModuleProcessor.Modules.Any(x => x is BaseModule module && module.GetEnabled() && module.ForceModded);

			public static void Postfix()
			{
				if (Community.Runtime == null || Community.Runtime.Config == null) return;

				try
				{
					ServerTagEx.SetRequiredTag("^y", true);

					if (Community.Runtime.ClientConfig.Enabled)
					{
						ServerTagEx.SetRequiredTag("c4c", false);
					}
					else
					{
						ServerTagEx.UnsetRequiredTag("c4c", false);
					}

					if (Community.Runtime.Config.IsModded || ForceModded)
					{
						ServerTagEx.SetRequiredTag("^z", true);
					}
					else
					{
						ServerTagEx.UnsetRequiredTag("^z", true);
					}
				}
				catch (Exception ex)
				{
					Logger.Error($"Couldn't patch UpdateServerInformation.", ex);
				}
			}
		}
	}
}
