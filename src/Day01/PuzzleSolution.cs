namespace AdventOfCode.Day01
{
	public sealed class PuzzleSolution
	{
		private readonly Task<string[]> _puzzleInput = PuzzleUtilities.GetPuzzleInput("Day01/puzzle-input.txt");

		[Fact]
		public async Task CalorieCountingPartOne()
		{
			Elf elf = (await GetElfs()).OrderBy(e => e.Calories).Last();
			Console.WriteLine($"Elf with the most calories is {elf.Id}, with {elf.Calories} calories.");

			elf.Id.Should().Be(41);
			elf.Calories.Should().Be(69795);
		}

		[Fact]
		public async Task CalorieCountingPartTwo()
		{
			IEnumerable<Elf> elfs = await GetElfs();
			elfs = elfs.OrderBy(e => e.Calories).TakeLast(3);

			int calorieTotal = 0;
			foreach	(Elf elf in elfs)
			{
				Console.WriteLine($"Elf {elf.Id} has {elf.Calories}");
				calorieTotal += elf.Calories;
			}
			Console.WriteLine($"Total calories for top 3 elfs is {calorieTotal}");

			calorieTotal.Should().Be(208437);
		}

		private async Task<IEnumerable<Elf>> GetElfs()
		{
			List<Elf> elfs = new();

			int currentElfId = 0;
			int currentCalorieCount = 0;

			foreach (string value in await _puzzleInput)
			{
				if (value == string.Empty)
				{
					elfs.Add(new Elf { Id = currentElfId, Calories = currentCalorieCount });
					currentElfId++;
					currentCalorieCount = 0;
				}
				else
				{
					currentCalorieCount += int.Parse(value);
				}
			}

			return elfs;
		}

		private class Elf
		{
			public int Id { get; set; }
			public int Calories { get; set; }
		}
	}
}
