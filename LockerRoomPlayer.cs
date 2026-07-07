using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class LockerRoomPlayer : MonoBehaviour
{
	// Token: 0x06000061 RID: 97 RVA: 0x0001617C File Offset: 0x0001437C
	private void Awake()
	{
		this.initialRotation = base.transform.rotation.eulerAngles;
		this.targetRotation = this.initialRotation;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00008E01 File Offset: 0x00007001
	private void Start()
	{
		this.SetRotationFromPreset(this.defaultRotationPreset);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000161B0 File Offset: 0x000143B0
	private void Update()
	{
		Vector2 vector = InputManager.PointAction.ReadValue<Vector2>();
		if (this.AllowRotation)
		{
			if (InputManager.ClickAction.WasPressedThisFrame() && !GlobalStateManager.UIState.IsMouseOverUI)
			{
				this.IsRotating = true;
				this.lastPointerPosition = vector;
			}
			else if (InputManager.ClickAction.WasReleasedThisFrame())
			{
				this.IsRotating = false;
			}
			if (this.IsRotating)
			{
				Vector2 vector2 = vector - this.lastPointerPosition;
				this.lastPointerPosition = vector;
				if (this.IsRotating)
				{
					this.targetRotation.y = this.targetRotation.y + vector2.x * this.rotationSpeed * Time.deltaTime;
				}
			}
		}
		Quaternion b = Quaternion.Euler(this.targetRotation);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime / this.rotationSmoothing);
		BaseCamera activeCamera = CameraManager.GetActiveCamera();
		if (activeCamera)
		{
			Plane plane = new Plane(activeCamera.transform.forward, activeCamera.transform.position + activeCamera.transform.forward);
			Ray ray = activeCamera.UnityCamera.ScreenPointToRay(vector);
			float distance;
			if (plane.Raycast(ray, out distance))
			{
				Vector3 point = ray.GetPoint(distance);
				Vector3 vector3 = activeCamera.transform.InverseTransformPoint(point);
				vector3.x *= Vector3.Dot(base.transform.forward, activeCamera.transform.forward);
				vector3.y += 0.5f;
				vector3.z *= 2f;
				Vector3 vector4 = base.transform.position + base.transform.right * vector3.x + base.transform.up * vector3.y + base.transform.forward * vector3.z;
				this.lookAtPosition = vector4;
			}
		}
		this.playerMesh.LookAt(this.lookAtPosition, Time.deltaTime, true, true);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00008E0F File Offset: 0x0000700F
	public void SetRotationFromPreset(string name)
	{
		if (!this.rotationPresets.ContainsKey(name))
		{
			LockerRoomPlayer.Logger.Error("Rotation preset " + name + " does not exist");
			return;
		}
		this.targetRotation = this.rotationPresets[name];
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00008E4C File Offset: 0x0000704C
	public void SetUsername(string username)
	{
		this.playerMesh.SetUsername(username);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00008E5A File Offset: 0x0000705A
	public void SetNumber(string number)
	{
		this.playerMesh.SetNumber(number);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00008E68 File Offset: 0x00007068
	public void SetLegsPadsActive(bool isActive)
	{
		this.playerMesh.SetLegsPadsActive(isActive);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00008E76 File Offset: 0x00007076
	public void SetFlagID(int flagID)
	{
		this.playerMesh.SetFlagID(flagID);
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00008E84 File Offset: 0x00007084
	public void SetHeadgearID(int headgearID, PlayerRole role)
	{
		this.playerMesh.SetHeadgearID(headgearID, role);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00008E93 File Offset: 0x00007093
	public void SetMustacheID(int mustacheID)
	{
		this.playerMesh.SetMustacheID(mustacheID);
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00008EA1 File Offset: 0x000070A1
	public void SetBeardID(int beardID)
	{
		this.playerMesh.SetBeardID(beardID);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00008EAF File Offset: 0x000070AF
	public void SetJerseyID(int jerseyID, PlayerTeam team)
	{
		this.playerMesh.SetJerseyID(jerseyID, team);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00008EBE File Offset: 0x000070BE
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawSphere(this.lookAtPosition, 0.05f);
	}

	// Token: 0x0400002F RID: 47
	private static readonly global::Logger Logger = new global::Logger("LockerRoomPlayer");

	// Token: 0x04000030 RID: 48
	[Header("Settings")]
	[SerializeField]
	private float rotationSpeed = 10f;

	// Token: 0x04000031 RID: 49
	[SerializeField]
	private float rotationSmoothing = 0.1f;

	// Token: 0x04000032 RID: 50
	[SerializeField]
	private SerializedDictionary<string, Vector3> rotationPresets = new SerializedDictionary<string, Vector3>();

	// Token: 0x04000033 RID: 51
	[SerializeField]
	private string defaultRotationPreset = "front";

	// Token: 0x04000034 RID: 52
	[Header("References")]
	[SerializeField]
	private PlayerMesh playerMesh;

	// Token: 0x04000035 RID: 53
	[HideInInspector]
	public bool AllowRotation;

	// Token: 0x04000036 RID: 54
	[HideInInspector]
	public bool IsRotating;

	// Token: 0x04000037 RID: 55
	private Vector2 lastPointerPosition = Vector2.zero;

	// Token: 0x04000038 RID: 56
	private Vector3 initialRotation;

	// Token: 0x04000039 RID: 57
	private Vector3 targetRotation;

	// Token: 0x0400003A RID: 58
	private Vector3 lookAtPosition;
}
