using System;
using System.IO;
using System.Text;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public static class LogManager
{
	// Token: 0x06000619 RID: 1561 RVA: 0x0002F808 File Offset: 0x0002DA08
	public static void Initialize()
	{
		Debug.unityLogger.logHandler = new LogManager.LogHandler(Debug.unityLogger.logHandler);
		if (!Directory.Exists(LogManager.logDirectoryPath))
		{
			Directory.CreateDirectory(LogManager.logDirectoryPath);
		}
		try
		{
			LogManager.streamWriter = new StreamWriter(LogManager.logFilePath, false, Encoding.UTF8);
			LogManager.streamWriter.AutoFlush = true;
		}
		catch (Exception ex)
		{
			LogManager.Logger.Error("Failed to initialize StreamWriter: " + ex.Message);
			LogManager.streamWriter = null;
		}
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0002F89C File Offset: 0x0002DA9C
	public static void Dispose()
	{
		object obj = LogManager.streamWriterLock;
		lock (obj)
		{
			if (LogManager.streamWriter != null)
			{
				LogManager.streamWriter.Flush();
				LogManager.streamWriter.Close();
				LogManager.streamWriter = null;
			}
		}
	}

	// Token: 0x040003D4 RID: 980
	private static readonly global::Logger Logger = new global::Logger("LogManager");

	// Token: 0x040003D5 RID: 981
	private static string logFileRawPath = Utils.GetCommandLineArgument("--logPath", null) ?? "./Logs/Puck.log";

	// Token: 0x040003D6 RID: 982
	private static string logFilePath = Path.GetFullPath(LogManager.logFileRawPath);

	// Token: 0x040003D7 RID: 983
	private static string logDirectoryPath = Path.GetDirectoryName(LogManager.logFilePath);

	// Token: 0x040003D8 RID: 984
	private static StreamWriter streamWriter;

	// Token: 0x040003D9 RID: 985
	private static readonly object streamWriterLock = new object();

	// Token: 0x020000C8 RID: 200
	private class LogHandler : ILogHandler
	{
		// Token: 0x0600061C RID: 1564 RVA: 0x0000CD72 File Offset: 0x0000AF72
		public LogHandler(ILogHandler blh)
		{
			this.baseLogHandler = blh;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0002F958 File Offset: 0x0002DB58
		private static string GetColor(LogType type)
		{
			if (Application.isEditor)
			{
				switch (type)
				{
				case LogType.Error:
					return "<color=red>";
				case LogType.Assert:
					return "<color=magenta>";
				case LogType.Warning:
					return "<color=yellow>";
				case LogType.Exception:
					return "<color=red>";
				}
				return "<color=grey>";
			}
			switch (type)
			{
			case LogType.Error:
				return "\u001b[31m";
			case LogType.Assert:
				return "\u001b[35m";
			case LogType.Warning:
				return "\u001b[33m";
			case LogType.Exception:
				return "\u001b[91m";
			}
			return "\u001b[90m";
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000CD81 File Offset: 0x0000AF81
		private static string GetReset()
		{
			if (Application.isEditor)
			{
				return "</color>";
			}
			return "\u001b[0m";
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0002F9F4 File Offset: 0x0002DBF4
		[HideInCallstack]
		public void LogFormat(LogType type, UnityEngine.Object context, string format, params object[] args)
		{
			string text;
			switch (type)
			{
			case LogType.Error:
				text = "ERROR";
				break;
			case LogType.Assert:
				text = "ASSERT";
				break;
			case LogType.Warning:
				text = "WARNING";
				break;
			case LogType.Log:
				text = "INFO";
				break;
			case LogType.Exception:
				text = "EXCEPTION";
				break;
			default:
				text = "UNKNOWN";
				break;
			}
			object streamWriterLock = LogManager.streamWriterLock;
			lock (streamWriterLock)
			{
				if (LogManager.streamWriter != null)
				{
					string arg = (args.Length != 0) ? string.Format(format, args) : format;
					LogManager.streamWriter.WriteLine(string.Format("[{0:yyyy-MM-dd HH:mm:ss.fff}] [{1}] {2}", DateTime.UtcNow, text, arg));
				}
			}
			string color = LogManager.LogHandler.GetColor(type);
			string reset = LogManager.LogHandler.GetReset();
			this.baseLogHandler.LogFormat(type, context, string.Format("{0}[{1:yyyy-MM-dd HH:mm:ss.fff}] [{2}]{3} {4}", new object[]
			{
				color,
				DateTime.UtcNow,
				text,
				reset,
				format
			}), args);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000CD95 File Offset: 0x0000AF95
		public void LogException(Exception exception, UnityEngine.Object context)
		{
			this.baseLogHandler.LogException(exception, context);
		}

		// Token: 0x040003DA RID: 986
		private ILogHandler baseLogHandler;
	}
}
