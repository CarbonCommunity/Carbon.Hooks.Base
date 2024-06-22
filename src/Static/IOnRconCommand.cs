﻿using System;
using System.Composition;
using System.Net;
using System.Runtime.Serialization;
using API.Hooks;
using Carbon.Components;
using Carbon.Extensions;
using Facepunch;
using Facepunch.Extend;
using static ConsoleSystem;
using Command = Oxide.Game.Rust.Libraries.Command;

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
	public partial class Static_RCon
	{
		[HookAttribute.Patch("OnRconCommand", "OnRconCommand", typeof(RCon), "OnCommand", new System.Type[] { typeof(Facepunch.RCon.Command) })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.IgnoreChecksum)]

		[MetadataAttribute.Info("Called when an RCON command is run.")]
		[MetadataAttribute.Parameter("ip", typeof(IPAddress))]
		[MetadataAttribute.Parameter("command", typeof(string))]
		[MetadataAttribute.Parameter("arguments", typeof(string[]))]

		public class IOnRconCommand : Patch
		{
			internal static string[] EmptyArgs = new string[0];
			internal static string Space = " ";

			public static bool Prefix(RCon.Command cmd)
			{
				if (Community.Runtime == null) return true;

				RCon.responseIdentifier = cmd.Identifier;
				RCon.responseConnection = cmd.ConnectionId;
				RCon.isInput = false;

				try
				{
					using var split = TempArray<string>.New(cmd.Message.Split(ConsoleArgEx.CommandSpacing, StringSplitOptions.RemoveEmptyEntries));
					var command = split.Get(0).Trim();
					var arguments = split.Length > 1 ? cmd.Message[(command.Length + 1)..].SplitQuotesStrings() : EmptyArgs;

					if (Community.Runtime.Config.Aliases.TryGetValue(command, out var alias))
					{
						command = alias;
						cmd.Message = $"{alias} {arguments.ToString(Space)}";
					}

					if (HookCaller.CallStaticHook(3740958730, cmd.Ip, command, arguments) != null)
					{
						return false;
					}

					Command.FromRcon = API.Commands.Command.FromRcon = true;

					var consoleArg = FormatterServices.GetUninitializedObject(typeof(Arg)) as Arg;
					var option = Option.Server;
					option.FromRcon = true;
					consoleArg.Option = option;
					consoleArg.FullString = cmd.Message;
					consoleArg.Args = arguments;

					try
					{
						if (Community.Runtime.CommandManager.Contains(Community.Runtime.CommandManager.RCon, command, out var outCommand))
						{
							var commandArgs = Facepunch.Pool.Get<API.Commands.Command.Args>();
							commandArgs.Token = consoleArg;
							commandArgs.Type = outCommand.Type;
							commandArgs.Arguments = arguments;
							commandArgs.IsRCon = true;
							commandArgs.IsServer = true;
							commandArgs.PrintOutput = consoleArg.Option.PrintOutput;

							Community.Runtime.CommandManager.Execute(outCommand, commandArgs);

							commandArgs.Dispose();
							Facepunch.Pool.Free(ref commandArgs);
						}
					}
					catch (Exception ex)
					{
						Logger.Error("RconCommand_OnCommand", ex);
					}

					Community.Runtime.Core.NextFrame(() => Command.FromRcon = API.Commands.Command.FromRcon = false);
				}
				catch { }

				return true;
			}
		}
	}
}
