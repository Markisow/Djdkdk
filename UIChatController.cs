using System;
using System.Collections.Generic;

// Token: 0x02000187 RID: 391
public class UIChatController : UIViewController<UIChat>
{
	// Token: 0x06000B66 RID: 2918 RVA: 0x00040E28 File Offset: 0x0003F028
	public override void Awake()
	{
		base.Awake();
		this.uiChat = base.GetComponent<UIChat>();
		EventManager.AddEventListener("Event_OnChatMessageAdded", new Action<Dictionary<string, object>>(this.Event_OnChatMessageAdded));
		EventManager.AddEventListener("Event_OnChatMessageRemoved", new Action<Dictionary<string, object>>(this.Event_OnChatMessageRemoved));
		EventManager.AddEventListener("Event_OnChatMessagesCleared", new Action<Dictionary<string, object>>(this.Event_OnChatMessagesCleared));
		EventManager.AddEventListener("Event_OnQuickChatEnabled", new Action<Dictionary<string, object>>(this.Event_OnQuickChatEnabled));
		EventManager.AddEventListener("Event_OnQuickChatDisabled", new Action<Dictionary<string, object>>(this.Event_OnQuickChatDisabled));
		EventManager.AddEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
		EventManager.AddEventListener("Event_OnChatOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnChatOpacityChanged));
		EventManager.AddEventListener("Event_OnChatScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnChatScaleChanged));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x000114BF File Offset: 0x0000F6BF
	private void Start()
	{
		this.uiChat.SetOpacity(SettingsManager.ChatOpacity);
		this.uiChat.SetScale(SettingsManager.ChatScale);
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00040F10 File Offset: 0x0003F110
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnChatMessageAdded", new Action<Dictionary<string, object>>(this.Event_OnChatMessageAdded));
		EventManager.RemoveEventListener("Event_OnChatMessageRemoved", new Action<Dictionary<string, object>>(this.Event_OnChatMessageRemoved));
		EventManager.RemoveEventListener("Event_OnChatMessagesCleared", new Action<Dictionary<string, object>>(this.Event_OnChatMessagesCleared));
		EventManager.RemoveEventListener("Event_OnQuickChatEnabled", new Action<Dictionary<string, object>>(this.Event_OnQuickChatEnabled));
		EventManager.RemoveEventListener("Event_OnQuickChatDisabled", new Action<Dictionary<string, object>>(this.Event_OnQuickChatDisabled));
		EventManager.RemoveEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
		EventManager.RemoveEventListener("Event_OnChatOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnChatOpacityChanged));
		EventManager.RemoveEventListener("Event_OnChatScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnChatScaleChanged));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		base.OnDestroy();
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00040FEC File Offset: 0x0003F1EC
	private void Event_OnChatMessageAdded(Dictionary<string, object> message)
	{
		ChatMessage chatMessage = (ChatMessage)message["chatMessage"];
		this.uiChat.AddChatMessage(chatMessage, SettingsManager.Units, SettingsManager.FilterChatProfanity);
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x00041020 File Offset: 0x0003F220
	private void Event_OnChatMessageRemoved(Dictionary<string, object> message)
	{
		ChatMessage chatMessage = (ChatMessage)message["chatMessage"];
		this.uiChat.RemoveChatMessage(chatMessage);
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x000114E1 File Offset: 0x0000F6E1
	private void Event_OnChatMessagesCleared(Dictionary<string, object> message)
	{
		this.uiChat.ClearChatMessages();
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x0004104C File Offset: 0x0003F24C
	private void Event_OnQuickChatEnabled(Dictionary<string, object> message)
	{
		QuickChatCategory category = (QuickChatCategory)message["category"];
		QuickChat[] quickChats = (QuickChat[])message["quickChats"];
		this.uiChat.ShowQuickChat(category, quickChats);
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x000114EE File Offset: 0x0000F6EE
	private void Event_OnQuickChatDisabled(Dictionary<string, object> message)
	{
		this.uiChat.HideQuickChat();
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x000114FB File Offset: 0x0000F6FB
	private void Event_OnShowGameUserInterfaceChanged(Dictionary<string, object> message)
	{
		if (GlobalStateManager.UIState.Phase == UIPhase.LockerRoom)
		{
			return;
		}
		if ((bool)message["value"])
		{
			this.uiChat.Show();
			return;
		}
		this.uiChat.Hide();
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00041088 File Offset: 0x0003F288
	private void Event_OnChatOpacityChanged(Dictionary<string, object> message)
	{
		float opacity = (float)message["value"];
		this.uiChat.SetOpacity(opacity);
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x000410B4 File Offset: 0x0003F2B4
	private void Event_OnChatScaleChanged(Dictionary<string, object> message)
	{
		float scale = (float)message["value"];
		this.uiChat.SetScale(scale);
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x000114EE File Offset: 0x0000F6EE
	private void Event_OnClientStopped(Dictionary<string, object> message)
	{
		this.uiChat.HideQuickChat();
	}

	// Token: 0x040006DF RID: 1759
	private UIChat uiChat;
}
