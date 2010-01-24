namespace SpeakFriend.Utilities
{
	public abstract class ButtonGenerator
	{
		public abstract ButtonGenerator SetBackground(string pathToBackground);
		public abstract ButtonGenerator SetHeader(string header);
		public abstract ButtonGenerator SetText(string text);
		public abstract void DrawButton(string outputPath);
	}
}