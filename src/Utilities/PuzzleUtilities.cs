namespace AdventOfCode.Utilites
{
	internal static class PuzzleUtilities
	{
		private const string Dll = "AdventOfCode.dll";
		internal static readonly string ProjectPath = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(Dll, string.Empty);

		internal static async Task<string[]> GetPuzzleInput(string path)
			=> await System.IO.File.ReadAllLinesAsync(path);
	}
}
