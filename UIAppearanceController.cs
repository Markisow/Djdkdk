using System;
using System.Collections.Generic;

// Token: 0x02000180 RID: 384
public class UIAppearanceController : UIViewController<UIAppearance>
{
	// Token: 0x06000B20 RID: 2848 RVA: 0x0003FE54 File Offset: 0x0003E054
	public override void Awake()
	{
		base.Awake();
		this.uiAppearance = base.GetComponent<UIAppearance>();
		EventManager.AddEventListener("Event_OnTeamChanged", new Action<Dictionary<string, object>>(this.Event_OnTeamChanged));
		EventManager.AddEventListener("Event_OnRoleChanged", new Action<Dictionary<string, object>>(this.Event_OnRoleChanged));
		EventManager.AddEventListener("Event_OnApplyForBothTeamsChanged", new Action<Dictionary<string, object>>(this.Event_OnApplyForBothTeamsChanged));
		EventManager.AddEventListener("Event_OnFlagIDChanged", new Action<Dictionary<string, object>>(this.Event_OnFlagIDChanged));
		EventManager.AddEventListener("Event_OnHeadgearIDChanged", new Action<Dictionary<string, object>>(this.Event_OnHeadgearIDChanged));
		EventManager.AddEventListener("Event_OnMustacheIDChanged", new Action<Dictionary<string, object>>(this.Event_OnMustacheIDChanged));
		EventManager.AddEventListener("Event_OnBeardIDChanged", new Action<Dictionary<string, object>>(this.Event_OnBeardIDChanged));
		EventManager.AddEventListener("Event_OnJerseyIDChanged", new Action<Dictionary<string, object>>(this.Event_OnJerseyIDChanged));
		EventManager.AddEventListener("Event_OnStickSkinIDChanged", new Action<Dictionary<string, object>>(this.Event_OnStickSkinIDChanged));
		EventManager.AddEventListener("Event_OnStickShaftTapeIDChanged", new Action<Dictionary<string, object>>(this.Event_OnStickShaftTapeIDChanged));
		EventManager.AddEventListener("Event_OnStickBladeTapeIDChanged", new Action<Dictionary<string, object>>(this.Event_OnStickBladeTapeIDChanged));
		EventManager.AddEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		EventManager.AddEventListener("Event_OnAppearanceHide", new Action<Dictionary<string, object>>(this.Event_OnAppearanceHide));
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0003FF94 File Offset: 0x0003E194
	private void Start()
	{
		this.uiAppearance.SetTeam(SettingsManager.Team);
		this.uiAppearance.SetRole(SettingsManager.Role);
		this.uiAppearance.SetApplyForBothTeams(SettingsManager.ApplyForBothTeams);
		this.uiAppearance.SetFlagID(SettingsManager.FlagID);
		this.uiAppearance.SetHeadgearID(PlayerTeam.Blue, PlayerRole.Attacker, SettingsManager.HeadgearIDBlueAttacker);
		this.uiAppearance.SetHeadgearID(PlayerTeam.Blue, PlayerRole.Goalie, SettingsManager.HeadgearIDBlueGoalie);
		this.uiAppearance.SetHeadgearID(PlayerTeam.Red, PlayerRole.Attacker, SettingsManager.HeadgearIDRedAttacker);
		this.uiAppearance.SetHeadgearID(PlayerTeam.Red, PlayerRole.Goalie, SettingsManager.HeadgearIDRedGoalie);
		this.uiAppearance.SetMustacheID(SettingsManager.MustacheID);
		this.uiAppearance.SetBeardID(SettingsManager.BeardID);
		this.uiAppearance.SetJerseyID(PlayerTeam.Blue, PlayerRole.Attacker, SettingsManager.JerseyIDBlueAttacker);
		this.uiAppearance.SetJerseyID(PlayerTeam.Blue, PlayerRole.Goalie, SettingsManager.JerseyIDBlueGoalie);
		this.uiAppearance.SetJerseyID(PlayerTeam.Red, PlayerRole.Attacker, SettingsManager.JerseyIDRedAttacker);
		this.uiAppearance.SetJerseyID(PlayerTeam.Red, PlayerRole.Goalie, SettingsManager.JerseyIDRedGoalie);
		this.uiAppearance.SetStickSkinID(PlayerTeam.Blue, PlayerRole.Attacker, SettingsManager.StickSkinIDBlueAttacker);
		this.uiAppearance.SetStickSkinID(PlayerTeam.Blue, PlayerRole.Goalie, SettingsManager.StickSkinIDBlueGoalie);
		this.uiAppearance.SetStickSkinID(PlayerTeam.Red, PlayerRole.Attacker, SettingsManager.StickSkinIDRedAttacker);
		this.uiAppearance.SetStickSkinID(PlayerTeam.Red, PlayerRole.Goalie, SettingsManager.StickSkinIDRedGoalie);
		this.uiAppearance.SetStickShaftTapeID(PlayerTeam.Blue, PlayerRole.Attacker, SettingsManager.StickShaftTapeIDBlueAttacker);
		this.uiAppearance.SetStickShaftTapeID(PlayerTeam.Blue, PlayerRole.Goalie, SettingsManager.StickShaftTapeIDBlueGoalie);
		this.uiAppearance.SetStickShaftTapeID(PlayerTeam.Red, PlayerRole.Attacker, SettingsManager.StickShaftTapeIDRedAttacker);
		this.uiAppearance.SetStickShaftTapeID(PlayerTeam.Red, PlayerRole.Goalie, SettingsManager.StickShaftTapeIDRedGoalie);
		this.uiAppearance.SetStickBladeTapeID(PlayerTeam.Blue, PlayerRole.Attacker, SettingsManager.StickBladeTapeIDBlueAttacker);
		this.uiAppearance.SetStickBladeTapeID(PlayerTeam.Blue, PlayerRole.Goalie, SettingsManager.StickBladeTapeIDBlueGoalie);
		this.uiAppearance.SetStickBladeTapeID(PlayerTeam.Red, PlayerRole.Attacker, SettingsManager.StickBladeTapeIDRedAttacker);
		this.uiAppearance.SetStickBladeTapeID(PlayerTeam.Red, PlayerRole.Goalie, SettingsManager.StickBladeTapeIDRedGoalie);
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0004016C File Offset: 0x0003E36C
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnTeamChanged", new Action<Dictionary<string, object>>(this.Event_OnTeamChanged));
		EventManager.RemoveEventListener("Event_OnRoleChanged", new Action<Dictionary<string, object>>(this.Event_OnRoleChanged));
		EventManager.RemoveEventListener("Event_OnApplyForBothTeamsChanged", new Action<Dictionary<string, object>>(this.Event_OnApplyForBothTeamsChanged));
		EventManager.RemoveEventListener("Event_OnFlagIDChanged", new Action<Dictionary<string, object>>(this.Event_OnFlagIDChanged));
		EventManager.RemoveEventListener("Event_OnHeadgearIDChanged", new Action<Dictionary<string, object>>(this.Event_OnHeadgearIDChanged));
		EventManager.RemoveEventListener("Event_OnMustacheIDChanged", new Action<Dictionary<string, object>>(this.Event_OnMustacheIDChanged));
		EventManager.RemoveEventListener("Event_OnBeardIDChanged", new Action<Dictionary<string, object>>(this.Event_OnBeardIDChanged));
		EventManager.RemoveEventListener("Event_OnJerseyIDChanged", new Action<Dictionary<string, object>>(this.Event_OnJerseyIDChanged));
		EventManager.RemoveEventListener("Event_OnStickSkinIDChanged", new Action<Dictionary<string, object>>(this.Event_OnStickSkinIDChanged));
		EventManager.RemoveEventListener("Event_OnStickShaftTapeIDChanged", new Action<Dictionary<string, object>>(this.Event_OnStickShaftTapeIDChanged));
		EventManager.RemoveEventListener("Event_OnStickBladeTapeIDChanged", new Action<Dictionary<string, object>>(this.Event_OnStickBladeTapeIDChanged));
		EventManager.RemoveEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		EventManager.RemoveEventListener("Event_OnAppearanceHide", new Action<Dictionary<string, object>>(this.Event_OnAppearanceHide));
		base.OnDestroy();
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x000402A0 File Offset: 0x0003E4A0
	private void Event_OnTeamChanged(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["value"];
		this.uiAppearance.SetTeam(team);
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x000402CC File Offset: 0x0003E4CC
	private void Event_OnRoleChanged(Dictionary<string, object> message)
	{
		PlayerRole role = (PlayerRole)message["value"];
		this.uiAppearance.SetRole(role);
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x000402F8 File Offset: 0x0003E4F8
	private void Event_OnApplyForBothTeamsChanged(Dictionary<string, object> message)
	{
		bool applyForBothTeams = (bool)message["value"];
		this.uiAppearance.SetApplyForBothTeams(applyForBothTeams);
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x00040324 File Offset: 0x0003E524
	private void Event_OnFlagIDChanged(Dictionary<string, object> message)
	{
		int flagID = (int)message["value"];
		this.uiAppearance.SetFlagID(flagID);
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00040350 File Offset: 0x0003E550
	private void Event_OnHeadgearIDChanged(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		int value = (int)message["value"];
		this.uiAppearance.SetHeadgearID(team, role, value);
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x000403A0 File Offset: 0x0003E5A0
	private void Event_OnMustacheIDChanged(Dictionary<string, object> message)
	{
		int mustacheID = (int)message["value"];
		this.uiAppearance.SetMustacheID(mustacheID);
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x000403CC File Offset: 0x0003E5CC
	private void Event_OnBeardIDChanged(Dictionary<string, object> message)
	{
		int beardID = (int)message["value"];
		this.uiAppearance.SetBeardID(beardID);
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x000403F8 File Offset: 0x0003E5F8
	private void Event_OnJerseyIDChanged(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		int value = (int)message["value"];
		this.uiAppearance.SetJerseyID(team, role, value);
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x00040448 File Offset: 0x0003E648
	private void Event_OnStickSkinIDChanged(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		int value = (int)message["value"];
		this.uiAppearance.SetStickSkinID(team, role, value);
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x00040498 File Offset: 0x0003E698
	private void Event_OnStickShaftTapeIDChanged(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		int value = (int)message["value"];
		this.uiAppearance.SetStickShaftTapeID(team, role, value);
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x000404E8 File Offset: 0x0003E6E8
	private void Event_OnStickBladeTapeIDChanged(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		int value = (int)message["value"];
		this.uiAppearance.SetStickBladeTapeID(team, role, value);
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x000111DB File Offset: 0x0000F3DB
	private void Event_OnPlayerDataChanged(Dictionary<string, object> message)
	{
		if ((PlayerData)message["newPlayerData"] == null)
		{
			return;
		}
		this.uiAppearance.StyleRadioButtonGroups();
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000111FB File Offset: 0x0000F3FB
	private void Event_OnAppearanceHide(Dictionary<string, object> message)
	{
		this.uiAppearance.UpdateRadioButtons();
	}

	// Token: 0x040006C1 RID: 1729
	private UIAppearance uiAppearance;
}
