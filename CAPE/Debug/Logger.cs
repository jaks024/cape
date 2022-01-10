using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Debug
{
	public static class Logger
	{
		public static DebugUIManager DebugUIManager { get; set; }
	
		public static void Print(string message, params object[] args)
		{
			System.Diagnostics.Debug.WriteLine(message, args);
		}

		public static void PrintToUI(string message, params object[] args)
		{
			DebugUIManager?.MsgConsole(message, args);
		}
	}
}
