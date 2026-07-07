using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

// Token: 0x02000090 RID: 144
public static class BackendManagerController
{
	// Token: 0x060004D6 RID: 1238 RVA: 0x0002A82C File Offset: 0x00028A2C
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_OnGetTicketForWebApiResponse", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnGetTicketForWebApiResponse));
		EventManager.AddEventListener("Event_OnFooterClickCreateParty", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnFooterClickCreateParty));
		EventManager.AddEventListener("Event_OnFooterClickLeaveParty", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnFooterClickLeaveParty));
		EventManager.AddEventListener("Event_OnFooterClickDisbandParty", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnFooterClickDisbandParty));
		EventManager.AddEventListener("Event_OnLobbyCreated", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnLobbyCreated));
		EventManager.AddEventListener("Event_OnLobbyEntered", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnLobbyEntered));
		EventManager.AddEventListener("Event_OnPlayClickThreeVsThree", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnPlayClickThreeVsThree));
		EventManager.AddEventListener("Event_OnPlayClickFiveVsFive", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnPlayClickFiveVsFive));
		EventManager.AddEventListener("Event_OnMatchmakingMatchingClickClose", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnMatchmakingMatchingClickClose));
		EventManager.AddEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnPopupClickOk));
		EventManager.AddEventListener("Event_OnAppearanceClickPurchaseItem", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnAppearanceClickPurchaseItem));
		EventManager.AddEventListener("Event_OnMicroTxnAuthorizationResponse", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnMicroTxnAuthorizationResponse));
		EventManager.AddEventListener("Event_OnNewServerClickStart", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnNewServerClickStart));
		WebSocketManager.AddMessageListener("disconnected", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnDisconnected));
		WebSocketManager.AddMessageListener("playerAuthenticateResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerAuthenticateResponse));
		WebSocketManager.AddMessageListener("playerData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerData));
		WebSocketManager.AddMessageListener("playerPartyData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerPartyData));
		WebSocketManager.AddMessageListener("playerGroupData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerGroupData));
		WebSocketManager.AddMessageListener("playerMatchData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerMatchData));
		WebSocketManager.AddMessageListener("playerStatistics", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerStatistics));
		WebSocketManager.AddMessageListener("playerKey", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerKey));
		WebSocketManager.AddMessageListener("playerBeaconRttRequest", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerBeaconRttRequest));
		WebSocketManager.AddMessageListener("playerStartTransactionResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerStartTransactionResponse));
		WebSocketManager.AddMessageListener("playerFinalizeTransactionResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerFinalizeTransactionResponse));
		WebSocketManager.AddMessageListener("serverAuthenticateResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerAuthenticateResponse));
		WebSocketManager.AddMessageListener("serverUnauthenticateResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerUnauthenticateResponse));
		WebSocketManager.AddMessageListener("serverData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerData));
		WebSocketManager.AddMessageListener("serverMatchData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerMatchData));
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0002AAA4 File Offset: 0x00028CA4
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnGetTicketForWebApiResponse", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnGetTicketForWebApiResponse));
		EventManager.RemoveEventListener("Event_OnFooterClickCreateParty", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnFooterClickCreateParty));
		EventManager.RemoveEventListener("Event_OnFooterClickLeaveParty", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnFooterClickLeaveParty));
		EventManager.RemoveEventListener("Event_OnFooterClickDisbandParty", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnFooterClickDisbandParty));
		EventManager.RemoveEventListener("Event_OnLobbyCreated", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnLobbyCreated));
		EventManager.RemoveEventListener("Event_OnLobbyEntered", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnLobbyEntered));
		EventManager.RemoveEventListener("Event_OnPlayClickThreeVsThree", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnPlayClickThreeVsThree));
		EventManager.RemoveEventListener("Event_OnPlayClickFiveVsFive", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnPlayClickFiveVsFive));
		EventManager.RemoveEventListener("Event_OnMatchmakingMatchingClickClose", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnMatchmakingMatchingClickClose));
		EventManager.RemoveEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnPopupClickOk));
		EventManager.RemoveEventListener("Event_OnAppearanceClickPurchaseItem", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnAppearanceClickPurchaseItem));
		EventManager.RemoveEventListener("Event_OnMicroTxnAuthorizationResponse", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnMicroTxnAuthorizationResponse));
		EventManager.RemoveEventListener("Event_OnNewServerClickStart", new Action<Dictionary<string, object>>(BackendManagerController.Event_OnNewServerClickStart));
		WebSocketManager.RemoveMessageListener("disconnected", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnDisconnected));
		WebSocketManager.RemoveMessageListener("playerAuthenticateResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerAuthenticateResponse));
		WebSocketManager.RemoveMessageListener("playerData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerData));
		WebSocketManager.RemoveMessageListener("playerPartyData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerPartyData));
		WebSocketManager.RemoveMessageListener("playerGroupData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerGroupData));
		WebSocketManager.RemoveMessageListener("playerMatchData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerMatchData));
		WebSocketManager.RemoveMessageListener("playerStatistics", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerStatistics));
		WebSocketManager.RemoveMessageListener("playerKey", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerKey));
		WebSocketManager.RemoveMessageListener("playerBeaconRttRequest", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerBeaconRttRequest));
		WebSocketManager.RemoveMessageListener("playerStartTransactionResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerStartTransactionResponse));
		WebSocketManager.RemoveMessageListener("playerFinalizeTransactionResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnPlayerFinalizeTransactionResponse));
		WebSocketManager.RemoveMessageListener("serverAuthenticateResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerAuthenticateResponse));
		WebSocketManager.RemoveMessageListener("serverUnauthenticateResponse", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerUnauthenticateResponse));
		WebSocketManager.RemoveMessageListener("serverData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerData));
		WebSocketManager.RemoveMessageListener("serverMatchData", new Action<Dictionary<string, object>>(BackendManagerController.WebSocket_Event_OnServerMatchData));
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0002AD1C File Offset: 0x00028F1C
	private static int? PingBeacon(Beacon beacon, int connectTimeout, int responseTimeout)
	{
		EndPoint endPoint = new EndPoint(beacon.host, beacon.tcp_port);
		TCPClient tcpClient = new TCPClient(endPoint, connectTimeout, 1000);
		double pingTimestamp = 0.0;
		int? rtt = null;
		ManualResetEventSlim responseEvent = new ManualResetEventSlim(false);
		tcpClient.OnConnected += delegate()
		{
			tcpClient.SendMessage("ping");
		};
		tcpClient.OnMessageSent += delegate(string message)
		{
			pingTimestamp = Utils.GetTimestamp();
		};
		tcpClient.OnMessageReceived += delegate(string message)
		{
			rtt = new int?((int)(Utils.GetTimestamp() - pingTimestamp));
			responseEvent.Set();
		};
		tcpClient.Connect();
		if (tcpClient.IsConnected)
		{
			responseEvent.Wait(responseTimeout);
			tcpClient.Disconnect();
		}
		return rtt;
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0002ADF8 File Offset: 0x00028FF8
	private static void Event_OnGetTicketForWebApiResponse(Dictionary<string, object> message)
	{
		string value = (string)message["ticket"];
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"authenticationPhase",
				AuthenticationPhase.Authenticating
			}
		});
		WebSocketManager.Emit("playerAuthenticateRequest", new Dictionary<string, object>
		{
			{
				"ticket",
				value
			}
		}, "playerAuthenticateResponse");
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0000BF74 File Offset: 0x0000A174
	private static void Event_OnFooterClickCreateParty(Dictionary<string, object> message)
	{
		WebSocketManager.Emit("playerCreatePartyRequest", null, "playerCreatePartyResponse");
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0000BF86 File Offset: 0x0000A186
	private static void Event_OnFooterClickLeaveParty(Dictionary<string, object> message)
	{
		WebSocketManager.Emit("playerLeavePartyRequest", null, "playerLeavePartyResponse");
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0000BF98 File Offset: 0x0000A198
	private static void Event_OnFooterClickDisbandParty(Dictionary<string, object> message)
	{
		WebSocketManager.Emit("playerDisbandPartyRequest", null, "playerDisbandPartyResponse");
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0002AE54 File Offset: 0x00029054
	private static void Event_OnLobbyCreated(Dictionary<string, object> message)
	{
		string value = (string)message["lobbyId"];
		WebSocketManager.Emit("playerUpdatePartyRequest", new Dictionary<string, object>
		{
			{
				"steamLobbyId",
				value
			}
		}, "playerUpdatePartyResponse");
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0002AE94 File Offset: 0x00029094
	private static void Event_OnLobbyEntered(Dictionary<string, object> message)
	{
		string text = (string)message["lobbyId"];
		string text2 = (string)message["ownerSteamId"];
		BackendManagerController.Logger.Info(string.Concat(new string[]
		{
			"Entered lobby ",
			text,
			" owned by ",
			text2,
			" (player's Steam ID: ",
			BackendManager.PlayerState.PlayerData.steamId,
			")"
		}));
		if (text2 != BackendManager.PlayerState.PlayerData.steamId)
		{
			WebSocketManager.Emit("playerJoinPartyRequest", new Dictionary<string, object>
			{
				{
					"steamLobbyId",
					text
				}
			}, "playerJoinPartyResponse");
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0000BFAA File Offset: 0x0000A1AA
	private static void Event_OnPlayClickThreeVsThree(Dictionary<string, object> message)
	{
		WebSocketManager.Emit("playerStartMatchmakingRequest", new Dictionary<string, object>
		{
			{
				"poolId",
				"3v3"
			},
			{
				"maxRtt",
				SettingsManager.MaxMatchmakingPing
			}
		}, "playerStartMatchmakingResponse");
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0000BFE5 File Offset: 0x0000A1E5
	private static void Event_OnPlayClickFiveVsFive(Dictionary<string, object> message)
	{
		WebSocketManager.Emit("playerStartMatchmakingRequest", new Dictionary<string, object>
		{
			{
				"poolId",
				"5v5"
			},
			{
				"maxRtt",
				SettingsManager.MaxMatchmakingPing
			}
		}, "playerStartMatchmakingResponse");
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0000C020 File Offset: 0x0000A220
	private static void Event_OnMatchmakingMatchingClickClose(Dictionary<string, object> message)
	{
		WebSocketManager.Emit("playerStopMatchmakingRequest", null, "playerStopMatchmakingResponse");
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0002AF4C File Offset: 0x0002914C
	private static void Event_OnPopupClickOk(Dictionary<string, object> message)
	{
		Popup popup = (Popup)message["popup"];
		if (popup.Name == "identity")
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)popup.Data;
			string value = (string)dictionary["username"];
			int num = (int)dictionary["number"];
			WebSocketManager.Emit("playerSetIdentityRequest", new Dictionary<string, object>
			{
				{
					"username",
					value
				},
				{
					"number",
					num
				}
			}, "playerSetIdentityResponse");
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0002AFDC File Offset: 0x000291DC
	private static void Event_OnAppearanceClickPurchaseItem(Dictionary<string, object> message)
	{
		Item item = (Item)message["item"];
		BackendManager.SetTransactionState(new Dictionary<string, object>
		{
			{
				"phase",
				TransactionPhase.Starting
			}
		});
		if (!SteamIntegrationManager.IsOverlayEnabled)
		{
			BackendManager.SetTransactionState(new Dictionary<string, object>
			{
				{
					"phase",
					TransactionPhase.None
				}
			});
			return;
		}
		WebSocketManager.Emit("playerStartTransactionRequest", new Dictionary<string, object>
		{
			{
				"itemId",
				item.id
			}
		}, "playerStartTransactionResponse");
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0002B064 File Offset: 0x00029264
	private static void Event_OnMicroTxnAuthorizationResponse(Dictionary<string, object> message)
	{
		if ((bool)message["authorized"])
		{
			WebSocketManager.Emit("playerFinalizeTransactionRequest", null, "playerFinalizeTransactionResponse");
			return;
		}
		WebSocketManager.Emit("playerCancelTransaction", null, null);
		BackendManager.SetTransactionState(new Dictionary<string, object>
		{
			{
				"phase",
				TransactionPhase.None
			}
		});
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0002B0BC File Offset: 0x000292BC
	private static void Event_OnNewServerClickStart(Dictionary<string, object> message)
	{
		if ((string)message["type"] != "dedicated")
		{
			return;
		}
		string value = (string)message["name"];
		int num = (int)message["maxPlayers"];
		string value2 = (string)message["password"];
		string value3 = (string)message["locationId"];
		WebSocketManager.Emit("playerDeployServerRequest", new Dictionary<string, object>
		{
			{
				"name",
				value
			},
			{
				"maxPlayers",
				num
			},
			{
				"password",
				value2
			},
			{
				"locationId",
				value3
			}
		}, "playerDeployServerResponse");
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0002B174 File Offset: 0x00029374
	private static void WebSocket_Event_OnDisconnected(Dictionary<string, object> message)
	{
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"steamId",
				null
			},
			{
				"playerData",
				null
			},
			{
				"partyData",
				null
			},
			{
				"key",
				null
			},
			{
				"authenticationPhase",
				AuthenticationPhase.None
			}
		});
		BackendManager.SetServerState(new Dictionary<string, object>
		{
			{
				"serverData",
				null
			},
			{
				"matchData",
				null
			},
			{
				"authenticationPhase",
				AuthenticationPhase.None
			}
		});
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0002B200 File Offset: 0x00029400
	private static void WebSocket_Event_OnPlayerAuthenticateResponse(Dictionary<string, object> message)
	{
		PlayerAuthenticateResponse data = ((InMessage)message["inMessage"]).GetData<PlayerAuthenticateResponse>();
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"authenticationPhase",
				data.success ? AuthenticationPhase.Authenticated : AuthenticationPhase.None
			}
		});
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0002B24C File Offset: 0x0002944C
	private static void WebSocket_Event_OnPlayerData(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		PlayerData playerData = inMessage.GetData<PlayerData>();
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"playerData",
				playerData
			}
		});
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0002B288 File Offset: 0x00029488
	private static void WebSocket_Event_OnPlayerPartyData(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"partyData",
				inMessage.GetData<PlayerPartyData>()
			}
		});
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0002B2C4 File Offset: 0x000294C4
	private static void WebSocket_Event_OnPlayerGroupData(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"groupData",
				inMessage.GetData<PlayerGroupData>()
			}
		});
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0002B300 File Offset: 0x00029500
	private static void WebSocket_Event_OnPlayerMatchData(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"matchData",
				inMessage.GetData<PlayerMatchData>()
			}
		});
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0002B33C File Offset: 0x0002953C
	private static void WebSocket_Event_OnPlayerStatistics(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"playerStatistics",
				inMessage.GetData<PlayerStatistics>()
			}
		});
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0002B378 File Offset: 0x00029578
	private static void WebSocket_Event_OnPlayerKey(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		BackendManager.SetPlayerState(new Dictionary<string, object>
		{
			{
				"key",
				inMessage.GetData<string>()
			}
		});
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0000C032 File Offset: 0x0000A232
	private static void WebSocket_Event_OnPlayerBeaconRttRequest(Dictionary<string, object> message)
	{
		BackendManagerController.<>c__DisplayClass25_0 CS$<>8__locals1 = new BackendManagerController.<>c__DisplayClass25_0();
		CS$<>8__locals1.inMessage = (InMessage)message["inMessage"];
		CS$<>8__locals1.beacons = CS$<>8__locals1.inMessage.GetData<Beacon[]>();
		Task.Run(delegate()
		{
			BackendManagerController.<>c__DisplayClass25_0.<<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d <<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d;
			<<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d.<>4__this = CS$<>8__locals1;
			<<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d.<>1__state = -1;
			<<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d.<>t__builder.Start<BackendManagerController.<>c__DisplayClass25_0.<<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d>(ref <<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d);
			return <<WebSocket_Event_OnPlayerBeaconRttRequest>b__0>d.<>t__builder.Task;
		});
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0002B3B4 File Offset: 0x000295B4
	private static void WebSocket_Event_OnPlayerStartTransactionResponse(Dictionary<string, object> message)
	{
		if (((InMessage)message["inMessage"]).GetData<PlayerStartTransactionResponse>().success)
		{
			BackendManager.SetTransactionState(new Dictionary<string, object>
			{
				{
					"phase",
					TransactionPhase.Started
				}
			});
			return;
		}
		BackendManager.SetTransactionState(new Dictionary<string, object>
		{
			{
				"phase",
				TransactionPhase.None
			}
		});
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0000C071 File Offset: 0x0000A271
	private static void WebSocket_Event_OnPlayerFinalizeTransactionResponse(Dictionary<string, object> message)
	{
		((InMessage)message["inMessage"]).GetData<PlayerFinalizeTransactionResponse>();
		BackendManager.SetTransactionState(new Dictionary<string, object>
		{
			{
				"phase",
				TransactionPhase.None
			}
		});
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0002B414 File Offset: 0x00029614
	private static void WebSocket_Event_OnServerAuthenticateResponse(Dictionary<string, object> message)
	{
		ServerAuthenticateResponse data = ((InMessage)message["inMessage"]).GetData<ServerAuthenticateResponse>();
		BackendManager.SetServerState(new Dictionary<string, object>
		{
			{
				"authenticationPhase",
				data.success ? AuthenticationPhase.Authenticated : AuthenticationPhase.None
			}
		});
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
	private static void WebSocket_Event_OnServerUnauthenticateResponse(Dictionary<string, object> message)
	{
		BackendManager.SetServerState(new Dictionary<string, object>
		{
			{
				"authenticationPhase",
				AuthenticationPhase.None
			}
		});
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0002B460 File Offset: 0x00029660
	private static void WebSocket_Event_OnServerData(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		BackendManager.SetServerState(new Dictionary<string, object>
		{
			{
				"serverData",
				inMessage.GetData<ServerData>()
			}
		});
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0002B49C File Offset: 0x0002969C
	private static void WebSocket_Event_OnServerMatchData(Dictionary<string, object> message)
	{
		InMessage inMessage = (InMessage)message["inMessage"];
		BackendManager.SetServerState(new Dictionary<string, object>
		{
			{
				"matchData",
				inMessage.GetData<ServerMatchData>()
			}
		});
	}

	// Token: 0x040002FB RID: 763
	private static readonly Logger Logger = new Logger("BackendManagerController");
}
