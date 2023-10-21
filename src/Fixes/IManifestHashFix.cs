using System;
using System.Security.Cryptography;
using System.Text;
using API.Hooks;
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
	public partial class Fixes_FixManifestHash
	{
		[HookAttribute.Patch("IManifestHashFix", "IManifestHashFix", typeof(UnityEngine.StringEx), "ManifestHash", new System.Type[] { typeof(string) })]
		[HookAttribute.Options(HookFlags.Hidden | HookFlags.Static | HookFlags.IgnoreChecksum)]

		public class IManifestHashFix : Patch
		{
			internal static MD5CryptoServiceProvider _provider = new();

			public static bool Prefix(string str, ref uint __result)
			{
				if (string.IsNullOrEmpty(str))
				{
					__result = 0;
					return false;
				}

				if (!str.IsLower())
				{
					str = str.ToLower();
				}

				__result = BitConverter.ToUInt32(_provider.ComputeHash(Encoding.UTF8.GetBytes(str)), 0);
				return false;
			}
		}
	}
}
