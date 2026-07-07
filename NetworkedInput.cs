using System;
using UnityEngine;

// Token: 0x020001EA RID: 490
public class NetworkedInput<T>
{
	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00013E93 File Offset: 0x00012093
	public bool HasChanged
	{
		get
		{
			return this.HasChangedValidator(this.LastSentValue, this.ClientValue);
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00013EAC File Offset: 0x000120AC
	public bool ShouldChange
	{
		get
		{
			return this.ShouldChangeValidator(this.LastReceivedValue, this.LastReceivedTime, this.ServerValue);
		}
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x0004C22C File Offset: 0x0004A42C
	public NetworkedInput(T initialValue = default(T), NetworkedInput<T>.HasChangedDelegate hasChangedValidator = null, NetworkedInput<T>.ShouldChangeDelegate shouldChangeValidator = null)
	{
		this.ClientValue = initialValue;
		this.LastSentValue = default(!0);
		this.ServerValue = default(!0);
		if (hasChangedValidator != null)
		{
			this.HasChangedValidator = hasChangedValidator;
		}
		else
		{
			this.HasChangedValidator = ((T lastSentValue, T clientValue) => !this.ClientValue.Equals(this.LastSentValue));
		}
		if (shouldChangeValidator != null)
		{
			this.ShouldChangeValidator = shouldChangeValidator;
			return;
		}
		this.ShouldChangeValidator = ((T lastReceivedValue, double lastReceivedTime, T serverValue) => !this.ServerValue.Equals(this.LastReceivedValue));
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00013ECB File Offset: 0x000120CB
	public void ClientTick()
	{
		this.LastSentValue = this.ClientValue;
		this.LastSentTime = Time.timeAsDouble;
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x00013EE4 File Offset: 0x000120E4
	public void ServerTick()
	{
		this.LastReceivedValue = this.ServerValue;
		this.LastReceivedTime = Time.timeAsDouble;
	}

	// Token: 0x040008EB RID: 2283
	public T ClientValue;

	// Token: 0x040008EC RID: 2284
	public T ServerValue;

	// Token: 0x040008ED RID: 2285
	public T LastSentValue;

	// Token: 0x040008EE RID: 2286
	public double LastSentTime;

	// Token: 0x040008EF RID: 2287
	public T LastReceivedValue;

	// Token: 0x040008F0 RID: 2288
	public double LastReceivedTime;

	// Token: 0x040008F1 RID: 2289
	private NetworkedInput<T>.HasChangedDelegate HasChangedValidator;

	// Token: 0x040008F2 RID: 2290
	private NetworkedInput<T>.ShouldChangeDelegate ShouldChangeValidator;

	// Token: 0x020001EB RID: 491
	// (Invoke) Token: 0x06000E9F RID: 3743
	public delegate bool HasChangedDelegate(T LastSentValue, T ClientValue);

	// Token: 0x020001EC RID: 492
	// (Invoke) Token: 0x06000EA3 RID: 3747
	public delegate bool ShouldChangeDelegate(T LastSentValue, double lastReceivedTime, T ClientValue);
}
