using System;
using UnityEngine.InputSystem;

// Token: 0x020000C0 RID: 192
public class ToggleInteraction : IInputInteraction
{
	// Token: 0x060005E8 RID: 1512 RVA: 0x0002F644 File Offset: 0x0002D844
	public void Process(ref InputInteractionContext context)
	{
		if (!context.action.IsPressed())
		{
			return;
		}
		InputActionPhase phase = context.phase;
		if (phase != InputActionPhase.Waiting)
		{
			if (phase != InputActionPhase.Started)
			{
				return;
			}
			if (this.isToggled)
			{
				this.isToggled = false;
				context.Canceled();
			}
		}
		else if (!this.isToggled)
		{
			this.isToggled = true;
			context.Started();
			return;
		}
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0000CADD File Offset: 0x0000ACDD
	public void Reset()
	{
		this.isToggled = false;
	}

	// Token: 0x040003C9 RID: 969
	private bool isToggled;
}
