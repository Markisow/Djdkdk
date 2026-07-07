using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x020000A4 RID: 164
public static class EventManager
{
	// Token: 0x0600054D RID: 1357 RVA: 0x0000895D File Offset: 0x00006B5D
	public static void Initialize()
	{
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0000895D File Offset: 0x00006B5D
	public static void Dispose()
	{
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0002CD54 File Offset: 0x0002AF54
	public static void AddEventListener(string eventName, Action<Dictionary<string, object>> listener)
	{
		if (!EventManager.events.ContainsKey(eventName))
		{
			EventManager.events.Add(eventName, null);
		}
		Dictionary<string, Action<Dictionary<string, object>>> dictionary = EventManager.events;
		dictionary[eventName] = (Action<Dictionary<string, object>>)Delegate.Combine(dictionary[eventName], listener);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0002CD9C File Offset: 0x0002AF9C
	public static void RemoveEventListener(string eventName, Action<Dictionary<string, object>> listener)
	{
		if (!EventManager.events.ContainsKey(eventName))
		{
			EventManager.Logger.Warning("Tried to remove listener for event " + eventName + ", but no listener was registered for it");
			return;
		}
		Dictionary<string, Action<Dictionary<string, object>>> dictionary = EventManager.events;
		dictionary[eventName] = (Action<Dictionary<string, object>>)Delegate.Remove(dictionary[eventName], listener);
		if (EventManager.events[eventName] == null)
		{
			EventManager.events.Remove(eventName);
		}
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0002CE0C File Offset: 0x0002B00C
	public static void TriggerEvent(string eventName, Dictionary<string, object> message = null)
	{
		if (message == null)
		{
			message = new Dictionary<string, object>
			{
				{
					"eventName",
					eventName
				}
			};
		}
		else if (!message.ContainsKey("eventName"))
		{
			message.Add("eventName", eventName);
		}
		if (!EventManager.events.ContainsKey(eventName))
		{
			return;
		}
		bool flag = eventName.StartsWith("Event_Server_");
		bool flag2 = eventName.StartsWith("Event_Client_");
		bool flag3 = eventName.StartsWith("Event_Everyone_");
		if ((flag2 || flag || flag3) && (!NetworkManager.Singleton || !NetworkManager.Singleton.IsListening))
		{
			EventManager.Logger.Warning("Triggering network event " + eventName + " without a NetworkManager listening");
		}
		Action<Dictionary<string, object>> action = EventManager.events[eventName];
		if (action == null)
		{
			return;
		}
		action(message);
	}

	// Token: 0x04000341 RID: 833
	private static readonly Logger Logger = new Logger("EventManager");

	// Token: 0x04000342 RID: 834
	private static Dictionary<string, Action<Dictionary<string, object>>> events = new Dictionary<string, Action<Dictionary<string, object>>>();
}
