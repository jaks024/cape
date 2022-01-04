using System;
using System.Collections.Generic;
using System.Text;

namespace wireman
{
	public class Logger
	{

		public static void Print(string message, params object?[] args)
		{
			System.Diagnostics.Debug.WriteLine(message, args);
		}
	}
}
