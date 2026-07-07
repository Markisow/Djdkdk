using System;
using UnityEngine;
using UnityEngine.Playables;

// Token: 0x0200006D RID: 109
public class Spectator : MonoBehaviour
{
	// Token: 0x06000384 RID: 900 RVA: 0x000252F8 File Offset: 0x000234F8
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
		this.animator.playableGraph.SetTimeUpdateMode(DirectorUpdateMode.Manual);
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0000B330 File Offset: 0x00009530
	public void PlayAnimation(string animationName)
	{
		this.StopAnimations();
		this.animator.SetBool(animationName, true);
		this.animationRequested = true;
	}

	// Token: 0x06000386 RID: 902 RVA: 0x0000B34C File Offset: 0x0000954C
	public void StopAnimations()
	{
		this.animator.SetBool("Seated", false);
		this.animator.SetBool("Cheering", false);
		this.animator.SetBool("Standing", false);
		this.animationRequested = true;
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00025328 File Offset: 0x00023528
	public void UpdateAnimation()
	{
		if (!this.playerMesh)
		{
			return;
		}
		AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
		bool flag = currentAnimatorStateInfo.loop || currentAnimatorStateInfo.normalizedTime < 1f || this.animator.IsInTransition(0) || this.animationRequested;
		double num = Time.timeAsDouble - this.lastUpdateTime;
		if (flag)
		{
			this.animator.Update((float)num);
		}
		else
		{
			this.playerMesh.LookAt(this.LookTarget ? this.LookTarget.position : Vector3.zero, (float)num, false, true);
		}
		if (this.animationRequested)
		{
			this.animationRequested = false;
		}
		this.lastUpdateTime = Time.timeAsDouble;
	}

	// Token: 0x06000388 RID: 904 RVA: 0x000253E4 File Offset: 0x000235E4
	public void RandomizeAppearance()
	{
		this.playerMesh.SetUsername(null);
		this.playerMesh.SetNumber(null);
		this.playerMesh.SetLegsPadsActive(false);
		int headgearID = this.headgearOptions[UnityEngine.Random.Range(0, this.headgearOptions.Length)];
		this.playerMesh.SetHeadgearID(headgearID, PlayerRole.None);
		int jerseyID = this.jerseyOptions[UnityEngine.Random.Range(0, this.jerseyOptions.Length)];
		this.playerMesh.SetJerseyID(jerseyID, PlayerTeam.None);
		int mustacheID = this.mustacheOptions[UnityEngine.Random.Range(0, this.mustacheOptions.Length)];
		this.playerMesh.SetMustacheID(mustacheID);
		int beardID = this.beardOptions[UnityEngine.Random.Range(0, this.beardOptions.Length)];
		this.playerMesh.SetBeardID(beardID);
	}

	// Token: 0x0400027E RID: 638
	[Header("References")]
	[SerializeField]
	private PlayerMesh playerMesh;

	// Token: 0x0400027F RID: 639
	[SerializeField]
	private Animator animator;

	// Token: 0x04000280 RID: 640
	[HideInInspector]
	public Transform LookTarget;

	// Token: 0x04000281 RID: 641
	private bool animationRequested;

	// Token: 0x04000282 RID: 642
	private double lastUpdateTime;

	// Token: 0x04000283 RID: 643
	private int[] headgearOptions = new int[]
	{
		-1,
		537,
		538,
		539
	};

	// Token: 0x04000284 RID: 644
	private int[] jerseyOptions = new int[]
	{
		2118,
		2119,
		2120,
		2121,
		2122
	};

	// Token: 0x04000285 RID: 645
	private int[] mustacheOptions = new int[]
	{
		-1,
		1024,
		1025,
		1026,
		1027,
		1028,
		1029,
		1030
	};

	// Token: 0x04000286 RID: 646
	private int[] beardOptions = new int[]
	{
		-1,
		1536,
		1537,
		1538,
		1539,
		1540
	};
}
