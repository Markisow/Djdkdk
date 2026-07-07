using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200014D RID: 333
public class ThreadManager : MonoBehaviourSingleton<ThreadManager>
{
	// Token: 0x06000A0B RID: 2571 RVA: 0x0003B774 File Offset: 0x00039974
	private void Update()
	{
		Queue<Action> obj = this.executionQueue;
		lock (obj)
		{
			while (this.executionQueue.Count > 0)
			{
				this.executionQueue.Dequeue()();
			}
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0003B7D0 File Offset: 0x000399D0
	public void Enqueue(IEnumerator action)
	{
		Queue<Action> obj = this.executionQueue;
		lock (obj)
		{
			this.executionQueue.Enqueue(delegate
			{
				this.StartCoroutine(action);
			});
		}
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x00010570 File Offset: 0x0000E770
	public void Enqueue(Action action)
	{
		this.Enqueue(this.ActionWrapper(action));
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0001057F File Offset: 0x0000E77F
	private IEnumerator ActionWrapper(Action action)
	{
		action();
		yield return null;
		yield break;
	}

	// Token: 0x040005D7 RID: 1495
	private Queue<Action> executionQueue = new Queue<Action>();
}
