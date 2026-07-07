using System;
using System.Collections.Generic;
using System.Linq;
using Steamworks;
using UnityEngine;

// Token: 0x02000139 RID: 313
public static class SteamIntegrationManager
{
	// Token: 0x170000EE RID: 238
	// (get) Token: 0x0600092F RID: 2351 RVA: 0x0000FBE8 File Offset: 0x0000DDE8
	public static bool IsOverlayEnabled
	{
		get
		{
			return SteamManager.IsInitialized && SteamUtils.IsOverlayEnabled();
		}
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
	public static void Initialize()
	{
		SteamIntegrationManager.RegisterCallbacks();
		SteamIntegrationManagerController.Initialize();
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0000FC04 File Offset: 0x0000DE04
	public static void Dispose()
	{
		SteamIntegrationManagerController.Dispose();
		SteamIntegrationManager.UnregisterCallbacks();
		SteamIntegrationManager.joinedLobbyIds.Clear();
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x0003817C File Offset: 0x0003637C
	private static void RegisterCallbacks()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamIntegrationManager.GetTicketForWebApiCallback = Callback<GetTicketForWebApiResponse_t>.Create(new Callback<GetTicketForWebApiResponse_t>.DispatchDelegate(SteamIntegrationManager.OnGetTicketForWebApiResponse));
		SteamIntegrationManager.MicroTxnAuthorizationResponse = Callback<MicroTxnAuthorizationResponse_t>.Create(new Callback<MicroTxnAuthorizationResponse_t>.DispatchDelegate(SteamIntegrationManager.OnMicroTxnAuthorizationResponse));
		SteamIntegrationManager.GameRichPresenceJoinRequested = Callback<GameRichPresenceJoinRequested_t>.Create(new Callback<GameRichPresenceJoinRequested_t>.DispatchDelegate(SteamIntegrationManager.OnGameRichPresenceJoinRequested));
		SteamIntegrationManager.NewUrlLaunchParameters = Callback<NewUrlLaunchParameters_t>.Create(new Callback<NewUrlLaunchParameters_t>.DispatchDelegate(SteamIntegrationManager.OnNewUrlLaunchParameters));
		SteamIntegrationManager.LobbyCreatedCallback = Callback<LobbyCreated_t>.Create(new Callback<LobbyCreated_t>.DispatchDelegate(SteamIntegrationManager.OnLobbyCreated));
		SteamIntegrationManager.LobbyEnterCallback = Callback<LobbyEnter_t>.Create(new Callback<LobbyEnter_t>.DispatchDelegate(SteamIntegrationManager.OnLobbyEntered));
		SteamIntegrationManager.LobbyChatUpdateCallback = Callback<LobbyChatUpdate_t>.Create(new Callback<LobbyChatUpdate_t>.DispatchDelegate(SteamIntegrationManager.OnLobbyChatUpdate));
		SteamIntegrationManager.GameLobbyJoinRequestedCallback = Callback<GameLobbyJoinRequested_t>.Create(new Callback<GameLobbyJoinRequested_t>.DispatchDelegate(SteamIntegrationManager.OnGameLobbyJoinRequested));
		SteamIntegrationManager.PersonaStateChangeCallback = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(SteamIntegrationManager.OnPersonaStateChange));
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00038258 File Offset: 0x00036458
	private static void UnregisterCallbacks()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamIntegrationManager.GetTicketForWebApiCallback.Unregister();
		SteamIntegrationManager.MicroTxnAuthorizationResponse.Unregister();
		SteamIntegrationManager.GameRichPresenceJoinRequested.Unregister();
		SteamIntegrationManager.NewUrlLaunchParameters.Unregister();
		SteamIntegrationManager.LobbyCreatedCallback.Unregister();
		SteamIntegrationManager.LobbyEnterCallback.Unregister();
		SteamIntegrationManager.LobbyChatUpdateCallback.Unregister();
		SteamIntegrationManager.GameLobbyJoinRequestedCallback.Unregister();
		SteamIntegrationManager.PersonaStateChangeCallback.Unregister();
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0000FC1A File Offset: 0x0000DE1A
	public static void SetRichPresenceMainMenu()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamFriends.ClearRichPresence();
		SteamFriends.SetRichPresence("steam_display", "#Status_MainMenu");
		SteamFriends.SetRichPresence("status", "In the changing room");
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x000382C8 File Offset: 0x000364C8
	public static void SetRichPresenceSpectating(Server server, int playerCount)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamFriends.SetRichPresence("steam_player_group", string.Format("{0}:{1}", server.IpAddress, server.Port));
		SteamFriends.SetRichPresence("steam_player_group_size", string.Format("{0}", playerCount));
		SteamFriends.SetRichPresence("steam_display", "#Status_Spectating");
		SteamFriends.SetRichPresence("status", "Spectating");
		SteamFriends.SetRichPresence("connect", string.Format("+ipAddress {0} +port {1}", server.IpAddress, server.Port));
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00038370 File Offset: 0x00036570
	public static void SetRichPresencePlaying(Server server, int playerCount)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamFriends.SetRichPresence("steam_player_group", string.Format("{0}:{1}", server.IpAddress, server.Port));
		SteamFriends.SetRichPresence("steam_player_group_size", string.Format("{0}", playerCount));
		SteamFriends.SetRichPresence("steam_display", "#Status_Playing");
		SteamFriends.SetRichPresence("status", "Playing");
		SteamFriends.SetRichPresence("connect", string.Format("+ipAddress {0} +port {1}", server.IpAddress, server.Port));
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00038418 File Offset: 0x00036618
	public static void UpdateRichPresenceScore(bool show, int period, int blueScore, int redScore)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		string pchValue = show ? string.Format(" | P{0} {1} - {2}", period, blueScore, redScore) : " ";
		SteamFriends.SetRichPresence("score", pchValue);
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00038460 File Offset: 0x00036660
	public static void UpdateRichPresenceRole(PlayerRole role)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		string pchValue = role.ToString().Replace("Attacker", "Skater");
		SteamFriends.SetRichPresence("role", pchValue);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x000384A0 File Offset: 0x000366A0
	public static void UpdateRichPresenceTeam(PlayerTeam team)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		string pchValue = team.ToString().Replace("Blue", "Team Blue").Replace("Red", "Team Red");
		SteamFriends.SetRichPresence("team", pchValue);
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x000384F0 File Offset: 0x000366F0
	public static void UpdateRichPresencePhase(GamePhase phase)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		string pchValue;
		if (phase == GamePhase.Warmup)
		{
			pchValue = "Warming up";
		}
		else
		{
			pchValue = "Playing";
		}
		SteamFriends.SetRichPresence("phase", pchValue);
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0000FC49 File Offset: 0x0000DE49
	public static void CreateLobby()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamIntegrationManager.Logger.Info("Creating lobby");
		SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, 128);
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0000FC6E File Offset: 0x0000DE6E
	public static void JoinLobby(string lobbyId)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		if (SteamIntegrationManager.IsInLobby(lobbyId))
		{
			return;
		}
		SteamIntegrationManager.Logger.Info("Joining lobby " + lobbyId);
		SteamMatchmaking.JoinLobby(new CSteamID(ulong.Parse(lobbyId)));
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00038524 File Offset: 0x00036724
	public static void LeaveLobby(string lobbyId)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		if (!SteamIntegrationManager.IsInLobby(lobbyId))
		{
			return;
		}
		SteamIntegrationManager.Logger.Info("Leaving lobby " + lobbyId);
		SteamMatchmaking.LeaveLobby(new CSteamID(ulong.Parse(lobbyId)));
		SteamIntegrationManager.joinedLobbyIds.Remove(lobbyId);
		EventManager.TriggerEvent("Event_OnLobbyLeft", new Dictionary<string, object>
		{
			{
				"lobbyId",
				lobbyId
			}
		});
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00038590 File Offset: 0x00036790
	public static void LeaveAllLobbies()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamIntegrationManager.Logger.Info("Leaving all lobbies");
		foreach (string lobbyId in SteamIntegrationManager.joinedLobbyIds.ToList<string>())
		{
			SteamIntegrationManager.LeaveLobby(lobbyId);
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0000FCA7 File Offset: 0x0000DEA7
	public static bool IsInLobby(string lobbyId)
	{
		return SteamIntegrationManager.joinedLobbyIds.Contains(lobbyId);
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000385FC File Offset: 0x000367FC
	public static string GetSteamId()
	{
		if (!SteamManager.IsInitialized)
		{
			return null;
		}
		return SteamUser.GetSteamID().ToString();
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
	public static void GetTicketForWebApi()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamUser.GetAuthTicketForWebApi("*");
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00038628 File Offset: 0x00036828
	public static void GetLaunchCommandLine()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		string text;
		SteamApps.GetLaunchCommandLine(out text, 256);
		string[] array = text.Split(" ", StringSplitOptions.None);
		if (array.Length != 0)
		{
			SteamIntegrationManager.Logger.Info(string.Format("GotLaunchCommandLine: {0} ({1})", text, array.Length));
			EventManager.TriggerEvent("Event_OnGotLaunchCommandLine", new Dictionary<string, object>
			{
				{
					"args",
					array
				}
			});
		}
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x00038694 File Offset: 0x00036894
	public static Texture2D GetAvatar(string steamId, AvatarSize size)
	{
		if (!SteamManager.IsInitialized)
		{
			return null;
		}
		CSteamID steamIDFriend = new CSteamID(ulong.Parse(steamId));
		int num;
		switch (size)
		{
		case AvatarSize.Small:
			num = SteamFriends.GetSmallFriendAvatar(steamIDFriend);
			break;
		case AvatarSize.Medium:
			num = SteamFriends.GetMediumFriendAvatar(steamIDFriend);
			break;
		case AvatarSize.Large:
			num = SteamFriends.GetLargeFriendAvatar(steamIDFriend);
			break;
		default:
			num = SteamFriends.GetMediumFriendAvatar(steamIDFriend);
			break;
		}
		int iImage = num;
		uint num2;
		uint num3;
		SteamUtils.GetImageSize(iImage, out num2, out num3);
		byte[] array = new byte[num2 * num3 * 4U];
		bool imageRGBA = SteamUtils.GetImageRGBA(iImage, array, array.Length);
		byte[] array2 = new byte[array.Length];
		int num4 = (int)(num2 * 4U);
		int num5 = 0;
		while ((long)num5 < (long)((ulong)num3))
		{
			Buffer.BlockCopy(array, num5 * num4, array2, (int)((num3 - 1U - (uint)num5) * (uint)num4), num4);
			num5++;
		}
		if (imageRGBA)
		{
			Texture2D texture2D = new Texture2D((int)num2, (int)num3, TextureFormat.RGBA32, false);
			texture2D.LoadRawTextureData(array2);
			texture2D.Apply();
			return texture2D;
		}
		return null;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0003876C File Offset: 0x0003696C
	public static string GetUsername(string steamId)
	{
		if (!SteamManager.IsInitialized)
		{
			return null;
		}
		CSteamID csteamID = new CSteamID(ulong.Parse(steamId));
		if (csteamID == SteamUser.GetSteamID())
		{
			return SteamFriends.GetPersonaName();
		}
		return SteamFriends.GetFriendPersonaName(csteamID);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x000387A8 File Offset: 0x000369A8
	public static string[] GetFriendSteamIds(bool includeOffline = false)
	{
		if (!SteamManager.IsInitialized)
		{
			return new string[0];
		}
		int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
		List<string> list = new List<string>();
		for (int i = 0; i < friendCount; i++)
		{
			string text = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate).ToString();
			if (SteamIntegrationManager.IsFriendOnline(text) || includeOffline)
			{
				list.Add(text);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00038810 File Offset: 0x00036A10
	public static string GetLobbyOwnerSteamId(string lobbyId)
	{
		if (!SteamManager.IsInitialized)
		{
			return null;
		}
		return SteamMatchmaking.GetLobbyOwner(new CSteamID(ulong.Parse(lobbyId))).ToString();
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00038844 File Offset: 0x00036A44
	public static string[] GetLobbyMemberSteamIds(string lobbyId)
	{
		if (!SteamManager.IsInitialized)
		{
			return new string[0];
		}
		CSteamID steamIDLobby = new CSteamID(ulong.Parse(lobbyId));
		int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(steamIDLobby);
		List<string> list = new List<string>();
		for (int i = 0; i < numLobbyMembers; i++)
		{
			string item = SteamMatchmaking.GetLobbyMemberByIndex(steamIDLobby, i).ToString();
			list.Add(item);
		}
		return list.ToArray();
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x0000FCC9 File Offset: 0x0000DEC9
	public static bool IsFriend(string steamId)
	{
		return SteamManager.IsInitialized && SteamFriends.GetFriendRelationship(new CSteamID(ulong.Parse(steamId))) == EFriendRelationship.k_EFriendRelationshipFriend;
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x0000FCE7 File Offset: 0x0000DEE7
	public static bool IsFriendOnline(string steamId)
	{
		return SteamManager.IsInitialized && SteamFriends.GetFriendPersonaState(new CSteamID(ulong.Parse(steamId))) > EPersonaState.k_EPersonaStateOffline;
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x000388AC File Offset: 0x00036AAC
	public static void InviteToLobby(string lobbyId, string invitedSteamId)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		CSteamID steamIDLobby = new CSteamID(ulong.Parse(lobbyId));
		CSteamID steamIDInvitee = new CSteamID(ulong.Parse(invitedSteamId));
		SteamMatchmaking.InviteUserToLobby(steamIDLobby, steamIDInvitee);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x000388E0 File Offset: 0x00036AE0
	private static void OnGetTicketForWebApiResponse(GetTicketForWebApiResponse_t response)
	{
		byte[] rgubTicket = response.m_rgubTicket;
		string value = BitConverter.ToString(rgubTicket, 0, rgubTicket.Length).Replace("-", string.Empty);
		EventManager.TriggerEvent("Event_OnGetTicketForWebApiResponse", new Dictionary<string, object>
		{
			{
				"ticket",
				value
			}
		});
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0003892C File Offset: 0x00036B2C
	private static void OnMicroTxnAuthorizationResponse(MicroTxnAuthorizationResponse_t response)
	{
		EventManager.TriggerEvent("Event_OnMicroTxnAuthorizationResponse", new Dictionary<string, object>
		{
			{
				"orderId",
				response.m_ulOrderID
			},
			{
				"authorized",
				Convert.ToBoolean(response.m_bAuthorized)
			}
		});
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0000FD05 File Offset: 0x0000DF05
	private static void OnGameRichPresenceJoinRequested(GameRichPresenceJoinRequested_t response)
	{
		EventManager.TriggerEvent("Event_OnGameRichPresenceJoinRequested", new Dictionary<string, object>
		{
			{
				"args",
				response.m_rgchConnect.Split(" ", StringSplitOptions.None)
			}
		});
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0000FD33 File Offset: 0x0000DF33
	private static void OnNewUrlLaunchParameters(NewUrlLaunchParameters_t response)
	{
		SteamIntegrationManager.GetLaunchCommandLine();
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0003897C File Offset: 0x00036B7C
	private static void OnLobbyCreated(LobbyCreated_t result)
	{
		if (result.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		string text = result.m_ulSteamIDLobby.ToString();
		SteamIntegrationManager.Logger.Info("Lobby " + text + " created");
		if (!SteamIntegrationManager.joinedLobbyIds.Contains(text))
		{
			SteamIntegrationManager.joinedLobbyIds.Add(text);
		}
		EventManager.TriggerEvent("Event_OnLobbyCreated", new Dictionary<string, object>
		{
			{
				"lobbyId",
				text
			}
		});
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x000389F0 File Offset: 0x00036BF0
	private static void OnLobbyEntered(LobbyEnter_t result)
	{
		string text = result.m_ulSteamIDLobby.ToString();
		SteamIntegrationManager.Logger.Info("Lobby " + text + " entered");
		if (!SteamIntegrationManager.joinedLobbyIds.Contains(text))
		{
			SteamIntegrationManager.joinedLobbyIds.Add(text);
		}
		EventManager.TriggerEvent("Event_OnLobbyEntered", new Dictionary<string, object>
		{
			{
				"lobbyId",
				text
			},
			{
				"ownerSteamId",
				SteamIntegrationManager.GetLobbyOwnerSteamId(text)
			},
			{
				"memberSteamIds",
				SteamIntegrationManager.GetLobbyMemberSteamIds(text)
			}
		});
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00038A7C File Offset: 0x00036C7C
	private static void OnLobbyChatUpdate(LobbyChatUpdate_t result)
	{
		string text = result.m_ulSteamIDLobby.ToString();
		SteamIntegrationManager.Logger.Info("Lobby " + text + " updated");
		EventManager.TriggerEvent("Event_OnLobbyChatUpdate", new Dictionary<string, object>
		{
			{
				"lobbyId",
				text
			},
			{
				"ownerSteamId",
				SteamIntegrationManager.GetLobbyOwnerSteamId(text)
			},
			{
				"memberSteamIds",
				SteamIntegrationManager.GetLobbyMemberSteamIds(text)
			}
		});
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x00038AF0 File Offset: 0x00036CF0
	private static void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t result)
	{
		CSteamID steamIDLobby = result.m_steamIDLobby;
		string text = steamIDLobby.ToString();
		SteamIntegrationManager.Logger.Info("Lobby " + text + " join requested");
		EventManager.TriggerEvent("Event_OnGameLobbyJoinRequested", new Dictionary<string, object>
		{
			{
				"lobbyId",
				text
			}
		});
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00038B48 File Offset: 0x00036D48
	private static void OnPersonaStateChange(PersonaStateChange_t result)
	{
		string value = result.m_ulSteamID.ToString();
		if ((result.m_nChangeFlags & (EPersonaChange.k_EPersonaChangeName | EPersonaChange.k_EPersonaChangeStatus | EPersonaChange.k_EPersonaChangeAvatar | EPersonaChange.k_EPersonaChangeRelationshipChanged | EPersonaChange.k_EPersonaChangeNickname)) != (EPersonaChange)0)
		{
			EventManager.TriggerEvent("Event_OnPersonaStateChange", new Dictionary<string, object>
			{
				{
					"steamId",
					value
				}
			});
		}
	}

	// Token: 0x04000575 RID: 1397
	private static readonly global::Logger Logger = new global::Logger("SteamIntegrationManager");

	// Token: 0x04000576 RID: 1398
	private static List<string> joinedLobbyIds = new List<string>();

	// Token: 0x04000577 RID: 1399
	private static Callback<GetTicketForWebApiResponse_t> GetTicketForWebApiCallback;

	// Token: 0x04000578 RID: 1400
	private static Callback<MicroTxnAuthorizationResponse_t> MicroTxnAuthorizationResponse;

	// Token: 0x04000579 RID: 1401
	private static Callback<GameRichPresenceJoinRequested_t> GameRichPresenceJoinRequested;

	// Token: 0x0400057A RID: 1402
	private static Callback<NewUrlLaunchParameters_t> NewUrlLaunchParameters;

	// Token: 0x0400057B RID: 1403
	private static Callback<LobbyCreated_t> LobbyCreatedCallback;

	// Token: 0x0400057C RID: 1404
	private static Callback<LobbyEnter_t> LobbyEnterCallback;

	// Token: 0x0400057D RID: 1405
	private static Callback<LobbyChatUpdate_t> LobbyChatUpdateCallback;

	// Token: 0x0400057E RID: 1406
	private static Callback<GameLobbyJoinRequested_t> GameLobbyJoinRequestedCallback;

	// Token: 0x0400057F RID: 1407
	private static Callback<PersonaStateChange_t> PersonaStateChangeCallback;
}
