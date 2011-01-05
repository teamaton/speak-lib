using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace Teamatools.AppConfigManager
{
	public enum BindingsAction
	{
		Undefined,
		Show,
		Add,
		Remove,
	}

	public class ApplicationHostConfig
	{
		public static void AddBindings(string siteName, IEnumerable<string> hostNames, BindingsAction action)
		{
			using (var serverManager = new ServerManager())
			{
				var site = serverManager.Sites[siteName];

				if (site == null)
					throw new InvalidOperationException(string.Format("Site with name {0} could not be found in IIS!", siteName));

				switch (action)
				{
					case BindingsAction.Show:
						Console.WriteLine("Bindings:");
						foreach (var binding in site.Bindings)
						{
							Console.WriteLine("\t{0}", binding.BindingInformation);
						}
						return;

					case BindingsAction.Add:
						foreach (var hostName in hostNames)
						{
							var name = hostName;
							if (site.Bindings.FirstOrDefault(b => b.Host == name) == null)
								site.Bindings.Add("*:80:" + hostName, "http");
						}
						break;

					case BindingsAction.Remove:
						foreach (var hostName in hostNames)
						{
							var name = hostName;
							var binding = site.Bindings.FirstOrDefault(b => b.Host == name);
							if (binding != null)
								site.Bindings.Remove(binding);
						}
						break;
				}

				serverManager.CommitChanges();

				Console.WriteLine("Bindings after edit:");
				foreach (var binding in site.Bindings)
				{
					Console.WriteLine("\t{0}", binding.BindingInformation);
				}
			}
		}
	}
}
