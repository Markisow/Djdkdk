using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

// Token: 0x020000CA RID: 202
public abstract class BasePlugin<T> where T : BasePluginState, new()
{
	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000626 RID: 1574 RVA: 0x0000CE18 File Offset: 0x0000B018
	// (set) Token: 0x06000627 RID: 1575 RVA: 0x0002FB1C File Offset: 0x0002DD1C
	public T State
	{
		get
		{
			return this.state;
		}
		set
		{
			if (this.state.Equals(value))
			{
				return;
			}
			T oldState = this.state;
			this.state = value;
			this.OnStateChanged(oldState, this.state);
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000CE20 File Offset: 0x0000B020
	public string Path
	{
		get
		{
			return this.state.Path;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000CE32 File Offset: 0x0000B032
	public bool IsReady
	{
		get
		{
			return this.state.IsReady;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000CE44 File Offset: 0x0000B044
	public bool IsEnabled
	{
		get
		{
			return this.state.IsEnabled;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000CE56 File Offset: 0x0000B056
	public bool HasAssembly
	{
		get
		{
			return this.assemblyPath != null;
		}
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0000CE61 File Offset: 0x0000B061
	public BasePlugin(T state)
	{
		this.state = state;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0000CE70 File Offset: 0x0000B070
	public virtual void Initialize()
	{
		this.assemblyPath = this.GetAssemblyPath();
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0000CE7E File Offset: 0x0000B07E
	public virtual void Dispose()
	{
		if (this.IsEnabled)
		{
			this.Disable();
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0002FB60 File Offset: 0x0002DD60
	public virtual void SetState(Dictionary<string, object> updates)
	{
		T t = Activator.CreateInstance<T>();
		t.Path = (updates.ContainsKey("path") ? ((string)updates["path"]) : this.state.Path);
		t.IsReady = (updates.ContainsKey("isReady") ? ((bool)updates["isReady"]) : this.state.IsReady);
		t.IsEnabled = (updates.ContainsKey("isEnabled") ? ((bool)updates["isEnabled"]) : this.state.IsEnabled);
		T t2 = t;
		this.State = t2;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0000CE8F File Offset: 0x0000B08F
	private string GetAssemblyPath()
	{
		if (this.Path == null || !Directory.Exists(this.Path))
		{
			return null;
		}
		return Directory.GetFiles(this.Path, "*.dll", SearchOption.TopDirectoryOnly).FirstOrDefault<string>();
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0002FC28 File Offset: 0x0002DE28
	private void LoadAssembly(string path)
	{
		if (this.instance != null)
		{
			return;
		}
		this.assembly = Assembly.LoadFile(path);
		Type type2 = this.assembly.GetTypes().FirstOrDefault((Type type) => type.IsClass && !type.IsAbstract && typeof(IPuckPlugin).IsAssignableFrom(type));
		if (type2 == null)
		{
			throw new Exception("IPuckPlugin missing from assembly");
		}
		this.instance = Activator.CreateInstance(type2);
		this.onEnableMethod = type2.GetMethod("OnEnable");
		this.onDisableMethod = type2.GetMethod("OnDisable");
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0000CEBE File Offset: 0x0000B0BE
	private void UnloadAssembly()
	{
		this.instance = null;
		this.assembly = null;
		this.onEnableMethod = null;
		this.onDisableMethod = null;
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0002FCBC File Offset: 0x0002DEBC
	public bool Enable()
	{
		if (this.IsEnabled)
		{
			return false;
		}
		if (!this.IsReady)
		{
			return false;
		}
		try
		{
			if (this.HasAssembly)
			{
				this.LoadAssembly(this.assemblyPath);
				if (!(bool)this.onEnableMethod.Invoke(this.instance, null))
				{
					throw new Exception("OnEnable returned false");
				}
			}
			this.SetState(new Dictionary<string, object>
			{
				{
					"isEnabled",
					true
				}
			});
		}
		catch (Exception exception)
		{
			this.OnEnableFailed(exception);
			return false;
		}
		return true;
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0002FD54 File Offset: 0x0002DF54
	public bool Disable()
	{
		if (!this.IsEnabled)
		{
			return false;
		}
		try
		{
			if (this.instance != null)
			{
				if (!(bool)this.onDisableMethod.Invoke(this.instance, null))
				{
					throw new Exception("OnDisable returned false");
				}
				this.UnloadAssembly();
			}
			this.SetState(new Dictionary<string, object>
			{
				{
					"isEnabled",
					false
				}
			});
		}
		catch (Exception exception)
		{
			this.OnDisableFailed(exception);
			return false;
		}
		return true;
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0000895D File Offset: 0x00006B5D
	public virtual void OnEnableFailed(Exception exception)
	{
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0000895D File Offset: 0x00006B5D
	public virtual void OnDisableFailed(Exception exception)
	{
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0000CEDC File Offset: 0x0000B0DC
	protected virtual void OnStateChanged(T oldState, T newState)
	{
		if (oldState.Path != newState.Path)
		{
			this.assemblyPath = this.GetAssemblyPath();
		}
	}

	// Token: 0x040003DE RID: 990
	private static readonly Logger Logger = new Logger("BasePlugin");

	// Token: 0x040003DF RID: 991
	protected T state;

	// Token: 0x040003E0 RID: 992
	private string assemblyPath;

	// Token: 0x040003E1 RID: 993
	private Assembly assembly;

	// Token: 0x040003E2 RID: 994
	private object instance;

	// Token: 0x040003E3 RID: 995
	private MethodInfo onEnableMethod;

	// Token: 0x040003E4 RID: 996
	private MethodInfo onDisableMethod;
}
