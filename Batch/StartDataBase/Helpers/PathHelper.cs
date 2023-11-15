namespace StartDataBase.Helpers
{
	public static class PathHelper
	{
		public static string GetPath()
		{
			var pathAbsolute = Path.GetFullPath("Program.cs");

			var array = pathAbsolute.Split("StartDataBase");

			return string.Concat(array[0], "StartDataBase\\", "Files\\");
		}
	}
}
