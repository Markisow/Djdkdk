using System;
using UnityEngine.UIElements;

// Token: 0x020001A9 RID: 425
public class UIPauseMenu : UIView
{
	// Token: 0x06000C8D RID: 3213 RVA: 0x00044EC8 File Offset: 0x000430C8
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("PauseMenuView", null);
		this.pauseMenu = base.View.Query("PauseMenu", null);
		this.selectTeamButton = this.pauseMenu.Query("SelectTeamButton", null);
		this.selectTeamButton.clicked += this.OnClickSelectTeam;
		this.selectPositionButton = this.pauseMenu.Query("SelectPositionButton", null);
		this.selectPositionButton.clicked += this.OnClickSelectPosition;
		this.serverBrowserButton = this.pauseMenu.Query("ServerBrowserButton", null);
		this.serverBrowserButton.clicked += this.OnClickServerBrowser;
		this.settingsButton = this.pauseMenu.Query("SettingsButton", null);
		this.settingsButton.clicked += this.OnClickSettings;
		this.disconnectButton = this.pauseMenu.Query("DisconnectButton", null);
		this.disconnectButton.clicked += this.OnClickDisconnect;
		this.exitGameButton = this.pauseMenu.Query("ExitGameButton", null);
		this.exitGameButton.clicked += this.OnClickExitGame;
	}

	// Token: 0x06000C8E RID: 3214 RVA: 0x0001267B File Offset: 0x0001087B
	private void OnClickSelectTeam()
	{
		EventManager.TriggerEvent("Event_OnPauseMenuClickSelectTeam", null);
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x00012688 File Offset: 0x00010888
	private void OnClickSelectPosition()
	{
		EventManager.TriggerEvent("Event_OnPauseMenuClickSelectPosition", null);
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x00012695 File Offset: 0x00010895
	private void OnClickServerBrowser()
	{
		EventManager.TriggerEvent("Event_OnPauseMenuClickServerBrowser", null);
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x000126A2 File Offset: 0x000108A2
	private void OnClickSettings()
	{
		EventManager.TriggerEvent("Event_OnPauseMenuClickSettings", null);
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x000126AF File Offset: 0x000108AF
	private void OnClickDisconnect()
	{
		EventManager.TriggerEvent("Event_OnPauseMenuClickDisconnect", null);
	}

	// Token: 0x06000C93 RID: 3219 RVA: 0x000126BC File Offset: 0x000108BC
	private void OnClickExitGame()
	{
		EventManager.TriggerEvent("Event_OnPauseMenuClickExitGame", null);
	}

	// Token: 0x04000787 RID: 1927
	private VisualElement pauseMenu;

	// Token: 0x04000788 RID: 1928
	private Button selectTeamButton;

	// Token: 0x04000789 RID: 1929
	private Button selectPositionButton;

	// Token: 0x0400078A RID: 1930
	private Button serverBrowserButton;

	// Token: 0x0400078B RID: 1931
	private Button disconnectButton;

	// Token: 0x0400078C RID: 1932
	private Button settingsButton;

	// Token: 0x0400078D RID: 1933
	private Button exitGameButton;
}
