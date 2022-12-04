namespace AdventOfCode.Day03
{
	public sealed class PuzzleSolution
	{
		private readonly Task<string[]> _puzzleInput = PuzzleUtilities.GetPuzzleInput("Day03/puzzle-input.txt");
		private readonly List<char> _items = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

		[Fact]
		public async Task RucksackReorganizationPartOne()
		{
			IEnumerable<Rucksack> rucksacks = await GetRucksacks();

			int totalPriortyScore = 0;
			foreach (Rucksack rucksack in rucksacks)
			{
				char sameItem = rucksack.CompartmentOne.Intersect(rucksack.CompartmentTwo).First();
				totalPriortyScore += (_items.IndexOf(sameItem) + 1);
			}

			totalPriortyScore.Should().Be(7811);
		}

		[Fact]
		public async Task RucksackReorganizationPartTwo()
		{
			IEnumerable<Rucksack> rucksacks = await GetRucksacks();

			int totalPriortyScore = 0;
			List<Rucksack> groupedRucksacks = new();
			foreach (Rucksack rucksack in rucksacks)
			{
				groupedRucksacks.Add(rucksack);
				
				if (groupedRucksacks.Count == 3)
				{
					char badgeItem = groupedRucksacks[0].Contents.Intersect(groupedRucksacks[1].Contents).Intersect(groupedRucksacks[2].Contents).First();
					totalPriortyScore += (_items.IndexOf(badgeItem) + 1);
					groupedRucksacks.Clear();
				}
			}

			totalPriortyScore.Should().Be(2639);
		}

		private async Task<IEnumerable<Rucksack>> GetRucksacks()
		{
			List<Rucksack> rucksacks = new();
			foreach (string rucksackContents in await _puzzleInput)
			{
				int rucksackLength = rucksackContents.Length;
				string rucksackCompartmentOne = string.Empty;
				string rucksackCompartmentTwo = string.Empty;

				for (int i = 0; i < rucksackLength; i++)
				{
					if (i < (rucksackLength / 2))
					{
						rucksackCompartmentOne += rucksackContents[i];
					}
					else
					{
						rucksackCompartmentTwo += rucksackContents[i];
					}
				}
				rucksacks.Add(new Rucksack
				{
					Contents = rucksackContents,
					CompartmentOne = rucksackCompartmentOne,
					CompartmentTwo = rucksackCompartmentTwo
				});
			}

			return rucksacks;
		}

		private class Rucksack
		{
			public string Contents { get; set; }
			public string CompartmentOne { get; set; }
			public string CompartmentTwo { get; set; }
		}
	}
}
