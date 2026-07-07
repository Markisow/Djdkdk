using System;
using System.Collections.Generic;
using Steamworks;
using Unity.Netcode;

// Token: 0x02000042 RID: 66
public class PlayerVoiceRecorderController : NetworkBehaviour
{
	// Token: 0x0600020C RID: 524 RVA: 0x00009EA3 File Offset: 0x000080A3
	private void Awake()
	{
		this.playerVoiceRecorder = base.GetComponent<PlayerVoiceRecorder>();
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00009EB1 File Offset: 0x000080B1
	private void Start()
	{
		this.playerVoiceRecorder.IsEnabled = NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value.UseVoip;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00009ED2 File Offset: 0x000080D2
	public override void OnNetworkSpawn()
	{
		EventManager.AddEventListener("Event_Everyone_OnPlayerTalkInput", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerTalkInput));
		EventManager.AddEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnServerChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00009F06 File Offset: 0x00008106
	public override void OnNetworkDespawn()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerTalkInput", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerTalkInput));
		EventManager.RemoveEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnServerChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0001F944 File Offset: 0x0001DB44
	private void Event_Everyone_OnPlayerTalkInput(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		bool flag = (bool)message["value"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		if (!player.IsLocalPlayer)
		{
			return;
		}
		if (flag)
		{
			this.playerVoiceRecorder.Client_RequestVoiceStartRpc(SteamUser.GetVoiceOptimalSampleRate(), default(RpcParams));
			return;
		}
		this.playerVoiceRecorder.Client_RequestVoiceStopRpc(default(RpcParams));
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00009EB1 File Offset: 0x000080B1
	private void Event_Everyone_OnServerChanged(Dictionary<string, object> message)
	{
		this.playerVoiceRecorder.IsEnabled = NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value.UseVoip;
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00009F3A File Offset: 0x0000813A
	protected internal override string __getTypeName()
	{
		return "PlayerVoiceRecorderController";
	}

	// Token: 0x04000159 RID: 345
	private PlayerVoiceRecorder playerVoiceRecorder;
}
