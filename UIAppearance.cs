using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x0200016D RID: 365
public class UIAppearance : UIView
{
	// Token: 0x06000ACB RID: 2763 RVA: 0x0003E164 File Offset: 0x0003C364
	public void Initialize(VisualElement rootVisualElement)
	{
		this.ValidateAppearanceItems();
		base.View = rootVisualElement.Query("AppearanceView", null);
		this.appearance = base.View.Query("Appearance", null);
		this.closeIconButton = this.appearance.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnClickClose;
		this.categoryTabView = this.appearance.Query(null, null);
		this.categoryTabView.activeTabChanged += this.OnCategoryTabChanged;
		this.headTab = this.categoryTabView.Query("HeadTab", null);
		this.headTabView = this.headTab.Query(null, null);
		this.headTabView.activeTabChanged += this.OnSubcategoryTabChanged;
		this.flagsTab = this.headTabView.Query("FlagsTab", null);
		this.flagsRadioButtonGroup = this.flagsTab.Query("AppearanceItemRadioButtonGroup", null);
		this.headgearTab = this.headTabView.Query("HeadgearTab", null);
		this.headgearRadioButtonGroup = this.headgearTab.Query("AppearanceItemRadioButtonGroup", null);
		this.mustachesTab = this.headTabView.Query("MustachesTab", null);
		this.mustachesRadioButtonGroup = this.mustachesTab.Query("AppearanceItemRadioButtonGroup", null);
		this.beardsTab = this.headTabView.Query("BeardsTab", null);
		this.beardsRadioButtonGroup = this.beardsTab.Query("AppearanceItemRadioButtonGroup", null);
		this.bodyTab = this.categoryTabView.Query("BodyTab", null);
		this.bodyTabView = this.bodyTab.Query(null, null);
		this.bodyTabView.activeTabChanged += this.OnSubcategoryTabChanged;
		this.jerseysTab = this.bodyTabView.Query("JerseysTab", null);
		this.jerseysRadioButtonGroup = this.jerseysTab.Query("AppearanceItemRadioButtonGroup", null);
		this.stickTab = this.categoryTabView.Query("StickTab", null);
		this.stickTabView = this.stickTab.Query(null, null);
		this.stickTabView.activeTabChanged += this.OnSubcategoryTabChanged;
		this.stickSkinsTab = this.stickTabView.Query("SkinsTab", null);
		this.stickSkinsRadioButtonGroup = this.stickSkinsTab.Query("AppearanceItemRadioButtonGroup", null);
		this.stickShaftTapesTab = this.stickTabView.Query("ShaftTapesTab", null);
		this.stickShaftTapesRadioButtonGroup = this.stickShaftTapesTab.Query("AppearanceItemRadioButtonGroup", null);
		this.stickBladeTapesTab = this.stickTabView.Query("BladeTapesTab", null);
		this.stickBladeTapesRadioButtonGroup = this.stickBladeTapesTab.Query("AppearanceItemRadioButtonGroup", null);
		this.teamDropdown = this.appearance.Query("TeamInput", null).First().Query(null, null);
		this.teamDropdown.choices = Utils.GetTeamNames();
		this.teamDropdown.value = Utils.GetNameFromTeam(SettingsManager.Team);
		this.teamDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTeamDropdownChanged));
		this.roleDropdown = this.appearance.Query("RoleInput", null).First().Query(null, null);
		this.roleDropdown.choices = Utils.GetRoleNames();
		this.roleDropdown.value = Utils.GetNameFromRole(SettingsManager.Role);
		this.roleDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnRoleDropdownChanged));
		this.applyForBothTeamsToggle = this.appearance.Query("ApplyForBothTeamsInput", null).First().Query(null, null);
		this.applyForBothTeamsToggle.value = SettingsManager.ApplyForBothTeams;
		this.applyForBothTeamsToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnApplyForBothTeamsToggleChanged));
		this.categoryTabView.activeTab = this.headTab;
		this.headTabView.activeTab = this.flagsTab;
		this.PopulateRadioButtonGroups();
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x0003E608 File Offset: 0x0003C808
	public override bool Show()
	{
		bool flag = base.Show();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnAppearanceShow", new Dictionary<string, object>
			{
				{
					"category",
					this.category
				},
				{
					"subcategory",
					this.categorySubcategoryMap[this.category]
				}
			});
		}
		return flag;
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x00010FB3 File Offset: 0x0000F1B3
	public override bool Hide()
	{
		bool flag = base.Hide();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnAppearanceHide", null);
		}
		return flag;
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x00010FC9 File Offset: 0x0000F1C9
	public void SetTeam(PlayerTeam value)
	{
		this.team = value;
		this.StyleRadioButtonGroups();
		this.UpdateRadioButtons();
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x00010FDE File Offset: 0x0000F1DE
	public void SetRole(PlayerRole value)
	{
		this.role = value;
		this.StyleRadioButtonGroups();
		this.UpdateRadioButtons();
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00010FF3 File Offset: 0x0000F1F3
	public void SetApplyForBothTeams(bool value)
	{
		this.applyForBothTeams = value;
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x00010FFC File Offset: 0x0000F1FC
	public void SetFlagID(int value)
	{
		this.flagID = value;
		this.UpdateFlagsRadioButtons();
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0003E664 File Offset: 0x0003C864
	public void SetHeadgearID(PlayerTeam team, PlayerRole role, int value)
	{
		if (team == PlayerTeam.Blue && role == PlayerRole.Attacker)
		{
			this.headgearIDBlueAttacker = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Attacker)
		{
			this.headgearIDRedAttacker = value;
		}
		else if (team == PlayerTeam.Blue && role == PlayerRole.Goalie)
		{
			this.headgearIDBlueGoalie = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Goalie)
		{
			this.headgearIDRedGoalie = value;
		}
		this.UpdateHeadgearRadioButtons();
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x0001100B File Offset: 0x0000F20B
	public void SetMustacheID(int value)
	{
		this.mustacheID = value;
		this.UpdateMustacheRadioButtons();
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0001101A File Offset: 0x0000F21A
	public void SetBeardID(int value)
	{
		this.beardID = value;
		this.UpdateBeardRadioButtons();
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0003E6BC File Offset: 0x0003C8BC
	public void SetJerseyID(PlayerTeam team, PlayerRole role, int value)
	{
		if (team == PlayerTeam.Blue && role == PlayerRole.Attacker)
		{
			this.jerseyIDBlueAttacker = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Attacker)
		{
			this.jerseyIDRedAttacker = value;
		}
		else if (team == PlayerTeam.Blue && role == PlayerRole.Goalie)
		{
			this.jerseyIDBlueGoalie = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Goalie)
		{
			this.jerseyIDRedGoalie = value;
		}
		this.UpdateJerseyRadioButtons();
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0003E714 File Offset: 0x0003C914
	public void SetStickSkinID(PlayerTeam team, PlayerRole role, int value)
	{
		if (team == PlayerTeam.Blue && role == PlayerRole.Attacker)
		{
			this.stickSkinIDBlueAttacker = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Attacker)
		{
			this.stickSkinIDRedAttacker = value;
		}
		else if (team == PlayerTeam.Blue && role == PlayerRole.Goalie)
		{
			this.stickSkinIDBlueGoalie = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Goalie)
		{
			this.stickSkinIDRedGoalie = value;
		}
		this.UpdateStickSkinRadioButtons();
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0003E76C File Offset: 0x0003C96C
	public void SetStickShaftTapeID(PlayerTeam team, PlayerRole role, int value)
	{
		if (team == PlayerTeam.Blue && role == PlayerRole.Attacker)
		{
			this.stickShaftTapeIDBlueAttacker = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Attacker)
		{
			this.stickShaftTapeIDRedAttacker = value;
		}
		else if (team == PlayerTeam.Blue && role == PlayerRole.Goalie)
		{
			this.stickShaftTapeIDBlueGoalie = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Goalie)
		{
			this.stickShaftTapeIDRedGoalie = value;
		}
		this.UpdateStickShaftTapeRadioButtons();
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0003E7C4 File Offset: 0x0003C9C4
	public void SetStickBladeTapeID(PlayerTeam team, PlayerRole role, int value)
	{
		if (team == PlayerTeam.Blue && role == PlayerRole.Attacker)
		{
			this.stickBladeTapeIDBlueAttacker = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Attacker)
		{
			this.stickBladeTapeIDRedAttacker = value;
		}
		else if (team == PlayerTeam.Blue && role == PlayerRole.Goalie)
		{
			this.stickBladeTapeIDBlueGoalie = value;
		}
		else if (team == PlayerTeam.Red && role == PlayerRole.Goalie)
		{
			this.stickBladeTapeIDRedGoalie = value;
		}
		this.UpdateStickBladeTapeRadioButtons();
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0003E81C File Offset: 0x0003CA1C
	public void StyleRadioButtonGroups()
	{
		this.StyleRadioButtonGroup(this.flagsRadioButtonGroup);
		this.StyleRadioButtonGroup(this.headgearRadioButtonGroup);
		this.StyleRadioButtonGroup(this.mustachesRadioButtonGroup);
		this.StyleRadioButtonGroup(this.beardsRadioButtonGroup);
		this.StyleRadioButtonGroup(this.jerseysRadioButtonGroup);
		this.StyleRadioButtonGroup(this.stickSkinsRadioButtonGroup);
		this.StyleRadioButtonGroup(this.stickShaftTapesRadioButtonGroup);
		this.StyleRadioButtonGroup(this.stickBladeTapesRadioButtonGroup);
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00011029 File Offset: 0x0000F229
	public void UpdateRadioButtons()
	{
		this.UpdateFlagsRadioButtons();
		this.UpdateHeadgearRadioButtons();
		this.UpdateMustacheRadioButtons();
		this.UpdateBeardRadioButtons();
		this.UpdateJerseyRadioButtons();
		this.UpdateStickSkinRadioButtons();
		this.UpdateStickShaftTapeRadioButtons();
		this.UpdateStickBladeTapeRadioButtons();
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0003E88C File Offset: 0x0003CA8C
	private void ValidateAppearanceItems()
	{
		ItemManager.GetItemsByCategories(new string[]
		{
			"flag"
		}).ForEach(delegate(Item item)
		{
			if (!this.flags.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Flag item {0} ({1}) is missing from the appearance flags list", item.name, item.id));
			}
		});
		ItemManager.GetItemsByCategories(new string[]
		{
			"headgear"
		}).ForEach(delegate(Item item)
		{
			if (!this.headgear.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Headgear item {0} ({1}) is missing from the appearance headgear list", item.name, item.id));
			}
		});
		ItemManager.GetItemsByCategories(new string[]
		{
			"mustache"
		}).ForEach(delegate(Item item)
		{
			if (!this.mustaches.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Mustache item {0} ({1}) is missing from the appearance mustaches list", item.name, item.id));
			}
		});
		ItemManager.GetItemsByCategories(new string[]
		{
			"beard"
		}).ForEach(delegate(Item item)
		{
			if (!this.beards.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Beard item {0} ({1}) is missing from the appearance beards list", item.name, item.id));
			}
		});
		ItemManager.GetItemsByCategories(new string[]
		{
			"jersey"
		}).ForEach(delegate(Item item)
		{
			if (!this.jerseys.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Jersey item {0} ({1}) is missing from the appearance jerseys list", item.name, item.id));
			}
		});
		ItemManager.GetItemsByCategories(new string[]
		{
			"stickSkin"
		}).ForEach(delegate(Item item)
		{
			if (!this.stickSkins.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Stick skin item {0} ({1}) is missing from the appearance stick skins list", item.name, item.id));
			}
		});
		ItemManager.GetItemsByCategories(new string[]
		{
			"stickShaftTape"
		}).ForEach(delegate(Item item)
		{
			if (!this.stickShaftTapes.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Stick shaft tape item {0} ({1}) is missing from the appearance stick shaft tapes list", item.name, item.id));
			}
		});
		ItemManager.GetItemsByCategories(new string[]
		{
			"stickBladeTape"
		}).ForEach(delegate(Item item)
		{
			if (!this.stickBladeTapes.Any((AppearanceItem appearanceItem) => appearanceItem.Id == item.id))
			{
				UIAppearance.Logger.Warning(string.Format("Stick blade tape item {0} ({1}) is missing from the appearance stick blade tapes list", item.name, item.id));
			}
		});
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x0003E9BC File Offset: 0x0003CBBC
	private void PopulateRadioButtonGroups()
	{
		this.PopulateRadioButtonGroup(this.flagsRadioButtonGroup, this.flags);
		this.PopulateRadioButtonGroup(this.headgearRadioButtonGroup, this.headgear);
		this.PopulateRadioButtonGroup(this.mustachesRadioButtonGroup, this.mustaches);
		this.PopulateRadioButtonGroup(this.beardsRadioButtonGroup, this.beards);
		this.PopulateRadioButtonGroup(this.jerseysRadioButtonGroup, this.jerseys);
		this.PopulateRadioButtonGroup(this.stickSkinsRadioButtonGroup, this.stickSkins);
		this.PopulateRadioButtonGroup(this.stickShaftTapesRadioButtonGroup, this.stickShaftTapes);
		this.PopulateRadioButtonGroup(this.stickBladeTapesRadioButtonGroup, this.stickBladeTapes);
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0003EA5C File Offset: 0x0003CC5C
	private void PopulateRadioButtonGroup(RadioButtonGroup radioButtonGroup, List<AppearanceItem> appearanceItems)
	{
		VisualElement visualElement = radioButtonGroup.Query("AppearanceItemList", null);
		visualElement.Clear();
		foreach (AppearanceItem appearanceItem in appearanceItems)
		{
			RadioButton radioButton = this.appearanceItemAsset.Instantiate().Query("AppearanceItemRadioButton", null);
			Button button = radioButton.Query("PurchaseButton", null).First().Query(null, null);
			Item item;
			if (appearanceItem.Id == -1)
			{
				item = new Item
				{
					id = -1,
					name = "NONE"
				};
			}
			else
			{
				item = ItemManager.GetItemById(appearanceItem.Id);
				if (item == null)
				{
					UIAppearance.Logger.Error(string.Format("Could not populate appearance item with ID {0} because the item was not found in ItemManager", appearanceItem.Id));
					continue;
				}
				button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					this.OnClickPurchase(item);
				}, TrickleDown.NoTrickleDown);
				button.text = "BUY $" + ((float)item.price / 100f).ToString("F2", CultureInfo.InvariantCulture);
			}
			radioButton.label = item.name.ToUpper();
			radioButton.userData = new Dictionary<string, object>
			{
				{
					"item",
					item
				}
			};
			radioButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OnClickAppearanceItem(item);
			}, TrickleDown.NoTrickleDown);
			visualElement.Add(radioButton);
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x0003EC20 File Offset: 0x0003CE20
	private void StyleRadioButtonGroup(RadioButtonGroup radioButtonGroup)
	{
		VisualElement visualElement = radioButtonGroup.Query("AppearanceItemList", null);
		visualElement.hierarchy.Sort(delegate(VisualElement a, VisualElement b)
		{
			Item item = (a.userData as Dictionary<string, object>)["item"] as Item;
			Item item2 = (b.userData as Dictionary<string, object>)["item"] as Item;
			if ((item.IsOwned && !item2.IsOwned) || item.id == -1)
			{
				return -1;
			}
			if ((!item.IsOwned && item2.IsOwned) || item2.id == -1)
			{
				return 1;
			}
			return string.Compare(item.name, item2.name, StringComparison.OrdinalIgnoreCase);
		});
		foreach (RadioButton radioButton in visualElement.Query(null, null).ToList())
		{
			this.StyleRadioButton(radioButton);
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x0003ECBC File Offset: 0x0003CEBC
	private void StyleRadioButton(RadioButton radioButton)
	{
		Item item = (radioButton.userData as Dictionary<string, object>)["item"] as Item;
		bool flag = !((this.role == PlayerRole.Attacker) ? item.IsAttackerItem : item.IsGoalieItem);
		radioButton.EnableInClassList("owned", item.IsOwned);
		radioButton.style.display = (flag ? DisplayStyle.None : DisplayStyle.Flex);
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x0003ED28 File Offset: 0x0003CF28
	private void UpdateFlagsRadioButtons()
	{
		List<RadioButton> radioButtons = this.flagsRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			if (((radioButton.userData as Dictionary<string, object>)["item"] as Item).id == this.flagID)
			{
				this.flagsRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x0003ED74 File Offset: 0x0003CF74
	private void UpdateHeadgearRadioButtons()
	{
		List<RadioButton> radioButtons = this.headgearRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			Item item = (radioButton.userData as Dictionary<string, object>)["item"] as Item;
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Attacker && item.id == this.headgearIDBlueAttacker)
			{
				this.headgearRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Attacker && item.id == this.headgearIDRedAttacker)
			{
				this.headgearRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Goalie && item.id == this.headgearIDBlueGoalie)
			{
				this.headgearRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Goalie && item.id == this.headgearIDRedGoalie)
			{
				this.headgearRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x0003EDC0 File Offset: 0x0003CFC0
	private void UpdateMustacheRadioButtons()
	{
		List<RadioButton> radioButtons = this.mustachesRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			if (((radioButton.userData as Dictionary<string, object>)["item"] as Item).id == this.mustacheID)
			{
				this.mustachesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x0003EE0C File Offset: 0x0003D00C
	private void UpdateBeardRadioButtons()
	{
		List<RadioButton> radioButtons = this.beardsRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			if (((radioButton.userData as Dictionary<string, object>)["item"] as Item).id == this.beardID)
			{
				this.beardsRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x0003EE58 File Offset: 0x0003D058
	private void UpdateJerseyRadioButtons()
	{
		List<RadioButton> radioButtons = this.jerseysRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			Item item = (radioButton.userData as Dictionary<string, object>)["item"] as Item;
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Attacker && item.id == this.jerseyIDBlueAttacker)
			{
				this.jerseysRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Attacker && item.id == this.jerseyIDRedAttacker)
			{
				this.jerseysRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Goalie && item.id == this.jerseyIDBlueGoalie)
			{
				this.jerseysRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Goalie && item.id == this.jerseyIDRedGoalie)
			{
				this.jerseysRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0003EEA4 File Offset: 0x0003D0A4
	private void UpdateStickSkinRadioButtons()
	{
		List<RadioButton> radioButtons = this.stickSkinsRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			Item item = (radioButton.userData as Dictionary<string, object>)["item"] as Item;
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Attacker && item.id == this.stickSkinIDBlueAttacker)
			{
				this.stickSkinsRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Attacker && item.id == this.stickSkinIDRedAttacker)
			{
				this.stickSkinsRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Goalie && item.id == this.stickSkinIDBlueGoalie)
			{
				this.stickSkinsRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Goalie && item.id == this.stickSkinIDRedGoalie)
			{
				this.stickSkinsRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE6 RID: 2790 RVA: 0x0003EEF0 File Offset: 0x0003D0F0
	private void UpdateStickShaftTapeRadioButtons()
	{
		List<RadioButton> radioButtons = this.stickShaftTapesRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			Item item = (radioButton.userData as Dictionary<string, object>)["item"] as Item;
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Attacker && item.id == this.stickShaftTapeIDBlueAttacker)
			{
				this.stickShaftTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Attacker && item.id == this.stickShaftTapeIDRedAttacker)
			{
				this.stickShaftTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Goalie && item.id == this.stickShaftTapeIDBlueGoalie)
			{
				this.stickShaftTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Goalie && item.id == this.stickShaftTapeIDRedGoalie)
			{
				this.stickShaftTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x0003EF3C File Offset: 0x0003D13C
	private void UpdateStickBladeTapeRadioButtons()
	{
		List<RadioButton> radioButtons = this.stickBladeTapesRadioButtonGroup.Query(null, null).ToList();
		radioButtons.ForEach(delegate(RadioButton radioButton)
		{
			Item item = (radioButton.userData as Dictionary<string, object>)["item"] as Item;
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Attacker && item.id == this.stickBladeTapeIDBlueAttacker)
			{
				this.stickBladeTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Attacker && item.id == this.stickBladeTapeIDRedAttacker)
			{
				this.stickBladeTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Blue && this.role == PlayerRole.Goalie && item.id == this.stickBladeTapeIDBlueGoalie)
			{
				this.stickBladeTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
				return;
			}
			if (this.team == PlayerTeam.Red && this.role == PlayerRole.Goalie && item.id == this.stickBladeTapeIDRedGoalie)
			{
				this.stickBladeTapesRadioButtonGroup.value = radioButtons.IndexOf(radioButton);
			}
		});
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x0001105B File Offset: 0x0000F25B
	private void OnClickClose()
	{
		EventManager.TriggerEvent("Event_OnAppearanceClickClose", null);
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x00011068 File Offset: 0x0000F268
	private void OnClickPurchase(Item item)
	{
		EventManager.TriggerEvent("Event_OnAppearanceClickPurchaseItem", new Dictionary<string, object>
		{
			{
				"item",
				item
			}
		});
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x00011085 File Offset: 0x0000F285
	private void OnTeamDropdownChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnAppearanceTeamChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x000110A7 File Offset: 0x0000F2A7
	private void OnRoleDropdownChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnAppearanceRoleChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000AEC RID: 2796 RVA: 0x000110C9 File Offset: 0x0000F2C9
	private void OnApplyForBothTeamsToggleChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnAppearanceApplyForBothTeamsChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000AED RID: 2797 RVA: 0x0003EF88 File Offset: 0x0003D188
	private void OnCategoryTabChanged(Tab oldTab, Tab newTab)
	{
		string name = newTab.name;
		if (!(name == "HeadTab"))
		{
			if (!(name == "BodyTab"))
			{
				if (name == "StickTab")
				{
					this.category = AppearanceCategory.Stick;
				}
			}
			else
			{
				this.category = AppearanceCategory.Body;
			}
		}
		else
		{
			this.category = AppearanceCategory.Head;
		}
		EventManager.TriggerEvent("Event_OnAppearanceCategoryChanged", new Dictionary<string, object>
		{
			{
				"category",
				this.category
			},
			{
				"subcategory",
				this.categorySubcategoryMap[this.category]
			}
		});
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0003F024 File Offset: 0x0003D224
	private void OnSubcategoryTabChanged(Tab oldTab, Tab newTab)
	{
		Dictionary<AppearanceCategory, AppearanceSubcategory> dictionary = this.categorySubcategoryMap;
		AppearanceCategory key = this.category;
		string name = newTab.name;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		AppearanceSubcategory value;
		if (num <= 2933584645U)
		{
			if (num <= 154799819U)
			{
				if (num != 108854953U)
				{
					if (num == 154799819U)
					{
						if (name == "FlagsTab")
						{
							value = AppearanceSubcategory.Flags;
							goto IL_14A;
						}
					}
				}
				else if (name == "JerseysTab")
				{
					value = AppearanceSubcategory.Jerseys;
					goto IL_14A;
				}
			}
			else if (num != 2528141619U)
			{
				if (num == 2933584645U)
				{
					if (name == "BladeTapesTab")
					{
						value = AppearanceSubcategory.StickBladeTapes;
						goto IL_14A;
					}
				}
			}
			else if (name == "ShaftTapesTab")
			{
				value = AppearanceSubcategory.StickShaftTapes;
				goto IL_14A;
			}
		}
		else if (num <= 3172671651U)
		{
			if (num != 3105626632U)
			{
				if (num == 3172671651U)
				{
					if (name == "MustachesTab")
					{
						value = AppearanceSubcategory.Mustaches;
						goto IL_14A;
					}
				}
			}
			else if (name == "SkinsTab")
			{
				value = AppearanceSubcategory.StickSkins;
				goto IL_14A;
			}
		}
		else if (num != 3753505521U)
		{
			if (num == 3900078259U)
			{
				if (name == "HeadgearTab")
				{
					value = AppearanceSubcategory.Headgear;
					goto IL_14A;
				}
			}
		}
		else if (name == "BeardsTab")
		{
			value = AppearanceSubcategory.Beards;
			goto IL_14A;
		}
		value = this.categorySubcategoryMap[this.category];
		IL_14A:
		dictionary[key] = value;
		EventManager.TriggerEvent("Event_OnAppearanceCategoryChanged", new Dictionary<string, object>
		{
			{
				"category",
				this.category
			},
			{
				"subcategory",
				this.categorySubcategoryMap[this.category]
			}
		});
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x0003F1CC File Offset: 0x0003D3CC
	private void OnClickAppearanceItem(Item item)
	{
		EventManager.TriggerEvent("Event_OnAppearanceClickItem", new Dictionary<string, object>
		{
			{
				"item",
				item
			},
			{
				"category",
				this.category
			},
			{
				"subcategory",
				this.categorySubcategoryMap[this.category]
			},
			{
				"team",
				this.team
			},
			{
				"role",
				this.role
			}
		});
	}

	// Token: 0x04000663 RID: 1635
	private static readonly global::Logger Logger = new global::Logger("UIAppearance");

	// Token: 0x04000664 RID: 1636
	[Header("Settings")]
	[SerializeField]
	private List<AppearanceItem> flags = new List<AppearanceItem>();

	// Token: 0x04000665 RID: 1637
	[SerializeField]
	private List<AppearanceItem> headgear = new List<AppearanceItem>();

	// Token: 0x04000666 RID: 1638
	[SerializeField]
	private List<AppearanceItem> mustaches = new List<AppearanceItem>();

	// Token: 0x04000667 RID: 1639
	[SerializeField]
	private List<AppearanceItem> beards = new List<AppearanceItem>();

	// Token: 0x04000668 RID: 1640
	[SerializeField]
	private List<AppearanceItem> jerseys = new List<AppearanceItem>();

	// Token: 0x04000669 RID: 1641
	[SerializeField]
	private List<AppearanceItem> stickSkins = new List<AppearanceItem>();

	// Token: 0x0400066A RID: 1642
	[SerializeField]
	private List<AppearanceItem> stickShaftTapes = new List<AppearanceItem>();

	// Token: 0x0400066B RID: 1643
	[SerializeField]
	private List<AppearanceItem> stickBladeTapes = new List<AppearanceItem>();

	// Token: 0x0400066C RID: 1644
	[Header("References")]
	public VisualTreeAsset appearanceItemAsset;

	// Token: 0x0400066D RID: 1645
	private AppearanceCategory category;

	// Token: 0x0400066E RID: 1646
	private Dictionary<AppearanceCategory, AppearanceSubcategory> categorySubcategoryMap = new Dictionary<AppearanceCategory, AppearanceSubcategory>
	{
		{
			AppearanceCategory.Head,
			AppearanceSubcategory.Flags
		},
		{
			AppearanceCategory.Body,
			AppearanceSubcategory.Jerseys
		},
		{
			AppearanceCategory.Stick,
			AppearanceSubcategory.StickSkins
		}
	};

	// Token: 0x0400066F RID: 1647
	private PlayerTeam team;

	// Token: 0x04000670 RID: 1648
	private PlayerRole role;

	// Token: 0x04000671 RID: 1649
	private bool applyForBothTeams;

	// Token: 0x04000672 RID: 1650
	private int flagID;

	// Token: 0x04000673 RID: 1651
	private int headgearIDBlueAttacker;

	// Token: 0x04000674 RID: 1652
	private int headgearIDRedAttacker;

	// Token: 0x04000675 RID: 1653
	private int headgearIDBlueGoalie;

	// Token: 0x04000676 RID: 1654
	private int headgearIDRedGoalie;

	// Token: 0x04000677 RID: 1655
	private int mustacheID;

	// Token: 0x04000678 RID: 1656
	private int beardID;

	// Token: 0x04000679 RID: 1657
	private int jerseyIDBlueAttacker;

	// Token: 0x0400067A RID: 1658
	private int jerseyIDRedAttacker;

	// Token: 0x0400067B RID: 1659
	private int jerseyIDBlueGoalie;

	// Token: 0x0400067C RID: 1660
	private int jerseyIDRedGoalie;

	// Token: 0x0400067D RID: 1661
	private int stickSkinIDBlueAttacker;

	// Token: 0x0400067E RID: 1662
	private int stickSkinIDRedAttacker;

	// Token: 0x0400067F RID: 1663
	private int stickSkinIDBlueGoalie;

	// Token: 0x04000680 RID: 1664
	private int stickSkinIDRedGoalie;

	// Token: 0x04000681 RID: 1665
	private int stickShaftTapeIDBlueAttacker;

	// Token: 0x04000682 RID: 1666
	private int stickShaftTapeIDRedAttacker;

	// Token: 0x04000683 RID: 1667
	private int stickShaftTapeIDBlueGoalie;

	// Token: 0x04000684 RID: 1668
	private int stickShaftTapeIDRedGoalie;

	// Token: 0x04000685 RID: 1669
	private int stickBladeTapeIDBlueAttacker;

	// Token: 0x04000686 RID: 1670
	private int stickBladeTapeIDRedAttacker;

	// Token: 0x04000687 RID: 1671
	private int stickBladeTapeIDBlueGoalie;

	// Token: 0x04000688 RID: 1672
	private int stickBladeTapeIDRedGoalie;

	// Token: 0x04000689 RID: 1673
	private VisualElement appearance;

	// Token: 0x0400068A RID: 1674
	private IconButton closeIconButton;

	// Token: 0x0400068B RID: 1675
	private TabView categoryTabView;

	// Token: 0x0400068C RID: 1676
	private Tab headTab;

	// Token: 0x0400068D RID: 1677
	private Tab bodyTab;

	// Token: 0x0400068E RID: 1678
	private Tab stickTab;

	// Token: 0x0400068F RID: 1679
	private TabView headTabView;

	// Token: 0x04000690 RID: 1680
	private Tab flagsTab;

	// Token: 0x04000691 RID: 1681
	private Tab headgearTab;

	// Token: 0x04000692 RID: 1682
	private Tab mustachesTab;

	// Token: 0x04000693 RID: 1683
	private Tab beardsTab;

	// Token: 0x04000694 RID: 1684
	private TabView bodyTabView;

	// Token: 0x04000695 RID: 1685
	private Tab jerseysTab;

	// Token: 0x04000696 RID: 1686
	private TabView stickTabView;

	// Token: 0x04000697 RID: 1687
	private Tab stickSkinsTab;

	// Token: 0x04000698 RID: 1688
	private Tab stickShaftTapesTab;

	// Token: 0x04000699 RID: 1689
	private Tab stickBladeTapesTab;

	// Token: 0x0400069A RID: 1690
	private Toggle applyForBothTeamsToggle;

	// Token: 0x0400069B RID: 1691
	private DropdownField teamDropdown;

	// Token: 0x0400069C RID: 1692
	private DropdownField roleDropdown;

	// Token: 0x0400069D RID: 1693
	private RadioButtonGroup flagsRadioButtonGroup;

	// Token: 0x0400069E RID: 1694
	private RadioButtonGroup headgearRadioButtonGroup;

	// Token: 0x0400069F RID: 1695
	private RadioButtonGroup mustachesRadioButtonGroup;

	// Token: 0x040006A0 RID: 1696
	private RadioButtonGroup beardsRadioButtonGroup;

	// Token: 0x040006A1 RID: 1697
	private RadioButtonGroup jerseysRadioButtonGroup;

	// Token: 0x040006A2 RID: 1698
	private RadioButtonGroup stickSkinsRadioButtonGroup;

	// Token: 0x040006A3 RID: 1699
	private RadioButtonGroup stickShaftTapesRadioButtonGroup;

	// Token: 0x040006A4 RID: 1700
	private RadioButtonGroup stickBladeTapesRadioButtonGroup;
}
