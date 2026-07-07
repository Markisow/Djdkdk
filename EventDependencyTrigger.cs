using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using DG.Tweening;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class EventDependencyTrigger : MonoBehaviour
{
	// Token: 0x06000027 RID: 39 RVA: 0x00015A34 File Offset: 0x00013C34
	private void Start()
	{
		foreach (string eventName in this.dependencyEvents.Keys)
		{
			EventManager.AddEventListener(eventName, new Action<Dictionary<string, object>>(this.OnDependencyEvent));
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00015A98 File Offset: 0x00013C98
	private void OnDestroy()
	{
		Tween tween = this.timeoutTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		foreach (string eventName in this.dependencyEvents.Keys)
		{
			EventManager.RemoveEventListener(eventName, new Action<Dictionary<string, object>>(this.OnDependencyEvent));
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00015B0C File Offset: 0x00013D0C
	private void OnDependencyEvent(Dictionary<string, object> message)
	{
		string key = (string)message["eventName"];
		if (!this.dependencyEvents.ContainsValue(true))
		{
			Tween tween = this.timeoutTween;
			if (tween != null)
			{
				tween.Kill(false);
			}
			this.timeoutTween = DOVirtual.DelayedCall(this.timeout, delegate
			{
				EventDependencyTrigger.Logger.Warning("Event " + this.triggerEventName + " timed out waiting for dependencies");
				if (this.isRepeating)
				{
					this.Reset();
				}
			}, true);
		}
		this.dependencyEvents[key] = true;
		if (!this.dependencyEvents.ContainsValue(false))
		{
			EventDependencyTrigger.Logger.Info("All dependencies met, triggering event " + this.triggerEventName);
			EventManager.TriggerEvent(this.triggerEventName, null);
			if (this.isRepeating)
			{
				this.Reset();
			}
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00008AFD File Offset: 0x00006CFD
	private void Reset()
	{
		Tween tween = this.timeoutTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.dependencyEvents.Keys.ToList<string>().ForEach(delegate(string key)
		{
			this.dependencyEvents[key] = false;
		});
	}

	// Token: 0x04000014 RID: 20
	private static readonly global::Logger Logger = new global::Logger("EventDependencyTrigger");

	// Token: 0x04000015 RID: 21
	[Header("Settings")]
	[SerializeField]
	private SerializedDictionary<string, bool> dependencyEvents = new SerializedDictionary<string, bool>();

	// Token: 0x04000016 RID: 22
	[SerializeField]
	private string triggerEventName;

	// Token: 0x04000017 RID: 23
	[SerializeField]
	private float timeout = 3f;

	// Token: 0x04000018 RID: 24
	[SerializeField]
	private bool isRepeating = true;

	// Token: 0x04000019 RID: 25
	private Tween timeoutTween;
}
