using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000134 RID: 308
public static class SettingsManager
{
	// Token: 0x060008B2 RID: 2226 RVA: 0x000364D4 File Offset: 0x000346D4
	public static void Initialize()
	{
		SettingsManager.Debug = SaveManager.GetBool("debug", false);
		SettingsManager.CameraAngle = SaveManager.GetFloat("cameraAngle", 30f);
		SettingsManager.Handedness = SaveManager.GetEnum<PlayerHandedness>("handedness", PlayerHandedness.Right);
		SettingsManager.ShowPuckSilhouette = SaveManager.GetBool("showPuckSilhouette", true);
		SettingsManager.ShowPuckOutline = SaveManager.GetBool("showPuckOutline", false);
		SettingsManager.ShowPuckElevation = SaveManager.GetBool("showPuckElevation", true);
		SettingsManager.ShowPlayerUsernames = SaveManager.GetBool("showPlayerUsernames", false);
		SettingsManager.PlayerUsernamesFadeThreshold = SaveManager.GetFloat("playerUsernamesFadeThreshold", 1f);
		SettingsManager.UseNetworkSmoothing = SaveManager.GetBool("useNetworkSmoothing", false);
		SettingsManager.NetworkSmoothingStrength = SaveManager.GetInt("networkSmoothingStrength", 1);
		SettingsManager.MaxMatchmakingPing = SaveManager.GetInt("maxMatchmakingPing", 50);
		SettingsManager.FilterChatProfanity = SaveManager.GetBool("filterChatProfanity", true);
		SettingsManager.Units = SaveManager.GetEnum<Units>("units", Units.Metric);
		SettingsManager.ShowGameUserInterface = SaveManager.GetBool("showGameUserInterface", true);
		SettingsManager.UserInterfaceScale = SaveManager.GetFloat("userInterfaceScale", 1f);
		SettingsManager.ChatOpacity = SaveManager.GetFloat("chatOpacity", 1f);
		SettingsManager.ChatScale = SaveManager.GetFloat("chatScale", 1f);
		SettingsManager.MinimapOpacity = SaveManager.GetFloat("minimapOpacity", 1f);
		SettingsManager.MinimapBackgroundOpacity = SaveManager.GetFloat("minimapBackgroundOpacity", 1f);
		SettingsManager.MinimapHorizontalPosition = SaveManager.GetFloat("minimapHorizontalPosition", 100f);
		SettingsManager.MinimapVerticalPosition = SaveManager.GetFloat("minimapVerticalPosition", 0f);
		SettingsManager.MinimapScale = SaveManager.GetFloat("minimapScale", 1f);
		SettingsManager.GlobalStickSensitivity = SaveManager.GetFloat("globalStickSensitivity", 0.2f);
		SettingsManager.HorizontalStickSensitivity = SaveManager.GetFloat("horizontalStickSensitivity", 1f);
		SettingsManager.VerticalStickSensitivity = SaveManager.GetFloat("verticalStickSensitivity", 1f);
		SettingsManager.LookSensitivity = SaveManager.GetFloat("lookSensitivity", 0.2f);
		SettingsManager.GlobalVolume = SaveManager.GetFloat("globalVolume", 0.5f);
		SettingsManager.AmbientVolume = SaveManager.GetFloat("ambientVolume", 1f);
		SettingsManager.GameVolume = SaveManager.GetFloat("gameVolume", 1f);
		SettingsManager.VoiceVolume = SaveManager.GetFloat("voiceVolume", 1f);
		SettingsManager.UIVolume = SaveManager.GetFloat("uiVolume", 0.5f);
		SettingsManager.FullScreenMode = SaveManager.GetEnum<FullScreenMode>("fullScreenMode", FullScreenMode.FullScreenWindow);
		SettingsManager.DisplayIndex = SaveManager.GetInt("displayIndex", 0);
		SettingsManager.ResolutionIndex = SaveManager.GetInt("resolutionIndex", -1);
		SettingsManager.VSync = SaveManager.GetBool("vSync", false);
		SettingsManager.FpsLimit = SaveManager.GetInt("fpsLimit", 240);
		SettingsManager.Fov = SaveManager.GetFloat("fov", 90f);
		SettingsManager.Quality = SaveManager.GetEnum<ApplicationQuality>("quality", ApplicationQuality.High);
		SettingsManager.MotionBlur = SaveManager.GetBool("motionBlur", true);
		SettingsManager.Team = SaveManager.GetEnum<PlayerTeam>("team", PlayerTeam.Blue);
		SettingsManager.Role = SaveManager.GetEnum<PlayerRole>("role", PlayerRole.Attacker);
		SettingsManager.ApplyForBothTeams = SaveManager.GetBool("applyForBothTeams", false);
		SettingsManager.FlagID = SaveManager.GetInt("flagID", -1);
		SettingsManager.HeadgearIDBlueAttacker = SaveManager.GetInt("headgearIDBlueAttacker", 513);
		SettingsManager.HeadgearIDRedAttacker = SaveManager.GetInt("headgearIDRedAttacker", 513);
		SettingsManager.HeadgearIDBlueGoalie = SaveManager.GetInt("headgearIDBlueGoalie", 527);
		SettingsManager.HeadgearIDRedGoalie = SaveManager.GetInt("headgearIDRedGoalie", 527);
		SettingsManager.MustacheID = SaveManager.GetInt("mustacheID", -1);
		SettingsManager.BeardID = SaveManager.GetInt("beardID", -1);
		SettingsManager.JerseyIDBlueAttacker = SaveManager.GetInt("jerseyIDBlueAttacker", 2048);
		SettingsManager.JerseyIDRedAttacker = SaveManager.GetInt("jerseyIDRedAttacker", 2048);
		SettingsManager.JerseyIDBlueGoalie = SaveManager.GetInt("jerseyIDBlueGoalie", 2048);
		SettingsManager.JerseyIDRedGoalie = SaveManager.GetInt("jerseyIDRedGoalie", 2048);
		SettingsManager.StickSkinIDBlueAttacker = SaveManager.GetInt("stickSkinIDBlueAttacker", 2621);
		SettingsManager.StickSkinIDRedAttacker = SaveManager.GetInt("stickSkinIDRedAttacker", 2621);
		SettingsManager.StickSkinIDBlueGoalie = SaveManager.GetInt("stickSkinIDBlueGoalie", 2621);
		SettingsManager.StickSkinIDRedGoalie = SaveManager.GetInt("stickSkinIDRedGoalie", 2621);
		SettingsManager.StickShaftTapeIDBlueAttacker = SaveManager.GetInt("stickShaftTapeIDBlueAttacker", -1);
		SettingsManager.StickShaftTapeIDRedAttacker = SaveManager.GetInt("stickShaftTapeIDRedAttacker", -1);
		SettingsManager.StickShaftTapeIDBlueGoalie = SaveManager.GetInt("stickShaftTapeIDBlueGoalie", -1);
		SettingsManager.StickShaftTapeIDRedGoalie = SaveManager.GetInt("stickShaftTapeIDRedGoalie", -1);
		SettingsManager.StickBladeTapeIDBlueAttacker = SaveManager.GetInt("stickBladeTapeIDBlueAttacker", -1);
		SettingsManager.StickBladeTapeIDRedAttacker = SaveManager.GetInt("stickBladeTapeIDRedAttacker", -1);
		SettingsManager.StickBladeTapeIDBlueGoalie = SaveManager.GetInt("stickBladeTapeIDBlueGoalie", -1);
		SettingsManager.StickBladeTapeIDRedGoalie = SaveManager.GetInt("stickBladeTapeIDRedGoalie", -1);
		SettingsManagerController.Initialize();
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0000ED94 File Offset: 0x0000CF94
	public static void Dispose()
	{
		SettingsManagerController.Dispose();
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0003697C File Offset: 0x00034B7C
	public static void ResetToDefault()
	{
		SettingsManager.UpdateDebug(false);
		SettingsManager.UpdateCameraAngle(30f);
		SettingsManager.UpdateHandedness(PlayerHandedness.Right);
		SettingsManager.UpdateShowPuckSilhouette(true);
		SettingsManager.UpdateShowPuckOutline(false);
		SettingsManager.UpdateShowPuckElevation(true);
		SettingsManager.UpdateShowPlayerUsernames(false);
		SettingsManager.UpdatePlayerUsernamesFadeThreshold(1f);
		SettingsManager.UpdateUseNetworkSmoothing(false);
		SettingsManager.UpdateNetworkSmoothingStrength(1);
		SettingsManager.UpdateMaxMatchmakingPing(50);
		SettingsManager.UpdateFilterChatProfanity(true);
		SettingsManager.UpdateUnits(Units.Metric);
		SettingsManager.UpdateShowGameUserInterface(true);
		SettingsManager.UpdateUserInterfaceScale(1f);
		SettingsManager.UpdateChatOpacity(1f);
		SettingsManager.UpdateChatScale(1f);
		SettingsManager.UpdateMinimapOpacity(1f);
		SettingsManager.UpdateMinimapBackgroundOpacity(1f);
		SettingsManager.UpdateMinimapHorizontalPosition(100f);
		SettingsManager.UpdateMinimapVerticalPosition(0f);
		SettingsManager.UpdateMinimapScale(1f);
		SettingsManager.UpdateGlobalStickSensitivity(0.2f);
		SettingsManager.UpdateHorizontalStickSensitivity(1f);
		SettingsManager.UpdateVerticalStickSensitivity(1f);
		SettingsManager.UpdateLookSensitivity(0.2f);
		SettingsManager.UpdateGlobalVolume(0.5f);
		SettingsManager.UpdateAmbientVolume(1f);
		SettingsManager.UpdateGameVolume(1f);
		SettingsManager.UpdateVoiceVolume(1f);
		SettingsManager.UpdateUIVolume(0.5f);
		SettingsManager.UpdateFullScreenMode(FullScreenMode.FullScreenWindow);
		SettingsManager.UpdateDisplayIndex(0);
		SettingsManager.UpdateResolutionIndex(-1);
		SettingsManager.UpdateVSync(false);
		SettingsManager.UpdateFpsLimit(240);
		SettingsManager.UpdateFov(90f);
		SettingsManager.UpdateQuality(ApplicationQuality.High);
		SettingsManager.UpdateMotionBlur(true);
		SettingsManager.UpdateTeam(PlayerTeam.Blue);
		SettingsManager.UpdateRole(PlayerRole.Attacker);
		SettingsManager.UpdateApplyForBothTeams(false);
		SettingsManager.UpdateFlagID(-1);
		SettingsManager.UpdateHeadgearID(PlayerTeam.Blue, PlayerRole.Attacker, 513);
		SettingsManager.UpdateHeadgearID(PlayerTeam.Red, PlayerRole.Attacker, 513);
		SettingsManager.UpdateHeadgearID(PlayerTeam.Blue, PlayerRole.Goalie, 527);
		SettingsManager.UpdateHeadgearID(PlayerTeam.Red, PlayerRole.Goalie, 527);
		SettingsManager.UpdateMustacheID(-1);
		SettingsManager.UpdateBeardID(-1);
		SettingsManager.UpdateJerseyID(PlayerTeam.Blue, PlayerRole.Attacker, 2048);
		SettingsManager.UpdateJerseyID(PlayerTeam.Red, PlayerRole.Attacker, 2048);
		SettingsManager.UpdateJerseyID(PlayerTeam.Blue, PlayerRole.Goalie, 2048);
		SettingsManager.UpdateJerseyID(PlayerTeam.Red, PlayerRole.Goalie, 2048);
		SettingsManager.UpdateStickSkinID(PlayerTeam.Blue, PlayerRole.Attacker, 2621);
		SettingsManager.UpdateStickSkinID(PlayerTeam.Red, PlayerRole.Attacker, 2621);
		SettingsManager.UpdateStickSkinID(PlayerTeam.Blue, PlayerRole.Goalie, 2621);
		SettingsManager.UpdateStickSkinID(PlayerTeam.Red, PlayerRole.Goalie, 2621);
		SettingsManager.UpdateStickShaftTapeID(PlayerTeam.Blue, PlayerRole.Attacker, -1);
		SettingsManager.UpdateStickShaftTapeID(PlayerTeam.Red, PlayerRole.Attacker, -1);
		SettingsManager.UpdateStickShaftTapeID(PlayerTeam.Blue, PlayerRole.Goalie, -1);
		SettingsManager.UpdateStickShaftTapeID(PlayerTeam.Red, PlayerRole.Goalie, -1);
		SettingsManager.UpdateStickBladeTapeID(PlayerTeam.Blue, PlayerRole.Attacker, -1);
		SettingsManager.UpdateStickBladeTapeID(PlayerTeam.Red, PlayerRole.Attacker, -1);
		SettingsManager.UpdateStickBladeTapeID(PlayerTeam.Blue, PlayerRole.Goalie, -1);
		SettingsManager.UpdateStickBladeTapeID(PlayerTeam.Red, PlayerRole.Goalie, -1);
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0000ED9B File Offset: 0x0000CF9B
	public static int GetHeadgearID(PlayerTeam team, PlayerRole role)
	{
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team == PlayerTeam.Blue)
				{
					return SettingsManager.HeadgearIDBlueGoalie;
				}
				if (team == PlayerTeam.Red)
				{
					return SettingsManager.HeadgearIDRedGoalie;
				}
			}
		}
		else
		{
			if (team == PlayerTeam.Blue)
			{
				return SettingsManager.HeadgearIDBlueAttacker;
			}
			if (team == PlayerTeam.Red)
			{
				return SettingsManager.HeadgearIDRedAttacker;
			}
		}
		return -1;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
	public static int GetJerseyID(PlayerTeam team, PlayerRole role)
	{
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team == PlayerTeam.Blue)
				{
					return SettingsManager.JerseyIDBlueGoalie;
				}
				if (team == PlayerTeam.Red)
				{
					return SettingsManager.JerseyIDRedGoalie;
				}
			}
		}
		else
		{
			if (team == PlayerTeam.Blue)
			{
				return SettingsManager.JerseyIDBlueAttacker;
			}
			if (team == PlayerTeam.Red)
			{
				return SettingsManager.JerseyIDRedAttacker;
			}
		}
		return 2048;
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0000EE11 File Offset: 0x0000D011
	public static int GetStickSkinID(PlayerTeam team, PlayerRole role)
	{
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team == PlayerTeam.Blue)
				{
					return SettingsManager.StickSkinIDBlueGoalie;
				}
				if (team == PlayerTeam.Red)
				{
					return SettingsManager.StickSkinIDRedGoalie;
				}
			}
		}
		else
		{
			if (team == PlayerTeam.Blue)
			{
				return SettingsManager.StickSkinIDBlueAttacker;
			}
			if (team == PlayerTeam.Red)
			{
				return SettingsManager.StickSkinIDRedAttacker;
			}
		}
		return 2621;
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0000EE4E File Offset: 0x0000D04E
	public static int GetStickShaftTapeID(PlayerTeam team, PlayerRole role)
	{
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team == PlayerTeam.Blue)
				{
					return SettingsManager.StickShaftTapeIDBlueGoalie;
				}
				if (team == PlayerTeam.Red)
				{
					return SettingsManager.StickShaftTapeIDRedGoalie;
				}
			}
		}
		else
		{
			if (team == PlayerTeam.Blue)
			{
				return SettingsManager.StickShaftTapeIDBlueAttacker;
			}
			if (team == PlayerTeam.Red)
			{
				return SettingsManager.StickShaftTapeIDRedAttacker;
			}
		}
		return -1;
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0000EE87 File Offset: 0x0000D087
	public static int GetStickBladeTapeID(PlayerTeam team, PlayerRole role)
	{
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team == PlayerTeam.Blue)
				{
					return SettingsManager.StickBladeTapeIDBlueGoalie;
				}
				if (team == PlayerTeam.Red)
				{
					return SettingsManager.StickBladeTapeIDRedGoalie;
				}
			}
		}
		else
		{
			if (team == PlayerTeam.Blue)
			{
				return SettingsManager.StickBladeTapeIDBlueAttacker;
			}
			if (team == PlayerTeam.Red)
			{
				return SettingsManager.StickBladeTapeIDRedAttacker;
			}
		}
		return -1;
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00036BBC File Offset: 0x00034DBC
	public static void UpdateDebug(bool value)
	{
		if (SettingsManager.Debug == value)
		{
			return;
		}
		SettingsManager.Debug = value;
		SaveManager.SetBool("debug", SettingsManager.Debug);
		EventManager.TriggerEvent("Event_OnDebugChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.Debug
			}
		});
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00036C0C File Offset: 0x00034E0C
	public static void UpdateCameraAngle(float value)
	{
		if (SettingsManager.CameraAngle == value)
		{
			return;
		}
		SettingsManager.CameraAngle = value;
		SaveManager.SetFloat("cameraAngle", SettingsManager.CameraAngle);
		EventManager.TriggerEvent("Event_OnCameraAngleChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.CameraAngle
			}
		});
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00036C5C File Offset: 0x00034E5C
	public static void UpdateHandedness(PlayerHandedness value)
	{
		if (SettingsManager.Handedness == value)
		{
			return;
		}
		SettingsManager.Handedness = value;
		SaveManager.SetEnum<PlayerHandedness>("handedness", SettingsManager.Handedness);
		EventManager.TriggerEvent("Event_OnHandednessChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.Handedness
			}
		});
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0000EEC0 File Offset: 0x0000D0C0
	public static void UpdateShowPuckSilhouette(bool value)
	{
		if (SettingsManager.ShowPuckSilhouette == value)
		{
			return;
		}
		SettingsManager.ShowPuckSilhouette = value;
		SaveManager.SetBool("showPuckSilhouette", SettingsManager.ShowPuckSilhouette);
		EventManager.TriggerEvent("Event_OnShowPuckSilhouetteChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0000EF00 File Offset: 0x0000D100
	public static void UpdateShowPuckOutline(bool value)
	{
		if (SettingsManager.ShowPuckOutline == value)
		{
			return;
		}
		SettingsManager.ShowPuckOutline = value;
		SaveManager.SetBool("showPuckOutline", SettingsManager.ShowPuckOutline);
		EventManager.TriggerEvent("Event_OnShowPuckOutlineChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0000EF40 File Offset: 0x0000D140
	public static void UpdateShowPuckElevation(bool value)
	{
		if (SettingsManager.ShowPuckElevation == value)
		{
			return;
		}
		SettingsManager.ShowPuckElevation = value;
		SaveManager.SetBool("showPuckElevation", SettingsManager.ShowPuckElevation);
		EventManager.TriggerEvent("Event_OnShowPuckElevationChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0000EF80 File Offset: 0x0000D180
	public static void UpdateShowPlayerUsernames(bool value)
	{
		if (SettingsManager.ShowPlayerUsernames == value)
		{
			return;
		}
		SettingsManager.ShowPlayerUsernames = value;
		SaveManager.SetBool("showPlayerUsernames", SettingsManager.ShowPlayerUsernames);
		EventManager.TriggerEvent("Event_OnShowPlayerUsernamesChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
	public static void UpdatePlayerUsernamesFadeThreshold(float value)
	{
		if (SettingsManager.PlayerUsernamesFadeThreshold == value)
		{
			return;
		}
		SettingsManager.PlayerUsernamesFadeThreshold = value;
		SaveManager.SetFloat("playerUsernamesFadeThreshold", SettingsManager.PlayerUsernamesFadeThreshold);
		EventManager.TriggerEvent("Event_OnPlayerUsernamesFadeThresholdChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0000F000 File Offset: 0x0000D200
	public static void UpdateUseNetworkSmoothing(bool value)
	{
		if (SettingsManager.UseNetworkSmoothing == value)
		{
			return;
		}
		SettingsManager.UseNetworkSmoothing = value;
		SaveManager.SetBool("useNetworkSmoothing", SettingsManager.UseNetworkSmoothing);
		EventManager.TriggerEvent("Event_OnUseNetworkSmoothingChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0000F040 File Offset: 0x0000D240
	public static void UpdateNetworkSmoothingStrength(int value)
	{
		if (SettingsManager.NetworkSmoothingStrength == value)
		{
			return;
		}
		SettingsManager.NetworkSmoothingStrength = value;
		SaveManager.SetInt("networkSmoothingStrength", SettingsManager.NetworkSmoothingStrength);
		EventManager.TriggerEvent("Event_OnNetworkSmoothingStrengthChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0000F080 File Offset: 0x0000D280
	public static void UpdateMaxMatchmakingPing(int value)
	{
		if (SettingsManager.MaxMatchmakingPing == value)
		{
			return;
		}
		SettingsManager.MaxMatchmakingPing = value;
		SaveManager.SetInt("maxMatchmakingPing", SettingsManager.MaxMatchmakingPing);
		EventManager.TriggerEvent("Event_OnMaxMatchmakingPingChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
	public static void UpdateFilterChatProfanity(bool value)
	{
		if (SettingsManager.FilterChatProfanity == value)
		{
			return;
		}
		SettingsManager.FilterChatProfanity = value;
		SaveManager.SetBool("filterChatProfanity", SettingsManager.FilterChatProfanity);
		EventManager.TriggerEvent("Event_OnFilterChatProfanityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00036CAC File Offset: 0x00034EAC
	public static void UpdateUnits(Units value)
	{
		if (SettingsManager.Units == value)
		{
			return;
		}
		SettingsManager.Units = value;
		SaveManager.SetEnum<Units>("units", SettingsManager.Units);
		EventManager.TriggerEvent("Event_OnUnitsChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.Units
			}
		});
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0000F100 File Offset: 0x0000D300
	public static void UpdateShowGameUserInterface(bool value)
	{
		if (SettingsManager.ShowGameUserInterface == value)
		{
			return;
		}
		SettingsManager.ShowGameUserInterface = value;
		SaveManager.SetBool("showGameUserInterface", SettingsManager.ShowGameUserInterface);
		EventManager.TriggerEvent("Event_OnShowGameUserInterfaceChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0000F140 File Offset: 0x0000D340
	public static void UpdateUserInterfaceScale(float value)
	{
		if (SettingsManager.UserInterfaceScale == value)
		{
			return;
		}
		SettingsManager.UserInterfaceScale = value;
		SaveManager.SetFloat("userInterfaceScale", SettingsManager.UserInterfaceScale);
		EventManager.TriggerEvent("Event_OnUserInterfaceScaleChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0000F180 File Offset: 0x0000D380
	public static void UpdateChatOpacity(float value)
	{
		if (SettingsManager.ChatOpacity == value)
		{
			return;
		}
		SettingsManager.ChatOpacity = value;
		SaveManager.SetFloat("chatOpacity", SettingsManager.ChatOpacity);
		EventManager.TriggerEvent("Event_OnChatOpacityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0000F1C0 File Offset: 0x0000D3C0
	public static void UpdateChatScale(float value)
	{
		if (SettingsManager.ChatScale == value)
		{
			return;
		}
		SettingsManager.ChatScale = value;
		SaveManager.SetFloat("chatScale", SettingsManager.ChatScale);
		EventManager.TriggerEvent("Event_OnChatScaleChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008CB RID: 2251 RVA: 0x0000F200 File Offset: 0x0000D400
	public static void UpdateMinimapOpacity(float value)
	{
		if (SettingsManager.MinimapOpacity == value)
		{
			return;
		}
		SettingsManager.MinimapOpacity = value;
		SaveManager.SetFloat("minimapOpacity", SettingsManager.MinimapOpacity);
		EventManager.TriggerEvent("Event_OnMinimapOpacityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008CC RID: 2252 RVA: 0x0000F240 File Offset: 0x0000D440
	public static void UpdateMinimapBackgroundOpacity(float value)
	{
		if (SettingsManager.MinimapBackgroundOpacity == value)
		{
			return;
		}
		SettingsManager.MinimapBackgroundOpacity = value;
		SaveManager.SetFloat("minimapBackgroundOpacity", SettingsManager.MinimapBackgroundOpacity);
		EventManager.TriggerEvent("Event_OnMinimapBackgroundOpacityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0000F280 File Offset: 0x0000D480
	public static void UpdateMinimapHorizontalPosition(float value)
	{
		if (SettingsManager.MinimapHorizontalPosition == value)
		{
			return;
		}
		SettingsManager.MinimapHorizontalPosition = value;
		SaveManager.SetFloat("minimapHorizontalPosition", SettingsManager.MinimapHorizontalPosition);
		EventManager.TriggerEvent("Event_OnMinimapHorizontalPositionChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0000F2C0 File Offset: 0x0000D4C0
	public static void UpdateMinimapVerticalPosition(float value)
	{
		if (SettingsManager.MinimapVerticalPosition == value)
		{
			return;
		}
		SettingsManager.MinimapVerticalPosition = value;
		SaveManager.SetFloat("minimapVerticalPosition", SettingsManager.MinimapVerticalPosition);
		EventManager.TriggerEvent("Event_OnMinimapVerticalPositionChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0000F300 File Offset: 0x0000D500
	public static void UpdateMinimapScale(float value)
	{
		if (SettingsManager.MinimapScale == value)
		{
			return;
		}
		SettingsManager.MinimapScale = value;
		SaveManager.SetFloat("minimapScale", SettingsManager.MinimapScale);
		EventManager.TriggerEvent("Event_OnMinimapScaleChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0000F340 File Offset: 0x0000D540
	public static void UpdateGlobalStickSensitivity(float value)
	{
		if (SettingsManager.GlobalStickSensitivity == value)
		{
			return;
		}
		SettingsManager.GlobalStickSensitivity = value;
		SaveManager.SetFloat("globalStickSensitivity", SettingsManager.GlobalStickSensitivity);
		EventManager.TriggerEvent("Event_OnGlobalStickSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0000F380 File Offset: 0x0000D580
	public static void UpdateHorizontalStickSensitivity(float value)
	{
		if (SettingsManager.HorizontalStickSensitivity == value)
		{
			return;
		}
		SettingsManager.HorizontalStickSensitivity = value;
		SaveManager.SetFloat("horizontalStickSensitivity", SettingsManager.HorizontalStickSensitivity);
		EventManager.TriggerEvent("Event_OnHorizontalStickSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
	public static void UpdateVerticalStickSensitivity(float value)
	{
		if (SettingsManager.VerticalStickSensitivity == value)
		{
			return;
		}
		SettingsManager.VerticalStickSensitivity = value;
		SaveManager.SetFloat("verticalStickSensitivity", SettingsManager.VerticalStickSensitivity);
		EventManager.TriggerEvent("Event_OnVerticalStickSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0000F400 File Offset: 0x0000D600
	public static void UpdateLookSensitivity(float value)
	{
		if (SettingsManager.LookSensitivity == value)
		{
			return;
		}
		SettingsManager.LookSensitivity = value;
		SaveManager.SetFloat("lookSensitivity", SettingsManager.LookSensitivity);
		EventManager.TriggerEvent("Event_OnLookSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0000F440 File Offset: 0x0000D640
	public static void UpdateGlobalVolume(float value)
	{
		if (SettingsManager.GlobalVolume == value)
		{
			return;
		}
		SettingsManager.GlobalVolume = value;
		SaveManager.SetFloat("globalVolume", SettingsManager.GlobalVolume);
		EventManager.TriggerEvent("Event_OnGlobalVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0000F480 File Offset: 0x0000D680
	public static void UpdateAmbientVolume(float value)
	{
		if (SettingsManager.AmbientVolume == value)
		{
			return;
		}
		SettingsManager.AmbientVolume = value;
		SaveManager.SetFloat("ambientVolume", SettingsManager.AmbientVolume);
		EventManager.TriggerEvent("Event_OnAmbientVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
	public static void UpdateGameVolume(float value)
	{
		if (SettingsManager.GameVolume == value)
		{
			return;
		}
		SettingsManager.GameVolume = value;
		SaveManager.SetFloat("gameVolume", SettingsManager.GameVolume);
		EventManager.TriggerEvent("Event_OnGameVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0000F500 File Offset: 0x0000D700
	public static void UpdateVoiceVolume(float value)
	{
		if (SettingsManager.VoiceVolume == value)
		{
			return;
		}
		SettingsManager.VoiceVolume = value;
		SaveManager.SetFloat("voiceVolume", SettingsManager.VoiceVolume);
		EventManager.TriggerEvent("Event_OnVoiceVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0000F540 File Offset: 0x0000D740
	public static void UpdateUIVolume(float value)
	{
		if (SettingsManager.UIVolume == value)
		{
			return;
		}
		SettingsManager.UIVolume = value;
		SaveManager.SetFloat("uiVolume", SettingsManager.UIVolume);
		EventManager.TriggerEvent("Event_OnUIVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00036CFC File Offset: 0x00034EFC
	public static void UpdateFullScreenMode(FullScreenMode value)
	{
		if (SettingsManager.FullScreenMode == value)
		{
			return;
		}
		SettingsManager.FullScreenMode = value;
		SaveManager.SetEnum<FullScreenMode>("fullScreenMode", SettingsManager.FullScreenMode);
		EventManager.TriggerEvent("Event_OnFullScreenModeChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.FullScreenMode
			}
		});
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00036D4C File Offset: 0x00034F4C
	public static void UpdateDisplayIndex(int value)
	{
		if (SettingsManager.DisplayIndex == value)
		{
			return;
		}
		SettingsManager.DisplayIndex = value;
		SaveManager.SetInt("displayIndex", SettingsManager.DisplayIndex);
		EventManager.TriggerEvent("Event_OnDisplayIndexChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.DisplayIndex
			}
		});
	}

	// Token: 0x060008DB RID: 2267 RVA: 0x00036D9C File Offset: 0x00034F9C
	public static void UpdateResolutionIndex(int value)
	{
		if (SettingsManager.ResolutionIndex == value)
		{
			return;
		}
		SettingsManager.ResolutionIndex = value;
		SaveManager.SetInt("resolutionIndex", SettingsManager.ResolutionIndex);
		EventManager.TriggerEvent("Event_OnResolutionIndexChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.ResolutionIndex
			}
		});
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x00036DEC File Offset: 0x00034FEC
	public static void UpdateVSync(bool value)
	{
		if (SettingsManager.VSync == value)
		{
			return;
		}
		SettingsManager.VSync = value;
		SaveManager.SetBool("vSync", SettingsManager.VSync);
		EventManager.TriggerEvent("Event_OnVSyncChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.VSync
			}
		});
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00036E3C File Offset: 0x0003503C
	public static void UpdateFpsLimit(int value)
	{
		if (SettingsManager.FpsLimit == value)
		{
			return;
		}
		SettingsManager.FpsLimit = value;
		SaveManager.SetInt("fpsLimit", SettingsManager.FpsLimit);
		EventManager.TriggerEvent("Event_OnFpsLimitChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.FpsLimit
			}
		});
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00036E8C File Offset: 0x0003508C
	public static void UpdateFov(float value)
	{
		if (SettingsManager.Fov == value)
		{
			return;
		}
		SettingsManager.Fov = value;
		SaveManager.SetFloat("fov", SettingsManager.Fov);
		EventManager.TriggerEvent("Event_OnFovChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.Fov
			}
		});
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00036EDC File Offset: 0x000350DC
	public static void UpdateQuality(ApplicationQuality value)
	{
		if (SettingsManager.Quality == value)
		{
			return;
		}
		SettingsManager.Quality = value;
		SaveManager.SetEnum<ApplicationQuality>("quality", SettingsManager.Quality);
		EventManager.TriggerEvent("Event_OnQualityChanged", new Dictionary<string, object>
		{
			{
				"value",
				SettingsManager.Quality
			}
		});
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0000F580 File Offset: 0x0000D780
	public static void UpdateMotionBlur(bool value)
	{
		if (SettingsManager.MotionBlur == value)
		{
			return;
		}
		SettingsManager.MotionBlur = value;
		SaveManager.SetBool("motionBlur", SettingsManager.MotionBlur);
		EventManager.TriggerEvent("Event_OnMotionBlurChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
	public static void UpdateTeam(PlayerTeam team)
	{
		if (SettingsManager.Team == team)
		{
			return;
		}
		SettingsManager.Team = team;
		SaveManager.SetEnum<PlayerTeam>("team", SettingsManager.Team);
		EventManager.TriggerEvent("Event_OnTeamChanged", new Dictionary<string, object>
		{
			{
				"value",
				team
			}
		});
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0000F600 File Offset: 0x0000D800
	public static void UpdateRole(PlayerRole role)
	{
		if (SettingsManager.Role == role)
		{
			return;
		}
		SettingsManager.Role = role;
		SaveManager.SetEnum<PlayerRole>("role", SettingsManager.Role);
		EventManager.TriggerEvent("Event_OnRoleChanged", new Dictionary<string, object>
		{
			{
				"value",
				role
			}
		});
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0000F640 File Offset: 0x0000D840
	public static void UpdateApplyForBothTeams(bool value)
	{
		if (SettingsManager.ApplyForBothTeams == value)
		{
			return;
		}
		SettingsManager.ApplyForBothTeams = value;
		SaveManager.SetBool("applyForBothTeams", SettingsManager.ApplyForBothTeams);
		EventManager.TriggerEvent("Event_OnApplyForBothTeamsChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0000F680 File Offset: 0x0000D880
	public static void UpdateFlagID(int value)
	{
		if (SettingsManager.FlagID == value)
		{
			return;
		}
		SettingsManager.FlagID = value;
		SaveManager.SetInt("flagID", SettingsManager.FlagID);
		EventManager.TriggerEvent("Event_OnFlagIDChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00036F2C File Offset: 0x0003512C
	public static void UpdateHeadgearID(PlayerTeam team, PlayerRole role, int value)
	{
		if (SettingsManager.GetHeadgearID(team, role) == value)
		{
			return;
		}
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team != PlayerTeam.Blue)
				{
					if (team == PlayerTeam.Red)
					{
						SettingsManager.HeadgearIDRedGoalie = value;
						SaveManager.SetInt("headgearIDRedGoalie", SettingsManager.HeadgearIDRedGoalie);
					}
				}
				else
				{
					SettingsManager.HeadgearIDBlueGoalie = value;
					SaveManager.SetInt("headgearIDBlueGoalie", SettingsManager.HeadgearIDBlueGoalie);
				}
			}
		}
		else if (team != PlayerTeam.Blue)
		{
			if (team == PlayerTeam.Red)
			{
				SettingsManager.HeadgearIDRedAttacker = value;
				SaveManager.SetInt("headgearIDRedAttacker", SettingsManager.HeadgearIDRedAttacker);
			}
		}
		else
		{
			SettingsManager.HeadgearIDBlueAttacker = value;
			SaveManager.SetInt("headgearIDBlueAttacker", SettingsManager.HeadgearIDBlueAttacker);
		}
		EventManager.TriggerEvent("Event_OnHeadgearIDChanged", new Dictionary<string, object>
		{
			{
				"team",
				team
			},
			{
				"role",
				role
			},
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
	public static void UpdateMustacheID(int value)
	{
		if (SettingsManager.MustacheID == value)
		{
			return;
		}
		SettingsManager.MustacheID = value;
		SaveManager.SetInt("mustacheID", SettingsManager.MustacheID);
		EventManager.TriggerEvent("Event_OnMustacheIDChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0000F700 File Offset: 0x0000D900
	public static void UpdateBeardID(int value)
	{
		if (SettingsManager.BeardID == value)
		{
			return;
		}
		SettingsManager.BeardID = value;
		SaveManager.SetInt("beardID", SettingsManager.BeardID);
		EventManager.TriggerEvent("Event_OnBeardIDChanged", new Dictionary<string, object>
		{
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00037000 File Offset: 0x00035200
	public static void UpdateJerseyID(PlayerTeam team, PlayerRole role, int value)
	{
		if (SettingsManager.GetJerseyID(team, role) == value)
		{
			return;
		}
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team != PlayerTeam.Blue)
				{
					if (team == PlayerTeam.Red)
					{
						SettingsManager.JerseyIDRedGoalie = value;
						SaveManager.SetInt("jerseyIDRedGoalie", SettingsManager.JerseyIDRedGoalie);
					}
				}
				else
				{
					SettingsManager.JerseyIDBlueGoalie = value;
					SaveManager.SetInt("jerseyIDBlueGoalie", SettingsManager.JerseyIDBlueGoalie);
				}
			}
		}
		else if (team != PlayerTeam.Blue)
		{
			if (team == PlayerTeam.Red)
			{
				SettingsManager.JerseyIDRedAttacker = value;
				SaveManager.SetInt("jerseyIDRedAttacker", SettingsManager.JerseyIDRedAttacker);
			}
		}
		else
		{
			SettingsManager.JerseyIDBlueAttacker = value;
			SaveManager.SetInt("jerseyIDBlueAttacker", SettingsManager.JerseyIDBlueAttacker);
		}
		EventManager.TriggerEvent("Event_OnJerseyIDChanged", new Dictionary<string, object>
		{
			{
				"team",
				team
			},
			{
				"role",
				role
			},
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x000370D4 File Offset: 0x000352D4
	public static void UpdateStickSkinID(PlayerTeam team, PlayerRole role, int value)
	{
		if (SettingsManager.GetStickSkinID(team, role) == value)
		{
			return;
		}
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team != PlayerTeam.Blue)
				{
					if (team == PlayerTeam.Red)
					{
						SettingsManager.StickSkinIDRedGoalie = value;
						SaveManager.SetInt("stickSkinIDRedGoalie", SettingsManager.StickSkinIDRedGoalie);
					}
				}
				else
				{
					SettingsManager.StickSkinIDBlueGoalie = value;
					SaveManager.SetInt("stickSkinIDBlueGoalie", SettingsManager.StickSkinIDBlueGoalie);
				}
			}
		}
		else if (team != PlayerTeam.Blue)
		{
			if (team == PlayerTeam.Red)
			{
				SettingsManager.StickSkinIDRedAttacker = value;
				SaveManager.SetInt("stickSkinIDRedAttacker", SettingsManager.StickSkinIDRedAttacker);
			}
		}
		else
		{
			SettingsManager.StickSkinIDBlueAttacker = value;
			SaveManager.SetInt("stickSkinIDBlueAttacker", SettingsManager.StickSkinIDBlueAttacker);
		}
		EventManager.TriggerEvent("Event_OnStickSkinIDChanged", new Dictionary<string, object>
		{
			{
				"team",
				team
			},
			{
				"role",
				role
			},
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x000371A8 File Offset: 0x000353A8
	public static void UpdateStickShaftTapeID(PlayerTeam team, PlayerRole role, int value)
	{
		if (SettingsManager.GetStickShaftTapeID(team, role) == value)
		{
			return;
		}
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team != PlayerTeam.Blue)
				{
					if (team == PlayerTeam.Red)
					{
						SettingsManager.StickShaftTapeIDRedGoalie = value;
						SaveManager.SetInt("stickShaftTapeIDRedGoalie", SettingsManager.StickShaftTapeIDRedGoalie);
					}
				}
				else
				{
					SettingsManager.StickShaftTapeIDBlueGoalie = value;
					SaveManager.SetInt("stickShaftTapeIDBlueGoalie", SettingsManager.StickShaftTapeIDBlueGoalie);
				}
			}
		}
		else if (team != PlayerTeam.Blue)
		{
			if (team == PlayerTeam.Red)
			{
				SettingsManager.StickShaftTapeIDRedAttacker = value;
				SaveManager.SetInt("stickShaftTapeIDRedAttacker", SettingsManager.StickShaftTapeIDRedAttacker);
			}
		}
		else
		{
			SettingsManager.StickShaftTapeIDBlueAttacker = value;
			SaveManager.SetInt("stickShaftTapeIDBlueAttacker", SettingsManager.StickShaftTapeIDBlueAttacker);
		}
		EventManager.TriggerEvent("Event_OnStickShaftTapeIDChanged", new Dictionary<string, object>
		{
			{
				"team",
				team
			},
			{
				"role",
				role
			},
			{
				"value",
				value
			}
		});
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0003727C File Offset: 0x0003547C
	public static void UpdateStickBladeTapeID(PlayerTeam team, PlayerRole role, int value)
	{
		if (SettingsManager.GetStickBladeTapeID(team, role) == value)
		{
			return;
		}
		if (role != PlayerRole.Attacker)
		{
			if (role == PlayerRole.Goalie)
			{
				if (team != PlayerTeam.Blue)
				{
					if (team == PlayerTeam.Red)
					{
						SettingsManager.StickBladeTapeIDRedGoalie = value;
						SaveManager.SetInt("stickBladeTapeIDRedGoalie", SettingsManager.StickBladeTapeIDRedGoalie);
					}
				}
				else
				{
					SettingsManager.StickBladeTapeIDBlueGoalie = value;
					SaveManager.SetInt("stickBladeTapeIDBlueGoalie", SettingsManager.StickBladeTapeIDBlueGoalie);
				}
			}
		}
		else if (team != PlayerTeam.Blue)
		{
			if (team == PlayerTeam.Red)
			{
				SettingsManager.StickBladeTapeIDRedAttacker = value;
				SaveManager.SetInt("stickBladeTapeIDRedAttacker", SettingsManager.StickBladeTapeIDRedAttacker);
			}
		}
		else
		{
			SettingsManager.StickBladeTapeIDBlueAttacker = value;
			SaveManager.SetInt("stickBladeTapeIDBlueAttacker", SettingsManager.StickBladeTapeIDBlueAttacker);
		}
		EventManager.TriggerEvent("Event_OnStickBladeTapeIDChanged", new Dictionary<string, object>
		{
			{
				"team",
				team
			},
			{
				"role",
				role
			},
			{
				"value",
				value
			}
		});
	}

	// Token: 0x04000527 RID: 1319
	public static bool Debug;

	// Token: 0x04000528 RID: 1320
	public static float CameraAngle;

	// Token: 0x04000529 RID: 1321
	public static PlayerHandedness Handedness;

	// Token: 0x0400052A RID: 1322
	public static bool ShowPuckSilhouette;

	// Token: 0x0400052B RID: 1323
	public static bool ShowPuckOutline;

	// Token: 0x0400052C RID: 1324
	public static bool ShowPuckElevation;

	// Token: 0x0400052D RID: 1325
	public static bool ShowPlayerUsernames;

	// Token: 0x0400052E RID: 1326
	public static float PlayerUsernamesFadeThreshold;

	// Token: 0x0400052F RID: 1327
	public static bool UseNetworkSmoothing;

	// Token: 0x04000530 RID: 1328
	public static int NetworkSmoothingStrength;

	// Token: 0x04000531 RID: 1329
	public static int MaxMatchmakingPing;

	// Token: 0x04000532 RID: 1330
	public static bool FilterChatProfanity;

	// Token: 0x04000533 RID: 1331
	public static Units Units;

	// Token: 0x04000534 RID: 1332
	public static bool ShowGameUserInterface;

	// Token: 0x04000535 RID: 1333
	public static float UserInterfaceScale;

	// Token: 0x04000536 RID: 1334
	public static float ChatOpacity;

	// Token: 0x04000537 RID: 1335
	public static float ChatScale;

	// Token: 0x04000538 RID: 1336
	public static float MinimapOpacity;

	// Token: 0x04000539 RID: 1337
	public static float MinimapBackgroundOpacity;

	// Token: 0x0400053A RID: 1338
	public static float MinimapHorizontalPosition;

	// Token: 0x0400053B RID: 1339
	public static float MinimapVerticalPosition;

	// Token: 0x0400053C RID: 1340
	public static float MinimapScale;

	// Token: 0x0400053D RID: 1341
	public static float GlobalStickSensitivity;

	// Token: 0x0400053E RID: 1342
	public static float HorizontalStickSensitivity;

	// Token: 0x0400053F RID: 1343
	public static float VerticalStickSensitivity;

	// Token: 0x04000540 RID: 1344
	public static float LookSensitivity;

	// Token: 0x04000541 RID: 1345
	public static float GlobalVolume;

	// Token: 0x04000542 RID: 1346
	public static float AmbientVolume;

	// Token: 0x04000543 RID: 1347
	public static float GameVolume;

	// Token: 0x04000544 RID: 1348
	public static float VoiceVolume;

	// Token: 0x04000545 RID: 1349
	public static float UIVolume;

	// Token: 0x04000546 RID: 1350
	public static FullScreenMode FullScreenMode;

	// Token: 0x04000547 RID: 1351
	public static int DisplayIndex;

	// Token: 0x04000548 RID: 1352
	public static int ResolutionIndex;

	// Token: 0x04000549 RID: 1353
	public static bool VSync;

	// Token: 0x0400054A RID: 1354
	public static int FpsLimit;

	// Token: 0x0400054B RID: 1355
	public static float Fov;

	// Token: 0x0400054C RID: 1356
	public static ApplicationQuality Quality;

	// Token: 0x0400054D RID: 1357
	public static bool MotionBlur;

	// Token: 0x0400054E RID: 1358
	public static PlayerTeam Team;

	// Token: 0x0400054F RID: 1359
	public static PlayerRole Role;

	// Token: 0x04000550 RID: 1360
	public static bool ApplyForBothTeams;

	// Token: 0x04000551 RID: 1361
	public static int FlagID;

	// Token: 0x04000552 RID: 1362
	public static int HeadgearIDBlueAttacker;

	// Token: 0x04000553 RID: 1363
	public static int HeadgearIDRedAttacker;

	// Token: 0x04000554 RID: 1364
	public static int HeadgearIDBlueGoalie;

	// Token: 0x04000555 RID: 1365
	public static int HeadgearIDRedGoalie;

	// Token: 0x04000556 RID: 1366
	public static int MustacheID;

	// Token: 0x04000557 RID: 1367
	public static int BeardID;

	// Token: 0x04000558 RID: 1368
	public static int JerseyIDBlueAttacker;

	// Token: 0x04000559 RID: 1369
	public static int JerseyIDRedAttacker;

	// Token: 0x0400055A RID: 1370
	public static int JerseyIDBlueGoalie;

	// Token: 0x0400055B RID: 1371
	public static int JerseyIDRedGoalie;

	// Token: 0x0400055C RID: 1372
	public static int StickSkinIDBlueAttacker;

	// Token: 0x0400055D RID: 1373
	public static int StickSkinIDRedAttacker;

	// Token: 0x0400055E RID: 1374
	public static int StickSkinIDBlueGoalie;

	// Token: 0x0400055F RID: 1375
	public static int StickSkinIDRedGoalie;

	// Token: 0x04000560 RID: 1376
	public static int StickShaftTapeIDBlueAttacker;

	// Token: 0x04000561 RID: 1377
	public static int StickShaftTapeIDRedAttacker;

	// Token: 0x04000562 RID: 1378
	public static int StickShaftTapeIDBlueGoalie;

	// Token: 0x04000563 RID: 1379
	public static int StickShaftTapeIDRedGoalie;

	// Token: 0x04000564 RID: 1380
	public static int StickBladeTapeIDBlueAttacker;

	// Token: 0x04000565 RID: 1381
	public static int StickBladeTapeIDRedAttacker;

	// Token: 0x04000566 RID: 1382
	public static int StickBladeTapeIDBlueGoalie;

	// Token: 0x04000567 RID: 1383
	public static int StickBladeTapeIDRedGoalie;
}
