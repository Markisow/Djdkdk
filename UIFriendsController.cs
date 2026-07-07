using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;

// Token: 0x02000190 RID: 400
public class UIFriendsController : UIViewController<UIFriends>
{
	// Token: 0x06000BA5 RID: 2981 RVA: 0x00011853 File Offset: 0x0000FA53
	public override void Awake()
	{
		base.Awake();
		this.uiFriends = base.GetComponent<UIFriends>();
		EventManager.AddEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(this.Event_OnSteamConnected));
		EventManager.AddEventListener("Event_OnPersonaStateChange", new Action<Dictionary<string, object>>(this.Event_OnPersonaStateChange));
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00011893 File Offset: 0x0000FA93
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(this.Event_OnSteamConnected));
		EventManager.RemoveEventListener("Event_OnPersonaStateChange", new Action<Dictionary<string, object>>(this.Event_OnPersonaStateChange));
		base.OnDestroy();
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x000418EC File Offset: 0x0003FAEC
	private void ParseSteamId(string steamId)
	{
		bool flag = this.uiFriends.IsFriendListed(steamId);
		bool flag2 = SteamIntegrationManager.IsFriend(steamId);
		bool flag3 = SteamIntegrationManager.IsFriendOnline(steamId);
		bool flag4 = !flag && flag2 && flag3;
		bool flag5 = flag && flag2 && flag3;
		bool flag6 = flag && (!flag2 || !flag3);
		if (flag4)
		{
			string username = SteamIntegrationManager.GetUsername(steamId);
			Texture2D avatar = SteamIntegrationManager.GetAvatar(steamId, AvatarSize.Medium);
			this.uiFriends.AddFriend(steamId, username, avatar);
			return;
		}
		if (flag5)
		{
			string username2 = SteamIntegrationManager.GetUsername(steamId);
			Texture2D avatar2 = SteamIntegrationManager.GetAvatar(steamId, AvatarSize.Medium);
			this.uiFriends.UpdateFriend(steamId, username2, avatar2);
			return;
		}
		if (flag6)
		{
			this.uiFriends.RemoveFriend(steamId);
		}
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x000118C7 File Offset: 0x0000FAC7
	private void Event_OnSteamConnected(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		SteamIntegrationManager.GetFriendSteamIds(false).ForEach(delegate(string steamId)
		{
			this.ParseSteamId(steamId);
		});
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00041990 File Offset: 0x0003FB90
	private void Event_OnPersonaStateChange(Dictionary<string, object> message)
	{
		string steamId = (string)message["steamId"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		this.ParseSteamId(steamId);
	}

	// Token: 0x04000700 RID: 1792
	private UIFriends uiFriends;
}
