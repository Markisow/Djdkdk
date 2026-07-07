using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x02000056 RID: 86
public class StickPositionerController : NetworkBehaviour
{
	// Token: 0x060002E8 RID: 744 RVA: 0x0000AB6E File Offset: 0x00008D6E
	private void Awake()
	{
		this.stickPositioner = base.GetComponent<StickPositioner>();
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000AB7C File Offset: 0x00008D7C
	public override void OnNetworkSpawn()
	{
		EventManager.AddEventListener("Event_Everyone_OnStickSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnStickSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerHandednessChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerHandednessChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000ABB0 File Offset: 0x00008DB0
	public override void OnNetworkDespawn()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnStickSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnStickSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerHandednessChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerHandednessChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00023A7C File Offset: 0x00021C7C
	private void Event_Everyone_OnStickSpawned(Dictionary<string, object> message)
	{
		Stick stick = (Stick)message["stick"];
		if (base.OwnerClientId == stick.OwnerClientId)
		{
			this.stickPositioner.PrepareShaftTarget(stick);
		}
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00023AB4 File Offset: 0x00021CB4
	private void Event_Everyone_OnPlayerHandednessChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId == player.OwnerClientId)
		{
			this.stickPositioner.Handedness = player.Handedness.Value;
		}
	}

	// Token: 0x060002EE RID: 750 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000ABE4 File Offset: 0x00008DE4
	protected internal override string __getTypeName()
	{
		return "StickPositionerController";
	}

	// Token: 0x04000215 RID: 533
	private StickPositioner stickPositioner;
}
