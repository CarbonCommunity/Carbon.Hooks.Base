﻿using API.Hooks;
using UnityEngine;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community
 * All rights reserved.
 *
 */

namespace Carbon.Hooks;
#pragma warning disable IDE0051

public partial class Category_Fixes
{
	public partial class Fixes_Excavator
	{
		[HookAttribute.Patch("IOnExcavatorInit", "IOnExcavatorInit", typeof(ExcavatorArm), "Init", new System.Type[] { })]
		[HookAttribute.Options(HookFlags.Hidden)]

		public class IOnExcavatorInit : Patch
		{
			private static void Postfix(ExcavatorArm __instance)
			{
				HookCaller.CallStaticHook(1290758824, __instance);
			}
		}
	}
}
