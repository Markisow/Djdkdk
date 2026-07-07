using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class PlayerBodyController : NetworkBehaviour
{
	// Token: 0x06000157 RID: 343 RVA: 0x00009AEE File Offset: 0x00007CEE
	private void Awake()
	{
		this.playerBody = base.GetComponent<PlayerBody>();
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000199F0 File Offset: 0x00017BF0
	public override void OnNetworkSpawn()
	{
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerUsernameChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerUsernameChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerNumberChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerNumberChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerCustomizationStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerCustomizationStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerVoiceStarted", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerVoiceStarted));
		EventManager.AddEventListener("Event_Everyone_OnPlayerVoiceStopped", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerVoiceStopped));
		EventManager.AddEventListener("Event_Server_OnPlayerJumpInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerJumpInput));
		EventManager.AddEventListener("Event_Server_OnPlayerDashLeftInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerDashLeftInput));
		EventManager.AddEventListener("Event_Server_OnPlayerDashRightInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerDashRightInput));
		EventManager.AddEventListener("Event_Server_OnPlayerTwistLeftInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerTwistLeftInput));
		EventManager.AddEventListener("Event_Server_OnPlayerTwistRightInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerTwistRightInput));
		EventManager.AddEventListener("Event_OnPlayerCameraEnabled", new Action<Dictionary<string, object>>(this.Event_OnPlayerCameraEnabled));
		EventManager.AddEventListener("Event_OnPlayerCameraDisabled", new Action<Dictionary<string, object>>(this.Event_OnPlayerCameraDisabled));
		base.OnNetworkSpawn();
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00019B24 File Offset: 0x00017D24
	public override void OnNetworkDespawn()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerUsernameChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerUsernameChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerNumberChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerNumberChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerCustomizationStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerCustomizationStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerVoiceStarted", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerVoiceStarted));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerVoiceStopped", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerVoiceStopped));
		EventManager.RemoveEventListener("Event_Server_OnPlayerJumpInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerJumpInput));
		EventManager.RemoveEventListener("Event_Server_OnPlayerDashLeftInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerDashLeftInput));
		EventManager.RemoveEventListener("Event_Server_OnPlayerDashRightInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerDashRightInput));
		EventManager.RemoveEventListener("Event_Server_OnPlayerTwistLeftInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerTwistLeftInput));
		EventManager.RemoveEventListener("Event_Server_OnPlayerTwistRightInput", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerTwistRightInput));
		EventManager.RemoveEventListener("Event_OnPlayerCameraEnabled", new Action<Dictionary<string, object>>(this.Event_OnPlayerCameraEnabled));
		EventManager.RemoveEventListener("Event_OnPlayerCameraDisabled", new Action<Dictionary<string, object>>(this.Event_OnPlayerCameraDisabled));
		base.OnNetworkDespawn();
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00019C58 File Offset: 0x00017E58
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
		this.playerBody.ApplyCustomizations();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00019CD0 File Offset: 0x00017ED0
	private void Event_Everyone_OnPlayerUsernameChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId == player.OwnerClientId)
		{
			this.playerBody.ApplyCustomizations();
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00019CD0 File Offset: 0x00017ED0
	private void Event_Everyone_OnPlayerNumberChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId == player.OwnerClientId)
		{
			this.playerBody.ApplyCustomizations();
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x00019CD0 File Offset: 0x00017ED0
	private void Event_Everyone_OnPlayerCustomizationStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId == player.OwnerClientId)
		{
			this.playerBody.ApplyCustomizations();
		}
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00019D08 File Offset: 0x00017F08
	private void Event_Everyone_OnPlayerVoiceStarted(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		AudioClip clip = (AudioClip)message["audioClip"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		if (player.IsLocalPlayer)
		{
			return;
		}
		this.playerBody.VoiceAudioSource.clip = clip;
		this.playerBody.VoiceAudioSource.loop = true;
		this.playerBody.VoiceAudioSource.Play();
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00019D84 File Offset: 0x00017F84
	private void Event_Everyone_OnPlayerVoiceStopped(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		if (player.IsLocalPlayer)
		{
			return;
		}
		this.playerBody.VoiceAudioSource.Stop();
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00019DCC File Offset: 0x00017FCC
	private void Event_Server_OnPlayerJumpInput(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		this.playerBody.Jump();
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00019E04 File Offset: 0x00018004
	private void Event_Server_OnPlayerTwistLeftInput(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		this.playerBody.TwistLeft();
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00019E3C File Offset: 0x0001803C
	private void Event_Server_OnPlayerTwistRightInput(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		this.playerBody.TwistRight();
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00019E74 File Offset: 0x00018074
	private void Event_Server_OnPlayerDashLeftInput(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		this.playerBody.DashLeft();
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00019EAC File Offset: 0x000180AC
	private void Event_Server_OnPlayerDashRightInput(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (base.OwnerClientId != player.OwnerClientId)
		{
			return;
		}
		this.playerBody.DashRight();
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00019EE4 File Offset: 0x000180E4
	private void Event_OnPlayerCameraEnabled(Dictionary<string, object> message)
	{
		PlayerCamera playerCamera = (PlayerCamera)message["playerCamera"];
		if (base.OwnerClientId != playerCamera.OwnerClientId)
		{
			return;
		}
		this.playerBody.MeshRendererHider.HideMeshRenderers();
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00019F24 File Offset: 0x00018124
	private void Event_OnPlayerCameraDisabled(Dictionary<string, object> message)
	{
		PlayerCamera playerCamera = (PlayerCamera)message["playerCamera"];
		if (base.OwnerClientId != playerCamera.OwnerClientId)
		{
			return;
		}
		this.playerBody.MeshRendererHider.ShowMeshRenderers();
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00009AFC File Offset: 0x00007CFC
	protected internal override string __getTypeName()
	{
		return "PlayerBodyController";
	}

	// Token: 0x04000117 RID: 279
	private PlayerBody playerBody;
}
