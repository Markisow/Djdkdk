using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class LockerRoomPlayerController : MonoBehaviour
{
	// Token: 0x06000070 RID: 112 RVA: 0x000163CC File Offset: 0x000145CC
	private void Awake()
	{
		this.lockerRoomPlayer = base.GetComponent<LockerRoomPlayer>();
		EventManager.AddEventListener("Event_OnTeamChanged", new Action<Dictionary<string, object>>(this.Event_OnTeamChanged));
		EventManager.AddEventListener("Event_OnRoleChanged", new Action<Dictionary<string, object>>(this.Event_OnRoleChanged));
		EventManager.AddEventListener("Event_OnAppearanceClickItem", new Action<Dictionary<string, object>>(this.Event_OnAppearanceClickItem));
		EventManager.AddEventListener("Event_OnAppearanceShow", new Action<Dictionary<string, object>>(this.Event_OnAppearanceShow));
		EventManager.AddEventListener("Event_OnAppearanceHide", new Action<Dictionary<string, object>>(this.Event_OnAppearanceHide));
		EventManager.AddEventListener("Event_OnIdentityShow", new Action<Dictionary<string, object>>(this.Event_OnIdentityShow));
		EventManager.AddEventListener("Event_OnIdentityHide", new Action<Dictionary<string, object>>(this.Event_OnIdentityHide));
		EventManager.AddEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00008F2A File Offset: 0x0000712A
	private void Start()
	{
		this.ApplySettings();
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00016498 File Offset: 0x00014698
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnTeamChanged", new Action<Dictionary<string, object>>(this.Event_OnTeamChanged));
		EventManager.RemoveEventListener("Event_OnRoleChanged", new Action<Dictionary<string, object>>(this.Event_OnRoleChanged));
		EventManager.RemoveEventListener("Event_OnAppearanceClickItem", new Action<Dictionary<string, object>>(this.Event_OnAppearanceClickItem));
		EventManager.RemoveEventListener("Event_OnAppearanceShow", new Action<Dictionary<string, object>>(this.Event_OnAppearanceShow));
		EventManager.RemoveEventListener("Event_OnAppearanceHide", new Action<Dictionary<string, object>>(this.Event_OnAppearanceHide));
		EventManager.RemoveEventListener("Event_OnIdentityShow", new Action<Dictionary<string, object>>(this.Event_OnIdentityShow));
		EventManager.RemoveEventListener("Event_OnIdentityHide", new Action<Dictionary<string, object>>(this.Event_OnIdentityHide));
		EventManager.RemoveEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00016558 File Offset: 0x00014758
	private void ApplySettings()
	{
		int flagID = SettingsManager.FlagID;
		int headgearID = SettingsManager.GetHeadgearID(SettingsManager.Team, SettingsManager.Role);
		int mustacheID = SettingsManager.MustacheID;
		int beardID = SettingsManager.BeardID;
		int jerseyID = SettingsManager.GetJerseyID(SettingsManager.Team, SettingsManager.Role);
		if (BackendManager.PlayerState.PlayerData != null)
		{
			this.lockerRoomPlayer.SetUsername(BackendManager.PlayerState.PlayerData.username);
			this.lockerRoomPlayer.SetNumber(BackendManager.PlayerState.PlayerData.number.ToString());
		}
		this.lockerRoomPlayer.SetLegsPadsActive(SettingsManager.Role == PlayerRole.Goalie);
		this.lockerRoomPlayer.SetFlagID(flagID);
		this.lockerRoomPlayer.SetHeadgearID(headgearID, SettingsManager.Role);
		this.lockerRoomPlayer.SetMustacheID(mustacheID);
		this.lockerRoomPlayer.SetBeardID(beardID);
		this.lockerRoomPlayer.SetJerseyID(jerseyID, SettingsManager.Team);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00008F2A File Offset: 0x0000712A
	private void Event_OnTeamChanged(Dictionary<string, object> message)
	{
		this.ApplySettings();
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00008F2A File Offset: 0x0000712A
	private void Event_OnRoleChanged(Dictionary<string, object> message)
	{
		this.ApplySettings();
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0001663C File Offset: 0x0001483C
	private void Event_OnAppearanceClickItem(Dictionary<string, object> message)
	{
		Item item = message["item"] as Item;
		AppearanceCategory appearanceCategory = (AppearanceCategory)message["category"];
		AppearanceSubcategory appearanceSubcategory = (AppearanceSubcategory)message["subcategory"];
		PlayerTeam team = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		switch (appearanceSubcategory)
		{
		case AppearanceSubcategory.Headgear:
			this.lockerRoomPlayer.SetHeadgearID(item.id, role);
			return;
		case AppearanceSubcategory.Flags:
			this.lockerRoomPlayer.SetFlagID(item.id);
			return;
		case AppearanceSubcategory.Mustaches:
			this.lockerRoomPlayer.SetMustacheID(item.id);
			return;
		case AppearanceSubcategory.Beards:
			this.lockerRoomPlayer.SetBeardID(item.id);
			return;
		case AppearanceSubcategory.Jerseys:
			this.lockerRoomPlayer.SetJerseyID(item.id, team);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00008F32 File Offset: 0x00007132
	private void Event_OnAppearanceShow(Dictionary<string, object> message)
	{
		this.lockerRoomPlayer.AllowRotation = true;
		this.lockerRoomPlayer.SetRotationFromPreset("front");
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00008F50 File Offset: 0x00007150
	private void Event_OnAppearanceHide(Dictionary<string, object> message)
	{
		this.ApplySettings();
		this.lockerRoomPlayer.AllowRotation = false;
		this.lockerRoomPlayer.SetRotationFromPreset("front");
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00008F74 File Offset: 0x00007174
	private void Event_OnIdentityShow(Dictionary<string, object> message)
	{
		this.lockerRoomPlayer.AllowRotation = true;
		this.lockerRoomPlayer.SetRotationFromPreset("back");
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00008F92 File Offset: 0x00007192
	private void Event_OnIdentityHide(Dictionary<string, object> message)
	{
		this.lockerRoomPlayer.AllowRotation = false;
		this.lockerRoomPlayer.SetRotationFromPreset("front");
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00008FB0 File Offset: 0x000071B0
	private void Event_OnPlayerDataChanged(Dictionary<string, object> message)
	{
		if ((PlayerData)message["newPlayerData"] == null)
		{
			return;
		}
		this.ApplySettings();
	}

	// Token: 0x0400003B RID: 59
	private LockerRoomPlayer lockerRoomPlayer;
}
