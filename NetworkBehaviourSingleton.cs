using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001E8 RID: 488
public class NetworkBehaviourSingleton<T> : NetworkBehaviour where T : NetworkBehaviour
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00013E85 File Offset: 0x00012085
	public static T Instance
	{
		get
		{
			return NetworkBehaviourSingleton<!0>.instance;
		}
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x0004C090 File Offset: 0x0004A290
	public virtual void Awake()
	{
		if (NetworkBehaviourSingleton<!0>.instance != null && NetworkBehaviourSingleton<!0>.instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (NetworkBehaviourSingleton<!0>.instance == null)
		{
			NetworkBehaviourSingleton<!0>.instance = (this as !0);
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x00013E60 File Offset: 0x00012060
	public void AllowSceneDestruction()
	{
		UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(base.gameObject, UnityEngine.SceneManagement.SceneManager.GetActiveScene());
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x00015888 File Offset: 0x00013A88
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00008975 File Offset: 0x00006B75
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x00013E8C File Offset: 0x0001208C
	protected internal override string __getTypeName()
	{
		return "NetworkBehaviourSingleton`1";
	}

	// Token: 0x040008EA RID: 2282
	private static T instance;
}
