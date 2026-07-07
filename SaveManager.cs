using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000115 RID: 277
public static class SaveManager
{
	// Token: 0x0600079C RID: 1948 RVA: 0x0000895D File Offset: 0x00006B5D
	public static void Initialize()
	{
	}

	// Token: 0x0600079D RID: 1949 RVA: 0x0000DCD6 File Offset: 0x0000BED6
	public static void Dispose()
	{
		Tween tween = SaveManager.saveDebounceTween;
		if (tween == null)
		{
			return;
		}
		tween.Kill(false);
	}

	// Token: 0x0600079E RID: 1950 RVA: 0x0000DCE8 File Offset: 0x0000BEE8
	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, value ? 1 : 0);
		SaveManager.Save();
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00033CDC File Offset: 0x00031EDC
	public static bool GetBool(string key, bool defaultValue)
	{
		int defaultValue2 = defaultValue ? 1 : 0;
		return PlayerPrefs.GetInt(key, defaultValue2) == 1;
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x0000DCFC File Offset: 0x0000BEFC
	public static void SetInt(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
		SaveManager.Save();
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x0000DD0A File Offset: 0x0000BF0A
	public static int GetInt(string key, int defaultValue)
	{
		return PlayerPrefs.GetInt(key, defaultValue);
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x0000DD13 File Offset: 0x0000BF13
	public static void SetFloat(string key, float value)
	{
		PlayerPrefs.SetFloat(key, value);
		SaveManager.Save();
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x0000DD21 File Offset: 0x0000BF21
	public static float GetFloat(string key, float defaultValue)
	{
		return PlayerPrefs.GetFloat(key, defaultValue);
	}

	// Token: 0x060007A4 RID: 1956 RVA: 0x0000DD2A File Offset: 0x0000BF2A
	public static void SetString(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
		SaveManager.Save();
	}

	// Token: 0x060007A5 RID: 1957 RVA: 0x0000DD38 File Offset: 0x0000BF38
	public static string GetString(string key, string defaultValue)
	{
		return PlayerPrefs.GetString(key, defaultValue);
	}

	// Token: 0x060007A6 RID: 1958 RVA: 0x0000DD41 File Offset: 0x0000BF41
	public static void SetEnum<T>(string key, T value) where T : Enum
	{
		PlayerPrefs.SetInt(key, Convert.ToInt32(value));
		SaveManager.Save();
	}

	// Token: 0x060007A7 RID: 1959 RVA: 0x00033CFC File Offset: 0x00031EFC
	public static T GetEnum<T>(string key, T defaultValue) where T : Enum
	{
		int defaultValue2 = Convert.ToInt32(defaultValue);
		int @int = PlayerPrefs.GetInt(key, defaultValue2);
		return (!!0)((object)Enum.ToObject(typeof(!!0), @int));
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00033D34 File Offset: 0x00031F34
	private static void Save()
	{
		Tween tween = SaveManager.saveDebounceTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		SaveManager.saveDebounceTween = DOVirtual.DelayedCall(0f, delegate
		{
			PlayerPrefs.Save();
		}, true);
	}

	// Token: 0x040004B6 RID: 1206
	private static Tween saveDebounceTween;
}
