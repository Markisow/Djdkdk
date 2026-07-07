using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class EdgegapManager : MonoBehaviour
{
	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06000812 RID: 2066 RVA: 0x0000E50D File Offset: 0x0000C70D
	// (set) Token: 0x06000813 RID: 2067 RVA: 0x0000E515 File Offset: 0x0000C715
	public string RequestId { get; private set; }

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06000814 RID: 2068 RVA: 0x0000E51E File Offset: 0x0000C71E
	// (set) Token: 0x06000815 RID: 2069 RVA: 0x0000E526 File Offset: 0x0000C726
	public string ArbitriumPublicIp { get; private set; }

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x06000816 RID: 2070 RVA: 0x0000E52F File Offset: 0x0000C72F
	// (set) Token: 0x06000817 RID: 2071 RVA: 0x0000E537 File Offset: 0x0000C737
	public ushort ArbitriumPortPuckExternal { get; private set; }

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x06000818 RID: 2072 RVA: 0x0000E540 File Offset: 0x0000C740
	// (set) Token: 0x06000819 RID: 2073 RVA: 0x0000E548 File Offset: 0x0000C748
	public string ArbitriumDeleteUrl { get; private set; }

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x0600081A RID: 2074 RVA: 0x0000E551 File Offset: 0x0000C751
	// (set) Token: 0x0600081B RID: 2075 RVA: 0x0000E559 File Offset: 0x0000C759
	public string ArbitriumDeleteToken { get; private set; }

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x0600081C RID: 2076 RVA: 0x0000E562 File Offset: 0x0000C762
	// (set) Token: 0x0600081D RID: 2077 RVA: 0x0000E56A File Offset: 0x0000C76A
	public bool IsEdgegap { get; private set; }

	// Token: 0x0600081E RID: 2078 RVA: 0x00034D60 File Offset: 0x00032F60
	private void Awake()
	{
		string environmentVariable = Environment.GetEnvironmentVariable("ARBITRIUM_REQUEST_ID");
		string environmentVariable2 = Environment.GetEnvironmentVariable("ARBITRIUM_PUBLIC_IP");
		string environmentVariable3 = Environment.GetEnvironmentVariable("ARBITRIUM_PORT_PUCK_EXTERNAL");
		string environmentVariable4 = Environment.GetEnvironmentVariable("ARBITRIUM_DELETE_URL");
		string environmentVariable5 = Environment.GetEnvironmentVariable("ARBITRIUM_DELETE_TOKEN");
		if (!string.IsNullOrEmpty(environmentVariable) && !string.IsNullOrEmpty(environmentVariable2) && !string.IsNullOrEmpty(environmentVariable3) && !string.IsNullOrEmpty(environmentVariable4) && !string.IsNullOrEmpty(environmentVariable5))
		{
			this.RequestId = environmentVariable;
			this.ArbitriumPublicIp = environmentVariable2;
			this.ArbitriumPortPuckExternal = ushort.Parse(environmentVariable3);
			this.ArbitriumDeleteUrl = environmentVariable4;
			this.ArbitriumDeleteToken = environmentVariable5;
			this.IsEdgegap = true;
			EdgegapManager.Logger.Info(string.Format("Running in Edgegap (RequestId: {0}, ArbitriumPublicIp: {1}, ArbitriumPortPuckExternal: {2}, ArbitriumDeleteUrl: {3}, ArbitriumDeleteToken: {4})", new object[]
			{
				this.RequestId,
				this.ArbitriumPublicIp,
				this.ArbitriumPortPuckExternal,
				this.ArbitriumDeleteUrl,
				this.ArbitriumDeleteToken
			}));
		}
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00034E58 File Offset: 0x00033058
	public void StartDependencyTimeout(EdgegapDependency dependency)
	{
		if (!this.IsEdgegap)
		{
			return;
		}
		EdgegapManager.Logger.Info(string.Format("Starting timeout for dependency {0}", dependency));
		if (this.dependencyTweenMap.ContainsKey(dependency))
		{
			Tween tween = this.dependencyTweenMap[dependency];
			if (tween != null)
			{
				tween.Kill(false);
			}
		}
		this.dependencyTweenMap[dependency] = DOVirtual.DelayedCall(Constants.EDGEGAP_DEPENDENCY_TIMEOUTS[dependency], delegate
		{
			this.OnDependencyFailed(dependency);
		}, true);
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00034F04 File Offset: 0x00033104
	public void StopDependencyTimeout(EdgegapDependency dependency)
	{
		if (!this.IsEdgegap)
		{
			return;
		}
		EdgegapManager.Logger.Info(string.Format("Stopping timeout for dependency {0}", dependency));
		if (this.dependencyTweenMap.ContainsKey(dependency))
		{
			Tween tween = this.dependencyTweenMap[dependency];
			if (tween == null)
			{
				return;
			}
			tween.Kill(false);
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0000E573 File Offset: 0x0000C773
	public void SetDependency(EdgegapDependency dependency, bool value)
	{
		if (!this.IsEdgegap)
		{
			return;
		}
		this.dependencyStatusMap[dependency] = value;
		if (value)
		{
			this.OnDependencyMet(dependency);
			return;
		}
		this.OnDependencyFailed(dependency);
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0000E59D File Offset: 0x0000C79D
	private void StartDeploymentDeletion(float repeatInterval = 1f)
	{
		if (!this.IsEdgegap)
		{
			return;
		}
		if (this.deploymentDeletionTween != null)
		{
			return;
		}
		this.DeleteDeployment();
		this.deploymentDeletionTween = DOVirtual.DelayedCall(repeatInterval, delegate
		{
			this.DeleteDeployment();
		}, true).SetLoops(-1);
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x00034F5C File Offset: 0x0003315C
	private void DeleteDeployment()
	{
		EdgegapManager.<DeleteDeployment>d__33 <DeleteDeployment>d__;
		<DeleteDeployment>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<DeleteDeployment>d__.<>4__this = this;
		<DeleteDeployment>d__.<>1__state = -1;
		<DeleteDeployment>d__.<>t__builder.Start<EdgegapManager.<DeleteDeployment>d__33>(ref <DeleteDeployment>d__);
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0000E5D6 File Offset: 0x0000C7D6
	private void OnDependencyMet(EdgegapDependency dependency)
	{
		if (!this.IsEdgegap)
		{
			return;
		}
		EdgegapManager.Logger.Info(string.Format("Dependency {0} met", dependency));
		this.StopDependencyTimeout(dependency);
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0000E602 File Offset: 0x0000C802
	private void OnDependencyFailed(EdgegapDependency dependency)
	{
		if (!this.IsEdgegap)
		{
			return;
		}
		EdgegapManager.Logger.Info(string.Format("Dependency {0} failed", dependency));
		this.StopDependencyTimeout(dependency);
		this.StartDeploymentDeletion(1f);
	}

	// Token: 0x040004DE RID: 1246
	private static readonly global::Logger Logger = new global::Logger("EdgegapManager");

	// Token: 0x040004E5 RID: 1253
	private Dictionary<EdgegapDependency, Tween> dependencyTweenMap = new Dictionary<EdgegapDependency, Tween>();

	// Token: 0x040004E6 RID: 1254
	private Dictionary<EdgegapDependency, bool> dependencyStatusMap = new Dictionary<EdgegapDependency, bool>();

	// Token: 0x040004E7 RID: 1255
	private Tween deploymentDeletionTween;
}
