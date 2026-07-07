using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Unity.Multiplayer.Tools.NetStatsMonitor;
using Unity.Netcode;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class ConnectionManagerController : MonoBehaviour
{
	// Token: 0x060006DA RID: 1754 RVA: 0x00030CAC File Offset: 0x0002EEAC
	private void Awake()
	{
		this.connectionManager = base.GetComponent<ConnectionManager>();
		this.runtimeNetStatsMonitor = base.GetComponent<RuntimeNetStatsMonitor>();
		EventManager.AddEventListener("Event_Server_OnConnectionRejected", new Action<Dictionary<string, object>>(this.Event_Server_OnConnectionRejected));
		EventManager.AddEventListener("Event_OnClientStarted", new Action<Dictionary<string, object>>(this.Event_OnClientStarted));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.AddEventListener("Event_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_OnClientConnected));
		EventManager.AddEventListener("Event_OnMainMenuClickJoinServer", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickJoinServer));
		EventManager.AddEventListener("Event_OnPauseMenuClickDisconnect", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickDisconnect));
		EventManager.AddEventListener("Event_OnDebugChanged", new Action<Dictionary<string, object>>(this.Event_OnDebugChanged));
		EventManager.AddEventListener("Event_OnGotLaunchCommandLine", new Action<Dictionary<string, object>>(this.Event_OnGotLaunchCommandLine));
		EventManager.AddEventListener("Event_OnGameRichPresenceJoinRequested", new Action<Dictionary<string, object>>(this.Event_OnGameRichPresenceJoinRequested));
		EventManager.AddEventListener("Event_OnServerBrowserClickEndPoint", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickEndPoint));
		EventManager.AddEventListener("Event_OnMatchmakingMatchingClickConnect", new Action<Dictionary<string, object>>(this.Event_OnMatchmakingMatchingClickConnect));
		EventManager.AddEventListener("Event_OnConnectionStateChanged", new Action<Dictionary<string, object>>(this.Event_OnConnectionStateChanged));
		EventManager.AddEventListener("Event_OnReconnectionStateChanged", new Action<Dictionary<string, object>>(this.Event_OnReconnectionStateChanged));
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x0000D597 File Offset: 0x0000B797
	private void Start()
	{
		NetworkManager.Singleton.NetworkConfig.NetworkMessageMetrics = SettingsManager.Debug;
		NetworkManager.Singleton.NetworkConfig.NetworkProfilingMetrics = SettingsManager.Debug;
		this.UpdateRnsmVisibility();
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00030DF0 File Offset: 0x0002EFF0
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Server_OnConnectionRejected", new Action<Dictionary<string, object>>(this.Event_Server_OnConnectionRejected));
		EventManager.RemoveEventListener("Event_OnClientStarted", new Action<Dictionary<string, object>>(this.Event_OnClientStarted));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.RemoveEventListener("Event_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_OnClientConnected));
		EventManager.RemoveEventListener("Event_OnMainMenuClickJoinServer", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickJoinServer));
		EventManager.RemoveEventListener("Event_OnPauseMenuClickDisconnect", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickDisconnect));
		EventManager.RemoveEventListener("Event_OnDebugChanged", new Action<Dictionary<string, object>>(this.Event_OnDebugChanged));
		EventManager.RemoveEventListener("Event_OnGotLaunchCommandLine", new Action<Dictionary<string, object>>(this.Event_OnGotLaunchCommandLine));
		EventManager.RemoveEventListener("Event_OnGameRichPresenceJoinRequested", new Action<Dictionary<string, object>>(this.Event_OnGameRichPresenceJoinRequested));
		EventManager.RemoveEventListener("Event_OnServerBrowserClickEndPoint", new Action<Dictionary<string, object>>(this.Event_OnServerBrowserClickEndPoint));
		EventManager.RemoveEventListener("Event_OnMatchmakingMatchingClickConnect", new Action<Dictionary<string, object>>(this.Event_OnMatchmakingMatchingClickConnect));
		EventManager.RemoveEventListener("Event_OnConnectionStateChanged", new Action<Dictionary<string, object>>(this.Event_OnConnectionStateChanged));
		EventManager.RemoveEventListener("Event_OnReconnectionStateChanged", new Action<Dictionary<string, object>>(this.Event_OnReconnectionStateChanged));
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0000D5C7 File Offset: 0x0000B7C7
	private void UpdateRnsmVisibility()
	{
		this.runtimeNetStatsMonitor.Visible = (SettingsManager.Debug && GlobalStateManager.ConnectionState.Phase == ConnectionPhase.Connected);
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00030F1C File Offset: 0x0002F11C
	private void HandleConnectionRejection(string reason)
	{
		ConnectionRejection connectionRejection;
		try
		{
			connectionRejection = JsonSerializer.Deserialize<ConnectionRejection>(reason, null);
		}
		catch
		{
			connectionRejection = new ConnectionRejection
			{
				code = ConnectionRejectionCode.Unreachable
			};
		}
		GlobalStateManager.SetConnectionState(new Dictionary<string, object>
		{
			{
				"connection",
				null
			},
			{
				"lastConnection",
				GlobalStateManager.ConnectionState.Connection
			},
			{
				"connectionRejection",
				connectionRejection
			},
			{
				"disconnection",
				null
			},
			{
				"phase",
				ConnectionPhase.Disconnected
			}
		});
		ConnectionRejectionCode code = connectionRejection.code;
		if (code - ConnectionRejectionCode.MissingPassword > 1)
		{
			if (code == ConnectionRejectionCode.MissingMods)
			{
				GlobalStateManager.SetReconnectionState(new Dictionary<string, object>
				{
					{
						"phase",
						ReconnectionPhase.AwaitingMods
					},
					{
						"clientRequiredModIds",
						connectionRejection.data.clientRequiredModIds
					},
					{
						"pendingReadinessModIds",
						new string[0]
					},
					{
						"pendingEnablingModIds",
						new string[0]
					}
				});
			}
		}
		else
		{
			GlobalStateManager.SetReconnectionState(new Dictionary<string, object>
			{
				{
					"phase",
					ReconnectionPhase.AwaitingPassword
				},
				{
					"password",
					null
				}
			});
		}
		EventManager.TriggerEvent("Event_OnConnectionRejected", new Dictionary<string, object>
		{
			{
				"connectionRejection",
				connectionRejection
			}
		});
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00031054 File Offset: 0x0002F254
	private void HandleDisconnection(string reason)
	{
		Disconnection value;
		try
		{
			value = JsonSerializer.Deserialize<Disconnection>(reason, null);
		}
		catch
		{
			if (NetworkManager.Singleton.DisconnectEvent == NetworkTransport.DisconnectEvents.TransportShutdown)
			{
				value = new Disconnection
				{
					code = DisconnectionCode.Disconnected
				};
			}
			else
			{
				value = new Disconnection
				{
					code = DisconnectionCode.ConnectionLost
				};
			}
		}
		GlobalStateManager.SetConnectionState(new Dictionary<string, object>
		{
			{
				"connection",
				null
			},
			{
				"lastConnection",
				GlobalStateManager.ConnectionState.Connection
			},
			{
				"connectionRejection",
				null
			},
			{
				"disconnection",
				value
			},
			{
				"phase",
				ConnectionPhase.Disconnected
			}
		});
		EventManager.TriggerEvent("Event_OnDisconnected", new Dictionary<string, object>
		{
			{
				"disconnection",
				value
			}
		});
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00031118 File Offset: 0x0002F318
	private void Event_Server_OnConnectionRejected(Dictionary<string, object> message)
	{
		ConnectionApproval connectionApproval = (ConnectionApproval)message["connectionApproval"];
		if (connectionApproval.IsHost)
		{
			this.connectionManager.Client_Disconnect();
			this.HandleConnectionRejection(connectionApproval.Response.Reason);
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0000D5EB File Offset: 0x0000B7EB
	private void Event_OnClientStarted(Dictionary<string, object> message)
	{
		GlobalStateManager.SetConnectionState(new Dictionary<string, object>
		{
			{
				"phase",
				ConnectionPhase.Connecting
			}
		});
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0003115C File Offset: 0x0002F35C
	private void Event_OnClientStopped(Dictionary<string, object> message)
	{
		if (GlobalStateManager.ConnectionState.Phase == ConnectionPhase.Connecting)
		{
			this.HandleConnectionRejection(NetworkManager.Singleton.DisconnectReason);
		}
		else
		{
			this.HandleDisconnection(NetworkManager.Singleton.DisconnectReason);
		}
		Connection pendingConnection = GlobalStateManager.ConnectionState.PendingConnection;
		if (pendingConnection != null)
		{
			this.connectionManager.Client_StartClient(pendingConnection.EndPoint.ipAddress, pendingConnection.EndPoint.port, pendingConnection.Password);
		}
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x0000D608 File Offset: 0x0000B808
	private void Event_OnClientConnected(Dictionary<string, object> message)
	{
		GlobalStateManager.SetConnectionState(new Dictionary<string, object>
		{
			{
				"phase",
				ConnectionPhase.Connected
			}
		});
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x000311D0 File Offset: 0x0002F3D0
	private void Event_OnMainMenuClickJoinServer(Dictionary<string, object> message)
	{
		string ipAddress = (string)message["ipAddress"];
		ushort port = (ushort)message["port"];
		string password = (string)message["password"];
		this.connectionManager.Client_StartClient(ipAddress, port, password);
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x0000D625 File Offset: 0x0000B825
	private void Event_OnPauseMenuClickDisconnect(Dictionary<string, object> message)
	{
		this.connectionManager.Client_Disconnect();
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00031220 File Offset: 0x0002F420
	private void Event_OnDebugChanged(Dictionary<string, object> message)
	{
		bool flag = (bool)message["value"];
		NetworkManager.Singleton.NetworkConfig.NetworkMessageMetrics = flag;
		NetworkManager.Singleton.NetworkConfig.NetworkProfilingMetrics = flag;
		this.UpdateRnsmVisibility();
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00031264 File Offset: 0x0002F464
	private void Event_OnGotLaunchCommandLine(Dictionary<string, object> message)
	{
		string[] args = (string[])message["args"];
		string commandLineArgument = Utils.GetCommandLineArgument("+ipAddress", args);
		ushort num;
		ushort port = ushort.TryParse(Utils.GetCommandLineArgument("+port", args), out num) ? num : 30609;
		string commandLineArgument2 = Utils.GetCommandLineArgument("+password", args);
		if (string.IsNullOrEmpty(commandLineArgument))
		{
			return;
		}
		this.connectionManager.Client_StartClient(commandLineArgument, port, commandLineArgument2);
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00031264 File Offset: 0x0002F464
	private void Event_OnGameRichPresenceJoinRequested(Dictionary<string, object> message)
	{
		string[] args = (string[])message["args"];
		string commandLineArgument = Utils.GetCommandLineArgument("+ipAddress", args);
		ushort num;
		ushort port = ushort.TryParse(Utils.GetCommandLineArgument("+port", args), out num) ? num : 30609;
		string commandLineArgument2 = Utils.GetCommandLineArgument("+password", args);
		if (string.IsNullOrEmpty(commandLineArgument))
		{
			return;
		}
		this.connectionManager.Client_StartClient(commandLineArgument, port, commandLineArgument2);
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x000312D0 File Offset: 0x0002F4D0
	private void Event_OnServerBrowserClickEndPoint(Dictionary<string, object> message)
	{
		EndPoint endPoint = (EndPoint)message["endPoint"];
		this.connectionManager.Client_StartClient(endPoint.ipAddress, endPoint.port, null);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x00031308 File Offset: 0x0002F508
	private void Event_OnMatchmakingMatchingClickConnect(Dictionary<string, object> message)
	{
		EndPoint endPoint = BackendManager.PlayerState.MatchData.endPoint;
		if (endPoint == null)
		{
			return;
		}
		this.connectionManager.Client_StartClient(endPoint.ipAddress, endPoint.port, null);
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00031348 File Offset: 0x0002F548
	private void Event_OnConnectionStateChanged(Dictionary<string, object> message)
	{
		ConnectionState connectionState = (ConnectionState)message["newConnectionState"];
		if (((ConnectionState)message["oldConnectionState"]).Phase == connectionState.Phase)
		{
			return;
		}
		this.UpdateRnsmVisibility();
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0003138C File Offset: 0x0002F58C
	private void Event_OnReconnectionStateChanged(Dictionary<string, object> message)
	{
		ReconnectionState reconnectionState = (ReconnectionState)message["newReconnectionState"];
		ReconnectionState reconnectionState2 = (ReconnectionState)message["oldReconnectionState"];
		ReconnectionPhase phase = reconnectionState.Phase;
		if (phase != ReconnectionPhase.AwaitingPassword)
		{
			if (phase != ReconnectionPhase.AwaitingMods)
			{
				return;
			}
			bool flag = !reconnectionState2.PendingEnablingModIds.SequenceEqual(reconnectionState.PendingEnablingModIds);
			bool flag2 = !reconnectionState2.PendingReadinessModIds.SequenceEqual(reconnectionState.PendingReadinessModIds);
			if ((flag || flag2) && reconnectionState.PendingModIds.Length == 0)
			{
				this.connectionManager.Client_StartClient(GlobalStateManager.ConnectionState.LastConnection.EndPoint.ipAddress, GlobalStateManager.ConnectionState.LastConnection.EndPoint.port, GlobalStateManager.ConnectionState.LastConnection.Password);
			}
		}
		else if (reconnectionState2.Password != reconnectionState.Password && reconnectionState.Password != null)
		{
			this.connectionManager.Client_StartClient(GlobalStateManager.ConnectionState.LastConnection.EndPoint.ipAddress, GlobalStateManager.ConnectionState.LastConnection.EndPoint.port, reconnectionState.Password);
			return;
		}
	}

	// Token: 0x04000433 RID: 1075
	private static readonly global::Logger Logger = new global::Logger("ConnectionManagerController");

	// Token: 0x04000434 RID: 1076
	private ConnectionManager connectionManager;

	// Token: 0x04000435 RID: 1077
	private RuntimeNetStatsMonitor runtimeNetStatsMonitor;
}
