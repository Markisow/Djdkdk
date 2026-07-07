using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x02000054 RID: 84
public class StickController : NetworkBehaviour
{
	// Token: 0x060002BF RID: 703 RVA: 0x0000A915 File Offset: 0x00008B15
	private void Awake()
	{
		this.stick = base.GetComponent<Stick>();
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0000A923 File Offset: 0x00008B23
	public override void OnNetworkSpawn()
	{
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerCustomizationStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerCustomizationStateChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000A957 File Offset: 0x00008B57
	public override void OnNetworkDespawn()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerCustomizationStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerCustomizationStateChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00022D4C File Offset: 0x00020F4C
	private void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerGameState playerGameState = (PlayerGameState)message["oldGameState"];
		PlayerGameState playerGameState2 = (PlayerGameState)message["newGameState"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		if (playerGameState.Team == playerGameState2.Team && playerGameState.Role == playerGameState2.Role)
		{
			return;
		}
		this.stick.ApplyCustomizations();
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00022DC4 File Offset: 0x00020FC4
	private void Event_Everyone_OnPlayerCustomizationStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId == player.OwnerClientId)
		{
			this.stick.ApplyCustomizations();
		}
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000A98B File Offset: 0x00008B8B
	protected internal override string __getTypeName()
	{
		return "StickController";
	}

	// Token: 0x040001E8 RID: 488
	private Stick stick;
}
