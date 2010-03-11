using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	public interface IRenderedButton
	{
		string GetImagePath();
	}

	public abstract class RenderedButton<T> : IRenderedButton where T : ButtonGenerator, new()
	{
		private static readonly Dictionary<string, Size> _sizes = new Dictionary<string, Size>();

		public string Header { get; set; }
		public string Text { get; set; }
		public ButtonEffect Effect { get; set; }
		protected abstract string Identifier { get; }
        protected abstract string RenderedButtonsDirAbsolute { get; }
        protected abstract string RenderedButtonsDirRelative { get; }

		protected string Suffix
		{
			get
			{
				switch (Effect)
				{
					case ButtonEffect.Normal:
						return String.Empty;

					case ButtonEffect.Hover:
						return String.Intern("_on");
				}
				return String.Empty;
			}
		}

		protected string ImageName
		{
			get { return string.Format("{0}{1}.png", Identifier, Suffix); }
		}

		private string ImagePathAbsolute
		{
			get { return string.Format("{0}\\{1}", RenderedButtonsDirAbsolute, ImageName); }
		}

		private string ImagePathRelative
		{
			get { return string.Format("{0}/{1}", RenderedButtonsDirRelative.EnsureStartsWith("/"), ImageName); }
		}

		protected abstract string PathToBackground{ get;}

		public Size Size
		{
			get
			{
				Size result;
				if (!_sizes.TryGetValue(Identifier, out result))
				{
					if (File.Exists(ImagePathAbsolute))
					{
						var image = Image.FromFile(ImagePathAbsolute);
						result = image.Size;
						_sizes.Add(Identifier, result);
					}
				}
				return result;
			}
		}

		public string GetImageName()
		{
			EnsureExists();
			return ImageName;
		}

		public string GetImagePath()
		{
			EnsureExists();
			return ImagePathRelative;
		}

		private void EnsureExists()
		{
			if (!File.Exists(ImagePathAbsolute))
				new T().SetBackground(PathToBackground).SetHeader(Header).SetText(Text).DrawButton(ImagePathAbsolute);
		}
	}
}