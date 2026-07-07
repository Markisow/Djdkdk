using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x02000040 RID: 64
public class PlayerInputController : NetworkBehaviour
{
	// Token: 0x060001E7 RID: 487 RVA: 0x00009DDA File Offset: 0x00007FDA
	private void Awake()
	{
		this.playerInput = base.GetComponent<PlayerInput>();
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00009DE8 File Offset: 0x00007FE8
	private void Start()
	{
		this.playerInput.InitialLookAngle = SettingsManager.CameraAngle;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0001EB60 File Offset: 0x0001CD60
	public override void OnNetworkSpawn()
	{
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerHandednessChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerHandednessChanged));
		EventManager.AddEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnServerChanged));
		EventManager.AddEventListener("Event_Server_OnClientSceneSynchronizeComplete", new Action<Dictionary<string, object>>(this.Event_Server_OnClientSceneSynchronizeComplete));
		EventManager.AddEventListener("Event_OnCameraAngleChanged", new Action<Dictionary<string, object>>(this.Event_OnCameraAngleChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
	public override void OnNetworkDespawn()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerHandednessChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerHandednessChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnServerChanged));
		EventManager.RemoveEventListener("Event_Server_OnClientSceneSynchronizeComplete", new Action<Dictionary<string, object>>(this.Event_Server_OnClientSceneSynchronizeComplete));
		EventManager.RemoveEventListener("Event_OnCameraAngleChanged", new Action<Dictionary<string, object>>(this.Event_OnCameraAngleChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0001EC68 File Offset: 0x0001CE68
	private void Event_Everyone_OnPlayerBodySpawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		if (playerBody.Player.IsLocalPlayer)
		{
			this.playerInput.ResetInputs(playerBody.Player.Handedness.Value);
		}
	}

	// Token: 0x060001EC RID: 492 RVA: 0x0001ECB0 File Offset: 0x0001CEB0
	private void Event_Everyone_OnPlayerHandednessChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsLocalPlayer)
		{
			this.playerInput.ResetInputs(player.Handedness.Value);
		}
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00009DFA File Offset: 0x00007FFA
	private void Event_Everyone_OnServerChanged(Dictionary<string, object> message)
	{
		this.playerInput.TickRate = NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value.TickRate;
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0001ECEC File Offset: 0x0001CEEC
	private void Event_Server_OnClientSceneSynchronizeComplete(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		if (num == 0UL)
		{
			return;
		}
		this.playerInput.Server_ForceSynchronizeClientId(num);
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0001ED1C File Offset: 0x0001CF1C
	private void Event_OnCameraAngleChanged(Dictionary<string, object> message)
	{
		float initialLookAngle = (float)message["value"];
		this.playerInput.InitialLookAngle = initialLookAngle;
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00009E1B File Offset: 0x0000801B
	protected internal override string __getTypeName()
	{
		return "PlayerInputController";
	}

	// Token: 0x04000152 RID: 338
	private PlayerInput playerInput;
}
