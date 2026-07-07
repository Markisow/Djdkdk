using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Netcode;

// Token: 0x020001DC RID: 476
public class CompressedNetworkVariable<TRaw, TNetwork> : NetworkVariable<!1> where TRaw : struct where TNetwork : struct
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000E55 RID: 3669 RVA: 0x0004B9AC File Offset: 0x00049BAC
	// (remove) Token: 0x06000E56 RID: 3670 RVA: 0x0004B9E4 File Offset: 0x00049BE4
	public event Action<!0, !0> OnRawValueChanged
	{
		[CompilerGenerated]
		add
		{
			Action<TRaw, TRaw> action = this.OnRawValueChanged;
			Action<TRaw, TRaw> action2;
			do
			{
				action2 = action;
				Action<TRaw, TRaw> value2 = (Action<!0, !0>)Delegate.Combine(action2, value);
				action = Interlocked.CompareExchange<Action<TRaw, TRaw>>(ref this.OnRawValueChanged, value2, action2);
			}
			while (action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<TRaw, TRaw> action = this.OnRawValueChanged;
			Action<TRaw, TRaw> action2;
			do
			{
				action2 = action;
				Action<TRaw, TRaw> value2 = (Action<!0, !0>)Delegate.Remove(action2, value);
				action = Interlocked.CompareExchange<Action<TRaw, TRaw>>(ref this.OnRawValueChanged, value2, action2);
			}
			while (action != action2);
		}
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x0004BA1C File Offset: 0x00049C1C
	public CompressedNetworkVariable(Func<TRaw, TNetwork> compressor, Func<TNetwork, TRaw> decompressor, TRaw initialValue = default(TRaw), NetworkVariableReadPermission readPerm = NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission writePerm = NetworkVariableWritePermission.Server) : base(compressor(initialValue), readPerm, writePerm)
	{
		this.compressor = compressor;
		this.decompressor = decompressor;
		this.cachedValue = initialValue;
		this.OnValueChanged = (NetworkVariable<!1>.OnValueChangedDelegate)Delegate.Combine(this.OnValueChanged, new NetworkVariable<!1>.OnValueChangedDelegate(this.OnCompressedValueChanged));
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00013D3F File Offset: 0x00011F3F
	// (set) Token: 0x06000E59 RID: 3673 RVA: 0x0004BA74 File Offset: 0x00049C74
	public new TRaw Value
	{
		get
		{
			return this.cachedValue;
		}
		set
		{
			TRaw traw = this.cachedValue;
			this.cachedValue = value;
			base.Value = this.compressor(value);
			if (!this.cachedValue.Equals(traw))
			{
				Action<!0, !0> onRawValueChanged = this.OnRawValueChanged;
				if (onRawValueChanged == null)
				{
					return;
				}
				onRawValueChanged(traw, this.cachedValue);
			}
		}
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x0004BAD4 File Offset: 0x00049CD4
	private void OnCompressedValueChanged(TNetwork previousCompressed, TNetwork newCompressed)
	{
		TRaw arg = this.cachedValue;
		this.cachedValue = this.decompressor(newCompressed);
		Action<!0, !0> onRawValueChanged = this.OnRawValueChanged;
		if (onRawValueChanged == null)
		{
			return;
		}
		onRawValueChanged(arg, this.cachedValue);
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x0004BB14 File Offset: 0x00049D14
	public static CompressedNetworkVariable<float, short> CreateFloatToShort(float minValue, float maxValue, float initialValue = 0f, NetworkVariableReadPermission readPerm = NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission writePerm = NetworkVariableWritePermission.Server)
	{
		return new CompressedNetworkVariable<float, short>(delegate(float value)
		{
			float val = (value - minValue) / (maxValue - minValue);
			return (short)(Math.Max(0f, Math.Min(1f, val)) * 32767f);
		}, delegate(short compressed)
		{
			float num = (float)compressed / 32767f;
			return minValue + num * (maxValue - minValue);
		}, initialValue, readPerm, writePerm);
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x0004BB58 File Offset: 0x00049D58
	public static CompressedNetworkVariable<float, byte> CreateFloatToByte(float minValue, float maxValue, float initialValue = 0f, NetworkVariableReadPermission readPerm = NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission writePerm = NetworkVariableWritePermission.Server)
	{
		return new CompressedNetworkVariable<float, byte>(delegate(float value)
		{
			float val = (value - minValue) / (maxValue - minValue);
			return (byte)(Math.Max(0f, Math.Min(1f, val)) * 255f);
		}, delegate(byte compressed)
		{
			float num = (float)compressed / 255f;
			return minValue + num * (maxValue - minValue);
		}, initialValue, readPerm, writePerm);
	}

	// Token: 0x04000871 RID: 2161
	private readonly Func<TRaw, TNetwork> compressor;

	// Token: 0x04000872 RID: 2162
	private readonly Func<TNetwork, TRaw> decompressor;

	// Token: 0x04000873 RID: 2163
	private TRaw cachedValue;
}
