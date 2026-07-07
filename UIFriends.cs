using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x0200018C RID: 396
public class UIFriends : UIView
{
	// Token: 0x06000B94 RID: 2964 RVA: 0x0004168C File Offset: 0x0003F88C
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("FriendsView", null);
		this.friends = base.View.Query("Friends", null);
		this.friendsList = this.friends.Query("FriendsList", null);
		this.closeIconButton = this.friends.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnCloseIconButtonClicked;
		this.friendsList.Clear();
		this.friendsMap.Clear();
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00041740 File Offset: 0x0003F940
	public void AddFriend(string steamId, string username, Texture2D avatar)
	{
		if (this.friendsMap.ContainsKey(steamId))
		{
			return;
		}
		TemplateContainer templateContainer = this.CreateFriend(steamId, username, avatar);
		this.friendsList.Add(templateContainer);
		this.friendsMap.Add(steamId, templateContainer);
		this.SortFriends();
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00041788 File Offset: 0x0003F988
	public void UpdateFriend(string steamId, string username, Texture2D texture)
	{
		if (!this.friendsMap.ContainsKey(steamId))
		{
			return;
		}
		Friend friend = this.friendsMap[steamId].Query("Friend", null);
		friend.Texture = texture;
		friend.Username = username;
		friend.InviteButtonClicked = delegate()
		{
			this.OnFriendInviteButtonClicked(steamId);
		};
		this.SortFriends();
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00041804 File Offset: 0x0003FA04
	public void RemoveFriend(string steamId)
	{
		if (!this.friendsMap.ContainsKey(steamId))
		{
			return;
		}
		TemplateContainer element = this.friendsMap[steamId];
		this.friendsList.Remove(element);
		this.friendsMap.Remove(steamId);
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x000117AA File Offset: 0x0000F9AA
	public bool IsFriendListed(string steamId)
	{
		return this.friendsMap.ContainsKey(steamId);
	}

	// Token: 0x06000B99 RID: 2969 RVA: 0x00041848 File Offset: 0x0003FA48
	private TemplateContainer CreateFriend(string steamId, string username, Texture2D texture)
	{
		TemplateContainer templateContainer = this.friendAsset.Instantiate();
		Friend friend = templateContainer.Query("Friend", null);
		friend.Texture = texture;
		friend.Username = username;
		friend.InviteButtonClicked = delegate()
		{
			this.OnFriendInviteButtonClicked(steamId);
		};
		return templateContainer;
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x000117B8 File Offset: 0x0000F9B8
	private void SortFriends()
	{
		this.friendsList.Sort(delegate(VisualElement a, VisualElement b)
		{
			Friend friend = a.Query("Friend", null).First();
			Friend friend2 = b.Query("Friend", null).First();
			return string.Compare(friend.Username, friend2.Username);
		});
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x000117E4 File Offset: 0x0000F9E4
	private void OnCloseIconButtonClicked()
	{
		EventManager.TriggerEvent("Event_OnFriendsClickClose", null);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x000117F1 File Offset: 0x0000F9F1
	private void OnFriendInviteButtonClicked(string steamId)
	{
		EventManager.TriggerEvent("Event_OnFriendInviteButtonClicked", new Dictionary<string, object>
		{
			{
				"steamId",
				steamId
			}
		});
	}

	// Token: 0x040006F5 RID: 1781
	[Header("References")]
	public VisualTreeAsset friendAsset;

	// Token: 0x040006F6 RID: 1782
	private VisualElement friends;

	// Token: 0x040006F7 RID: 1783
	private VisualElement friendsList;

	// Token: 0x040006F8 RID: 1784
	private IconButton closeIconButton;

	// Token: 0x040006F9 RID: 1785
	private Dictionary<string, TemplateContainer> friendsMap = new Dictionary<string, TemplateContainer>();
}
