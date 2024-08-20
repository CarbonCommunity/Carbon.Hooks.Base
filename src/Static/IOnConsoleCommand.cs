using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using API.Commands;
using API.Hooks;
using Carbon.Components;
using Carbon.Extensions;
using Facepunch.Extend;
using static ConsoleSystem;
using Command = API.Commands.Command;

namespace Carbon.Hooks;

#pragma warning disable IDE0051
#pragma warning disable IDE0060

public partial class Category_Static
{
	public partial class Static_ConsoleSystem
	{
		[HookAttribute.Patch("OnConsoleCommand", "OnConsoleCommand", typeof(ConsoleSystem), "Run", new System.Type[] { typeof(Option), typeof(string), typeof(object[]) })]
		[HookAttribute.Options(HookFlags.Static | HookFlags.IgnoreChecksum)]

		[MetadataAttribute.Info("Called whenever a Carbon server command is called.")]

		public class IOnConsoleCommand : Patch
		{
			internal static string[] EmptyArgs = new string[0];
			internal static string Space = " ";
			internal static readonly string[] Filters = ["no_input"];

			public static bool Prefix(Option options, string strCommand, object[] args)
			{
				if (Community.Runtime == null || Filters.Contains(strCommand))
				{
					return true;
				}

				try
				{
					using var split = TempArray<string>.New(strCommand.Split(ConsoleArgEx.CommandSpacing,
						StringSplitOptions.RemoveEmptyEntries));
					var command = split.Length == 0 ? string.Empty : split.Get(0).Trim();

					var temp = Facepunch.Pool.Get<List<string>>();
					temp.AddRange(split.Length > 1 ? strCommand[(command.Length + 1)..].SplitQuotesStrings() : EmptyArgs);
					if (args != null)
					{
						temp.AddRange(args.Select(arg => arg?.ToString()));
					}
					var arguments = temp.ToArray();
					Facepunch.Pool.FreeUnmanaged(ref temp);

					if (!Command.FromRcon)
					{
						var player = options.Connection?.player as BasePlayer;
						var commands = player == null
							? Community.Runtime.CommandManager.RCon
							: Community.Runtime.CommandManager.ClientConsole;

						if (Community.Runtime.Config.Aliases.TryGetValue(command, out var alias))
						{
							command = alias;
							strCommand = $"{alias} {arguments.ToString(Space)}";
						}

						if (Community.Runtime.CommandManager.Contains(commands, command, out var commandInstance))
						{
							var arg = FormatterServices.GetUninitializedObject(typeof(Arg)) as Arg;
							arg.Option = options;
							arg.FullString = strCommand;
							arg.Args = arguments;
							arg.cmd = commandInstance.RustCommand;

							var commandArgs = Facepunch.Pool.Get<PlayerArgs>();
							commandArgs.Token = arg;
							commandArgs.Type = commandInstance.Type;
							commandArgs.Arguments = arguments;
							commandArgs.Player = player;
							commandArgs.IsServer = player == null;
							commandArgs.PrintOutput = options.PrintOutput || player != null;

							Command.FromRcon = false;
							Community.Runtime.CommandManager.Execute(commandInstance, commandArgs);
							return false;
						}

						if (Community.Runtime.Config.Logging.CommandSuggestions)
						{
							if (player != null && !player.IsAdmin)
							{
								return true;
							}

							if (ConsoleSystem.Index.Server.Find(command) != null)
							{
								return true;
							}

							var suggestion = Suggestions.Lookup(command,
								Community.Runtime.CommandManager.ClientConsole.Select(x => x.Name)
									.Concat(Community.Runtime.Config.Aliases.Select(x => x.Key)), minimumConfidence: 5);

							if (suggestion.Any())
							{
								var log = $"Command '{command}' not found. Suggesting: {suggestion.Select(x => x.Result).ToString(", ", " or ")}";

								if (player != null)
								{
									player.ConsoleMessage(log);
								}
								else
								{
									Logger.Log(log);
								}

								return false;
							}
						}
					}
				}
				catch (Exception exception)
				{
					Logger.Error($"Failed ConsoleSystem.Run [{strCommand}] [{string.Join(" ", args)}]", exception);
				}

				return true;
			}
		}
	}
}
