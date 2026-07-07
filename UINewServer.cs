using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001A1 RID: 417
public class UINewServer : UIView
{
	// Token: 0x06000C4E RID: 3150 RVA: 0x00043FA4 File Offset: 0x000421A4
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("NewServerView", null);
		this.newServer = base.View.Query("NewServer", null);
		this.closeIconButton = this.newServer.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnClickClose;
		this.startButton = this.newServer.Query("StartButton", null);
		this.startButton.clicked += this.OnClickStart;
		this.tabView = this.newServer.Query("TabView", null);
		this.dedicatedTab = this.newServer.Query("DedicatedTab", null);
		this.dedicatedNameTextField = this.dedicatedTab.Query("NameTextFieldInput", null).First().Query(null, null);
		this.dedicatedNameTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnDedicatedNameChanged));
		this.dedicatedNameTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnDedicatedNameFocusOut), TrickleDown.NoTrickleDown);
		this.dedicatedNameTextField.value = this.dedicatedName;
		this.dedicatedLocationDropdown = this.dedicatedTab.Query("LocationDropdownInput", null).First().Query(null, null);
		this.dedicatedLocationDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnDedicatedLocationChanged));
		this.dedicatedMaxPlayerSlider = this.dedicatedTab.Query("MaxPlayersSliderInput", null).First().Query(null, null);
		this.dedicatedMaxPlayerSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnDedicatedMaxPlayersChanged));
		this.dedicatedMaxPlayerSlider.value = (float)this.dedicatedMaxPlayers;
		this.dedicatedPasswordTextField = this.dedicatedTab.Query("PasswordTextFieldInput", null).First().Query(null, null);
		this.dedicatedPasswordTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnDedicatedPasswordChanged));
		this.dedicatedPasswordTextField.value = this.dedicatedPassword;
		this.dedicatedPasswordProtectedToggle = this.dedicatedTab.Query("PasswordProtectedToggleInput", null).First().Query(null, null);
		this.dedicatedPasswordProtectedToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnDedicatedPasswordProtectedChanged));
		this.dedicatedPasswordProtectedToggle.value = !string.IsNullOrEmpty(this.dedicatedPassword);
		this.patreonOverlay = this.dedicatedTab.Query("PatreonOverlay", null);
		this.selfHostedTab = this.newServer.Query("SelfHostedTab", null);
		this.selfHostedNameTextField = this.selfHostedTab.Query("NameTextFieldInput", null).First().Query(null, null);
		this.selfHostedNameTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnSelfHostedNameChanged));
		this.selfHostedNameTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnSelfHostedNameFocusOut), TrickleDown.NoTrickleDown);
		this.selfHostedNameTextField.value = this.selfHostedName;
		this.selfHostedPortIntegerField = this.selfHostedTab.Query("PortIntegerFieldInput", null).First().Query(null, null);
		this.selfHostedPortIntegerField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<int>>(this.OnSelfHostedPortChanged));
		this.selfHostedPortIntegerField.value = this.selfHostedPort;
		this.selfHostedMaxPlayerSlider = this.selfHostedTab.Query("MaxPlayersSliderInput", null).First().Query(null, null);
		this.selfHostedMaxPlayerSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnSelfHostedMaxPlayersChanged));
		this.selfHostedMaxPlayerSlider.value = (float)this.selfHostedMaxPlayers;
		this.selfHostedPasswordTextField = this.selfHostedTab.Query("PasswordTextFieldInput", null).First().Query(null, null);
		this.selfHostedPasswordTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnSelfHostedPasswordChanged));
		this.selfHostedPasswordTextField.value = this.selfHostedPassword;
		this.selfHostedPasswordProtectedToggle = this.selfHostedTab.Query("PasswordProtectedToggleInput", null).First().Query(null, null);
		this.selfHostedPasswordProtectedToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnSelfHostedPasswordProtectedChanged));
		this.selfHostedPasswordProtectedToggle.value = !string.IsNullOrEmpty(this.selfHostedPassword);
		this.selfHostedVoipToggle = this.selfHostedTab.Query("VOIPToggleInput", null).First().Query(null, null);
		this.selfHostedVoipToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnSelfHostedVoipChanged));
		this.selfHostedVoipToggle.value = this.selfHostedUseVoip;
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x00012124 File Offset: 0x00010324
	public override bool Show()
	{
		if (!base.IsVisible)
		{
			this.Refresh();
		}
		return base.Show();
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x0004449C File Offset: 0x0004269C
	public void Refresh()
	{
		this.startButton.SetEnabled(false);
		this.dedicatedLauncherLocations = new Location[0];
		this.dedicatedLocationDropdown.choices = new List<string>();
		this.dedicatedLocationDropdown.value = null;
		this.dedicatedLocationDropdown.SetEnabled(false);
		WebSocketManager.Emit("playerGetLocationsRequest", null, "playerGetLocationsResponse");
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0001213A File Offset: 0x0001033A
	public void HidePatreonOverlay()
	{
		this.dedicatedPasswordProtectedToggle.SetEnabled(true);
		this.patreonOverlay.style.display = DisplayStyle.None;
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x0001215E File Offset: 0x0001035E
	public void ShowPatreonOverlay()
	{
		this.dedicatedPasswordProtectedToggle.value = false;
		this.dedicatedPasswordProtectedToggle.SetEnabled(false);
		this.patreonOverlay.style.display = DisplayStyle.Flex;
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x000444FC File Offset: 0x000426FC
	public void SetDedicatedLocations(Location[] locations)
	{
		this.dedicatedLauncherLocations = locations;
		List<string> list = (from location in this.dedicatedLauncherLocations
		select (location.continent + ": " + location.city).ToUpper()).ToList<string>();
		list.Sort();
		this.dedicatedLocationDropdown.choices = list;
		this.dedicatedLocationDropdown.value = this.dedicatedLocationDropdown.choices.First<string>();
		this.dedicatedLocationDropdown.SetEnabled(true);
		this.startButton.SetEnabled(true);
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x0001218E File Offset: 0x0001038E
	private void ResetDedicatedName()
	{
		this.dedicatedName = "MY PUCK SERVER";
		this.dedicatedNameTextField.value = this.dedicatedName;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x000121AC File Offset: 0x000103AC
	private void ResetSelfHostedName()
	{
		this.selfHostedName = "MY PUCK SERVER";
		this.selfHostedNameTextField.value = this.selfHostedName;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x000121CA File Offset: 0x000103CA
	private void OnClickClose()
	{
		EventManager.TriggerEvent("Event_OnNewServerClickClose", null);
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00044588 File Offset: 0x00042788
	private void OnClickStart()
	{
		if (this.tabView.activeTab == this.dedicatedTab)
		{
			EventManager.TriggerEvent("Event_OnNewServerClickStart", new Dictionary<string, object>
			{
				{
					"type",
					"dedicated"
				},
				{
					"name",
					this.dedicatedName
				},
				{
					"maxPlayers",
					this.dedicatedMaxPlayers
				},
				{
					"password",
					this.dedicatedPassword
				},
				{
					"locationId",
					this.dedicatedLocation.id
				}
			});
			return;
		}
		if (this.tabView.activeTab == this.selfHostedTab)
		{
			EventManager.TriggerEvent("Event_OnNewServerClickStart", new Dictionary<string, object>
			{
				{
					"type",
					"selfHosted"
				},
				{
					"port",
					this.selfHostedPort
				},
				{
					"name",
					this.selfHostedName
				},
				{
					"maxPlayers",
					this.selfHostedMaxPlayers
				},
				{
					"password",
					this.selfHostedPassword
				},
				{
					"useVoip",
					this.selfHostedUseVoip
				}
			});
		}
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x000121D7 File Offset: 0x000103D7
	private void OnDedicatedNameChanged(ChangeEvent<string> changeEvent)
	{
		this.dedicatedName = StringUtils.FilterStringSpecialCharacters(changeEvent.newValue, null, null);
		this.dedicatedNameTextField.value = this.dedicatedName;
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x000121FD File Offset: 0x000103FD
	private void OnDedicatedNameFocusOut(FocusOutEvent focusOutEvent)
	{
		this.dedicatedName = StringUtils.FilterStringProfanity(this.dedicatedName, false);
		if (string.IsNullOrEmpty(this.dedicatedName))
		{
			this.ResetDedicatedName();
			return;
		}
		this.dedicatedNameTextField.value = this.dedicatedName;
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x000446B0 File Offset: 0x000428B0
	private void OnDedicatedLocationChanged(ChangeEvent<string> changeEvent)
	{
		Location location2 = this.dedicatedLauncherLocations.FirstOrDefault((Location location) => (location.continent + ": " + location.city).ToUpper() == changeEvent.newValue);
		if (location2 == null)
		{
			return;
		}
		this.dedicatedLocation = location2;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00012236 File Offset: 0x00010436
	private void OnDedicatedMaxPlayersChanged(ChangeEvent<float> changeEvent)
	{
		this.dedicatedMaxPlayers = Mathf.RoundToInt(changeEvent.newValue);
		this.dedicatedMaxPlayerSlider.value = (float)this.dedicatedMaxPlayers;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x0001225B File Offset: 0x0001045B
	private void OnDedicatedPasswordProtectedChanged(ChangeEvent<bool> changeEvent)
	{
		if (changeEvent.newValue)
		{
			this.dedicatedPasswordTextField.SetEnabled(true);
			return;
		}
		this.dedicatedPasswordTextField.SetEnabled(false);
		this.dedicatedPasswordTextField.value = string.Empty;
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0001228E File Offset: 0x0001048E
	private void OnDedicatedPasswordChanged(ChangeEvent<string> changeEvent)
	{
		this.dedicatedPassword = changeEvent.newValue;
		this.dedicatedPasswordTextField.value = this.dedicatedPassword;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x000122AD File Offset: 0x000104AD
	private void OnSelfHostedNameChanged(ChangeEvent<string> changeEvent)
	{
		this.selfHostedName = StringUtils.FilterStringSpecialCharacters(changeEvent.newValue, null, null);
		this.selfHostedNameTextField.value = this.selfHostedName;
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000122D3 File Offset: 0x000104D3
	private void OnSelfHostedNameFocusOut(FocusOutEvent focusOutEvent)
	{
		this.selfHostedName = StringUtils.FilterStringProfanity(this.selfHostedName, false);
		if (string.IsNullOrEmpty(this.selfHostedName))
		{
			this.ResetSelfHostedName();
			return;
		}
		this.selfHostedNameTextField.value = this.selfHostedName;
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0001230C File Offset: 0x0001050C
	private void OnSelfHostedPortChanged(ChangeEvent<int> changeEvent)
	{
		this.selfHostedPort = changeEvent.newValue;
		this.selfHostedPortIntegerField.value = this.selfHostedPort;
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x0001232B File Offset: 0x0001052B
	private void OnSelfHostedMaxPlayersChanged(ChangeEvent<float> changeEvent)
	{
		this.selfHostedMaxPlayers = Mathf.RoundToInt(changeEvent.newValue);
		this.selfHostedMaxPlayerSlider.value = (float)this.selfHostedMaxPlayers;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00012350 File Offset: 0x00010550
	private void OnSelfHostedPasswordProtectedChanged(ChangeEvent<bool> changeEvent)
	{
		if (changeEvent.newValue)
		{
			this.selfHostedPasswordTextField.SetEnabled(true);
			return;
		}
		this.selfHostedPasswordTextField.SetEnabled(false);
		this.selfHostedPasswordTextField.value = string.Empty;
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00012383 File Offset: 0x00010583
	private void OnSelfHostedPasswordChanged(ChangeEvent<string> changeEvent)
	{
		this.selfHostedPassword = changeEvent.newValue;
		this.selfHostedPasswordTextField.value = this.selfHostedPassword;
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x000123A2 File Offset: 0x000105A2
	private void OnSelfHostedVoipChanged(ChangeEvent<bool> changeEvent)
	{
		this.selfHostedUseVoip = changeEvent.newValue;
	}

	// Token: 0x04000750 RID: 1872
	private VisualElement newServer;

	// Token: 0x04000751 RID: 1873
	private IconButton closeIconButton;

	// Token: 0x04000752 RID: 1874
	private Button startButton;

	// Token: 0x04000753 RID: 1875
	private TabView tabView;

	// Token: 0x04000754 RID: 1876
	private Tab dedicatedTab;

	// Token: 0x04000755 RID: 1877
	private Tab selfHostedTab;

	// Token: 0x04000756 RID: 1878
	private TextField dedicatedNameTextField;

	// Token: 0x04000757 RID: 1879
	private DropdownField dedicatedLocationDropdown;

	// Token: 0x04000758 RID: 1880
	private Slider dedicatedMaxPlayerSlider;

	// Token: 0x04000759 RID: 1881
	private Toggle dedicatedPasswordProtectedToggle;

	// Token: 0x0400075A RID: 1882
	private TextField dedicatedPasswordTextField;

	// Token: 0x0400075B RID: 1883
	private TextField selfHostedNameTextField;

	// Token: 0x0400075C RID: 1884
	private IntegerField selfHostedPortIntegerField;

	// Token: 0x0400075D RID: 1885
	private Slider selfHostedMaxPlayerSlider;

	// Token: 0x0400075E RID: 1886
	private Toggle selfHostedPasswordProtectedToggle;

	// Token: 0x0400075F RID: 1887
	private TextField selfHostedPasswordTextField;

	// Token: 0x04000760 RID: 1888
	private Toggle selfHostedVoipToggle;

	// Token: 0x04000761 RID: 1889
	private VisualElement patreonOverlay;

	// Token: 0x04000762 RID: 1890
	private Location[] dedicatedLauncherLocations = new Location[0];

	// Token: 0x04000763 RID: 1891
	private string dedicatedName = "MY PUCK SERVER";

	// Token: 0x04000764 RID: 1892
	private Location dedicatedLocation;

	// Token: 0x04000765 RID: 1893
	private int dedicatedMaxPlayers = 6;

	// Token: 0x04000766 RID: 1894
	private string dedicatedPassword;

	// Token: 0x04000767 RID: 1895
	private int selfHostedPort = 30609;

	// Token: 0x04000768 RID: 1896
	private string selfHostedName = "MY PUCK SERVER";

	// Token: 0x04000769 RID: 1897
	private int selfHostedMaxPlayers = 12;

	// Token: 0x0400076A RID: 1898
	private string selfHostedPassword;

	// Token: 0x0400076B RID: 1899
	private bool selfHostedUseVoip;
}
