namespace AdventOfCode.Day02
{
	public sealed class PuzzleSolution
	{
		private Task<string[]> _puzzleInput = PuzzleUtilities.GetPuzzleInput("Day02/puzzle-input.txt");

		[Fact]
		public async Task RockPaperScissorsPartOne()
		{
			int totalScore = 0;
			foreach (string value in await _puzzleInput)
			{
				totalScore += GetRockPaperScissorsOutcome(value.First(), value.Last());
				totalScore += You.SelectionScore(value.Last());
			}
			System.Console.WriteLine($"Total score is: {totalScore}");

			totalScore.Should().Be(13009);
		}

		private int GetRockPaperScissorsOutcome(char opponent, char you)
		{
			const int LOSS = 0;
			const int DRAW = 3;
			const int WIN = 6;

			if (IsDraw(opponent, you)) return DRAW;

			return opponent switch
			{
				Opponent.Rock => you == You.Paper ? WIN : LOSS,
				Opponent.Paper => you == You.Scissors ? WIN : LOSS,
				Opponent.Scissors => you == You.Rock ? WIN: LOSS,
				_ => throw new System.ArgumentOutOfRangeException()
			};
		}

		private bool IsDraw(char opponent, char you)
		{
			return opponent switch
			{
				Opponent.Rock => you == You.Rock,
				Opponent.Paper => you == You.Paper,
				Opponent.Scissors => you == You.Scissors,
				_ => throw new System.ArgumentOutOfRangeException()
			};
		}

		private static class Opponent
		{
			public const char Rock = 'A';
			public const char Paper = 'B';
			public const char Scissors = 'C';
		}

		private static class You
		{
			public const char Rock = 'X';
			public const char Paper = 'Y';
			public const char Scissors = 'Z';

			public static int SelectionScore(char you)
			{
				return you switch
				{
					Rock => 1,
					Paper => 2,
					Scissors => 3,
					_ => throw new System.ArgumentOutOfRangeException()
				};
			}
		}
	}
}
