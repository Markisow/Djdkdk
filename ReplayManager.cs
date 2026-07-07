using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class ReplayManager : MonoBehaviourSingleton<ReplayManager>
{
	// Token: 0x06000762 RID: 1890 RVA: 0x0000DB1A File Offset: 0x0000BD1A
	public override void Awake()
	{
		base.Awake();
		this.ReplayRecorder = base.GetComponent<ReplayRecorder>();
		this.ReplayPlayer = base.GetComponent<ReplayPlayer>();
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0000DB3A File Offset: 0x0000BD3A
	public void Server_StartRecording()
	{
		this.ReplayRecorder.Server_StartRecording(this.tickRate);
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0000DB4D File Offset: 0x0000BD4D
	public void Server_StopRecording()
	{
		this.ReplayRecorder.Server_StopRecording();
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x00032214 File Offset: 0x00030414
	public void Server_StartReplaying(float secondsToReplay)
	{
		SortedList<int, List<ValueTuple<string, object>>> sortedList = new SortedList<int, List<ValueTuple<string, object>>>(this.ReplayRecorder.EventMap);
		if (sortedList.Count == 0)
		{
			return;
		}
		int num = sortedList.Keys.Max();
		int num2 = (int)((float)this.tickRate * secondsToReplay);
		int fromTick = num - num2;
		this.ReplayPlayer.Server_StartReplay(sortedList, this.tickRate, fromTick);
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x0000DB5A File Offset: 0x0000BD5A
	public void Server_StopReplaying()
	{
		this.ReplayPlayer.Server_StopReplay();
	}

	// Token: 0x0400049B RID: 1179
	private static readonly global::Logger Logger = new global::Logger("ReplayManager");

	// Token: 0x0400049C RID: 1180
	[Header("Settings")]
	[SerializeField]
	private int tickRate = 15;

	// Token: 0x0400049D RID: 1181
	public ReplayRecorder ReplayRecorder;

	// Token: 0x0400049E RID: 1182
	public ReplayPlayer ReplayPlayer;
}
