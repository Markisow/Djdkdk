using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200011E RID: 286
[RequireComponent(typeof(ServerManager))]
[RequireComponent(typeof(TimeoutManager))]
[RequireComponent(typeof(BanManager))]
public class ConnectionApprovalManager : MonoBehaviourSingleton<ConnectionApprovalManager>
{
	// Token: 0x060007F2 RID: 2034 RVA: 0x0000E388 File Offset: 0x0000C588
	public override void Awake()
	{
		base.Awake();
		this.ServerManager = base.GetComponent<ServerManager>();
		this.TimeoutManager = base.GetComponent<TimeoutManager>();
		this.BanManager = base.GetComponent<BanManager>();
		this.WhitelistManager = base.GetComponent<WhitelistManager>();
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
	private void Start()
	{
		this.bufferConnectionApprovals = true;
		NetworkManager.Singleton.ConnectionApprovalCallback = new Action<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse>(this.ConnectionApprovalCallback);
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0000E3DF File Offset: 0x0000C5DF
	public void Dispose()
	{
		this.clientIdConnectionApprovalMap.Clear();
		this.bufferConnectionApprovals = true;
		this.bufferedConnectionApprovals.Clear();
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0000E3FE File Offset: 0x0000C5FE
	private void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
	{
		if (this.bufferConnectionApprovals)
		{
			this.bufferedConnectionApprovals.Add(new ValueTuple<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse>(request, response));
			return;
		}
		this.HandleConnectionApproval(request, response);
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0003444C File Offset: 0x0003264C
	private void HandleConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
	{
		ulong clientNetworkId = request.ClientNetworkId;
		ConnectionData connectionData = null;
		try
		{
			connectionData = JsonSerializer.Deserialize<ConnectionData>(Encoding.UTF8.GetString(request.Payload), null);
		}
		catch (Exception ex)
		{
			ConnectionApprovalManager.Logger.Error(string.Format("Error deserializing connection data for client {0}: {1}", clientNetworkId, ex.Message));
		}
		this.AddConnectionApproval(clientNetworkId, request, response, connectionData);
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x000344B8 File Offset: 0x000326B8
	private void AddConnectionApproval(ulong clientId, NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response, ConnectionData connectionData)
	{
		if (this.clientIdConnectionApprovalMap.ContainsKey(clientId))
		{
			return;
		}
		ConnectionApproval connectionApproval = new ConnectionApproval
		{
			Request = request,
			Response = response,
			ConnectionData = connectionData,
			IpAddress = ((clientId == 0UL) ? "127.0.0.1" : this.ServerManager.UnityTransport.GetEndpoint(clientId).Address)
		};
		this.clientIdConnectionApprovalMap.Add(clientId, connectionApproval);
		this.OnConnectionApprovalStarted(clientId, connectionApproval);
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00034530 File Offset: 0x00032730
	public void ConsumeBufferedConnectionApprovals(bool stopBuffering = true)
	{
		ConnectionApprovalManager.Logger.Info(string.Format("Consuming {0} buffered connection approvals", this.bufferedConnectionApprovals.Count));
		this.bufferConnectionApprovals = !stopBuffering;
		foreach (ValueTuple<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse> valueTuple in this.bufferedConnectionApprovals.ToList<ValueTuple<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse>>())
		{
			NetworkManager.ConnectionApprovalRequest item = valueTuple.Item1;
			NetworkManager.ConnectionApprovalResponse item2 = valueTuple.Item2;
			this.HandleConnectionApproval(item, item2);
			this.bufferedConnectionApprovals.Remove(new ValueTuple<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse>(item, item2));
		}
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x0000E423 File Offset: 0x0000C623
	public void RemoveConnectionApproval(ulong clientId)
	{
		if (!this.clientIdConnectionApprovalMap.ContainsKey(clientId))
		{
			return;
		}
		this.clientIdConnectionApprovalMap.Remove(clientId);
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x0000E441 File Offset: 0x0000C641
	public ConnectionApproval GetConnectionApprovalByClientId(ulong clientId)
	{
		if (!this.clientIdConnectionApprovalMap.ContainsKey(clientId))
		{
			return null;
		}
		return this.clientIdConnectionApprovalMap[clientId];
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x000345D8 File Offset: 0x000327D8
	public ConnectionApproval GetConnectionApprovalBySteamId(string steamId)
	{
		return this.clientIdConnectionApprovalMap.Values.FirstOrDefault((ConnectionApproval approval) => approval.ConnectionData.SteamId == steamId);
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x00034610 File Offset: 0x00032810
	public void ApproveConnection(ulong clientId, PlayerData playerData)
	{
		if (!this.clientIdConnectionApprovalMap.ContainsKey(clientId))
		{
			return;
		}
		ConnectionApproval connectionApproval = this.clientIdConnectionApprovalMap[clientId];
		connectionApproval.Approve(playerData);
		ConnectionApprovalManager.Logger.Info(string.Format("Approved connection for client {0}", clientId));
		EventManager.TriggerEvent("Event_Server_OnConnectionApproved", new Dictionary<string, object>
		{
			{
				"clientId",
				clientId
			},
			{
				"connectionApproval",
				connectionApproval
			}
		});
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00034688 File Offset: 0x00032888
	public string GetRejectionReason(ConnectionRejectionCode code, string message = null)
	{
		ConnectionRejection connectionRejection = new ConnectionRejection
		{
			code = code,
			message = message,
			data = null
		};
		if (code == ConnectionRejectionCode.MissingMods)
		{
			connectionRejection.data = new ConnectionRejectionData
			{
				clientRequiredModIds = this.ServerManager.ServerConfig.ClientRequiredModIds
			};
		}
		return JsonSerializer.Serialize<ConnectionRejection>(connectionRejection, new JsonSerializerOptions
		{
			WriteIndented = true
		});
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x000346E8 File Offset: 0x000328E8
	public void RejectConnection(ulong clientId, ConnectionRejectionCode code, string message = null)
	{
		if (!this.clientIdConnectionApprovalMap.ContainsKey(clientId))
		{
			return;
		}
		ConnectionApproval connectionApproval = this.clientIdConnectionApprovalMap[clientId];
		connectionApproval.Reject(this.GetRejectionReason(code, message));
		ConnectionApprovalManager.Logger.Info(string.Format("Rejected connection for client {0}: {1}", clientId, Utils.GetConnectionRejectionMessage(code, message)));
		EventManager.TriggerEvent("Event_Server_OnConnectionRejected", new Dictionary<string, object>
		{
			{
				"clientId",
				clientId
			},
			{
				"connectionApproval",
				connectionApproval
			},
			{
				"rejectionCode",
				code
			}
		});
		this.RemoveConnectionApproval(clientId);
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x00034784 File Offset: 0x00032984
	public ConnectionRejectionCode? GetConnectionRejectionCode(ConnectionApproval connectionApproval)
	{
		int num = NetworkManager.Singleton.ConnectedClientsList.Count((NetworkClient c) => c.ClientId != connectionApproval.ClientID);
		bool flag = !string.IsNullOrEmpty(this.ServerManager.ServerConfig.password);
		bool useWhitelist = this.ServerManager.ServerConfig.useWhitelist;
		bool flag2 = connectionApproval.ConnectionData == null;
		bool flag3 = num >= this.ServerManager.ServerConfig.maxPlayers;
		bool flag4 = this.TimeoutManager.IsSteamIdTimedOut(connectionApproval.ConnectionData.SteamId);
		bool flag5 = this.BanManager.IsSteamIdBanned(connectionApproval.ConnectionData.SteamId);
		bool flag6 = this.BanManager.IsIpAddressBanned(connectionApproval.IpAddress);
		bool flag7 = this.WhitelistManager.IsSteamIdWhitelisted(connectionApproval.ConnectionData.SteamId);
		bool flag8 = string.IsNullOrEmpty(connectionApproval.ConnectionData.Password) && flag;
		bool flag9 = connectionApproval.ConnectionData.Password == this.ServerManager.ServerConfig.password || !flag;
		bool flag10 = this.ServerManager.ServerConfig.ClientRequiredModIds.Any((string modId) => !connectionApproval.ConnectionData.EnabledModIds.Contains(modId));
		if (flag2)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.Unknown);
		}
		if (flag3)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.ServerFull);
		}
		if (flag4)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.TimedOut);
		}
		if (flag5 || flag6)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.Banned);
		}
		if (useWhitelist && !flag7)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.NotWhitelisted);
		}
		if (flag8)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.MissingPassword);
		}
		if (!flag9)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.InvalidPassword);
		}
		if (flag10)
		{
			return new ConnectionRejectionCode?(ConnectionRejectionCode.MissingMods);
		}
		return null;
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0003494C File Offset: 0x00032B4C
	private void OnConnectionApprovalStarted(ulong clientId, ConnectionApproval connectionApproval)
	{
		ConnectionApprovalManager.Logger.Info(string.Format("Started connection approval for client {0}", clientId));
		ConnectionRejectionCode? connectionRejectionCode = this.GetConnectionRejectionCode(connectionApproval);
		if (connectionRejectionCode == null)
		{
			connectionApproval.Halt();
			WebSocketManager.Emit("serverConnectionApprovalRequest", new Dictionary<string, object>
			{
				{
					"steamId",
					connectionApproval.ConnectionData.SteamId
				},
				{
					"key",
					connectionApproval.ConnectionData.Key
				}
			}, "serverConnectionApprovalResponse");
			return;
		}
		this.RejectConnection(clientId, connectionRejectionCode.Value, null);
	}

	// Token: 0x040004D0 RID: 1232
	private static readonly global::Logger Logger = new global::Logger("ConnectionApprovalManager");

	// Token: 0x040004D1 RID: 1233
	[HideInInspector]
	public ServerManager ServerManager;

	// Token: 0x040004D2 RID: 1234
	[HideInInspector]
	public TimeoutManager TimeoutManager;

	// Token: 0x040004D3 RID: 1235
	[HideInInspector]
	public BanManager BanManager;

	// Token: 0x040004D4 RID: 1236
	[HideInInspector]
	public WhitelistManager WhitelistManager;

	// Token: 0x040004D5 RID: 1237
	private Dictionary<ulong, ConnectionApproval> clientIdConnectionApprovalMap = new Dictionary<ulong, ConnectionApproval>();

	// Token: 0x040004D6 RID: 1238
	private bool bufferConnectionApprovals = true;

	// Token: 0x040004D7 RID: 1239
	private List<ValueTuple<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse>> bufferedConnectionApprovals = new List<ValueTuple<NetworkManager.ConnectionApprovalRequest, NetworkManager.ConnectionApprovalResponse>>();
}
