using API.Hooks;
using Carbon.Core;

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

				if(userid == 0ul && Community.Runtime.Core is CorePlugin core)
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
