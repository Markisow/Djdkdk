using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x02000205 RID: 517
public static class Utils
{
	// Token: 0x06000F35 RID: 3893 RVA: 0x000142F8 File Offset: 0x000124F8
	public static float WrapEulerAngle(float angle)
	{
		angle %= 360f;
		if (angle > 180f)
		{
			angle -= 360f;
		}
		if (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}

	// Token: 0x06000F36 RID: 3894 RVA: 0x0004DE20 File Offset: 0x0004C020
	public static Vector3 WrapEulerAngles(Vector3 eulerAngles)
	{
		eulerAngles.x %= 360f;
		if (eulerAngles.x > 180f)
		{
			eulerAngles.x -= 360f;
		}
		if (eulerAngles.x < -180f)
		{
			eulerAngles.x += 360f;
		}
		eulerAngles.y %= 360f;
		if (eulerAngles.y > 180f)
		{
			eulerAngles.y -= 360f;
		}
		if (eulerAngles.y < -180f)
		{
			eulerAngles.y += 360f;
		}
		eulerAngles.z %= 360f;
		if (eulerAngles.z > 180f)
		{
			eulerAngles.z -= 360f;
		}
		if (eulerAngles.z < -180f)
		{
			eulerAngles.z += 360f;
		}
		return eulerAngles;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x0004DF0C File Offset: 0x0004C10C
	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		Vector3 vector = point - pivot;
		vector = Quaternion.Euler(angles) * vector;
		point = vector + pivot;
		return point;
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x00014326 File Offset: 0x00012526
	public static Vector3 Vector2Clamp(Vector2 value, Vector2 min, Vector2 max)
	{
		return new Vector3(Mathf.Clamp(value.x, min.x, max.x), Mathf.Clamp(value.y, min.y, max.y));
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x0004DF38 File Offset: 0x0004C138
	public static Vector3 Vector3Clamp(Vector3 value, Vector3 min, Vector3 max)
	{
		return new Vector3(Mathf.Clamp(value.x, min.x, max.x), Mathf.Clamp(value.y, min.y, max.y), Mathf.Clamp(value.z, min.z, max.z));
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x0001435B File Offset: 0x0001255B
	public static Vector3 Vector3Abs(Vector3 value)
	{
		return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x00014383 File Offset: 0x00012583
	public static Vector3 Vector3Slerp3(Vector3 a, Vector3 b, Vector3 c, float t)
	{
		if (t <= 0f)
		{
			return Vector3.Slerp(a, b, t + 1f);
		}
		return Vector3.Slerp(b, c, t);
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x000143A4 File Offset: 0x000125A4
	public static float Map(float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0004DF90 File Offset: 0x0004C190
	public static Quaternion GetLocalLookRotation(Transform transform, Vector3 target)
	{
		if (transform.parent == null)
		{
			return Quaternion.LookRotation(target - transform.position);
		}
		Quaternion rhs = Quaternion.LookRotation(target - transform.position);
		return Quaternion.Inverse(transform.parent.rotation) * rhs;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x000143B4 File Offset: 0x000125B4
	public static float GameUnitsToMetric(float value)
	{
		return value * 3.6f;
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x000143BD File Offset: 0x000125BD
	public static float GameUnitsToImperial(float value)
	{
		return value * 2.2369363f;
	}

	// Token: 0x06000F40 RID: 3904 RVA: 0x0004DFE8 File Offset: 0x0004C1E8
	public static float GetCollisionForce(Collision collision)
	{
		if (collision == null)
		{
			return 0f;
		}
		float result = 0f;
		if (collision.contacts.Length != 0)
		{
			result = Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity.normalized) * collision.relativeVelocity.magnitude;
		}
		return result;
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x0004E044 File Offset: 0x0004C244
	public static void SetRigidbodyCollisionDetectionMode(Rigidbody rigidbody, CollisionDetectionMode mode)
	{
		if (rigidbody == null)
		{
			return;
		}
		if (rigidbody.collisionDetectionMode == mode)
		{
			return;
		}
		bool isKinematic = rigidbody.isKinematic;
		Vector3 linearVelocity = rigidbody.linearVelocity;
		Vector3 angularVelocity = rigidbody.angularVelocity;
		rigidbody.collisionDetectionMode = mode;
		rigidbody.isKinematic = true;
		rigidbody.isKinematic = false;
		rigidbody.isKinematic = isKinematic;
		rigidbody.linearVelocity = linearVelocity;
		rigidbody.angularVelocity = angularVelocity;
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x000143C6 File Offset: 0x000125C6
	public static List<string> GetTeamNames()
	{
		return new List<string>
		{
			"BLUE",
			"RED"
		};
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
	public static PlayerTeam GetTeamFromName(string name)
	{
		PlayerTeam result;
		if (!(name == "BLUE"))
		{
			if (!(name == "RED"))
			{
				result = PlayerTeam.Blue;
			}
			else
			{
				result = PlayerTeam.Red;
			}
		}
		else
		{
			result = PlayerTeam.Blue;
		}
		return result;
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x0004E0D8 File Offset: 0x0004C2D8
	public static string GetNameFromTeam(PlayerTeam team)
	{
		string result;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				result = "UNKNOWN";
			}
			else
			{
				result = "RED";
			}
		}
		else
		{
			result = "BLUE";
		}
		return result;
	}

	// Token: 0x06000F45 RID: 3909 RVA: 0x000143E3 File Offset: 0x000125E3
	public static List<string> GetRoleNames()
	{
		return new List<string>
		{
			"SKATER",
			"GOALIE"
		};
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x0004E108 File Offset: 0x0004C308
	public static PlayerRole GetRoleFromName(string name)
	{
		PlayerRole result;
		if (!(name == "SKATER"))
		{
			if (!(name == "GOALIE"))
			{
				result = PlayerRole.Attacker;
			}
			else
			{
				result = PlayerRole.Goalie;
			}
		}
		else
		{
			result = PlayerRole.Attacker;
		}
		return result;
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x0004E13C File Offset: 0x0004C33C
	public static string GetNameFromRole(PlayerRole role)
	{
		string result;
		if (role != PlayerRole.Attacker)
		{
			if (role != PlayerRole.Goalie)
			{
				result = "UNKNOWN";
			}
			else
			{
				result = "GOALIE";
			}
		}
		else
		{
			result = "SKATER";
		}
		return result;
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x00014400 File Offset: 0x00012600
	public static List<string> GetHandednessNames()
	{
		return new List<string>
		{
			"LEFT",
			"RIGHT"
		};
	}

	// Token: 0x06000F49 RID: 3913 RVA: 0x0004E16C File Offset: 0x0004C36C
	public static PlayerHandedness GetHandednessFromName(string name)
	{
		PlayerHandedness result;
		if (!(name == "LEFT"))
		{
			if (!(name == "RIGHT"))
			{
				result = PlayerHandedness.Right;
			}
			else
			{
				result = PlayerHandedness.Right;
			}
		}
		else
		{
			result = PlayerHandedness.Left;
		}
		return result;
	}

	// Token: 0x06000F4A RID: 3914 RVA: 0x0004E1A0 File Offset: 0x0004C3A0
	public static string GetNameFromHandedness(PlayerHandedness handedness)
	{
		string result;
		if (handedness != PlayerHandedness.Left)
		{
			if (handedness != PlayerHandedness.Right)
			{
				result = "UNKNOWN";
			}
			else
			{
				result = "RIGHT";
			}
		}
		else
		{
			result = "LEFT";
		}
		return result;
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x0001441D File Offset: 0x0001261D
	public static List<string> GetUnitsNames()
	{
		return new List<string>
		{
			"METRIC",
			"IMPERIAL"
		};
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x0004E1D0 File Offset: 0x0004C3D0
	public static Units GetUnitsFromName(string name)
	{
		Units result;
		if (!(name == "METRIC"))
		{
			if (!(name == "IMPERIAL"))
			{
				result = Units.Metric;
			}
			else
			{
				result = Units.Imperial;
			}
		}
		else
		{
			result = Units.Metric;
		}
		return result;
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x0004E204 File Offset: 0x0004C404
	public static string GetNameFromUnits(Units units)
	{
		string result;
		if (units != Units.Metric)
		{
			if (units != Units.Imperial)
			{
				result = "UNKNOWN";
			}
			else
			{
				result = "IMPERIAL";
			}
		}
		else
		{
			result = "METRIC";
		}
		return result;
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x0001443A File Offset: 0x0001263A
	public static List<string> GetFullScreenModeNames()
	{
		return new List<string>
		{
			"FULLSCREEN",
			"BORDERLESS",
			"WINDOWED"
		};
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x0004E234 File Offset: 0x0004C434
	public static FullScreenMode GetFullScreenModeFromName(string name)
	{
		FullScreenMode result;
		if (!(name == "FULLSCREEN"))
		{
			if (!(name == "BORDERLESS"))
			{
				if (!(name == "WINDOWED"))
				{
					result = FullScreenMode.FullScreenWindow;
				}
				else
				{
					result = FullScreenMode.Windowed;
				}
			}
			else
			{
				result = FullScreenMode.FullScreenWindow;
			}
		}
		else
		{
			result = FullScreenMode.ExclusiveFullScreen;
		}
		return result;
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x0004E27C File Offset: 0x0004C47C
	public static string GetNameFromFullScreenMode(FullScreenMode mode)
	{
		switch (mode)
		{
		case FullScreenMode.ExclusiveFullScreen:
			return "FULLSCREEN";
		case FullScreenMode.FullScreenWindow:
			return "BORDERLESS";
		case FullScreenMode.Windowed:
			return "WINDOWED";
		}
		return "UNKNOWN";
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00014462 File Offset: 0x00012662
	public static List<DisplayInfo> GetDisplayLayout()
	{
		List<DisplayInfo> list = new List<DisplayInfo>();
		Screen.GetDisplayLayout(list);
		return list;
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x0001446F File Offset: 0x0001266F
	public static List<string> GetDisplayNames()
	{
		return Utils.GetDisplayLayout().Select((DisplayInfo displayInfo, int index) => Utils.FormatDisplay(index, displayInfo)).ToList<string>();
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x0004E2C0 File Offset: 0x0004C4C0
	public static string GetDisplayNameFromIndex(int index)
	{
		List<DisplayInfo> displayLayout = Utils.GetDisplayLayout();
		if (index < 0 || index > displayLayout.Count - 1)
		{
			return "UNKNOWN";
		}
		return Utils.FormatDisplay(index, displayLayout[index]);
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x0004E2F8 File Offset: 0x0004C4F8
	public static int GetDisplayIndexFromName(string name)
	{
		List<DisplayInfo> displayLayout = Utils.GetDisplayLayout();
		for (int i = 0; i < displayLayout.Count; i++)
		{
			if (Utils.FormatDisplay(i, displayLayout[i]) == name)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x0001449F File Offset: 0x0001269F
	public static string FormatDisplay(int index, DisplayInfo displayInfo)
	{
		return string.Format("{0} ({1})", displayInfo.name, index);
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000144B7 File Offset: 0x000126B7
	public static List<Resolution> GetResolutions()
	{
		return Screen.resolutions.ToList<Resolution>();
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x000144C3 File Offset: 0x000126C3
	public static List<string> GetResolutionNames()
	{
		return (from resolution in Utils.GetResolutions()
		select Utils.FormatResolution(resolution)).ToList<string>();
	}

	// Token: 0x06000F58 RID: 3928 RVA: 0x0004E334 File Offset: 0x0004C534
	public static string GetResolutionNameFromIndex(int index)
	{
		List<Resolution> resolutions = Utils.GetResolutions();
		if (index < 0 || index > resolutions.Count - 1)
		{
			return "UNKNOWN";
		}
		return Utils.FormatResolution(resolutions[index]);
	}

	// Token: 0x06000F59 RID: 3929 RVA: 0x0004E368 File Offset: 0x0004C568
	public static int GetResolutionIndexFromName(string name)
	{
		List<Resolution> resolutions = Utils.GetResolutions();
		for (int i = 0; i < resolutions.Count; i++)
		{
			if (Utils.FormatResolution(resolutions[i]) == name)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x0004E3A4 File Offset: 0x0004C5A4
	public static string FormatResolution(Resolution resolution)
	{
		return string.Format("{0}x{1} @ {2}Hz", resolution.width, resolution.height, resolution.refreshRateRatio.value.ToString("F0"));
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x000144F3 File Offset: 0x000126F3
	public static List<string> GetApplicationQualityNames()
	{
		return new List<string>
		{
			"LOW",
			"MEDIUM",
			"HIGH",
			"ULTRA"
		};
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x0004E3F0 File Offset: 0x0004C5F0
	public static ApplicationQuality GetApplicationQualityFromName(string name)
	{
		ApplicationQuality result;
		if (!(name == "LOW"))
		{
			if (!(name == "MEDIUM"))
			{
				if (!(name == "HIGH"))
				{
					if (!(name == "ULTRA"))
					{
						result = ApplicationQuality.High;
					}
					else
					{
						result = ApplicationQuality.Ultra;
					}
				}
				else
				{
					result = ApplicationQuality.High;
				}
			}
			else
			{
				result = ApplicationQuality.Medium;
			}
		}
		else
		{
			result = ApplicationQuality.Low;
		}
		return result;
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x0004E448 File Offset: 0x0004C648
	public static string GetNameFromApplicationQuality(ApplicationQuality quality)
	{
		string result;
		switch (quality)
		{
		case ApplicationQuality.Low:
			result = "LOW";
			break;
		case ApplicationQuality.Medium:
			result = "MEDIUM";
			break;
		case ApplicationQuality.High:
			result = "HIGH";
			break;
		case ApplicationQuality.Ultra:
			result = "ULTRA";
			break;
		default:
			result = "UNKNOWN";
			break;
		}
		return result;
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x0004E494 File Offset: 0x0004C694
	public static KeyBindInteraction GetKeyBindInteractionFromInteraction(string interaction, KeyBindInteractionType interactionType)
	{
		if (interaction == "Press(behavior=1)")
		{
			return KeyBindInteraction.Release;
		}
		if (interaction == "DoublePress")
		{
			return KeyBindInteraction.DoublePress;
		}
		if (interaction == "Hold")
		{
			return KeyBindInteraction.Hold;
		}
		if (interaction == "Toggle")
		{
			return KeyBindInteraction.Toggle;
		}
		if (interactionType != KeyBindInteractionType.Press)
		{
			return KeyBindInteraction.Continuous;
		}
		return KeyBindInteraction.Press;
	}

	// Token: 0x06000F5F RID: 3935 RVA: 0x00014526 File Offset: 0x00012726
	public static string GetInteractionFromKeyBindInteraction(KeyBindInteraction keyBindInteraction)
	{
		switch (keyBindInteraction)
		{
		case KeyBindInteraction.Release:
			return "Press(behavior=1)";
		case KeyBindInteraction.DoublePress:
			return "DoublePress";
		case KeyBindInteraction.Hold:
			return "Hold";
		case KeyBindInteraction.Toggle:
			return "Toggle";
		}
		return string.Empty;
	}

	// Token: 0x06000F60 RID: 3936 RVA: 0x0004E4E8 File Offset: 0x0004C6E8
	public static string GetHumanizedGamePhase(GamePhase phase, int period, bool isOvertime)
	{
		string result;
		switch (phase)
		{
		case GamePhase.None:
			result = "";
			break;
		case GamePhase.Warmup:
			result = "WARMUP";
			break;
		case GamePhase.PreGame:
			result = "PRE-GAME";
			break;
		case GamePhase.FaceOff:
			result = "FACE-OFF";
			break;
		case GamePhase.Play:
			result = (isOvertime ? "OVERTIME" : string.Format("PERIOD {0}", period));
			break;
		case GamePhase.BlueScore:
			result = "SCORE!";
			break;
		case GamePhase.RedScore:
			result = "SCORE!";
			break;
		case GamePhase.Replay:
			result = "REPLAY";
			break;
		case GamePhase.Intermission:
			result = "INTERMISSION";
			break;
		case GamePhase.GameOver:
			result = "GAME OVER";
			break;
		case GamePhase.PostGame:
			result = "POST-GAME";
			break;
		default:
			result = phase.ToString();
			break;
		}
		return result;
	}

	// Token: 0x06000F61 RID: 3937 RVA: 0x0004E5A8 File Offset: 0x0004C7A8
	public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(sourceDir);
		if (!directoryInfo.Exists)
		{
			throw new DirectoryNotFoundException("Source directory not found: " + directoryInfo.FullName);
		}
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		Directory.CreateDirectory(destinationDir);
		foreach (FileInfo fileInfo in directoryInfo.GetFiles())
		{
			string destFileName = Path.Combine(destinationDir, fileInfo.Name);
			fileInfo.CopyTo(destFileName, true);
		}
		if (recursive)
		{
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				string destinationDir2 = Path.Combine(destinationDir, directoryInfo2.Name);
				Utils.CopyDirectory(directoryInfo2.FullName, destinationDir2, true);
			}
		}
	}

	// Token: 0x06000F62 RID: 3938 RVA: 0x0004E658 File Offset: 0x0004C858
	public static string GetConnectionRejectionMessage(ConnectionRejectionCode code, string message = null)
	{
		if (!string.IsNullOrEmpty(message))
		{
			return message;
		}
		switch (code)
		{
		default:
			return "Server unreachable";
		case ConnectionRejectionCode.ServerFull:
			return "Server full";
		case ConnectionRejectionCode.TimedOut:
			return "Timed out";
		case ConnectionRejectionCode.Banned:
			return "Banned";
		case ConnectionRejectionCode.NotWhitelisted:
			return "Not whitelisted";
		case ConnectionRejectionCode.MissingPassword:
			return "Missing password";
		case ConnectionRejectionCode.InvalidPassword:
			return "Invalid password";
		case ConnectionRejectionCode.MissingMods:
			return "Missing mods";
		}
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x00014563 File Offset: 0x00012763
	public static string GetDisconnectionMessage(DisconnectionCode code, string message = null)
	{
		if (!string.IsNullOrEmpty(message))
		{
			return message;
		}
		switch (code)
		{
		default:
			return "Connection lost";
		case DisconnectionCode.Disconnected:
			return "Disconnected";
		case DisconnectionCode.Kicked:
			return "Kicked";
		case DisconnectionCode.Banned:
			return "Banned";
		}
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x0004E6C4 File Offset: 0x0004C8C4
	public static string GetCommandLineArgument(string name, string[] args = null)
	{
		if (args == null)
		{
			args = Environment.GetCommandLineArgs();
		}
		int i = 0;
		while (i < args.Length)
		{
			if (args[i] == (name ?? ""))
			{
				if (i + 1 >= args.Length)
				{
					return null;
				}
				return args[i + 1];
			}
			else
			{
				i++;
			}
		}
		return null;
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0004E710 File Offset: 0x0004C910
	public static void PrintUPnPLogs()
	{
		if (uPnPHelper.DebugMode)
		{
			List<string> debugMessageArray = uPnPHelper.GetDebugMessageArray();
			foreach (string text in debugMessageArray.ToList<string>())
			{
				Utils.uPnPLogger.Info(text ?? "");
			}
			debugMessageArray.Clear();
		}
		List<string> errorMessageArray = uPnPHelper.GetErrorMessageArray();
		foreach (string text2 in errorMessageArray.ToList<string>())
		{
			Utils.uPnPLogger.Error(text2 ?? "");
		}
		errorMessageArray.Clear();
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0004E7E0 File Offset: 0x0004C9E0
	public static double GetTimestamp()
	{
		return DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
	}

	// Token: 0x06000F67 RID: 3943 RVA: 0x0004E810 File Offset: 0x0004CA10
	public static void WhenAllActions(Action callback, params Action<Action>[] subscriptions)
	{
		bool[] state = new bool[subscriptions.Length];
		bool callbackInvoked = false;
		for (int i = 0; i < subscriptions.Length; i++)
		{
			int index = i;
			subscriptions[index](delegate
			{
				if (state[index])
				{
					return;
				}
				state[index] = true;
				if (!callbackInvoked)
				{
					if (state.All((bool s) => s))
					{
						callbackInvoked = true;
						Action callback2 = callback;
						if (callback2 == null)
						{
							return;
						}
						callback2();
					}
				}
			});
		}
	}

	// Token: 0x06000F68 RID: 3944 RVA: 0x0001459C File Offset: 0x0001279C
	public static int GetVoteMajority(int playerCount)
	{
		if (playerCount == 1)
		{
			return 1;
		}
		if (playerCount != 2)
		{
			return Mathf.CeilToInt((float)(playerCount - 1) * 0.75f);
		}
		return 2;
	}

	// Token: 0x06000F69 RID: 3945 RVA: 0x0004E87C File Offset: 0x0004CA7C
	public static PlayerTeam? GetOpposingTeam(PlayerTeam team)
	{
		PlayerTeam? result;
		if (team != PlayerTeam.Blue)
		{
			if (team != PlayerTeam.Red)
			{
				result = null;
			}
			else
			{
				result = new PlayerTeam?(PlayerTeam.Blue);
			}
		}
		else
		{
			result = new PlayerTeam?(PlayerTeam.Red);
		}
		return result;
	}

	// Token: 0x04000940 RID: 2368
	private static readonly global::Logger uPnPLogger = new global::Logger("uPnPHelper");
}
