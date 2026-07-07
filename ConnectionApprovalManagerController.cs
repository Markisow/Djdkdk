using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000121 RID: 289
public class ConnectionApprovalManagerController : MonoBehaviour
{
	// Token: 0x06000808 RID: 2056 RVA: 0x000349E0 File Offset: 0x00032BE0
	private void Awake()
	{
		this.connectionApprovalManager = base.GetComponent<ConnectionApprovalManager>();
		EventManager.AddEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.AddEventListener("Event_Everyone_OnClientDisconnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientDisconnected));
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
		EventManager.AddEventListener("Event_Server_OnLoadSceneEventCompleted", new Action<Dictionary<string, object>>(this.Event_Server_OnLoadSceneEventCompleted));
		EventManager.AddEventListener("Event_Server_OnConnectionApproved", new Action<Dictionary<string, object>>(this.Event_Server_OnConnectionApproved));
		WebSocketManager.AddMessageListener("serverConnectionApprovalResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnServerConnectionApprovalResponse));
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x00034A80 File Offset: 0x00032C80
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.RemoveEventListener("Event_Everyone_OnClientDisconnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientDisconnected));
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
		EventManager.RemoveEventListener("Event_Server_OnLoadSceneEventCompleted", new Action<Dictionary<string, object>>(this.Event_Server_OnLoadSceneEventCompleted));
		EventManager.RemoveEventListener("Event_Server_OnConnectionApproved", new Action<Dictionary<string, object>>(this.Event_Server_OnConnectionApproved));
		WebSocketManager.RemoveMessageListener("serverConnectionApprovalResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnServerConnectionApprovalResponse));
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x00034B14 File Offset: 0x00032D14
	private void Event_Everyone_OnClientConnected(Dictionary<string, object> message)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		ulong num = (ulong)message["clientId"];
		ConnectionApproval connectionApprovalByClientId = this.connectionApprovalManager.GetConnectionApprovalByClientId(num);
		if (connectionApprovalByClientId == null || !connectionApprovalByClientId.IsApproved)
		{
			return;
		}
		if (connectionApprovalByClientId.IsHost)
		{
			return;
		}
		EventManager.TriggerEvent("Event_Server_OnApprovedClientConnected", new Dictionary<string, object>
		{
			{
				"clientId",
				num
			},
			{
				"connectionApproval",
				connectionApprovalByClientId
			}
		});
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00034B90 File Offset: 0x00032D90
	private void Event_Everyone_OnClientDisconnected(Dictionary<string, object> message)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		ulong num = (ulong)message["clientId"];
		ConnectionApproval connectionApprovalByClientId = this.connectionApprovalManager.GetConnectionApprovalByClientId(num);
		if (connectionApprovalByClientId == null || !connectionApprovalByClientId.IsApproved)
		{
			return;
		}
		EventManager.TriggerEvent("Event_Server_OnApprovedClientDisconnected", new Dictionary<string, object>
		{
			{
				"clientId",
				num
			},
			{
				"connectionApproval",
				connectionApprovalByClientId
			}
		});
		this.connectionApprovalManager.RemoveConnectionApproval(num);
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.connectionApprovalManager.Dispose();
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x0000E4ED File Offset: 0x0000C6ED
	private void Event_Server_OnLoadSceneEventCompleted(Dictionary<string, object> message)
	{
		if ((bool)message["isInitialScene"])
		{
			this.connectionApprovalManager.ConsumeBufferedConnectionApprovals(true);
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x00034C0C File Offset: 0x00032E0C
	private void Event_Server_OnConnectionApproved(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		ConnectionApproval connectionApproval = (ConnectionApproval)message["connectionApproval"];
		if (connectionApproval.IsHost)
		{
			EventManager.TriggerEvent("Event_Server_OnApprovedClientConnected", new Dictionary<string, object>
			{
				{
					"clientId",
					num
				},
				{
					"connectionApproval",
					connectionApproval
				}
			});
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00034C70 File Offset: 0x00032E70
	private void WebSocket_Event_OnServerConnectionApprovalResponse(Dictionary<string, object> message)
	{
		OutMessage outMessage = (OutMessage)message["outMessage"];
		ServerConnectionApprovalResponse data = ((InMessage)message["inMessage"]).GetData<ServerConnectionApprovalResponse>();
		string steamId = (string)outMessage.Data["steamId"];
		ConnectionApproval connectionApprovalBySteamId = this.connectionApprovalManager.GetConnectionApprovalBySteamId(steamId);
		if (connectionApprovalBySteamId == null)
		{
			return;
		}
		ulong clientID = connectionApprovalBySteamId.ClientID;
		if (!data.success)
		{
			this.connectionApprovalManager.RejectConnection(clientID, ConnectionRejectionCode.Unknown, data.errorData.message);
			return;
		}
		ConnectionRejectionCode? connectionRejectionCode = this.connectionApprovalManager.GetConnectionRejectionCode(connectionApprovalBySteamId);
		bool flag = connectionRejectionCode == null;
		if (flag && BackendUtils.GetActivePlayerDataBan(data.data.playerData) != null)
		{
			connectionRejectionCode = new ConnectionRejectionCode?(ConnectionRejectionCode.Banned);
			flag = false;
		}
		if (flag)
		{
			this.connectionApprovalManager.ApproveConnection(clientID, data.data.playerData);
			return;
		}
		this.connectionApprovalManager.RejectConnection(clientID, connectionRejectionCode.Value, null);
	}

	// Token: 0x040004DA RID: 1242
	private ConnectionApprovalManager connectionApprovalManager;
}
