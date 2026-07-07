using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x0200005C RID: 92
public class PuckPosition : NetworkBehaviour
{
	// Token: 0x0600030E RID: 782 RVA: 0x0000AD65 File Offset: 0x00008F65
	protected override void OnNetworkPostSpawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnPuckPositionSpawned", new Dictionary<string, object>
		{
			{
				"puckPosition",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0000AD88 File Offset: 0x00008F88
	public override void OnNetworkDespawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnPuckPositionDespawned", new Dictionary<string, object>
		{
			{
				"puckPosition",
				this
			}
		});
		base.OnNetworkDespawn();
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0000ADAB File Offset: 0x00008FAB
	protected internal override string __getTypeName()
	{
		return "PuckPosition";
	}

	// Token: 0x04000225 RID: 549
	public GamePhase Phase;
}
