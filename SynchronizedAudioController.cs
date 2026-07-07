using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x02000070 RID: 112
public class SynchronizedAudioController : NetworkBehaviour
{
	// Token: 0x060003A8 RID: 936 RVA: 0x0000B593 File Offset: 0x00009793
	private void Awake()
	{
		this.synchronizedAudio = base.GetComponent<SynchronizedAudio>();
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0000B5A1 File Offset: 0x000097A1
	public override void OnNetworkSpawn()
	{
		EventManager.AddEventListener("Event_Server_OnClientSceneSynchronizeComplete", new Action<Dictionary<string, object>>(this.Event_Server_OnClientSceneSynchronizeComplete));
		base.OnNetworkSpawn();
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0000B5BF File Offset: 0x000097BF
	public override void OnNetworkDespawn()
	{
		EventManager.RemoveEventListener("Event_Server_OnClientSceneSynchronizeComplete", new Action<Dictionary<string, object>>(this.Event_Server_OnClientSceneSynchronizeComplete));
		base.OnNetworkDespawn();
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00025D9C File Offset: 0x00023F9C
	private void Event_Server_OnClientSceneSynchronizeComplete(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		if (num == 0UL)
		{
			return;
		}
		this.synchronizedAudio.Server_ForceSynchronizeClientId(num);
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0000B5DD File Offset: 0x000097DD
	protected internal override string __getTypeName()
	{
		return "SynchronizedAudioController";
	}

	// Token: 0x04000299 RID: 665
	private SynchronizedAudio synchronizedAudio;
}
