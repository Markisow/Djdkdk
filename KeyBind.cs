using System;
using System.Text.Json.Serialization;
using UnityEngine.InputSystem;

// Token: 0x020000B6 RID: 182
public class KeyBind
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060005BC RID: 1468 RVA: 0x0002E508 File Offset: 0x0002C708
	[JsonIgnore]
	public bool IsComposite
	{
		get
		{
			return this.InputAction.bindings[0].isComposite;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000C97E File Offset: 0x0000AB7E
	// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000C986 File Offset: 0x0000AB86
	public string ModifierPath { get; set; }

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000C98F File Offset: 0x0000AB8F
	// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000C997 File Offset: 0x0000AB97
	public string Path { get; set; }

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
	// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
	public string Interactions { get; set; }

	// Token: 0x060005C3 RID: 1475 RVA: 0x00008ACE File Offset: 0x00006CCE
	[JsonConstructor]
	public KeyBind()
	{
	}

	// Token: 0x060005C4 RID: 1476 RVA: 0x0000C9B1 File Offset: 0x0000ABB1
	public KeyBind(InputAction inputAction)
	{
		this.InputAction = inputAction;
		this.Update(this.InputAction);
	}

	// Token: 0x060005C5 RID: 1477 RVA: 0x0002E534 File Offset: 0x0002C734
	public void Update(InputAction inputAction)
	{
		this.InputAction = inputAction;
		this.ModifierPath = (this.IsComposite ? inputAction.bindings[1].effectivePath : null);
		this.Path = (this.IsComposite ? inputAction.bindings[2].effectivePath : inputAction.bindings[0].effectivePath);
		this.Interactions = inputAction.bindings[0].effectiveInteractions;
	}

	// Token: 0x04000388 RID: 904
	[JsonIgnore]
	public InputAction InputAction;
}
