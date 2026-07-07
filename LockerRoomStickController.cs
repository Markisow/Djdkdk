using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class LockerRoomStickController : MonoBehaviour
{
	// Token: 0x06000082 RID: 130 RVA: 0x00016714 File Offset: 0x00014914
	private void Awake()
	{
		this.lockerRoomStick = base.GetComponent<LockerRoomStick>();
		EventManager.AddEventListener("Event_OnTeamChanged", new Action<Dictionary<string, object>>(this.Event_OnTeamChanged));
		EventManager.AddEventListener("Event_OnRoleChanged", new Action<Dictionary<string, object>>(this.Event_OnRoleChanged));
		EventManager.AddEventListener("Event_OnAppearanceClickItem", new Action<Dictionary<string, object>>(this.Event_OnAppearanceClickItem));
		EventManager.AddEventListener("Event_OnAppearanceHide", new Action<Dictionary<string, object>>(this.Event_OnAppearanceHide));
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00009044 File Offset: 0x00007244
	private void Start()
	{
		this.ApplySettings();
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00016788 File Offset: 0x00014988
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnTeamChanged", new Action<Dictionary<string, object>>(this.Event_OnTeamChanged));
		EventManager.RemoveEventListener("Event_OnRoleChanged", new Action<Dictionary<string, object>>(this.Event_OnRoleChanged));
		EventManager.RemoveEventListener("Event_OnAppearanceClickItem", new Action<Dictionary<string, object>>(this.Event_OnAppearanceClickItem));
		EventManager.RemoveEventListener("Event_OnAppearanceHide", new Action<Dictionary<string, object>>(this.Event_OnAppearanceHide));
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000167F0 File Offset: 0x000149F0
	private void ApplySettings()
	{
		int stickSkinID = SettingsManager.GetStickSkinID(SettingsManager.Team, SettingsManager.Role);
		int stickShaftTapeID = SettingsManager.GetStickShaftTapeID(SettingsManager.Team, SettingsManager.Role);
		int stickBladeTapeID = SettingsManager.GetStickBladeTapeID(SettingsManager.Team, SettingsManager.Role);
		this.lockerRoomStick.ShowRoleStick(SettingsManager.Role);
		this.lockerRoomStick.SetSkinID(stickSkinID, SettingsManager.Team, SettingsManager.Role);
		this.lockerRoomStick.SetShaftTapeID(stickShaftTapeID, SettingsManager.Role);
		this.lockerRoomStick.SetBladeTapeID(stickBladeTapeID, SettingsManager.Role);
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00009044 File Offset: 0x00007244
	private void Event_OnTeamChanged(Dictionary<string, object> message)
	{
		this.ApplySettings();
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00009044 File Offset: 0x00007244
	private void Event_OnRoleChanged(Dictionary<string, object> message)
	{
		this.ApplySettings();
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00016878 File Offset: 0x00014A78
	private void Event_OnAppearanceClickItem(Dictionary<string, object> message)
	{
		Item item = message["item"] as Item;
		AppearanceCategory appearanceCategory = (AppearanceCategory)message["category"];
		AppearanceSubcategory appearanceSubcategory = (AppearanceSubcategory)message["subcategory"];
		PlayerTeam team = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		switch (appearanceSubcategory)
		{
		case AppearanceSubcategory.StickSkins:
			this.lockerRoomStick.SetSkinID(item.id, team, role);
			return;
		case AppearanceSubcategory.StickShaftTapes:
			this.lockerRoomStick.SetShaftTapeID(item.id, role);
			return;
		case AppearanceSubcategory.StickBladeTapes:
			this.lockerRoomStick.SetBladeTapeID(item.id, role);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00009044 File Offset: 0x00007244
	private void Event_OnAppearanceHide(Dictionary<string, object> message)
	{
		this.ApplySettings();
	}

	// Token: 0x0400003E RID: 62
	private LockerRoomStick lockerRoomStick;
}
