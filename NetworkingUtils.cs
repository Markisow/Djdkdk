using System;
using Unity.Netcode;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public static class NetworkingUtils
{
	// Token: 0x06000E90 RID: 3728 RVA: 0x0004C0FC File Offset: 0x0004A2FC
	public static Player GetPlayerFromNetworkObjectReference(NetworkObjectReference reference)
	{
		NetworkObject networkObject;
		if (reference.TryGet(out networkObject, null))
		{
			return networkObject.GetComponent<Player>();
		}
		return null;
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x0004C120 File Offset: 0x0004A320
	public static PlayerPosition GetPlayerPositionFromNetworkObjectReference(NetworkObjectReference reference)
	{
		NetworkObject networkObject;
		if (reference.TryGet(out networkObject, null))
		{
			return networkObject.GetComponent<PlayerPosition>();
		}
		return null;
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x0004C144 File Offset: 0x0004A344
	public static Puck GetPuckFromNetworkObjectReference(NetworkObjectReference reference)
	{
		NetworkObject networkObject;
		if (reference.TryGet(out networkObject, null))
		{
			return networkObject.GetComponent<Puck>();
		}
		return null;
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x0004C168 File Offset: 0x0004A368
	public static byte CompressFloatToByte(float value, float minValue, float maxValue)
	{
		int num = -128;
		int num2 = 127;
		float t = Mathf.InverseLerp(minValue, maxValue, value);
		return (byte)((sbyte)Mathf.Clamp(Mathf.RoundToInt(Mathf.Lerp((float)num, (float)num2, t)), num, num2));
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x0004C19C File Offset: 0x0004A39C
	public static short CompressFloatToShort(float value, float minValue, float maxValue)
	{
		int num = -32768;
		int num2 = 32767;
		float t = Mathf.InverseLerp(minValue, maxValue, value);
		return (short)Mathf.Clamp(Mathf.RoundToInt(Mathf.Lerp((float)num, (float)num2, t)), num, num2);
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x0004C1D8 File Offset: 0x0004A3D8
	public static float DecompressByteToFloat(byte compressedValue, float minValue, float maxValue)
	{
		float num = (float)-128;
		int num2 = 127;
		float t = Mathf.InverseLerp(num, (float)num2, (float)((sbyte)compressedValue));
		return Mathf.Lerp(minValue, maxValue, t);
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x0004C200 File Offset: 0x0004A400
	public static float DecompressShortToFloat(short compressedValue, float minValue, float maxValue)
	{
		float num = (float)-32768;
		int num2 = 32767;
		float t = Mathf.InverseLerp(num, (float)num2, (float)compressedValue);
		return Mathf.Lerp(minValue, maxValue, t);
	}
}
