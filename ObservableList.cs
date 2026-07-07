using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

// Token: 0x020001ED RID: 493
internal class ObservableList<T> : List<!0> where T : INotifyPropertyChanged
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000EA6 RID: 3750 RVA: 0x0004C29C File Offset: 0x0004A49C
	// (remove) Token: 0x06000EA7 RID: 3751 RVA: 0x0004C2D4 File Offset: 0x0004A4D4
	public event ObservableList<!0>.OnAdd onAdd
	{
		[CompilerGenerated]
		add
		{
			ObservableList<T>.OnAdd onAdd = this.onAdd;
			ObservableList<T>.OnAdd onAdd2;
			do
			{
				onAdd2 = onAdd;
				ObservableList<T>.OnAdd value2 = (ObservableList<!0>.OnAdd)Delegate.Combine(onAdd2, value);
				onAdd = Interlocked.CompareExchange<ObservableList<T>.OnAdd>(ref this.onAdd, value2, onAdd2);
			}
			while (onAdd != onAdd2);
		}
		[CompilerGenerated]
		remove
		{
			ObservableList<T>.OnAdd onAdd = this.onAdd;
			ObservableList<T>.OnAdd onAdd2;
			do
			{
				onAdd2 = onAdd;
				ObservableList<T>.OnAdd value2 = (ObservableList<!0>.OnAdd)Delegate.Remove(onAdd2, value);
				onAdd = Interlocked.CompareExchange<ObservableList<T>.OnAdd>(ref this.onAdd, value2, onAdd2);
			}
			while (onAdd != onAdd2);
		}
	}

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000EA8 RID: 3752 RVA: 0x0004C30C File Offset: 0x0004A50C
	// (remove) Token: 0x06000EA9 RID: 3753 RVA: 0x0004C344 File Offset: 0x0004A544
	public event ObservableList<!0>.OnRemove onRemove
	{
		[CompilerGenerated]
		add
		{
			ObservableList<T>.OnRemove onRemove = this.onRemove;
			ObservableList<T>.OnRemove onRemove2;
			do
			{
				onRemove2 = onRemove;
				ObservableList<T>.OnRemove value2 = (ObservableList<!0>.OnRemove)Delegate.Combine(onRemove2, value);
				onRemove = Interlocked.CompareExchange<ObservableList<T>.OnRemove>(ref this.onRemove, value2, onRemove2);
			}
			while (onRemove != onRemove2);
		}
		[CompilerGenerated]
		remove
		{
			ObservableList<T>.OnRemove onRemove = this.onRemove;
			ObservableList<T>.OnRemove onRemove2;
			do
			{
				onRemove2 = onRemove;
				ObservableList<T>.OnRemove value2 = (ObservableList<!0>.OnRemove)Delegate.Remove(onRemove2, value);
				onRemove = Interlocked.CompareExchange<ObservableList<T>.OnRemove>(ref this.onRemove, value2, onRemove2);
			}
			while (onRemove != onRemove2);
		}
	}

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000EAA RID: 3754 RVA: 0x0004C37C File Offset: 0x0004A57C
	// (remove) Token: 0x06000EAB RID: 3755 RVA: 0x0004C3B4 File Offset: 0x0004A5B4
	public event ObservableList<!0>.OnClear onClear
	{
		[CompilerGenerated]
		add
		{
			ObservableList<T>.OnClear onClear = this.onClear;
			ObservableList<T>.OnClear onClear2;
			do
			{
				onClear2 = onClear;
				ObservableList<T>.OnClear value2 = (ObservableList<!0>.OnClear)Delegate.Combine(onClear2, value);
				onClear = Interlocked.CompareExchange<ObservableList<T>.OnClear>(ref this.onClear, value2, onClear2);
			}
			while (onClear != onClear2);
		}
		[CompilerGenerated]
		remove
		{
			ObservableList<T>.OnClear onClear = this.onClear;
			ObservableList<T>.OnClear onClear2;
			do
			{
				onClear2 = onClear;
				ObservableList<T>.OnClear value2 = (ObservableList<!0>.OnClear)Delegate.Remove(onClear2, value);
				onClear = Interlocked.CompareExchange<ObservableList<T>.OnClear>(ref this.onClear, value2, onClear2);
			}
			while (onClear != onClear2);
		}
	}

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x06000EAC RID: 3756 RVA: 0x0004C3EC File Offset: 0x0004A5EC
	// (remove) Token: 0x06000EAD RID: 3757 RVA: 0x0004C424 File Offset: 0x0004A624
	public event ObservableList<!0>.OnModify onModify
	{
		[CompilerGenerated]
		add
		{
			ObservableList<T>.OnModify onModify = this.onModify;
			ObservableList<T>.OnModify onModify2;
			do
			{
				onModify2 = onModify;
				ObservableList<T>.OnModify value2 = (ObservableList<!0>.OnModify)Delegate.Combine(onModify2, value);
				onModify = Interlocked.CompareExchange<ObservableList<T>.OnModify>(ref this.onModify, value2, onModify2);
			}
			while (onModify != onModify2);
		}
		[CompilerGenerated]
		remove
		{
			ObservableList<T>.OnModify onModify = this.onModify;
			ObservableList<T>.OnModify onModify2;
			do
			{
				onModify2 = onModify;
				ObservableList<T>.OnModify value2 = (ObservableList<!0>.OnModify)Delegate.Remove(onModify2, value);
				onModify = Interlocked.CompareExchange<ObservableList<T>.OnModify>(ref this.onModify, value2, onModify2);
			}
			while (onModify != onModify2);
		}
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x0004C45C File Offset: 0x0004A65C
	public new void Add(T item)
	{
		base.Add(item);
		ObservableList<!0>.OnAdd onAdd = this.onAdd;
		if (onAdd != null)
		{
			onAdd(item);
		}
		item.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
		{
			ObservableList<!0>.OnModify onModify = this.onModify;
			if (onModify == null)
			{
				return;
			}
			onModify(item, e);
		};
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x00013F3F File Offset: 0x0001213F
	public new void Remove(T item)
	{
		base.Remove(item);
		ObservableList<!0>.OnRemove onRemove = this.onRemove;
		if (onRemove == null)
		{
			return;
		}
		onRemove(item);
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x00013F5A File Offset: 0x0001215A
	public new void Clear()
	{
		base.Clear();
		ObservableList<!0>.OnClear onClear = this.onClear;
		if (onClear == null)
		{
			return;
		}
		onClear();
	}

	// Token: 0x020001EE RID: 494
	// (Invoke) Token: 0x06000EB3 RID: 3763
	public delegate void OnAdd(T item);

	// Token: 0x020001EF RID: 495
	// (Invoke) Token: 0x06000EB7 RID: 3767
	public delegate void OnRemove(T item);

	// Token: 0x020001F0 RID: 496
	// (Invoke) Token: 0x06000EBB RID: 3771
	public delegate void OnClear();

	// Token: 0x020001F1 RID: 497
	// (Invoke) Token: 0x06000EBF RID: 3775
	public delegate void OnModify(T item, PropertyChangedEventArgs e);
}
