using System;
using Unity.Netcode;

// Token: 0x02000061 RID: 97
public class PuckController : NetworkBehaviour
{
	// Token: 0x06000343 RID: 835 RVA: 0x0000AFDC File Offset: 0x000091DC
	private void Awake()
	{
		this.puck = base.GetComponent<Puck>();
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0000AFEA File Offset: 0x000091EA
	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0000AFF2 File Offset: 0x000091F2
	public override void OnNetworkDespawn()
	{
		base.OnNetworkDespawn();
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0000AFFA File Offset: 0x000091FA
	protected internal override string __getTypeName()
	{
		return "PuckController";
	}

	// Token: 0x0400024F RID: 591
	private Puck puck;
}
