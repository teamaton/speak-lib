using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace SpeakFriend.Utilities.Wpf.Application
{
	public class SingleInstanceController : WindowsFormsApplicationBase
	{
		private readonly Func<Form> _mainForm;

		public SingleInstanceController()
		{
			// Set whether the application is single instance
			IsSingleInstance = true;
		}

		public SingleInstanceController(Func<Form> mainForm) : this()
		{
			_mainForm = mainForm;
		}

		protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
		{
			base.OnStartupNextInstance(eventArgs);
			eventArgs.BringToForeground = true; // works only if main form is visible
		}

		protected override void OnCreateMainForm()
		{
			// Instantiate your main application form
			MainForm = _mainForm();
		}
	}

}