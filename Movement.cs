using System;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000023 RID: 35
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerBody))]
[RequireComponent(typeof(Hover))]
public class Movement : MonoBehaviour
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x060000C3 RID: 195 RVA: 0x000170D8 File Offset: 0x000152D8
	[HideInInspector]
	public float Speed
	{
		get
		{
			return new Vector3(this.Rigidbody.linearVelocity.x, 0f, this.Rigidbody.linearVelocity.z).magnitude;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060000C4 RID: 196 RVA: 0x000092CE File Offset: 0x000074CE
	[HideInInspector]
	public float NormalizedMaximumSpeed
	{
		get
		{
			return this.Speed / this.MaximumSpeed;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000C5 RID: 197 RVA: 0x000092DD File Offset: 0x000074DD
	[HideInInspector]
	public float NormalizedMinimumSpeed
	{
		get
		{
			return this.Speed / this.MinimumSpeed;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000C6 RID: 198 RVA: 0x000092EC File Offset: 0x000074EC
	[HideInInspector]
	public float TurnSpeed
	{
		get
		{
			return Math.Abs(base.transform.InverseTransformVector(this.Rigidbody.angularVelocity).y);
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000930E File Offset: 0x0000750E
	[HideInInspector]
	public float MaximumSpeed
	{
		get
		{
			return Mathf.Max(new float[]
			{
				this.maxForwardsSpeed,
				this.maxForwardsSprintSpeed,
				this.maxBackwardsSpeed,
				this.maxBackwardsSprintSpeed
			});
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000933F File Offset: 0x0000753F
	[HideInInspector]
	public float MinimumSpeed
	{
		get
		{
			return Mathf.Min(new float[]
			{
				this.maxForwardsSpeed,
				this.maxForwardsSprintSpeed,
				this.maxBackwardsSpeed,
				this.maxBackwardsSprintSpeed
			});
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000C9 RID: 201 RVA: 0x00009370 File Offset: 0x00007570
	[HideInInspector]
	public bool IsMovingForwards
	{
		get
		{
			return this.MovementDirection.InverseTransformVector(this.Rigidbody.linearVelocity).z > 0f;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000CA RID: 202 RVA: 0x00009394 File Offset: 0x00007594
	[HideInInspector]
	public bool IsMovingBackwards
	{
		get
		{
			return this.MovementDirection.InverseTransformVector(this.Rigidbody.linearVelocity).z < 0f;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000CB RID: 203 RVA: 0x000093B8 File Offset: 0x000075B8
	[HideInInspector]
	public bool IsTurningLeft
	{
		get
		{
			return base.transform.InverseTransformVector(this.Rigidbody.angularVelocity).y < 0f;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060000CC RID: 204 RVA: 0x000093DC File Offset: 0x000075DC
	[HideInInspector]
	public bool IsTurningRight
	{
		get
		{
			return base.transform.InverseTransformVector(this.Rigidbody.angularVelocity).y > 0f;
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00009400 File Offset: 0x00007600
	private void Awake()
	{
		this.Rigidbody = base.GetComponent<Rigidbody>();
		this.PlayerBody = base.GetComponent<PlayerBody>();
		this.Hover = base.GetComponent<Hover>();
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00009426 File Offset: 0x00007626
	private void Start()
	{
		this.currentMaxSpeed = this.maxForwardsSpeed;
		this.currentAcceleration = this.forwardsAcceleration;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00009440 File Offset: 0x00007640
	private void FixedUpdate()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.Move();
		this.Turn();
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00017118 File Offset: 0x00015318
	private void Move()
	{
		if (!this.Hover.IsGrounded)
		{
			return;
		}
		if (this.IsMovingForwards)
		{
			if (this.Sprint)
			{
				this.currentMaxSpeed = this.maxForwardsSprintSpeed;
				this.currentAcceleration = ((this.Speed < this.maxForwardsSpeed) ? this.forwardsSprintAcceleration : this.forwardsSprintOverspeedAcceleration);
			}
			else
			{
				this.currentMaxSpeed = this.maxForwardsSpeed;
				this.currentAcceleration = this.forwardsAcceleration;
			}
		}
		else if (this.IsMovingBackwards)
		{
			if (this.Sprint)
			{
				this.currentMaxSpeed = this.maxBackwardsSprintSpeed;
				this.currentAcceleration = ((this.Speed < this.maxForwardsSpeed) ? this.backwardsSprintAcceleration : this.backwardsSprintOverspeedAcceleration);
			}
			else
			{
				this.currentMaxSpeed = this.maxBackwardsSpeed;
				this.currentAcceleration = this.backwardsAcceleration;
			}
		}
		if (this.MoveForwards)
		{
			if (this.IsMovingForwards)
			{
				float d = (this.Speed < this.currentMaxSpeed) ? this.currentAcceleration : 0f;
				this.Rigidbody.AddForce(this.MovementDirection.forward * d, ForceMode.Acceleration);
			}
			else if (this.IsMovingBackwards)
			{
				float d2 = this.brakeAcceleration;
				this.Rigidbody.AddForce(this.MovementDirection.forward * d2, ForceMode.Acceleration);
			}
		}
		else if (this.MoveBackwards)
		{
			if (this.IsMovingBackwards)
			{
				float d3 = (this.Speed < this.currentMaxSpeed) ? this.currentAcceleration : 0f;
				this.Rigidbody.AddForce(-this.MovementDirection.forward * d3, ForceMode.Acceleration);
			}
			else if (this.IsMovingForwards)
			{
				float d4 = this.brakeAcceleration;
				this.Rigidbody.AddForce(-this.MovementDirection.forward * d4, ForceMode.Acceleration);
			}
		}
		if (this.Speed > this.MaximumSpeed)
		{
			this.Rigidbody.linearVelocity *= 1f - this.overspeedDrag * Time.fixedDeltaTime;
		}
		else
		{
			this.Rigidbody.linearVelocity *= 1f - this.drag * Time.fixedDeltaTime;
		}
		this.Rigidbody.linearVelocity *= 1f - this.AmbientDrag * Time.fixedDeltaTime;
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00017370 File Offset: 0x00015570
	private void Turn()
	{
		if (this.TurnLeft)
		{
			if (this.IsTurningLeft)
			{
				float num = (this.TurnSpeed < this.turnMaxSpeed * this.TurnMultiplier) ? this.turnAcceleration : 0f;
				this.Rigidbody.AddTorque(base.transform.up * -num * this.TurnMultiplier, ForceMode.Acceleration);
			}
			else if (this.IsTurningRight)
			{
				float num2 = this.turnBrakeAcceleration;
				this.Rigidbody.AddTorque(base.transform.up * -num2 * this.TurnMultiplier, ForceMode.Acceleration);
			}
		}
		else if (this.TurnRight)
		{
			if (this.IsTurningRight)
			{
				float d = (this.TurnSpeed < this.turnMaxSpeed * this.TurnMultiplier) ? this.turnAcceleration : 0f;
				this.Rigidbody.AddTorque(base.transform.up * d * this.TurnMultiplier, ForceMode.Acceleration);
			}
			else if (this.IsTurningLeft)
			{
				float d2 = this.turnBrakeAcceleration;
				this.Rigidbody.AddTorque(base.transform.up * d2 * this.TurnMultiplier, ForceMode.Acceleration);
			}
		}
		else if (this.TurnSpeed < this.turnMaxSpeed * this.TurnMultiplier)
		{
			this.Rigidbody.angularVelocity *= 1f - this.turnDrag * Time.fixedDeltaTime;
		}
		if (this.TurnSpeed > this.turnMaxSpeed * this.TurnMultiplier)
		{
			this.Rigidbody.angularVelocity *= 1f - this.turnOverspeedDrag * Time.fixedDeltaTime;
		}
	}

	// Token: 0x04000066 RID: 102
	[Header("Settings")]
	[SerializeField]
	private float forwardsAcceleration = 2f;

	// Token: 0x04000067 RID: 103
	[SerializeField]
	private float forwardsSprintAcceleration = 4.75f;

	// Token: 0x04000068 RID: 104
	[SerializeField]
	private float forwardsSprintOverspeedAcceleration = 1f;

	// Token: 0x04000069 RID: 105
	[SerializeField]
	private float backwardsAcceleration = 1.8f;

	// Token: 0x0400006A RID: 106
	[SerializeField]
	private float backwardsSprintAcceleration = 2f;

	// Token: 0x0400006B RID: 107
	[SerializeField]
	private float backwardsSprintOverspeedAcceleration = 1f;

	// Token: 0x0400006C RID: 108
	[SerializeField]
	private float brakeAcceleration = 5f;

	// Token: 0x0400006D RID: 109
	[SerializeField]
	private float drag = 0.025f;

	// Token: 0x0400006E RID: 110
	[SerializeField]
	private float overspeedDrag = 0.025f;

	// Token: 0x0400006F RID: 111
	[Space(20f)]
	[SerializeField]
	private float maxForwardsSpeed = 7.5f;

	// Token: 0x04000070 RID: 112
	[SerializeField]
	private float maxForwardsSprintSpeed = 8.75f;

	// Token: 0x04000071 RID: 113
	[SerializeField]
	private float maxBackwardsSpeed = 7.25f;

	// Token: 0x04000072 RID: 114
	[SerializeField]
	private float maxBackwardsSprintSpeed = 7.25f;

	// Token: 0x04000073 RID: 115
	[Space(20f)]
	[SerializeField]
	private float turnAcceleration = 1.625f;

	// Token: 0x04000074 RID: 116
	[SerializeField]
	private float turnBrakeAcceleration = 3.25f;

	// Token: 0x04000075 RID: 117
	[SerializeField]
	private float turnMaxSpeed = 1.375f;

	// Token: 0x04000076 RID: 118
	[SerializeField]
	private float turnDrag = 3f;

	// Token: 0x04000077 RID: 119
	[SerializeField]
	private float turnOverspeedDrag = 2.25f;

	// Token: 0x04000078 RID: 120
	[HideInInspector]
	public Rigidbody Rigidbody;

	// Token: 0x04000079 RID: 121
	[HideInInspector]
	public PlayerBody PlayerBody;

	// Token: 0x0400007A RID: 122
	[HideInInspector]
	public Hover Hover;

	// Token: 0x0400007B RID: 123
	[HideInInspector]
	public bool MoveForwards;

	// Token: 0x0400007C RID: 124
	[HideInInspector]
	public bool MoveBackwards;

	// Token: 0x0400007D RID: 125
	[HideInInspector]
	public bool TurnLeft;

	// Token: 0x0400007E RID: 126
	[HideInInspector]
	public bool TurnRight;

	// Token: 0x0400007F RID: 127
	[HideInInspector]
	public float TurnMultiplier;

	// Token: 0x04000080 RID: 128
	[HideInInspector]
	public bool Sprint;

	// Token: 0x04000081 RID: 129
	[HideInInspector]
	public float AmbientDrag;

	// Token: 0x04000082 RID: 130
	[HideInInspector]
	public Transform MovementDirection;

	// Token: 0x04000083 RID: 131
	private float currentMaxSpeed;

	// Token: 0x04000084 RID: 132
	private float currentAcceleration;
}
