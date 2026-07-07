using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020000A2 RID: 162
public class ChatManagerController : MonoBehaviour
{
	// Token: 0x0600053D RID: 1341 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
	private void Awake()
	{
		this.chatManager = base.GetComponent<ChatManager>();
		InputManager.QuickChat1Action.performed += this.OnQuickChatAction1Performed;
		InputManager.QuickChat2Action.performed += this.OnQuickChatAction2Performed;
		InputManager.QuickChat3Action.performed += this.OnQuickChatAction3Performed;
		InputManager.QuickChat4Action.performed += this.OnQuickChatAction4Performed;
		InputManager.QuickChat5Action.performed += this.OnQuickChatAction5Performed;
		EventManager.AddEventListener("Event_OnChatSubmitMessage", new Action<Dictionary<string, object>>(this.Event_OnChatSubmitMessage));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.AddEventListener("Event_Server_OnChatMessageReceived", new Action<Dictionary<string, object>>(this.Event_Server_OnChatMessageReceived));
		WebSocketManager.AddMessageListener("playerAnnouncement", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerAnnouncement));
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
	private void OnDestroy()
	{
		InputManager.QuickChat1Action.performed -= this.OnQuickChatAction1Performed;
		InputManager.QuickChat2Action.performed -= this.OnQuickChatAction2Performed;
		InputManager.QuickChat3Action.performed -= this.OnQuickChatAction3Performed;
		InputManager.QuickChat4Action.performed -= this.OnQuickChatAction4Performed;
		InputManager.QuickChat5Action.performed -= this.OnQuickChatAction5Performed;
		EventManager.RemoveEventListener("Event_OnChatSubmitMessage", new Action<Dictionary<string, object>>(this.Event_OnChatSubmitMessage));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.RemoveEventListener("Event_Server_OnChatMessageReceived", new Action<Dictionary<string, object>>(this.Event_Server_OnChatMessageReceived));
		WebSocketManager.RemoveMessageListener("playerAnnouncement", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerAnnouncement));
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0002CA7C File Offset: 0x0002AC7C
	private void OnQuickChatAction1Performed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.chatManager.Client_QuickChatAction(0);
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0002CAB4 File Offset: 0x0002ACB4
	private void OnQuickChatAction2Performed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.chatManager.Client_QuickChatAction(1);
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0002CAEC File Offset: 0x0002ACEC
	private void OnQuickChatAction3Performed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.chatManager.Client_QuickChatAction(2);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0002CB24 File Offset: 0x0002AD24
	private void OnQuickChatAction4Performed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.chatManager.Client_QuickChatAction(3);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0002CB5C File Offset: 0x0002AD5C
	private void OnQuickChatAction5Performed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		this.chatManager.Client_QuickChatAction(4);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0002CB94 File Offset: 0x0002AD94
	private void Event_OnChatSubmitMessage(Dictionary<string, object> message)
	{
		string content = (string)message["content"];
		bool isTeamChat = (bool)message["isTeamChat"];
		this.chatManager.Client_SendChatMessage(content, false, isTeamChat);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
	private void Event_OnClientStopped(Dictionary<string, object> message)
	{
		this.chatManager.ClearChatMessages();
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
	private void Event_Server_OnChatMessageReceived(Dictionary<string, object> message)
	{
		ChatMessage chatMessage = (ChatMessage)message["chatMessage"];
		if (!chatMessage.IsTeamChat)
		{
			this.chatManager.Server_BroadcastChatMessage(chatMessage);
			return;
		}
		if (chatMessage.Team == null)
		{
			return;
		}
		List<ulong> list = new List<ulong>();
		if (chatMessage.Team.Value == PlayerTeam.Spectator || chatMessage.Team.Value == PlayerTeam.None)
		{
			list.AddRange(MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayersByTeams(new PlayerTeam[]
			{
				PlayerTeam.None,
				PlayerTeam.Spectator
			}, false).ConvertAll<ulong>((Player player) => player.OwnerClientId));
		}
		else
		{
			list.AddRange(MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayersByTeam(chatMessage.Team.Value, false).ConvertAll<ulong>((Player player) => player.OwnerClientId));
		}
		this.chatManager.Server_SendChatMessage(chatMessage, list.ToArray());
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0002CCCC File Offset: 0x0002AECC
	private void WebSocket_Event_OnPlayerAnnouncement(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		ChatMessage chatMessage = new ChatMessage
		{
			SteamID = null,
			Username = null,
			Team = null,
			Content = inMessage.GetData<string>(),
			Timestamp = Utils.GetTimestamp(),
			IsQuickChat = false,
			IsTeamChat = false,
			IsSystem = true
		};
		this.chatManager.AddChatMessage(chatMessage);
	}

	// Token: 0x0400033D RID: 829
	private ChatManager chatManager;
}
