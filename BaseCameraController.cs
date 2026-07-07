using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class BaseCameraController : MonoBehaviour
{
	// Token: 0x06000010 RID: 16 RVA: 0x00008986 File Offset: 0x00006B86
	public virtual void Awake()
	{
		this.baseCamera = base.GetComponent<BaseCamera>();
		EventManager.AddEventListener("Event_OnSynchronizeObjects", new Action<Dictionary<string, object>>(this.Event_OnSynchronizeObjects));
		EventManager.AddEventListener("Event_OnFovChanged", new Action<Dictionary<string, object>>(this.Event_OnFovChanged));
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000089C0 File Offset: 0x00006BC0
	public virtual void Start()
	{
		this.baseCamera.SetFieldOfView(SettingsManager.Fov);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000089D2 File Offset: 0x00006BD2
	public virtual void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnSynchronizeObjects", new Action<Dictionary<string, object>>(this.Event_OnSynchronizeObjects));
		EventManager.RemoveEventListener("Event_OnFovChanged", new Action<Dictionary<string, object>>(this.Event_OnFovChanged));
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00008A00 File Offset: 0x00006C00
	public virtual void LateUpdate()
	{
		if (NetworkManager.Singleton == null || NetworkManager.Singleton.IsHost || SettingsManager.UseNetworkSmoothing)
		{
			this.baseCamera.OnTick(Time.deltaTime);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000158A0 File Offset: 0x00013AA0
	private void Event_OnSynchronizeObjects(Dictionary<string, object> message)
	{
		if (SettingsManager.UseNetworkSmoothing)
		{
			return;
		}
		float deltaTime = (float)message["serverDeltaTime"];
		this.baseCamera.OnTick(deltaTime);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000158D4 File Offset: 0x00013AD4
	private void Event_OnFovChanged(Dictionary<string, object> message)
	{
		float fieldOfView = (float)message["value"];
		this.baseCamera.SetFieldOfView(fieldOfView);
	}

	// Token: 0x04000007 RID: 7
	private BaseCamera baseCamera;
}
