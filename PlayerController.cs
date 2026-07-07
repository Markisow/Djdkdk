using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine.InputSystem;

// Token: 0x0200004B RID: 75
public class PlayerController : NetworkBehaviour
{
	// Token: 0x0600027A RID: 634 RVA: 0x0000A682 File Offset: 0x00008882
	private void Awake()
	{
		this.player = base.GetComponent<Player>();
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00021E9C File Offset: 0x0002009C
	public override void OnNetworkSpawn()
	{
		InputManager.PositionSelectAction.performed += this.OnPositionSelectActionPerformed;
		EventManager.AddEventListener("Event_OnTeamSelectClickTeam", new Action<Dictionary<string, object>>(this.Event_OnTeamSelectClickTeam));
		EventManager.AddEventListener("Event_OnPositionSelectClickPosition", new Action<Dictionary<string, object>>(this.Event_OnPositionSelectClickPosition));
		EventManager.AddEventListener("Event_OnPauseMenuClickSelectTeam", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectTeam));
		EventManager.AddEventListener("Event_OnPauseMenuClickSelectPosition", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectPosition));
		EventManager.AddEventListener("Event_OnHandednessChanged", new Action<Dictionary<string, object>>(this.Event_OnHandednessChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerPositionClaimedByPlayerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionClaimedByPlayerChanged));
		if (NetworkManager.Singleton.IsServer)
		{
			this.pingTween = DOVirtual.DelayedCall(1f, delegate
			{
				this.player.Server_UpdatePing();
			}, true).SetLoops(-1);
		}
		base.OnNetworkSpawn();
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00021F78 File Offset: 0x00020178
	public override void OnNetworkDespawn()
	{
		InputManager.PositionSelectAction.performed -= this.OnPositionSelectActionPerformed;
		EventManager.RemoveEventListener("Event_OnTeamSelectClickTeam", new Action<Dictionary<string, object>>(this.Event_OnTeamSelectClickTeam));
		EventManager.RemoveEventListener("Event_OnPositionSelectClickPosition", new Action<Dictionary<string, object>>(this.Event_OnPositionSelectClickPosition));
		EventManager.RemoveEventListener("Event_OnPauseMenuClickSelectTeam", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectTeam));
		EventManager.RemoveEventListener("Event_OnPauseMenuClickSelectPosition", new Action<Dictionary<string, object>>(this.Event_OnPauseMenuClickSelectPosition));
		EventManager.RemoveEventListener("Event_OnHandednessChanged", new Action<Dictionary<string, object>>(this.Event_OnHandednessChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPositionClaimedByPlayerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionClaimedByPlayerChanged));
		if (NetworkManager.Singleton.IsServer)
		{
			Tween tween = this.pingTween;
			if (tween != null)
			{
				tween.Kill(false);
			}
		}
		base.OnNetworkDespawn();
	}

	// Token: 0x0600027D RID: 637 RVA: 0x00022044 File Offset: 0x00020244
	private void OnPositionSelectActionPerformed(InputAction.CallbackContext context)
	{
		if (GlobalStateManager.UIState.Phase != UIPhase.Playing)
		{
			return;
		}
		if (GlobalStateManager.UIState.IsInteracting)
		{
			return;
		}
		if (!this.player.IsLocalPlayer)
		{
			return;
		}
		this.player.Client_RequestPositionSelectRpc(default(RpcParams));
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00022094 File Offset: 0x00020294
	private void Event_OnTeamSelectClickTeam(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["team"];
		if (!this.player.IsLocalPlayer)
		{
			return;
		}
		this.player.Client_RequestTeamRpc(team, default(RpcParams));
	}

	// Token: 0x0600027F RID: 639 RVA: 0x000220D8 File Offset: 0x000202D8
	private void Event_OnPositionSelectClickPosition(Dictionary<string, object> message)
	{
		PlayerPosition playerPosition = (PlayerPosition)message["playerPosition"];
		if (!this.player.IsLocalPlayer)
		{
			return;
		}
		NetworkObjectReference playerPositionReference = new NetworkObjectReference(playerPosition.NetworkObject);
		this.player.Client_RequestClaimPositionRpc(playerPositionReference, default(RpcParams));
	}

	// Token: 0x06000280 RID: 640 RVA: 0x00022128 File Offset: 0x00020328
	private void Event_OnPauseMenuClickSelectTeam(Dictionary<string, object> message)
	{
		if (!this.player.IsLocalPlayer)
		{
			return;
		}
		this.player.Client_RequestTeamSelectRpc(default(RpcParams));
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00022158 File Offset: 0x00020358
	private void Event_OnPauseMenuClickSelectPosition(Dictionary<string, object> message)
	{
		if (!this.player.IsLocalPlayer)
		{
			return;
		}
		this.player.Client_RequestPositionSelectRpc(default(RpcParams));
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00022188 File Offset: 0x00020388
	private void Event_OnHandednessChanged(Dictionary<string, object> message)
	{
		PlayerHandedness handedness = (PlayerHandedness)message["value"];
		if (!this.player.IsLocalPlayer)
		{
			return;
		}
		this.player.Client_RequestHandednessRpc(handedness, default(RpcParams));
	}

	// Token: 0x06000283 RID: 643 RVA: 0x000221CC File Offset: 0x000203CC
	private void Event_Everyone_OnPlayerPositionClaimedByPlayerChanged(Dictionary<string, object> message)
	{
		PlayerPosition playerPosition = (PlayerPosition)message["playerPosition"];
		Player x = (Player)message["oldClaimedByPlayer"];
		Player x2 = (Player)message["newClaimedByPlayer"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (x2 == this.player)
		{
			this.player.PlayerPositionReference.Value = new NetworkObjectReference(playerPosition.NetworkObject);
			return;
		}
		if (x == this.player && playerPosition == this.player.PlayerPosition)
		{
			this.player.PlayerPositionReference.Value = default(NetworkObjectReference);
		}
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000288 RID: 648 RVA: 0x0000A69D File Offset: 0x0000889D
	protected internal override string __getTypeName()
	{
		return "PlayerController";
	}

	// Token: 0x040001AE RID: 430
	private Player player;

	// Token: 0x040001AF RID: 431
	private Tween pingTween;
}
