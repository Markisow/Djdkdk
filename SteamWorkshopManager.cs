using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DG.Tweening;
using Steamworks;

// Token: 0x02000144 RID: 324
public static class SteamWorkshopManager
{
	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060009A7 RID: 2471 RVA: 0x000101B4 File Offset: 0x0000E3B4
	public static string[] ItemIds
	{
		get
		{
			return (from item in SteamWorkshopManager.Items
			select item.Id).ToArray<string>();
		}
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x000101E4 File Offset: 0x0000E3E4
	public static void Initialize()
	{
		SteamWorkshopManager.RegisterCallbacks();
		SteamWorkshopManagerController.Initialize();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00039C7C File Offset: 0x00037E7C
	public static void Dispose()
	{
		SteamWorkshopManager.ugcQueryCompletedCallResultMap.Values.ToList<CallResult<SteamUGCQueryCompleted_t>>().ForEach(delegate(CallResult<SteamUGCQueryCompleted_t> callResult)
		{
			callResult.Dispose();
		});
		SteamWorkshopManager.ugcQueryCompletedCallResultMap.Clear();
		SteamWorkshopManager.debouncedGetItemDetailsItemIds.Clear();
		Tween tween = SteamWorkshopManager.getItemDetailsDebounceTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		SteamWorkshopManagerController.Dispose();
		SteamWorkshopManager.UnregisterCallbacks();
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00039CEC File Offset: 0x00037EEC
	private static void RegisterCallbacks()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamWorkshopManager.DownloadItemResult = Callback<DownloadItemResult_t>.CreateGameServer(new Callback<DownloadItemResult_t>.DispatchDelegate(SteamWorkshopManager.OnDownloadItemResult));
			SteamWorkshopManager.UserSubscribedItemsListChanged = Callback<UserSubscribedItemsListChanged_t>.CreateGameServer(new Callback<UserSubscribedItemsListChanged_t>.DispatchDelegate(SteamWorkshopManager.OnUserSubscribedItemsListChanged));
			SteamWorkshopManager.RemoteStorageSubscribePublishedFileResult = Callback<RemoteStorageSubscribePublishedFileResult_t>.CreateGameServer(new Callback<RemoteStorageSubscribePublishedFileResult_t>.DispatchDelegate(SteamWorkshopManager.OnRemoteStorageSubscribePublishedFileResult));
			SteamWorkshopManager.RemoteStorageUnsubscribePublishedFileResult = Callback<RemoteStorageUnsubscribePublishedFileResult_t>.CreateGameServer(new Callback<RemoteStorageUnsubscribePublishedFileResult_t>.DispatchDelegate(SteamWorkshopManager.OnRemoteStorageUnsubscribePublishedFileResult));
			return;
		}
		SteamWorkshopManager.DownloadItemResult = Callback<DownloadItemResult_t>.Create(new Callback<DownloadItemResult_t>.DispatchDelegate(SteamWorkshopManager.OnDownloadItemResult));
		SteamWorkshopManager.UserSubscribedItemsListChanged = Callback<UserSubscribedItemsListChanged_t>.Create(new Callback<UserSubscribedItemsListChanged_t>.DispatchDelegate(SteamWorkshopManager.OnUserSubscribedItemsListChanged));
		SteamWorkshopManager.RemoteStorageSubscribePublishedFileResult = Callback<RemoteStorageSubscribePublishedFileResult_t>.Create(new Callback<RemoteStorageSubscribePublishedFileResult_t>.DispatchDelegate(SteamWorkshopManager.OnRemoteStorageSubscribePublishedFileResult));
		SteamWorkshopManager.RemoteStorageUnsubscribePublishedFileResult = Callback<RemoteStorageUnsubscribePublishedFileResult_t>.Create(new Callback<RemoteStorageUnsubscribePublishedFileResult_t>.DispatchDelegate(SteamWorkshopManager.OnRemoteStorageUnsubscribePublishedFileResult));
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x000101F0 File Offset: 0x0000E3F0
	private static void UnregisterCallbacks()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamWorkshopManager.DownloadItemResult.Unregister();
		SteamWorkshopManager.UserSubscribedItemsListChanged.Unregister();
		SteamWorkshopManager.RemoteStorageSubscribePublishedFileResult.Unregister();
		SteamWorkshopManager.RemoteStorageUnsubscribePublishedFileResult.Unregister();
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00039DBC File Offset: 0x00037FBC
	private static SteamWorkshopItem AddItem(string id, string path = null)
	{
		if (SteamWorkshopManager.GetItemById(id) != null)
		{
			return null;
		}
		SteamWorkshopItem steamWorkshopItem = new SteamWorkshopItem(id, path);
		SteamWorkshopManager.Items.Add(steamWorkshopItem);
		steamWorkshopItem.Initialize();
		EventManager.TriggerEvent("Event_OnSteamWorkshopItemAdded", new Dictionary<string, object>
		{
			{
				"item",
				steamWorkshopItem
			}
		});
		return steamWorkshopItem;
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00039E08 File Offset: 0x00038008
	private static SteamWorkshopItem RemoveItem(string id)
	{
		SteamWorkshopItem itemById = SteamWorkshopManager.GetItemById(id);
		if (itemById == null)
		{
			return null;
		}
		SteamWorkshopManager.Items.Remove(itemById);
		itemById.Dispose();
		EventManager.TriggerEvent("Event_OnSteamWorkshopItemRemoved", new Dictionary<string, object>
		{
			{
				"item",
				itemById
			}
		});
		return itemById;
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00039E50 File Offset: 0x00038050
	public static SteamWorkshopItem GetItemById(string id)
	{
		return SteamWorkshopManager.Items.Find((SteamWorkshopItem item) => item.Id == id);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x00039E80 File Offset: 0x00038080
	public static void VerifyIntegrity()
	{
		string[] subscribedItemIds = SteamWorkshopManager.GetSubscribedItemIds();
		string[] array = SteamWorkshopManager.ItemIds.Union(subscribedItemIds).ToArray<string>();
		for (int i = 0; i < array.Length; i++)
		{
			SteamWorkshopManager.VerifyItemIntegrity(array[i]);
		}
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x00039EBC File Offset: 0x000380BC
	public static void VerifyItemIntegrity(string itemId)
	{
		SteamWorkshopItem steamWorkshopItem = SteamWorkshopManager.GetItemById(itemId);
		if (!SteamWorkshopManager.IsItemSubscribed(itemId) && !ApplicationManager.IsDedicatedGameServer)
		{
			if (!SteamWorkshopManager.IsItemSubscribed(itemId) && !ApplicationManager.IsDedicatedGameServer && steamWorkshopItem != null)
			{
				SteamWorkshopManager.RemoveItem(itemId);
			}
			return;
		}
		if (steamWorkshopItem == null)
		{
			steamWorkshopItem = SteamWorkshopManager.AddItem(itemId, null);
		}
		if (!SteamWorkshopManager.IsItemInstalled(itemId))
		{
			steamWorkshopItem.SetState(new Dictionary<string, object>
			{
				{
					"phase",
					SteamWorkshopItemPhase.Downloading
				}
			});
			SteamWorkshopManager.DownloadItem(itemId);
			return;
		}
		if (SteamWorkshopManager.IsItemNeedsUpdate(itemId))
		{
			steamWorkshopItem.SetState(new Dictionary<string, object>
			{
				{
					"phase",
					SteamWorkshopItemPhase.Updating
				}
			});
			SteamWorkshopManager.DownloadItem(itemId);
			return;
		}
		string value;
		if (SteamWorkshopManager.GetItemInstallInfo(itemId, out value))
		{
			steamWorkshopItem.SetState(new Dictionary<string, object>
			{
				{
					"path",
					value
				},
				{
					"phase",
					SteamWorkshopItemPhase.Installed
				}
			});
			return;
		}
		SteamWorkshopManager.Logger.Error("Failed to get install info for item " + itemId);
	}

	// Token: 0x060009B1 RID: 2481 RVA: 0x00010222 File Offset: 0x0000E422
	public static bool IsItemInstalled(string itemId)
	{
		return SteamManager.IsInitialized && (SteamWorkshopManager.GetItemState(itemId) & 4U) > 0U;
	}

	// Token: 0x060009B2 RID: 2482 RVA: 0x00010238 File Offset: 0x0000E438
	public static bool IsItemSubscribed(string itemId)
	{
		return SteamManager.IsInitialized && (SteamWorkshopManager.GetItemState(itemId) & 1U) > 0U;
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0001024E File Offset: 0x0000E44E
	public static bool IsItemNeedsUpdate(string itemId)
	{
		return SteamManager.IsInitialized && (SteamWorkshopManager.GetItemState(itemId) & 8U) > 0U;
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x00010264 File Offset: 0x0000E464
	public static uint GetNumSubscribedItems()
	{
		if (!SteamManager.IsInitialized)
		{
			return 0U;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.GetNumSubscribedItems(false);
		}
		return SteamUGC.GetNumSubscribedItems(false);
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00039FA8 File Offset: 0x000381A8
	public static string[] GetSubscribedItemIds()
	{
		if (!SteamManager.IsInitialized)
		{
			return null;
		}
		uint numSubscribedItems = SteamWorkshopManager.GetNumSubscribedItems();
		PublishedFileId_t[] array = new PublishedFileId_t[numSubscribedItems];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamGameServerUGC.GetSubscribedItems(array, numSubscribedItems, false);
		}
		else
		{
			SteamUGC.GetSubscribedItems(array, numSubscribedItems, false);
		}
		return (from id in array
		select id.m_PublishedFileId.ToString()).ToArray<string>();
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0003A010 File Offset: 0x00038210
	public static bool GetItemInstallInfo(string itemId, out string path)
	{
		path = null;
		if (!SteamManager.IsInitialized)
		{
			return false;
		}
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(itemId));
		if (ApplicationManager.IsDedicatedGameServer)
		{
			ulong num;
			uint num2;
			return SteamGameServerUGC.GetItemInstallInfo(nPublishedFileID, out num, out path, 4096U, out num2);
		}
		ulong num3;
		uint num4;
		return SteamUGC.GetItemInstallInfo(nPublishedFileID, out num3, out path, 4096U, out num4);
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0003A060 File Offset: 0x00038260
	public static uint GetItemState(string itemId)
	{
		if (!SteamManager.IsInitialized)
		{
			return 0U;
		}
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(itemId));
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.GetItemState(nPublishedFileID);
		}
		return SteamUGC.GetItemState(nPublishedFileID);
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0003A098 File Offset: 0x00038298
	public static void GetItemDetails(params string[] itemIds)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		if (itemIds == null || itemIds.Length == 0)
		{
			return;
		}
		SteamWorkshopManager.debouncedGetItemDetailsItemIds.AddRange(from id in itemIds
		where !SteamWorkshopManager.debouncedGetItemDetailsItemIds.Contains(id)
		select id);
		Tween tween = SteamWorkshopManager.getItemDetailsDebounceTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		SteamWorkshopManager.getItemDetailsDebounceTween = DOVirtual.DelayedCall(0f, delegate
		{
			UGCQueryHandle_t ugcqueryHandle_t = SteamWorkshopManager.CreateQueryUGCDetailsRequest(SteamWorkshopManager.debouncedGetItemDetailsItemIds.ToArray());
			SteamWorkshopManager.debouncedGetItemDetailsItemIds.Clear();
			CallResult<SteamUGCQueryCompleted_t> callResult = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate(SteamWorkshopManager.OnUGCQueryCompleted));
			SteamWorkshopManager.ugcQueryCompletedCallResultMap[ugcqueryHandle_t] = callResult;
			SteamAPICall_t steamAPICall_t = SteamWorkshopManager.SendQueryUGCRequest(ugcqueryHandle_t);
			if (steamAPICall_t == SteamAPICall_t.Invalid)
			{
				return;
			}
			callResult.Set(steamAPICall_t, null);
		}, true);
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0003A124 File Offset: 0x00038324
	public static UGCQueryHandle_t CreateQueryUGCDetailsRequest(string[] itemIds)
	{
		if (!SteamManager.IsInitialized)
		{
			return UGCQueryHandle_t.Invalid;
		}
		PublishedFileId_t[] array = new PublishedFileId_t[itemIds.Length];
		for (int i = 0; i < itemIds.Length; i++)
		{
			array[i] = new PublishedFileId_t(ulong.Parse(itemIds[i]));
		}
		UGCQueryHandle_t ugcqueryHandle_t;
		if (ApplicationManager.IsDedicatedGameServer)
		{
			ugcqueryHandle_t = SteamGameServerUGC.CreateQueryUGCDetailsRequest(array, (uint)array.Length);
			SteamGameServerUGC.SetReturnLongDescription(ugcqueryHandle_t, true);
		}
		else
		{
			ugcqueryHandle_t = SteamUGC.CreateQueryUGCDetailsRequest(array, (uint)array.Length);
			SteamUGC.SetReturnLongDescription(ugcqueryHandle_t, true);
		}
		return ugcqueryHandle_t;
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x00010283 File Offset: 0x0000E483
	public static SteamAPICall_t SendQueryUGCRequest(UGCQueryHandle_t queryHandle)
	{
		if (!SteamManager.IsInitialized)
		{
			return SteamAPICall_t.Invalid;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.SendQueryUGCRequest(queryHandle);
		}
		return SteamUGC.SendQueryUGCRequest(queryHandle);
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x000102A6 File Offset: 0x0000E4A6
	private static bool GetQueryUGCResult(UGCQueryHandle_t queryHandle, uint index, out SteamUGCDetails_t details)
	{
		details = default(SteamUGCDetails_t);
		if (!SteamManager.IsInitialized)
		{
			return false;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.GetQueryUGCResult(queryHandle, index, out details);
		}
		return SteamUGC.GetQueryUGCResult(queryHandle, index, out details);
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x000102D0 File Offset: 0x0000E4D0
	private static bool GetQueryUGCPreviewURL(UGCQueryHandle_t queryHandle, uint index, out string previewUrl)
	{
		previewUrl = null;
		if (!SteamManager.IsInitialized)
		{
			return false;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.GetQueryUGCPreviewURL(queryHandle, index, out previewUrl, 2048U);
		}
		return SteamUGC.GetQueryUGCPreviewURL(queryHandle, index, out previewUrl, 2048U);
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00010300 File Offset: 0x0000E500
	private static bool GetQueryUGCMetadata(UGCQueryHandle_t queryHandle, uint index, out string metadata)
	{
		metadata = null;
		if (!SteamManager.IsInitialized)
		{
			return false;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.GetQueryUGCMetadata(queryHandle, index, out metadata, 8000U);
		}
		return SteamUGC.GetQueryUGCMetadata(queryHandle, index, out metadata, 8000U);
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00010330 File Offset: 0x0000E530
	private static bool GetQueryUGCStatistic(UGCQueryHandle_t queryHandle, uint index, EItemStatistic eStatType, out ulong statValue)
	{
		statValue = 0UL;
		if (!SteamManager.IsInitialized)
		{
			return false;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.GetQueryUGCStatistic(queryHandle, index, eStatType, out statValue);
		}
		return SteamUGC.GetQueryUGCStatistic(queryHandle, index, eStatType, out statValue);
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0003A198 File Offset: 0x00038398
	public static bool DownloadItem(string itemId)
	{
		if (!SteamManager.IsInitialized)
		{
			return false;
		}
		SteamWorkshopManager.Logger.Info("Downloading item " + itemId);
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(itemId));
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return SteamGameServerUGC.DownloadItem(nPublishedFileID, true);
		}
		return SteamUGC.DownloadItem(nPublishedFileID, true);
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0003A1E8 File Offset: 0x000383E8
	public static void SubscribeItem(string itemId)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamWorkshopManager.Logger.Info("Subscribing item " + itemId);
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(itemId));
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamGameServerUGC.SubscribeItem(nPublishedFileID);
			return;
		}
		SteamUGC.SubscribeItem(nPublishedFileID);
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0003A238 File Offset: 0x00038438
	public static void UnsubscribeItem(string itemId)
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamWorkshopManager.Logger.Info("Unsubscribing item " + itemId);
		PublishedFileId_t nPublishedFileID = new PublishedFileId_t(ulong.Parse(itemId));
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamGameServerUGC.UnsubscribeItem(nPublishedFileID);
			return;
		}
		SteamUGC.UnsubscribeItem(nPublishedFileID);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0003A288 File Offset: 0x00038488
	private static void OnItemUpdated(object sender, PropertyChangedEventArgs e)
	{
		SteamWorkshopItem value = (SteamWorkshopItem)sender;
		EventManager.TriggerEvent("Event_OnSteamWorkshopItemUpdated", new Dictionary<string, object>
		{
			{
				"item",
				value
			}
		});
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0003A2B8 File Offset: 0x000384B8
	private static void OnDownloadItemResult(DownloadItemResult_t response)
	{
		if (response.m_unAppID != new AppId_t(2994020U))
		{
			return;
		}
		if (response.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		string value = response.m_nPublishedFileId.ToString();
		EventManager.TriggerEvent("Event_OnSteamWorkshopItemDownloaded", new Dictionary<string, object>
		{
			{
				"itemId",
				value
			}
		});
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00010359 File Offset: 0x0000E559
	private static void OnUserSubscribedItemsListChanged(UserSubscribedItemsListChanged_t response)
	{
		if (response.m_nAppID != new AppId_t(2994020U))
		{
			return;
		}
		EventManager.TriggerEvent("Event_OnSteamWorkshopSubscribedItemsListChanged", null);
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0003A318 File Offset: 0x00038518
	private static void OnRemoteStorageSubscribePublishedFileResult(RemoteStorageSubscribePublishedFileResult_t response)
	{
		if (response.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		string value = response.m_nPublishedFileId.ToString();
		EventManager.TriggerEvent("Event_OnSteamWorkshopItemSubscribed", new Dictionary<string, object>
		{
			{
				"itemId",
				value
			}
		});
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0003A360 File Offset: 0x00038560
	private static void OnRemoteStorageUnsubscribePublishedFileResult(RemoteStorageUnsubscribePublishedFileResult_t response)
	{
		if (response.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		string value = response.m_nPublishedFileId.ToString();
		EventManager.TriggerEvent("Event_OnSteamWorkshopItemUnsubscribed", new Dictionary<string, object>
		{
			{
				"itemId",
				value
			}
		});
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0003A3A8 File Offset: 0x000385A8
	private static void OnUGCQueryCompleted(SteamUGCQueryCompleted_t response, bool bIOFailure)
	{
		if (response.m_eResult != EResult.k_EResultOK)
		{
			return;
		}
		for (uint num = 0U; num < response.m_unNumResultsReturned; num += 1U)
		{
			SteamUGCDetails_t steamUGCDetails_t;
			if (SteamWorkshopManager.GetQueryUGCResult(response.m_handle, num, out steamUGCDetails_t) && steamUGCDetails_t.m_eResult == EResult.k_EResultOK)
			{
				PublishedFileId_t nPublishedFileId = steamUGCDetails_t.m_nPublishedFileId;
				string rgchTitle = steamUGCDetails_t.m_rgchTitle;
				string rgchDescription = steamUGCDetails_t.m_rgchDescription;
				int unVotesUp = (int)steamUGCDetails_t.m_unVotesUp;
				int unVotesDown = (int)steamUGCDetails_t.m_unVotesDown;
				string value;
				SteamWorkshopManager.GetQueryUGCPreviewURL(response.m_handle, num, out value);
				ulong num2;
				SteamWorkshopManager.GetQueryUGCStatistic(response.m_handle, num, EItemStatistic.k_EItemStatistic_NumSubscriptions, out num2);
				string value2;
				SteamWorkshopManager.GetQueryUGCMetadata(response.m_handle, num, out value2);
				EventManager.TriggerEvent("Event_OnSteamWorkshopItemDetails", new Dictionary<string, object>
				{
					{
						"id",
						nPublishedFileId.ToString()
					},
					{
						"title",
						rgchTitle
					},
					{
						"description",
						rgchDescription
					},
					{
						"previewUrl",
						value
					},
					{
						"subscriptions",
						(int)num2
					},
					{
						"upvotes",
						unVotesUp
					},
					{
						"downvotes",
						unVotesDown
					},
					{
						"metadata",
						value2
					}
				});
			}
		}
		if (SteamWorkshopManager.ugcQueryCompletedCallResultMap.ContainsKey(response.m_handle))
		{
			SteamWorkshopManager.ugcQueryCompletedCallResultMap[response.m_handle].Dispose();
			SteamWorkshopManager.ugcQueryCompletedCallResultMap.Remove(response.m_handle);
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamGameServerUGC.ReleaseQueryUGCRequest(response.m_handle);
			return;
		}
		SteamUGC.ReleaseQueryUGCRequest(response.m_handle);
	}

	// Token: 0x040005A7 RID: 1447
	private static readonly Logger Logger = new Logger("SteamWorkshopManager");

	// Token: 0x040005A8 RID: 1448
	public static List<SteamWorkshopItem> Items = new List<SteamWorkshopItem>();

	// Token: 0x040005A9 RID: 1449
	private static Callback<DownloadItemResult_t> DownloadItemResult;

	// Token: 0x040005AA RID: 1450
	private static Callback<UserSubscribedItemsListChanged_t> UserSubscribedItemsListChanged;

	// Token: 0x040005AB RID: 1451
	private static Callback<RemoteStorageSubscribePublishedFileResult_t> RemoteStorageSubscribePublishedFileResult;

	// Token: 0x040005AC RID: 1452
	private static Callback<RemoteStorageUnsubscribePublishedFileResult_t> RemoteStorageUnsubscribePublishedFileResult;

	// Token: 0x040005AD RID: 1453
	private static Dictionary<UGCQueryHandle_t, CallResult<SteamUGCQueryCompleted_t>> ugcQueryCompletedCallResultMap = new Dictionary<UGCQueryHandle_t, CallResult<SteamUGCQueryCompleted_t>>();

	// Token: 0x040005AE RID: 1454
	private static List<string> debouncedGetItemDetailsItemIds = new List<string>();

	// Token: 0x040005AF RID: 1455
	private static Tween getItemDetailsDebounceTween;
}
