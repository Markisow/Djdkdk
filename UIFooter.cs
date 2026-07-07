using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x0200018A RID: 394
public class UIFooter : UIView
{
	// Token: 0x06000B7D RID: 2941 RVA: 0x000410E0 File Offset: 0x0003F2E0
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("FooterView", null);
		this.footer = base.View.Query("Footer", null);
		this.left = this.footer.Query("Left", null);
		this.center = this.footer.Query("Center", null);
		this.right = this.footer.Query("Right", null);
		this.localUserContainer = this.left.Query("LocalUserContainer", null);
		this.mmr = this.left.Query("LocalUserMmr", null).First().Query(null, null);
		this.party = this.right.Query("Party", null);
		this.partyUsers = this.party.Query("Users", null);
		this.createPartyIconButtonInstance = this.party.Query("CreatePartyIconButtonContainer", null);
		this.createPartyIconButton = this.createPartyIconButtonInstance.Query(null, null);
		this.createPartyIconButton.clicked += this.OnClickCreateParty;
		this.inviteIconButtonInstance = this.party.Query("InviteIconButtonContainer", null);
		this.inviteIconButton = this.inviteIconButtonInstance.Query(null, null);
		this.inviteIconButton.clicked += this.OnClickInvite;
		this.leavePartyIconButtonInstance = this.party.Query("LeavePartyIconButtonContainer", null);
		this.leavePartyIconButton = this.leavePartyIconButtonInstance.Query(null, null);
		this.leavePartyIconButton.clicked += this.OnClickLeaveParty;
		this.disbandPartyIconButtonInstance = this.party.Query("DisbandPartyIconButtonContainer", null);
		this.disbandPartyIconButton = this.disbandPartyIconButtonInstance.Query(null, null);
		this.disbandPartyIconButton.clicked += this.OnClickDisbandParty;
		this.ClearLocalUser();
		this.ClearPartyUsers();
		this.SetCreatePartyButtonVisibility(false);
		this.SetInviteButtonVisibility(false);
		this.SetDisbandPartyButtonVisibility(false);
		this.SetLeavePartyButtonVisibility(false);
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x00041348 File Offset: 0x0003F548
	public void SetLocalUser(string username, Texture2D avatar)
	{
		this.ClearLocalUser();
		TemplateContainer child = this.CreateUser(username, avatar, false, false);
		this.localUserContainer.Add(child);
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00011655 File Offset: 0x0000F855
	public void ClearLocalUser()
	{
		this.localUserContainer.Clear();
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00011662 File Offset: 0x0000F862
	public void SetMmr(int value)
	{
		this.mmr.TargetValue = new int?(value);
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x00041374 File Offset: 0x0003F574
	public void AddPartyUser(string steamId, string username, Texture2D texture)
	{
		if (this.partyUserMap.ContainsKey(steamId))
		{
			return;
		}
		TemplateContainer templateContainer = this.CreateUser(username, texture, true, true);
		this.partyUsers.Add(templateContainer);
		this.partyUsers.style.display = DisplayStyle.Flex;
		this.partyUserMap.Add(steamId, templateContainer);
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x000413CC File Offset: 0x0003F5CC
	public void RemovePartyUser(string steamId)
	{
		if (!this.partyUserMap.ContainsKey(steamId))
		{
			return;
		}
		VisualElement element = this.partyUserMap[steamId];
		this.partyUsers.Remove(element);
		this.partyUsers.style.display = ((this.partyUsers.childCount > 0) ? DisplayStyle.Flex : DisplayStyle.None);
		this.partyUserMap.Remove(steamId);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x00011675 File Offset: 0x0000F875
	public void ClearPartyUsers()
	{
		this.partyUsers.Clear();
		this.partyUserMap.Clear();
		this.partyUsers.style.display = DisplayStyle.None;
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x000116A3 File Offset: 0x0000F8A3
	private TemplateContainer CreateUser(string username, Texture2D texture, bool small = false, bool hideUsername = false)
	{
		TemplateContainer templateContainer = this.userAsset.Instantiate();
		User user = templateContainer.Query(null, null);
		user.AvatarTexture = texture;
		user.Username = username;
		templateContainer.EnableInClassList("small", small);
		templateContainer.EnableInClassList("hideUsername", hideUsername);
		return templateContainer;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x000116E3 File Offset: 0x0000F8E3
	public void SetCreatePartyButtonVisibility(bool show)
	{
		this.createPartyIconButtonInstance.style.display = (show ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000B86 RID: 2950 RVA: 0x00011701 File Offset: 0x0000F901
	public void SetInviteButtonVisibility(bool show)
	{
		this.inviteIconButtonInstance.style.display = (show ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0001171F File Offset: 0x0000F91F
	public void SetLeavePartyButtonVisibility(bool show)
	{
		this.leavePartyIconButtonInstance.style.display = (show ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0001173D File Offset: 0x0000F93D
	public void SetDisbandPartyButtonVisibility(bool show)
	{
		this.disbandPartyIconButtonInstance.style.display = (show ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0001175B File Offset: 0x0000F95B
	private void OnClickCreateParty()
	{
		EventManager.TriggerEvent("Event_OnFooterClickCreateParty", null);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x00011768 File Offset: 0x0000F968
	private void OnClickInvite()
	{
		EventManager.TriggerEvent("Event_OnFooterClickInvite", null);
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x00011775 File Offset: 0x0000F975
	private void OnClickLeaveParty()
	{
		EventManager.TriggerEvent("Event_OnFooterClickLeaveParty", null);
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x00011782 File Offset: 0x0000F982
	private void OnClickDisbandParty()
	{
		EventManager.TriggerEvent("Event_OnFooterClickDisbandParty", null);
	}

	// Token: 0x040006E2 RID: 1762
	[Header("References")]
	public VisualTreeAsset userAsset;

	// Token: 0x040006E3 RID: 1763
	private VisualElement footer;

	// Token: 0x040006E4 RID: 1764
	private VisualElement left;

	// Token: 0x040006E5 RID: 1765
	private VisualElement center;

	// Token: 0x040006E6 RID: 1766
	private VisualElement right;

	// Token: 0x040006E7 RID: 1767
	private VisualElement localUserContainer;

	// Token: 0x040006E8 RID: 1768
	private Mmr mmr;

	// Token: 0x040006E9 RID: 1769
	private VisualElement party;

	// Token: 0x040006EA RID: 1770
	private VisualElement partyUsers;

	// Token: 0x040006EB RID: 1771
	private TemplateContainer createPartyIconButtonInstance;

	// Token: 0x040006EC RID: 1772
	private IconButton createPartyIconButton;

	// Token: 0x040006ED RID: 1773
	private TemplateContainer inviteIconButtonInstance;

	// Token: 0x040006EE RID: 1774
	private IconButton inviteIconButton;

	// Token: 0x040006EF RID: 1775
	private TemplateContainer leavePartyIconButtonInstance;

	// Token: 0x040006F0 RID: 1776
	private IconButton leavePartyIconButton;

	// Token: 0x040006F1 RID: 1777
	private TemplateContainer disbandPartyIconButtonInstance;

	// Token: 0x040006F2 RID: 1778
	private IconButton disbandPartyIconButton;

	// Token: 0x040006F3 RID: 1779
	private Dictionary<string, TemplateContainer> partyUserMap = new Dictionary<string, TemplateContainer>();
}
