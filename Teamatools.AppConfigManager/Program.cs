using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teamatools.AppConfigManager
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 2)
			{
				ShowHelp();
				return;
			}

			var siteName = args[0];
			BindingsAction action;
			if (!Enum.TryParse(args[1], true, out action))
				Console.Error.WriteLine("Operation {0} not supported.", args[1]);

			if (action != BindingsAction.Show && args.Length < 3)
			{
				ShowHelp();
				return;
			}

			var hostNames = args.Skip(2).Take(args.Count() - 2);

			ApplicationHostConfig.AddBindings(siteName, hostNames, action);
		}

		private static void ShowHelp()
		{
			Console.WriteLine();
			Console.WriteLine("\tShow, add or remove bindings for an IIS website.");
			Console.WriteLine();
			Console.WriteLine("\tUsage: {0} <sitename> show|add|remove [<hostname1, hostname2, ...>]", Environment.GetCommandLineArgs()[0]);
		}
	}
}
