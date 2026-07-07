using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class Level : NetworkBehaviour
{
	// Token: 0x0600003F RID: 63 RVA: 0x00015E14 File Offset: 0x00014014
	private void Awake()
	{
		MeshRenderer component = this.boundsGameObject.GetComponent<MeshRenderer>();
		if (component == null)
		{
			Level.Logger.Warning("boundsGameObject does not have a MeshRenderer component");
			return;
		}
		this.Bounds = component.bounds;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00008C93 File Offset: 0x00006E93
	protected override void OnNetworkPostSpawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnLevelSpawned", new Dictionary<string, object>
		{
			{
				"level",
				this
			}
		});
		base.OnNetworkPostSpawn();
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00008CB6 File Offset: 0x00006EB6
	public override void OnNetworkDespawn()
	{
		EventManager.TriggerEvent("Event_Everyone_OnLevelDespawned", new Dictionary<string, object>
		{
			{
				"level",
				this
			}
		});
		base.OnNetworkDespawn();
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00008CD9 File Offset: 0x00006ED9
	public void SetBlueGoalLightEnabled(bool isEnabled)
	{
		this.blueGoalLight.enabled = isEnabled;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00008CE7 File Offset: 0x00006EE7
	public void SetRedGoalLightEnabled(bool isEnabled)
	{
		this.redGoalLight.enabled = isEnabled;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00015E54 File Offset: 0x00014054
	public void Server_PlayBlueGoalSound()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.blueGoalSound.Server_Play(-1f, -1f, false, -1, 0f, false, false, false, 0f, false, 0f, -1f);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00015EA0 File Offset: 0x000140A0
	public void Server_PlayRedGoalSound()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.redGoalSound.Server_Play(-1f, -1f, false, -1, 0f, false, false, false, 0f, false, 0f, -1f);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00015EEC File Offset: 0x000140EC
	public void Server_PlayerCheerSound(float duration)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.cheerSounds.ForEach(delegate(SynchronizedAudio cheerSound)
		{
			cheerSound.Server_Play(-1f, -1f, false, -1, 0f, false, false, true, 3f, true, 3f, duration);
		});
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00015F2C File Offset: 0x0001412C
	public void Server_PlayHornSound()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.hornSound.Server_Play(-1f, -1f, false, -1, 0f, false, false, false, 0f, false, 0f, -1f);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00008D19 File Offset: 0x00006F19
	protected internal override string __getTypeName()
	{
		return "Level";
	}

	// Token: 0x04000022 RID: 34
	private static readonly global::Logger Logger = new global::Logger("Level");

	// Token: 0x04000023 RID: 35
	[Header("References")]
	[SerializeField]
	private GameObject boundsGameObject;

	// Token: 0x04000024 RID: 36
	[SerializeField]
	private Light blueGoalLight;

	// Token: 0x04000025 RID: 37
	[SerializeField]
	private Light redGoalLight;

	// Token: 0x04000026 RID: 38
	[SerializeField]
	private SynchronizedAudio blueGoalSound;

	// Token: 0x04000027 RID: 39
	[SerializeField]
	private SynchronizedAudio redGoalSound;

	// Token: 0x04000028 RID: 40
	[SerializeField]
	private List<SynchronizedAudio> cheerSounds = new List<SynchronizedAudio>();

	// Token: 0x04000029 RID: 41
	[SerializeField]
	private SynchronizedAudio hornSound;

	// Token: 0x0400002A RID: 42
	[HideInInspector]
	public Bounds Bounds;
}
