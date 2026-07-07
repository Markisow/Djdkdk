using System;
using System.Collections.Generic;

// Token: 0x02000099 RID: 153
public static class CameraManager
{
	// Token: 0x06000509 RID: 1289 RVA: 0x0000C179 File Offset: 0x0000A379
	public static void Initialize()
	{
		CameraManagerController.Initialize();
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0000C180 File Offset: 0x0000A380
	public static void Dispose()
	{
		CameraManagerController.Dispose();
		CameraManager.DisableAllCameras();
		CameraManager.cameras.Clear();
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0002B86C File Offset: 0x00029A6C
	public static void RegisterCamera(BaseCamera camera)
	{
		if (CameraManager.cameras.Contains(camera))
		{
			return;
		}
		CameraManager.cameras.Add(camera);
		EventManager.TriggerEvent("Event_OnCameraRegistered", new Dictionary<string, object>
		{
			{
				"camera",
				camera
			}
		});
		if (CameraManager.IsActiveCamera(camera))
		{
			CameraManager.EnableCamera(camera);
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0002B8BC File Offset: 0x00029ABC
	public static void UnregisterCamera(BaseCamera camera)
	{
		if (CameraManager.cameras.Contains(camera))
		{
			CameraManager.cameras.Remove(camera);
		}
		if (CameraManager.activeCamera == camera)
		{
			CameraManager.activeCameraType = CameraType.None;
			CameraManager.activeCameraOwnerClientId = null;
			CameraManager.activeCamera = null;
		}
		EventManager.TriggerEvent("Event_OnCameraUnregistered", new Dictionary<string, object>
		{
			{
				"camera",
				camera
			}
		});
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0002B924 File Offset: 0x00029B24
	public static BaseCamera GetCameraByType(CameraType cameraType)
	{
		return CameraManager.cameras.Find((BaseCamera camera) => camera.Type == cameraType);
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0002B954 File Offset: 0x00029B54
	public static BaseCamera GetCameraByOwnerClientId(ulong ownerClientId)
	{
		return CameraManager.cameras.Find((BaseCamera camera) => camera.OwnerClientId == ownerClientId);
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x0000C196 File Offset: 0x0000A396
	public static BaseCamera GetActiveCamera()
	{
		return CameraManager.cameras.Find((BaseCamera camera) => CameraManager.IsActiveCamera(camera));
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x0002B984 File Offset: 0x00029B84
	public static void SetActiveCamera(CameraType type, ulong? ownerClientId = null)
	{
		CameraManager.Logger.Info(string.Format("Setting active camera to type {0}", type));
		CameraManager.activeCameraType = type;
		CameraManager.activeCameraOwnerClientId = ownerClientId;
		BaseCamera baseCamera = CameraManager.GetActiveCamera();
		if (baseCamera != null)
		{
			CameraManager.EnableCamera(baseCamera);
		}
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0002B9CC File Offset: 0x00029BCC
	public static bool IsActiveCamera(BaseCamera camera)
	{
		if (CameraManager.activeCameraType != camera.Type)
		{
			return false;
		}
		if (CameraManager.activeCameraOwnerClientId != null)
		{
			ulong? num = CameraManager.activeCameraOwnerClientId;
			ulong ownerClientId = camera.OwnerClientId;
			return num.GetValueOrDefault() == ownerClientId & num != null;
		}
		return true;
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
	public static void EnableCamera(BaseCamera camera)
	{
		if (camera.IsEnabled)
		{
			return;
		}
		CameraManager.DisableAllCameras();
		CameraManager.Logger.Info(string.Format("Enabling camera of type {0}", camera.Type));
		camera.Enable();
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0000C1F7 File Offset: 0x0000A3F7
	public static void DisableCamera(BaseCamera camera)
	{
		if (!camera.IsEnabled)
		{
			return;
		}
		CameraManager.Logger.Info(string.Format("Disabling camera of type {0}", camera.Type));
		camera.Disable();
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0002BA18 File Offset: 0x00029C18
	public static void DisableAllCameras()
	{
		foreach (BaseCamera camera in CameraManager.cameras)
		{
			CameraManager.DisableCamera(camera);
		}
	}

	// Token: 0x0400031C RID: 796
	private static readonly Logger Logger = new Logger("CameraManager");

	// Token: 0x0400031D RID: 797
	private static List<BaseCamera> cameras = new List<BaseCamera>();

	// Token: 0x0400031E RID: 798
	private static BaseCamera activeCamera = null;

	// Token: 0x0400031F RID: 799
	private static CameraType activeCameraType = CameraType.None;

	// Token: 0x04000320 RID: 800
	private static ulong? activeCameraOwnerClientId = null;
}
