using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000149 RID: 329
public class SynchronizedObjectManager : NetworkBehaviourSingleton<SynchronizedObjectManager>
{
	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060009DD RID: 2525 RVA: 0x00010437 File Offset: 0x0000E637
	// (set) Token: 0x060009DE RID: 2526 RVA: 0x0003A998 File Offset: 0x00038B98
	[HideInInspector]
	public int TickRate
	{
		get
		{
			return this.tickRate;
		}
		set
		{
			if (this.tickRate == value)
			{
				return;
			}
			this.driftEma = new ExponentialMovingAverage(value * this.snapshotInterpolationSettings.driftEmaDuration);
			this.deliveryTimeEma = new ExponentialMovingAverage(value * this.snapshotInterpolationSettings.deliveryTimeEmaDuration);
			this.tickRate = value;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060009DF RID: 2527 RVA: 0x0001043F File Offset: 0x0000E63F
	// (set) Token: 0x060009E0 RID: 2528 RVA: 0x00010447 File Offset: 0x0000E647
	[HideInInspector]
	public bool UseNetworkSmoothing
	{
		get
		{
			return this.useNetworkSmoothing;
		}
		set
		{
			if (this.useNetworkSmoothing != value)
			{
				this.snapshots.Clear();
			}
			this.useNetworkSmoothing = value;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00010464 File Offset: 0x0000E664
	[HideInInspector]
	public float TickInterval
	{
		get
		{
			return 1f / (float)this.TickRate;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00010473 File Offset: 0x0000E673
	private double clientBufferTime
	{
		get
		{
			return (double)this.TickInterval * this.snapshotInterpolationSettings.bufferTimeMultiplier;
		}
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0003A9E8 File Offset: 0x00038BE8
	public override void Awake()
	{
		base.Awake();
		this.driftEma = new ExponentialMovingAverage(this.TickRate * this.snapshotInterpolationSettings.driftEmaDuration);
		this.deliveryTimeEma = new ExponentialMovingAverage(this.TickRate * this.snapshotInterpolationSettings.deliveryTimeEmaDuration);
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0003AA38 File Offset: 0x00038C38
	private void Update()
	{
		if (!base.IsSpawned)
		{
			return;
		}
		if (NetworkManager.Singleton.IsServer)
		{
			this.serverTickAccumulator += Time.deltaTime * (float)this.TickRate;
			if (this.serverTickAccumulator >= 1f)
			{
				while (this.serverTickAccumulator >= 1f)
				{
					this.serverTickAccumulator -= 1f;
				}
				this.Server_ServerTick();
				return;
			}
		}
		else if (this.UseNetworkSmoothing)
		{
			this.clientAccumulatedDeltaTime += Time.unscaledDeltaTime;
			if (this.snapshots.Count > 0)
			{
				SynchronizedObjectsSnapshot from;
				SynchronizedObjectsSnapshot to;
				double t;
				SnapshotInterpolation.Step<SynchronizedObjectsSnapshot>(this.snapshots, (double)this.clientAccumulatedDeltaTime, ref this.clientLocalTimeline, this.clientLocalTimescale, out from, out to, out t);
				SynchronizedObjectsSnapshot.Interpolate(from, to, t);
				this.clientAccumulatedDeltaTime = 0f;
			}
		}
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0003AB0C File Offset: 0x00038D0C
	public void Dispose()
	{
		this.serverTickAccumulator = 0f;
		this.serverLastSentTickId = 0;
		this.clientLastReceivedTickId = 0;
		this.clientHasReceivedFirstTick = false;
		this.clientAccumulatedDeltaTime = 0f;
		this.snapshots.Clear();
		this.ClearSynchronizedObjects();
		this.ClearSynchronizedClientIds();
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00010488 File Offset: 0x0000E688
	public void AddSynchronizedObject(SynchronizedObject synchronizedObject)
	{
		this.synchronizedObjects.Add(synchronizedObject);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00010496 File Offset: 0x0000E696
	public void RemoveSynchronizedObject(SynchronizedObject synchronizedObject)
	{
		this.synchronizedObjects.Remove(synchronizedObject);
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x000104A5 File Offset: 0x0000E6A5
	private void ClearSynchronizedObjects()
	{
		this.synchronizedObjects.Clear();
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x000104B2 File Offset: 0x0000E6B2
	public void Server_AddSynchronizedClientId(ulong clientId)
	{
		this.synchronizedClientIds.Add(clientId);
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000104C0 File Offset: 0x0000E6C0
	public void Server_RemoveSynchronizedClientId(ulong clientId)
	{
		this.synchronizedClientIds.Remove(clientId);
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x000104CF File Offset: 0x0000E6CF
	private void ClearSynchronizedClientIds()
	{
		this.synchronizedClientIds.Clear();
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0003AB5C File Offset: 0x00038D5C
	private ValueTuple<ushort, short[], short[]> EncodeSynchronizedObject(ulong networkObjectId, Vector3 position, Quaternion rotation)
	{
		short num = (short)(rotation.x * 32767f);
		short num2 = (short)(rotation.y * 32767f);
		short num3 = (short)(rotation.z * 32767f);
		short num4 = (short)(rotation.w * 32767f);
		return new ValueTuple<ushort, short[], short[]>((ushort)networkObjectId, new short[]
		{
			(short)(position.x * 655f),
			(short)(position.y * 655f),
			(short)(position.z * 655f)
		}, new short[]
		{
			num,
			num2,
			num3,
			num4
		});
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0003ABF4 File Offset: 0x00038DF4
	private ValueTuple<ushort, Vector3, Quaternion> DecodeSynchronizedObjectData(SynchronizedObjectData synchronizedObjectData)
	{
		float x = (float)synchronizedObjectData.Rx / 32767f;
		float y = (float)synchronizedObjectData.Ry / 32767f;
		float z = (float)synchronizedObjectData.Rz / 32767f;
		float w = (float)synchronizedObjectData.Rw / 32767f;
		Quaternion item = new Quaternion(x, y, z, w);
		return new ValueTuple<ushort, Vector3, Quaternion>(synchronizedObjectData.NetworkObjectId, new Vector3((float)synchronizedObjectData.X / 655f, (float)synchronizedObjectData.Y / 655f, (float)synchronizedObjectData.Z / 655f), item);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0003AC80 File Offset: 0x00038E80
	private void Server_ServerTick()
	{
		this.serverLastSentTickId += 1;
		if (this.serverLastSentTickId >= 65535)
		{
			this.serverLastSentTickId = 0;
		}
		SynchronizedObjectData[] synchronizedObjectsData = this.Server_GatherSynchronizedObjectData(false);
		this.serverLastSentServerTime = NetworkManager.Singleton.ServerTime.Time;
		this.Server_SynchronizeObjectsRpc(this.serverLastSentTickId, this.serverLastSentServerTime, synchronizedObjectsData, base.RpcTarget.Group<List<ulong>>(this.synchronizedClientIds, RpcTargetUse.Persistent));
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0003ACFC File Offset: 0x00038EFC
	public void Server_ForceSynchronizeClientId(ulong clientId)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		SynchronizedObjectData[] synchronizedObjectsData = this.Server_GatherSynchronizedObjectData(true);
		this.Server_SynchronizeObjectsRpc(this.serverLastSentTickId, this.serverLastSentServerTime, synchronizedObjectsData, base.RpcTarget.Single(clientId, RpcTargetUse.Persistent));
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0003AD44 File Offset: 0x00038F44
	private SynchronizedObjectData[] Server_GatherSynchronizedObjectData(bool forceAllObjects = false)
	{
		List<SynchronizedObjectData> list = new List<SynchronizedObjectData>();
		foreach (SynchronizedObject synchronizedObject in this.synchronizedObjects)
		{
			if (synchronizedObject && (forceAllObjects || synchronizedObject.ShouldSendPosition(this.TickRate) || synchronizedObject.ShouldSendRotation(this.TickRate)))
			{
				ValueTuple<Vector3, Quaternion, ulong> valueTuple = synchronizedObject.OnServerTick((float)(NetworkManager.Singleton.ServerTime.Time - this.serverLastSentServerTime));
				Vector3 item = valueTuple.Item1;
				Quaternion item2 = valueTuple.Item2;
				ulong item3 = valueTuple.Item3;
				ValueTuple<ushort, short[], short[]> valueTuple2 = this.EncodeSynchronizedObject(item3, item, item2);
				ushort item4 = valueTuple2.Item1;
				short[] item5 = valueTuple2.Item2;
				short[] item6 = valueTuple2.Item3;
				list.Add(new SynchronizedObjectData
				{
					NetworkObjectId = item4,
					X = item5[0],
					Y = item5[1],
					Z = item5[2],
					Rx = item6[0],
					Ry = item6[1],
					Rz = item6[2],
					Rw = item6[3]
				});
			}
		}
		return list.ToArray();
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0003AE9C File Offset: 0x0003909C
	[Rpc(SendTo.SpecifiedInParams, InvokePermission = RpcInvokePermission.Server, Delivery = RpcDelivery.Unreliable, DeferLocal = true)]
	private void Server_SynchronizeObjectsRpc(ushort tickId, double serverTime, SynchronizedObjectData[] synchronizedObjectsData, RpcParams rpcParams = default(RpcParams))
	{
		NetworkManager networkManager = base.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			uint rpcMethodId = 1738927239U;
			RpcAttribute.RpcAttributeParams attributeParams = new RpcAttribute.RpcAttributeParams
			{
				InvokePermission = RpcInvokePermission.Server,
				Delivery = RpcDelivery.Unreliable,
				DeferLocal = true
			};
			FastBufferWriter writer = base.__beginSendRpc(rpcMethodId, rpcParams, attributeParams, SendTo.SpecifiedInParams, RpcDelivery.Unreliable);
			BytePacker.WriteValueBitPacked(writer, tickId);
			writer.WriteValueSafe<double>(serverTime, default(FastBufferWriter.ForPrimitives));
			bool flag = synchronizedObjectsData != null;
			writer.WriteValueSafe<bool>(flag, default(FastBufferWriter.ForPrimitives));
			if (flag)
			{
				writer.WriteValueSafe<SynchronizedObjectData>(synchronizedObjectsData, default(FastBufferWriter.ForNetworkSerializable));
			}
			base.__endSendRpc(ref writer, 1738927239U, rpcParams, attributeParams, SendTo.SpecifiedInParams, RpcDelivery.Unreliable);
		}
		if (this.__rpc_exec_stage != NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		this.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
		if (this.skipLateTicks && this.clientHasReceivedFirstTick && this.clientLastReceivedTickId - tickId < 32767 && tickId <= this.clientLastReceivedTickId)
		{
			SynchronizedObjectManager.Logger.Info(string.Format("Dropped tick {0} because it's older than the last received tick {1}", tickId, this.clientLastReceivedTickId));
			return;
		}
		float num = (float)(serverTime - this.clientLastReceivedServerTime);
		this.Client_SynchronizeObjects(synchronizedObjectsData, num, serverTime);
		if (!this.clientHasReceivedFirstTick)
		{
			this.clientHasReceivedFirstTick = true;
		}
		this.clientLastReceivedTickId = tickId;
		this.clientLastReceivedServerTime = serverTime;
		EventManager.TriggerEvent("Event_OnSynchronizeObjects", new Dictionary<string, object>
		{
			{
				"serverDeltaTime",
				num
			}
		});
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0003B094 File Offset: 0x00039294
	private void Client_SynchronizeObjects(SynchronizedObjectData[] synchronizedObjectsData, float serverDeltaTime, double serverTime)
	{
		if (this.UseNetworkSmoothing && this.clientHasReceivedFirstTick)
		{
			List<SynchronizedObjectSnapshot> list = new List<SynchronizedObjectSnapshot>();
			for (int i = 0; i < synchronizedObjectsData.Length; i++)
			{
				SynchronizedObjectData synchronizedObjectData = synchronizedObjectsData[i];
				ValueTuple<ushort, Vector3, Quaternion> valueTuple = this.DecodeSynchronizedObjectData(synchronizedObjectData);
				ushort networkObjectId = valueTuple.Item1;
				Vector3 item = valueTuple.Item2;
				Quaternion item2 = valueTuple.Item3;
				SynchronizedObject synchronizedObject3 = this.synchronizedObjects.Find((SynchronizedObject synchronizedObject) => synchronizedObject.NetworkObjectId == (ulong)networkObjectId);
				if (!(synchronizedObject3 == null))
				{
					SynchronizedObjectSnapshot item3 = synchronizedObject3.OnClientSmoothTick(item, item2, synchronizedObject3, serverDeltaTime);
					list.Add(item3);
				}
			}
			SynchronizedObjectsSnapshot snapshot = new SynchronizedObjectsSnapshot(serverTime, NetworkManager.Singleton.LocalTime.Time, list);
			if (this.snapshotInterpolationSettings.dynamicAdjustment)
			{
				this.snapshotInterpolationSettings.bufferTimeMultiplier = SnapshotInterpolation.DynamicAdjustment((double)this.TickInterval, this.deliveryTimeEma.StandardDeviation, (double)this.snapshotInterpolationSettings.dynamicAdjustmentTolerance) * (double)this.NetworkSmoothingStrength;
			}
			SnapshotInterpolation.InsertAndAdjust<SynchronizedObjectsSnapshot>(this.snapshots, this.snapshotInterpolationSettings.bufferLimit, snapshot, ref this.clientLocalTimeline, ref this.clientLocalTimescale, this.TickInterval, this.clientBufferTime, this.snapshotInterpolationSettings.catchupSpeed, this.snapshotInterpolationSettings.slowdownSpeed, ref this.driftEma, this.snapshotInterpolationSettings.catchupNegativeThreshold, this.snapshotInterpolationSettings.catchupPositiveThreshold, ref this.deliveryTimeEma);
			return;
		}
		for (int i = 0; i < synchronizedObjectsData.Length; i++)
		{
			SynchronizedObjectData synchronizedObjectData2 = synchronizedObjectsData[i];
			ValueTuple<ushort, Vector3, Quaternion> valueTuple = this.DecodeSynchronizedObjectData(synchronizedObjectData2);
			ushort networkObjectId = valueTuple.Item1;
			Vector3 item4 = valueTuple.Item2;
			Quaternion item5 = valueTuple.Item3;
			SynchronizedObject synchronizedObject2 = this.synchronizedObjects.Find((SynchronizedObject synchronizedObject) => synchronizedObject.NetworkObjectId == (ulong)networkObjectId);
			if (!(synchronizedObject2 == null))
			{
				synchronizedObject2.OnClientTick(item4, item5, serverDeltaTime);
			}
		}
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0003B284 File Offset: 0x00039484
	private void OnValidate()
	{
		this.snapshotInterpolationSettings.catchupNegativeThreshold = Mathf.Min(this.snapshotInterpolationSettings.catchupNegativeThreshold, 0f);
		this.snapshotInterpolationSettings.catchupPositiveThreshold = Mathf.Max(this.snapshotInterpolationSettings.catchupPositiveThreshold, 0f);
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0003B330 File Offset: 0x00039530
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x000104ED File Offset: 0x0000E6ED
	protected override void __initializeRpcs()
	{
		base.__registerRpc(1738927239U, new NetworkBehaviour.RpcReceiveHandler(SynchronizedObjectManager.__rpc_handler_1738927239), "Server_SynchronizeObjectsRpc", RpcInvokePermission.Server);
		base.__initializeRpcs();
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0003B348 File Offset: 0x00039548
	private static void __rpc_handler_1738927239(NetworkBehaviour target, FastBufferReader reader, __RpcParams rpcParams)
	{
		NetworkManager networkManager = target.NetworkManager;
		if (networkManager == null || !networkManager.IsListening)
		{
			return;
		}
		ushort tickId;
		ByteUnpacker.ReadValueBitPacked(reader, out tickId);
		double serverTime;
		reader.ReadValueSafe<double>(out serverTime, default(FastBufferWriter.ForPrimitives));
		bool flag;
		reader.ReadValueSafe<bool>(out flag, default(FastBufferWriter.ForPrimitives));
		SynchronizedObjectData[] synchronizedObjectsData = null;
		if (flag)
		{
			reader.ReadValueSafe<SynchronizedObjectData>(out synchronizedObjectsData, default(FastBufferWriter.ForNetworkSerializable));
		}
		RpcParams ext = rpcParams.Ext;
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Execute;
		((SynchronizedObjectManager)target).Server_SynchronizeObjectsRpc(tickId, serverTime, synchronizedObjectsData, ext);
		target.__rpc_exec_stage = NetworkBehaviour.__RpcExecStage.Send;
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x00010518 File Offset: 0x0000E718
	protected internal override string __getTypeName()
	{
		return "SynchronizedObjectManager";
	}

	// Token: 0x040005C0 RID: 1472
	private static readonly global::Logger Logger = new global::Logger("SynchronizedObjectManager");

	// Token: 0x040005C1 RID: 1473
	[Header("Settings")]
	[SerializeField]
	private SnapshotInterpolationSettings snapshotInterpolationSettings;

	// Token: 0x040005C2 RID: 1474
	[SerializeField]
	private bool skipLateTicks = true;

	// Token: 0x040005C3 RID: 1475
	private int tickRate = 100;

	// Token: 0x040005C4 RID: 1476
	private bool useNetworkSmoothing;

	// Token: 0x040005C5 RID: 1477
	[HideInInspector]
	public int NetworkSmoothingStrength = 1;

	// Token: 0x040005C6 RID: 1478
	private float serverTickAccumulator;

	// Token: 0x040005C7 RID: 1479
	private ushort serverLastSentTickId;

	// Token: 0x040005C8 RID: 1480
	private double serverLastSentServerTime;

	// Token: 0x040005C9 RID: 1481
	private ushort clientLastReceivedTickId;

	// Token: 0x040005CA RID: 1482
	private double clientLastReceivedServerTime;

	// Token: 0x040005CB RID: 1483
	private bool clientHasReceivedFirstTick;

	// Token: 0x040005CC RID: 1484
	private float clientAccumulatedDeltaTime;

	// Token: 0x040005CD RID: 1485
	private double clientLocalTimeline;

	// Token: 0x040005CE RID: 1486
	private double clientLocalTimescale = 1.0;

	// Token: 0x040005CF RID: 1487
	private List<SynchronizedObject> synchronizedObjects = new List<SynchronizedObject>();

	// Token: 0x040005D0 RID: 1488
	private List<ulong> synchronizedClientIds = new List<ulong>();

	// Token: 0x040005D1 RID: 1489
	private SortedList<double, SynchronizedObjectsSnapshot> snapshots = new SortedList<double, SynchronizedObjectsSnapshot>();

	// Token: 0x040005D2 RID: 1490
	private ExponentialMovingAverage driftEma;

	// Token: 0x040005D3 RID: 1491
	private ExponentialMovingAverage deliveryTimeEma;
}
