using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001E6 RID: 486
public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00013E59 File Offset: 0x00012059
	public static T Instance
	{
		get
		{
			return MonoBehaviourSingleton<!0>.instance;
		}
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x0004BFB0 File Offset: 0x0004A1B0
	public virtual void Awake()
	{
		if (MonoBehaviourSingleton<!0>.instance != null && MonoBehaviourSingleton<!0>.instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		if (MonoBehaviourSingleton<!0>.instance == null)
		{
			MonoBehaviourSingleton<!0>.instance = (this as !0);
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x00013E60 File Offset: 0x00012060
	public void AllowSceneDestruction()
	{
		UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(base.gameObject, UnityEngine.SceneManagement.SceneManager.GetActiveScene());
	}

	// Token: 0x040008E6 RID: 2278
	private static T instance;
}
