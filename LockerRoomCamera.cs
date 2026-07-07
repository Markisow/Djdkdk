using System;

// Token: 0x02000013 RID: 19
public class LockerRoomCamera : BaseCamera
{
	// Token: 0x06000053 RID: 83 RVA: 0x00008D5C File Offset: 0x00006F5C
	public override void Awake()
	{
		base.Awake();
		this.smoothPositioner = base.GetComponent<SmoothPositioner>();
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00008D70 File Offset: 0x00006F70
	public void SetPosition(string positionName)
	{
		this.smoothPositioner.SetPosition(positionName, false);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00016048 File Offset: 0x00014248
	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00008D87 File Offset: 0x00006F87
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00008D91 File Offset: 0x00006F91
	protected internal override string __getTypeName()
	{
		return "LockerRoomCamera";
	}

	// Token: 0x0400002D RID: 45
	private SmoothPositioner smoothPositioner;
}
