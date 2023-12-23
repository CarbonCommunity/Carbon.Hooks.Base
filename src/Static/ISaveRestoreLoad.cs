using System.Linq;
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
		[HookAttribute.Patch("ISaveRestoreLoad", "ISaveRestoreLoad", typeof(SaveRestore), nameof(SaveRestore.Load), new System.Type[] { typeof(string), typeof(bool) })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.Hidden | HookFlags.IgnoreChecksum)]

		public class ISaveRestoreLoad : Patch
		{
			public static void Prefix(string strFilename, bool allowOutOfDateSaves, ref bool __result)
			{
				if (Community.Runtime.ClientConfig.Enabled)
				{
					Community.Runtime.CorePlugin.To<CorePlugin>().ReloadCarbonClientAddons(false);
				}
			}
		}
	}
#endif
}
