namespace AdventOfCode.Day05
{
	public sealed class PuzzleSolution
	{
		private readonly Task<string[]> _puzzleInput = PuzzleUtilities.GetPuzzleInput("Day05/puzzle-input.txt");

		private List<List<char>> _supplyStackObject = new List<List<char>>()
		{
			new char[] { 'B', 'G', 'S', 'C' }.ToList(),
			new char[] { 'T', 'M', 'W', 'H', 'J', 'N', 'V', 'G' }.ToList(),
			new char[] { 'M', 'Q', 'S' }.ToList(),
			new char[] { 'B', 'S', 'L', 'T', 'W', 'N', 'M' }.ToList(),
			new char[] { 'J', 'Z', 'F', 'T', 'V', 'G', 'W', 'P' }.ToList(),
			new char[] { 'C', 'T', 'B', 'G', 'Q', 'H', 'S' }.ToList(),
			new char[] { 'T', 'J', 'P', 'B', 'W' }.ToList(),
			new char[] { 'G', 'D', 'C', 'Z', 'F', 'T', 'Q', 'M' }.ToList(),
			new char[] { 'N', 'S', 'H', 'B', 'P', 'F' }.ToList()
		};

		[Fact]
		public async Task SupplyStacksPartOne()
		{
			foreach (SupplyStackMove supplyStackMove in await GetSupplyStackMoves())
			{
				OperateCrateMover9000(supplyStackMove);
			}

			GetAnswer().Should().Be("CFFHVVHNC");
		}

		[Fact]
		public async Task SupplyStacksPartTwo()
		{
			foreach (SupplyStackMove supplyStackMove in await GetSupplyStackMoves())
			{
				OperateCrateMover9001(supplyStackMove);
			}

			GetAnswer().Should().Be("FSZWBPTBG");
		}

		private async Task<IEnumerable<SupplyStackMove>> GetSupplyStackMoves()
		{
			List<SupplyStackMove> supplyStackMoveList = new();
			foreach (string move in await _puzzleInput)
			{
				if (string.IsNullOrWhiteSpace(move) || move.First() != 'm') continue;

				supplyStackMoveList.Add(new SupplyStackMove
				{
					Quantity = int.Parse(move.Split("from")[0].Replace("move ", string.Empty).Trim()),
					Origin = int.Parse(move.Split("from")[1].Split("to")[0].Trim()),
					Destination = int.Parse(move.Split("to")[1].Trim())
				});
			}

			return supplyStackMoveList;
		}

		private void OperateCrateMover9000(SupplyStackMove move)
		{
			for (int i = 0; i < move.Quantity; i++)
			{
				char fromOrigin = _supplyStackObject[move.Origin - 1].Last();
				_supplyStackObject[move.Origin - 1].RemoveAt(_supplyStackObject[move.Origin - 1].Count - 1);
				_supplyStackObject[move.Destination - 1].Add(fromOrigin);
			}
		}

		private void OperateCrateMover9001(SupplyStackMove move)
		{
			IEnumerable<char> fromOrigin = _supplyStackObject[move.Origin - 1].TakeLast(move.Quantity);

			int supplyStackObjectIndex = move.Origin - 1;
			int selectedSupplyStackObjectCount = _supplyStackObject[supplyStackObjectIndex].Count;
			int index = selectedSupplyStackObjectCount - move.Quantity;
			int count = move.Quantity;
			_supplyStackObject[move.Destination - 1].AddRange(fromOrigin);
			_supplyStackObject[move.Origin - 1].RemoveRange(index, count);
		}

		private string GetAnswer()
		{
			string answer = string.Empty;
			for (int i = 0; i < _supplyStackObject.Count; i++)
			{
				answer += _supplyStackObject[i].Last();
			}

			return answer;
		}

		private class SupplyStackMove
		{
			public int Quantity { get; set; }
			public int Origin { get; set; }
			public int Destination { get; set; }
		}
	}
}
