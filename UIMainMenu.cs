using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

// Token: 0x02000197 RID: 407
public class UIMainMenu : UIView
{
	// Token: 0x06000BD2 RID: 3026 RVA: 0x000420B4 File Offset: 0x000402B4
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("MainMenuView", null);
		this.mainMenu = base.View.Query("MainMenu", null);
		this.debug = base.View.Query("Debug", null);
		this.social = base.View.Query("Social", null);
		this.playButton = this.mainMenu.Query("PlayButton", null);
		this.playButton.clicked += this.OnClickPlay;
		this.playerButton = this.mainMenu.Query("PlayerButton", null);
		this.playerButton.clicked += this.OnClickPlayer;
		this.settingsButton = this.mainMenu.Query("SettingsButton", null);
		this.settingsButton.clicked += this.OnClickSettings;
		this.modsButton = this.mainMenu.Query("ModsButton", null);
		this.modsButton.clicked += this.OnClickMods;
		this.exitGameButton = this.mainMenu.Query("ExitGameButton", null);
		this.exitGameButton.clicked += this.OnClickExitGame;
		this.discordButton = this.social.Query("DiscordButton", null);
		this.discordButton.clicked += this.OnClickDiscord;
		this.patreonButton = this.social.Query("PatreonButton", null);
		this.patreonButton.clicked += this.OnClickPatreon;
		this.ipAddressTextField = this.debug.Query("IpAddressTextField", null).First().Query(null, null);
		this.ipAddressTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnIpAddressChanged));
		this.ipAddressTextField.value = this.ipAddress;
		this.portIntegerField = this.debug.Query("PortIntegerField", null).First().Query(null, null);
		this.portIntegerField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<int>>(this.OnPortChanged));
		this.portIntegerField.value = (int)this.port;
		this.passwordTextField = this.debug.Query("PasswordTextField", null).First().Query(null, null);
		this.passwordTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnPasswordChanged));
		this.passwordTextField.value = this.password;
		this.joinServerButton = this.debug.Query("JoinServerButton", null);
		this.joinServerButton.clicked += this.OnClickJoinServer;
		this.hostServerButton = this.debug.Query("HostServerButton", null);
		this.hostServerButton.clicked += this.OnClickHostServer;
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00011B65 File Offset: 0x0000FD65
	public override bool Show()
	{
		bool flag = base.Show();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnMainMenuShow", null);
		}
		return flag;
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00011B7B File Offset: 0x0000FD7B
	public override bool Hide()
	{
		bool flag = base.Hide();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnMainMenuHide", null);
		}
		return flag;
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00011B91 File Offset: 0x0000FD91
	public void ShowDebug()
	{
		this.debug.style.display = DisplayStyle.Flex;
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x00011BA9 File Offset: 0x0000FDA9
	public void HideDebug()
	{
		this.debug.style.display = DisplayStyle.None;
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00011BC1 File Offset: 0x0000FDC1
	private void OnIpAddressChanged(ChangeEvent<string> changeEvent)
	{
		this.ipAddress = changeEvent.newValue;
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00011BCF File Offset: 0x0000FDCF
	private void OnPortChanged(ChangeEvent<int> changeEvent)
	{
		this.port = (ushort)changeEvent.newValue;
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00011BDE File Offset: 0x0000FDDE
	private void OnPasswordChanged(ChangeEvent<string> changeEvent)
	{
		this.password = changeEvent.newValue;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x000423F4 File Offset: 0x000405F4
	private void OnClickJoinServer()
	{
		EventManager.TriggerEvent("Event_OnMainMenuClickJoinServer", new Dictionary<string, object>
		{
			{
				"ipAddress",
				this.ipAddress
			},
			{
				"port",
				this.port
			},
			{
				"password",
				this.password
			}
		});
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00011BEC File Offset: 0x0000FDEC
	private void OnClickHostServer()
	{
		EventManager.TriggerEvent("Event_OnMainMenuClickHostServer", new Dictionary<string, object>
		{
			{
				"port",
				this.port
			},
			{
				"password",
				this.password
			}
		});
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00011C24 File Offset: 0x0000FE24
	private void OnClickPlay()
	{
		EventManager.TriggerEvent("Event_OnMainMenuClickPlay", null);
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00011C31 File Offset: 0x0000FE31
	private void OnClickPlayer()
	{
		EventManager.TriggerEvent("Event_OnMainMenuClickPlayer", null);
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00011C3E File Offset: 0x0000FE3E
	private void OnClickSettings()
	{
		EventManager.TriggerEvent("Event_OnMainMenuClickSettings", null);
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00011C4B File Offset: 0x0000FE4B
	private void OnClickMods()
	{
		EventManager.TriggerEvent("Event_OnMainMenuClickMods", null);
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00011C58 File Offset: 0x0000FE58
	private void OnClickExitGame()
	{
		EventManager.TriggerEvent("Event_OnMainMenuClickExitGame", null);
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00011C65 File Offset: 0x0000FE65
	private void OnClickDiscord()
	{
		EventManager.TriggerEvent("Event_OnSocialClickDiscord", null);
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00011C72 File Offset: 0x0000FE72
	private void OnClickPatreon()
	{
		EventManager.TriggerEvent("Event_OnSocialClickPatreon", null);
	}

	// Token: 0x04000714 RID: 1812
	private VisualElement mainMenu;

	// Token: 0x04000715 RID: 1813
	private VisualElement debug;

	// Token: 0x04000716 RID: 1814
	private TextField ipAddressTextField;

	// Token: 0x04000717 RID: 1815
	private IntegerField portIntegerField;

	// Token: 0x04000718 RID: 1816
	private TextField passwordTextField;

	// Token: 0x04000719 RID: 1817
	private Button joinServerButton;

	// Token: 0x0400071A RID: 1818
	private Button hostServerButton;

	// Token: 0x0400071B RID: 1819
	private Button playButton;

	// Token: 0x0400071C RID: 1820
	private Button playerButton;

	// Token: 0x0400071D RID: 1821
	private Button settingsButton;

	// Token: 0x0400071E RID: 1822
	private Button modsButton;

	// Token: 0x0400071F RID: 1823
	private Button exitGameButton;

	// Token: 0x04000720 RID: 1824
	private VisualElement social;

	// Token: 0x04000721 RID: 1825
	private Button discordButton;

	// Token: 0x04000722 RID: 1826
	private Button patreonButton;

	// Token: 0x04000723 RID: 1827
	private string ipAddress = "127.0.0.1";

	// Token: 0x04000724 RID: 1828
	private ushort port = 30609;

	// Token: 0x04000725 RID: 1829
	private string password;
}
