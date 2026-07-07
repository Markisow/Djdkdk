using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class UIFooterController : UIViewController<UIFooter>
{
	// Token: 0x06000B8E RID: 2958 RVA: 0x00041438 File Offset: 0x0003F638
	public override void Awake()
	{
		base.Awake();
		this.uiFooter = base.GetComponent<UIFooter>();
		EventManager.AddEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(this.Event_OnSteamConnected));
		EventManager.AddEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		EventManager.AddEventListener("Event_OnPlayerPartyDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerPartyDataChanged));
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0004149C File Offset: 0x0003F69C
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(this.Event_OnSteamConnected));
		EventManager.RemoveEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		EventManager.RemoveEventListener("Event_OnPlayerPartyDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerPartyDataChanged));
		base.OnDestroy();
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x000414F4 File Offset: 0x0003F6F4
	private void Event_OnSteamConnected(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		this.uiFooter.SetCreatePartyButtonVisibility(true);
		string steamId = SteamIntegrationManager.GetSteamId();
		if (string.IsNullOrEmpty(steamId))
		{
			return;
		}
		Texture2D avatar = SteamIntegrationManager.GetAvatar(steamId, AvatarSize.Medium);
		string username = SteamIntegrationManager.GetUsername(steamId);
		this.uiFooter.SetLocalUser(username, avatar);
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00041540 File Offset: 0x0003F740
	private void Event_OnPlayerDataChanged(Dictionary<string, object> message)
	{
		PlayerData playerData = (PlayerData)message["oldPlayerData"];
		PlayerData playerData2 = (PlayerData)message["newPlayerData"];
		if (playerData2 == null)
		{
			return;
		}
		this.uiFooter.SetMmr(playerData2.mmr);
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00041584 File Offset: 0x0003F784
	private void Event_OnPlayerPartyDataChanged(Dictionary<string, object> message)
	{
		PlayerPartyData playerPartyData = (PlayerPartyData)message["oldPlayerPartyData"];
		PlayerPartyData playerPartyData2 = (PlayerPartyData)message["newPlayerPartyData"];
		if (playerPartyData2 == null)
		{
			this.uiFooter.ClearPartyUsers();
			this.uiFooter.SetCreatePartyButtonVisibility(true);
			this.uiFooter.SetInviteButtonVisibility(false);
			this.uiFooter.SetLeavePartyButtonVisibility(false);
			this.uiFooter.SetDisbandPartyButtonVisibility(false);
			return;
		}
		bool flag = BackendManager.PlayerState.PlayerData.steamId == playerPartyData2.ownerSteamId;
		this.uiFooter.ClearPartyUsers();
		foreach (string steamId in playerPartyData2.memberSteamIds)
		{
			string username = SteamIntegrationManager.GetUsername(steamId);
			Texture2D avatar = SteamIntegrationManager.GetAvatar(steamId, AvatarSize.Medium);
			this.uiFooter.AddPartyUser(steamId, username, avatar);
		}
		this.uiFooter.SetCreatePartyButtonVisibility(false);
		this.uiFooter.SetInviteButtonVisibility(true);
		this.uiFooter.SetLeavePartyButtonVisibility(!flag);
		this.uiFooter.SetDisbandPartyButtonVisibility(flag);
	}

	// Token: 0x040006F4 RID: 1780
	private UIFooter uiFooter;
}
