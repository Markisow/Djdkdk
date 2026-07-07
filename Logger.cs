using System;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public readonly struct Logger
{
	// Token: 0x06000E7C RID: 3708 RVA: 0x00013E04 File Offset: 0x00012004
	private static string GetColor(LogType type)
	{
		if (Application.isEditor)
		{
			return "<color=white>";
		}
		return "\u001b[37m";
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x0000CD81 File Offset: 0x0000AF81
	private static string GetReset()
	{
		if (Application.isEditor)
		{
			return "</color>";
		}
		return "\u001b[0m";
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0004BF60 File Offset: 0x0004A160
	public Logger(string tag)
	{
		string color = global::Logger.GetColor(LogType.Log);
		string reset = global::Logger.GetReset();
		this.prefix = string.Concat(new string[]
		{
			color,
			"[",
			tag,
			"]",
			reset,
			" "
		});
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x00013E18 File Offset: 0x00012018
	[HideInCallstack]
	public void Info(string message)
	{
		Debug.Log(this.prefix + message);
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x00013E2B File Offset: 0x0001202B
	[HideInCallstack]
	public void Warning(string message)
	{
		Debug.LogWarning(this.prefix + message);
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x00013E3E File Offset: 0x0001203E
	[HideInCallstack]
	public void Error(string message)
	{
		Debug.LogError(this.prefix + message);
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x00013E51 File Offset: 0x00012051
	[HideInCallstack]
	public void Exception(Exception exception)
	{
		Debug.LogException(exception);
	}

	// Token: 0x040008E5 RID: 2277
	private readonly string prefix;
}
