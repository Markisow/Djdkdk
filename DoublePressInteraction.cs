using System;
using UnityEngine.InputSystem;

// Token: 0x020000B5 RID: 181
public class DoublePressInteraction : IInputInteraction
{
	// Token: 0x060005B9 RID: 1465 RVA: 0x0002E488 File Offset: 0x0002C688
	public void Process(ref InputInteractionContext context)
	{
		if (context.timerHasExpired)
		{
			context.Canceled();
			return;
		}
		InputActionPhase phase = context.phase;
		if (phase != InputActionPhase.Waiting)
		{
			if (phase != InputActionPhase.Started)
			{
				return;
			}
			if (this.released)
			{
				if (context.ReadValue<float>() > this.pressThreshold)
				{
					context.Performed();
					return;
				}
			}
			else if (context.ReadValue<float>() < this.releaseThreshold)
			{
				this.released = true;
			}
		}
		else if (context.ReadValue<float>() > this.pressThreshold)
		{
			context.Started();
			context.SetTimeout(this.maxTapDuration);
			return;
		}
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0000C94C File Offset: 0x0000AB4C
	public void Reset()
	{
		this.released = false;
	}

	// Token: 0x04000384 RID: 900
	public float maxTapDuration = 0.2f;

	// Token: 0x04000385 RID: 901
	public float pressThreshold = 0.5f;

	// Token: 0x04000386 RID: 902
	public float releaseThreshold = 0.5f;

	// Token: 0x04000387 RID: 903
	private bool released;
}
