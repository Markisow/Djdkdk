using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public static class Constants
{
	// Token: 0x0400087A RID: 2170
	public const uint APP_ID = 2994020U;

	// Token: 0x0400087B RID: 2171
	public const float STEAM_INITIALIZATION_RETRY_DELAY = 5f;

	// Token: 0x0400087C RID: 2172
	public static readonly Dictionary<EdgegapDependency, float> EDGEGAP_DEPENDENCY_TIMEOUTS = new Dictionary<EdgegapDependency, float>
	{
		{
			EdgegapDependency.IsAuthenticated,
			60f
		},
		{
			EdgegapDependency.IsOccupied,
			60f
		}
	};

	// Token: 0x0400087D RID: 2173
	public const int WEB_SOCKET_CONNECTION_TIMEOUT = 5000;

	// Token: 0x0400087E RID: 2174
	public const string TEAM_BLUE_COLOR = "#3b82f6";

	// Token: 0x0400087F RID: 2175
	public const string TEAM_RED_COLOR = "#d13333";

	// Token: 0x04000880 RID: 2176
	public const string TEAM_SPECTATOR_COLOR = "#404040";

	// Token: 0x04000881 RID: 2177
	public const string PATREON_COLOR = "#f1c40f";

	// Token: 0x04000882 RID: 2178
	public const string MODERATOR_COLOR = "#206694";

	// Token: 0x04000883 RID: 2179
	public const string ADMIN_COLOR = "#992d22";

	// Token: 0x04000884 RID: 2180
	public const string DEVELOPER_COLOR = "#71368a";

	// Token: 0x04000885 RID: 2181
	public const string SERVER_COLOR = "#b8b8b8";

	// Token: 0x04000886 RID: 2182
	public const string VOTE_COLOR = "#e67e22";

	// Token: 0x04000887 RID: 2183
	public const string GAME_COLOR = "#ffe97f";

	// Token: 0x04000888 RID: 2184
	public const string ERROR_COLOR = "#e74c3c";

	// Token: 0x04000889 RID: 2185
	public const bool DEFAULT_SETTINGS_DEBUG = false;

	// Token: 0x0400088A RID: 2186
	public const float DEFAULT_SETTINGS_CAMERA_ANGLE = 30f;

	// Token: 0x0400088B RID: 2187
	public const PlayerHandedness DEFAULT_SETTINGS_HANDEDNESS = PlayerHandedness.Right;

	// Token: 0x0400088C RID: 2188
	public const bool DEFAULT_SETTINGS_SHOW_PUCK_SILHOUETTE = true;

	// Token: 0x0400088D RID: 2189
	public const bool DEFAULT_SETTINGS_SHOW_PUCK_OUTLINE = false;

	// Token: 0x0400088E RID: 2190
	public const bool DEFAULT_SETTINGS_SHOW_PUCK_ELEVATION = true;

	// Token: 0x0400088F RID: 2191
	public const bool DEFAULT_SETTINGS_SHOW_PLAYER_USERNAMES = false;

	// Token: 0x04000890 RID: 2192
	public const float DEFAULT_SETTINGS_PLAYER_USERNAMES_FADE_THRESHOLD = 1f;

	// Token: 0x04000891 RID: 2193
	public const bool DEFAULT_SETTINGS_USE_NETWORK_SMOOTHING = false;

	// Token: 0x04000892 RID: 2194
	public const int DEFAULT_SETTINGS_NETWORK_SMOOTHING_STRENGTH = 1;

	// Token: 0x04000893 RID: 2195
	public const int DEFAULT_SETTINGS_MAX_MATCHMAKING_RTT = 50;

	// Token: 0x04000894 RID: 2196
	public const bool DEFAULT_SETTINGS_FILTER_CHAT_PROFANITY = true;

	// Token: 0x04000895 RID: 2197
	public const Units DEFAULT_SETTINGS_UNITS = Units.Metric;

	// Token: 0x04000896 RID: 2198
	public const bool DEFAULT_SETTINGS_SHOW_GAME_USER_INTERFACE = true;

	// Token: 0x04000897 RID: 2199
	public const float DEFAULT_SETTINGS_USER_INTERFACE_SCALE = 1f;

	// Token: 0x04000898 RID: 2200
	public const float DEFAULT_SETTINGS_CHAT_OPACITY = 1f;

	// Token: 0x04000899 RID: 2201
	public const float DEFAULT_SETTINGS_CHAT_SCALE = 1f;

	// Token: 0x0400089A RID: 2202
	public const float DEFAULT_SETTINGS_MINIMAP_OPACITY = 1f;

	// Token: 0x0400089B RID: 2203
	public const float DEFAULT_SETTINGS_MINIMAP_BACKGROUND_OPACITY = 1f;

	// Token: 0x0400089C RID: 2204
	public const float DEFAULT_SETTINGS_MINIMAP_HORIZONTAL_POSITION = 100f;

	// Token: 0x0400089D RID: 2205
	public const float DEFAULT_SETTINGS_MINIMAP_VERTICAL_POSITION = 0f;

	// Token: 0x0400089E RID: 2206
	public const float DEFAULT_SETTINGS_MINIMAP_SCALE = 1f;

	// Token: 0x0400089F RID: 2207
	public const float DEFAULT_SETTINGS_GLOBAL_STICK_SENSITIVITY = 0.2f;

	// Token: 0x040008A0 RID: 2208
	public const float DEFAULT_SETTINGS_HORIZONTAL_STICK_SENSITIVITY = 1f;

	// Token: 0x040008A1 RID: 2209
	public const float DEFAULT_SETTINGS_VERTICAL_STICK_SENSITIVITY = 1f;

	// Token: 0x040008A2 RID: 2210
	public const float DEFAULT_SETTINGS_LOOK_SENSITIVITY = 0.2f;

	// Token: 0x040008A3 RID: 2211
	public const float DEFAULT_SETTINGS_GLOBAL_VOLUME = 0.5f;

	// Token: 0x040008A4 RID: 2212
	public const float DEFAULT_SETTINGS_AMBIENT_VOLUME = 1f;

	// Token: 0x040008A5 RID: 2213
	public const float DEFAULT_SETTINGS_GAME_VOLUME = 1f;

	// Token: 0x040008A6 RID: 2214
	public const float DEFAULT_SETTINGS_VOICE_VOLUME = 1f;

	// Token: 0x040008A7 RID: 2215
	public const float DEFAULT_SETTINGS_UI_VOLUME = 0.5f;

	// Token: 0x040008A8 RID: 2216
	public const FullScreenMode DEFAULT_SETTINGS_FULL_SCREEN_MODE = FullScreenMode.FullScreenWindow;

	// Token: 0x040008A9 RID: 2217
	public const int DEFAULT_SETTINGS_DISPLAY_INDEX = 0;

	// Token: 0x040008AA RID: 2218
	public const int DEFAULT_SETTINGS_RESOLUTION_INDEX = -1;

	// Token: 0x040008AB RID: 2219
	public const bool DEFAULT_SETTINGS_VSYNC = false;

	// Token: 0x040008AC RID: 2220
	public const int DEFAULT_SETTINGS_FPS_LIMIT = 240;

	// Token: 0x040008AD RID: 2221
	public const float DEFAULT_SETTINGS_FOV = 90f;

	// Token: 0x040008AE RID: 2222
	public const ApplicationQuality DEFAULT_SETTINGS_QUALITY = ApplicationQuality.High;

	// Token: 0x040008AF RID: 2223
	public const bool DEFAULT_SETTINGS_MOTION_BLUR = true;

	// Token: 0x040008B0 RID: 2224
	public const PlayerTeam DEFAULT_TEAM = PlayerTeam.Blue;

	// Token: 0x040008B1 RID: 2225
	public const PlayerRole DEFAULT_ROLE = PlayerRole.Attacker;

	// Token: 0x040008B2 RID: 2226
	public const bool DEFAULT_APPLY_FOR_BOTH_TEAMS = false;

	// Token: 0x040008B3 RID: 2227
	public const int DEFAULT_FLAG_ID = -1;

	// Token: 0x040008B4 RID: 2228
	public const int DEFAULT_HEADGEAR_ID_BLUE_ATTACKER = 513;

	// Token: 0x040008B5 RID: 2229
	public const int DEFAULT_HEADGEAR_ID_RED_ATTACKER = 513;

	// Token: 0x040008B6 RID: 2230
	public const int DEFAULT_HEADGEAR_ID_BLUE_GOALIE = 527;

	// Token: 0x040008B7 RID: 2231
	public const int DEFAULT_HEADGEAR_ID_RED_GOALIE = 527;

	// Token: 0x040008B8 RID: 2232
	public const int DEFAULT_MUSTACHE_ID = -1;

	// Token: 0x040008B9 RID: 2233
	public const int DEFAULT_BEARD_ID = -1;

	// Token: 0x040008BA RID: 2234
	public const int DEFAULT_JERSEY_ID_BLUE_ATTACKER = 2048;

	// Token: 0x040008BB RID: 2235
	public const int DEFAULT_JERSEY_ID_RED_ATTACKER = 2048;

	// Token: 0x040008BC RID: 2236
	public const int DEFAULT_JERSEY_ID_BLUE_GOALIE = 2048;

	// Token: 0x040008BD RID: 2237
	public const int DEFAULT_JERSEY_ID_RED_GOALIE = 2048;

	// Token: 0x040008BE RID: 2238
	public const int DEFAULT_STICK_SKIN_ID_BLUE_ATTACKER = 2621;

	// Token: 0x040008BF RID: 2239
	public const int DEFAULT_STICK_SKIN_ID_RED_ATTACKER = 2621;

	// Token: 0x040008C0 RID: 2240
	public const int DEFAULT_STICK_SKIN_ID_BLUE_GOALIE = 2621;

	// Token: 0x040008C1 RID: 2241
	public const int DEFAULT_STICK_SKIN_ID_RED_GOALIE = 2621;

	// Token: 0x040008C2 RID: 2242
	public const int DEFAULT_STICK_SHAFT_TAPE_ID_BLUE_ATTACKER = -1;

	// Token: 0x040008C3 RID: 2243
	public const int DEFAULT_STICK_SHAFT_TAPE_ID_RED_ATTACKER = -1;

	// Token: 0x040008C4 RID: 2244
	public const int DEFAULT_STICK_SHAFT_TAPE_ID_BLUE_GOALIE = -1;

	// Token: 0x040008C5 RID: 2245
	public const int DEFAULT_STICK_SHAFT_TAPE_ID_RED_GOALIE = -1;

	// Token: 0x040008C6 RID: 2246
	public const int DEFAULT_STICK_BLADE_TAPE_ID_BLUE_ATTACKER = -1;

	// Token: 0x040008C7 RID: 2247
	public const int DEFAULT_STICK_BLADE_TAPE_ID_RED_ATTACKER = -1;

	// Token: 0x040008C8 RID: 2248
	public const int DEFAULT_STICK_BLADE_TAPE_ID_BLUE_GOALIE = -1;

	// Token: 0x040008C9 RID: 2249
	public const int DEFAULT_STICK_BLADE_TAPE_ID_RED_GOALIE = -1;

	// Token: 0x040008CA RID: 2250
	public const ushort DEFAULT_SERVER_PORT = 30609;

	// Token: 0x040008CB RID: 2251
	public const string DEFAULT_SERVER_NAME = "MY PUCK SERVER";

	// Token: 0x040008CC RID: 2252
	public const ushort DEFAULT_SERVER_MAX_PLAYERS = 12;

	// Token: 0x040008CD RID: 2253
	public const string DEFAULT_SERVER_PASSWORD = null;

	// Token: 0x040008CE RID: 2254
	public const ushort DEFAULT_SERVER_TICK_RATE = 200;

	// Token: 0x040008CF RID: 2255
	public const bool DEFAULT_SERVER_IS_PUBLIC = true;

	// Token: 0x040008D0 RID: 2256
	public const bool DEFAULT_SERVER_USE_VOIP = false;

	// Token: 0x040008D1 RID: 2257
	public const bool DEFAULT_SERVER_USE_WHITELIST = false;

	// Token: 0x040008D2 RID: 2258
	public static readonly ModConfig[] DEFAULT_SERVER_MODS = new ModConfig[0];

	// Token: 0x040008D3 RID: 2259
	public const string DEFAULT_SERVER_GAME_MODE = "public";

	// Token: 0x040008D4 RID: 2260
	public const string DEFAULT_SERVER_LEVEL = "default";

	// Token: 0x040008D5 RID: 2261
	public const float KICK_TIMEOUT = 60f;

	// Token: 0x040008D6 RID: 2262
	public static readonly string[] CHAT_BLACKLIST = new string[]
	{
		"卐",
		"卍",
		"☭",
		"⛧"
	};

	// Token: 0x040008D7 RID: 2263
	public static readonly string[] CHAT_WHITELIST = new string[]
	{
		"❤️",
		"\ud83d\ude2d",
		"\ud83d\udd25",
		"\ud83d\udcaf"
	};

	// Token: 0x040008D8 RID: 2264
	public const float INPUT_DEADZONE = 0.05f;

	// Token: 0x040008D9 RID: 2265
	public const float SPRINT_STAMINA_THRESHOLD = 0.25f;

	// Token: 0x040008DA RID: 2266
	public const float MATCH_JOIN_TIMEOUT = 60f;

	// Token: 0x040008DB RID: 2267
	public const float MATCH_ABANDONMENT_TIMEOUT = 120f;
}
