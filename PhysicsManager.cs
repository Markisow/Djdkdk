using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class PhysicsManager : MonoBehaviourSingleton<PhysicsManager>
{
	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x060006FF RID: 1791 RVA: 0x0000D6F1 File Offset: 0x0000B8F1
	[HideInInspector]
	public int TickRate
	{
		get
		{
			return this.tickRate;
		}
	}

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000D6F9 File Offset: 0x0000B8F9
	[HideInInspector]
	public float TickInterval
	{
		get
		{
			return 1f / (float)this.TickRate;
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0000D708 File Offset: 0x0000B908
	public override void Awake()
	{
		base.Awake();
		Physics.simulationMode = this.simulationMode;
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x000316E0 File Offset: 0x0002F8E0
	private void Update()
	{
		if (this.simulationMode != SimulationMode.Script)
		{
			return;
		}
		this.tickAccumulator += Time.deltaTime;
		if (this.tickAccumulator >= this.TickInterval)
		{
			Time.fixedDeltaTime = this.TickInterval;
			Physics.Simulate(Time.fixedDeltaTime);
			this.tickAccumulator -= this.TickInterval;
		}
	}

	// Token: 0x04000444 RID: 1092
	[Header("Settings")]
	[SerializeField]
	private SimulationMode simulationMode = SimulationMode.Script;

	// Token: 0x04000445 RID: 1093
	[SerializeField]
	private int tickRate = 50;

	// Token: 0x04000446 RID: 1094
	private float tickAccumulator;
}
