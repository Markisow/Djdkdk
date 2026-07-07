using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class NetworkObjectCollisionRecorder : NetworkBehaviour
{
	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600009B RID: 155 RVA: 0x00009114 File Offset: 0x00007314
	[HideInInspector]
	public List<NetworkObjectCollision> NetworkObjectCollisions
	{
		get
		{
			return this.Buffer.AsNativeArray().ToList<NetworkObjectCollision>();
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000912B File Offset: 0x0000732B
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		this.InitializeNetworkVariables(null);
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000913B File Offset: 0x0000733B
	public override void OnNetworkSpawn()
	{
		this.Buffer.OnListChanged += this.OnBufferChanged;
		base.OnNetworkSpawn();
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000915A File Offset: 0x0000735A
	protected override void OnNetworkPostSpawn()
	{
		if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsConnectedClient)
		{
			this.ProcessInitialNetworkVariableValues();
		}
		base.OnNetworkPostSpawn();
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00009180 File Offset: 0x00007380
	protected override void OnNetworkSessionSynchronized()
	{
		this.ProcessInitialNetworkVariableValues();
		base.OnNetworkSessionSynchronized();
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x0000918E File Offset: 0x0000738E
	public override void OnNetworkDespawn()
	{
		this.Buffer.OnListChanged -= this.OnBufferChanged;
		base.OnNetworkDespawn();
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000091AD File Offset: 0x000073AD
	public void InitializeNetworkVariables(List<NetworkObjectCollision> buffer = null)
	{
		if (this.isNetworkVariablesInitialized)
		{
			return;
		}
		this.isNetworkVariablesInitialized = true;
		this.Buffer = new NetworkList<NetworkObjectCollision>(buffer, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00016AB4 File Offset: 0x00014CB4
	private void ProcessInitialNetworkVariableValues()
	{
		this.OnBufferChanged(new NetworkListEvent<NetworkObjectCollision>
		{
			Type = NetworkListEvent<NetworkObjectCollision>.EventType.Full
		});
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x0000895D File Offset: 0x00006B5D
	private void OnBufferChanged(NetworkListEvent<NetworkObjectCollision> changeEvent)
	{
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00016AD8 File Offset: 0x00014CD8
	private void OnCollisionEnter(Collision collision)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if ((this.collisionLayers.value & 1 << collision.gameObject.layer) == 0)
		{
			return;
		}
		NetworkObject component = collision.gameObject.GetComponent<NetworkObject>();
		if (!component)
		{
			return;
		}
		NetworkObjectReference networkObjectReference = new NetworkObjectReference(component);
		NetworkObjectCollision? networkObjectCollision = null;
		foreach (NetworkObjectCollision networkObjectCollision2 in this.Buffer)
		{
			NetworkObjectReference networkObjectReference2 = networkObjectCollision2.NetworkObjectReference;
			if (networkObjectReference2.Equals(networkObjectReference))
			{
				networkObjectCollision = new NetworkObjectCollision?(networkObjectCollision2);
				break;
			}
		}
		if (networkObjectCollision != null && this.Buffer.Contains(networkObjectCollision.Value))
		{
			this.Buffer.Remove(networkObjectCollision.Value);
		}
		if (this.Buffer.Count >= this.bufferSize)
		{
			this.Buffer.RemoveAt(0);
		}
		this.Buffer.Add(new NetworkObjectCollision
		{
			NetworkObjectReference = networkObjectReference,
			Time = Time.time
		});
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00016C08 File Offset: 0x00014E08
	protected override void __initializeVariables()
	{
		bool flag = this.Buffer == null;
		if (flag)
		{
			throw new Exception("NetworkObjectCollisionRecorder.Buffer cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Buffer.Initialize(this);
		base.__nameNetworkVariable(this.Buffer, "Buffer");
		this.NetworkVariableFields.Add(this.Buffer);
		base.__initializeVariables();
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000091DD File Offset: 0x000073DD
	protected internal override string __getTypeName()
	{
		return "NetworkObjectCollisionRecorder";
	}

	// Token: 0x04000048 RID: 72
	[Header("Settings")]
	[SerializeField]
	private int bufferSize = 10;

	// Token: 0x04000049 RID: 73
	[SerializeField]
	private LayerMask collisionLayers;

	// Token: 0x0400004A RID: 74
	[HideInInspector]
	public NetworkList<NetworkObjectCollision> Buffer;

	// Token: 0x0400004B RID: 75
	private bool isNetworkVariablesInitialized;
}
