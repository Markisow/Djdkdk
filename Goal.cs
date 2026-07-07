using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class Goal : MonoBehaviour
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600002F RID: 47 RVA: 0x00008BA6 File Offset: 0x00006DA6
	public Cloth NetCloth
	{
		get
		{
			return this.netCloth;
		}
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00008BAE File Offset: 0x00006DAE
	public void Server_OnPuckEnterGoal(Puck puck)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		EventManager.TriggerEvent("Event_Server_OnPuckEnterGoal", new Dictionary<string, object>
		{
			{
				"puck",
				puck
			},
			{
				"team",
				this.Team
			}
		});
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00015BB8 File Offset: 0x00013DB8
	public void Client_AddNetClothSphereCollider(SphereCollider sphereCollider)
	{
		if (!NetworkManager.Singleton.IsClient)
		{
			return;
		}
		if (!sphereCollider)
		{
			return;
		}
		List<ClothSphereColliderPair> list = this.netCloth.sphereColliders.ToList<ClothSphereColliderPair>();
		list.Add(new ClothSphereColliderPair(sphereCollider));
		this.netCloth.sphereColliders = list.ToArray();
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00015C0C File Offset: 0x00013E0C
	public void Client_RemoveNetClothSphereCollider(SphereCollider sphereCollider)
	{
		if (!NetworkManager.Singleton.IsClient)
		{
			return;
		}
		if (!sphereCollider)
		{
			return;
		}
		List<ClothSphereColliderPair> list = this.netCloth.sphereColliders.ToList<ClothSphereColliderPair>();
		list.RemoveAll((ClothSphereColliderPair pair) => pair.first == sphereCollider);
		this.netCloth.sphereColliders = list.ToArray();
	}

	// Token: 0x0400001A RID: 26
	[Header("Settings")]
	[SerializeField]
	private PlayerTeam Team;

	// Token: 0x0400001B RID: 27
	[Header("References")]
	[SerializeField]
	private Cloth netCloth;
}
