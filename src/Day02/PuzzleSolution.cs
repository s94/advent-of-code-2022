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
				RockPaperScissorsRound round = new(value);
				totalScore += (int)round.RoundResult;
				totalScore += (int)round.PlayerTwo;
			}
			System.Console.WriteLine($"Total score is: {totalScore}");

			totalScore.Should().Be(13009);
		}

		[Fact]
		public async Task RockPaperScissorsPartTwo()
		{
			int totalScore = 0;
			foreach (string value in await _puzzleInput)
			{
				RockPaperScissorsRound round = new(value);
				totalScore += (int)round.DesiredRoundResult;
				totalScore += (int)round.DesiredPlayerTwo;
			}
			System.Console.WriteLine($"Total score is: {totalScore}");

			totalScore.Should().Be(10398);
		}

		private class RockPaperScissorsRound
		{
			public string Round { get; private set; }
			public RockPaperScissors PlayerOne => GetPlayersHand(Round.First());
			public RockPaperScissors PlayerTwo => GetPlayersHand(Round.Last());
			public RockPaperScissorsResult RoundResult => GetResult();
			public RockPaperScissors DesiredPlayerTwo => GetDesiredHandForPlayerTwo(GetResult(Round.Last()));
			public RockPaperScissorsResult DesiredRoundResult => GetResult(Round.Last());

			public RockPaperScissorsRound(string round)
			{
				Round = round;
			}

			private RockPaperScissors GetPlayersHand(char player)
			{
				return player switch
				{
					'A' or 'X' => RockPaperScissors.Rock,
					'B' or 'Y' => RockPaperScissors.Paper,
					'C' or 'Z' => RockPaperScissors.Scissors,
					_ => throw new System.ArgumentOutOfRangeException()
				};
			}

			private RockPaperScissorsResult GetResult()
			{
				if (PlayerOne == PlayerTwo) return RockPaperScissorsResult.Draw;

				return PlayerOne switch
				{
					RockPaperScissors.Rock => PlayerTwo == RockPaperScissors.Paper ? RockPaperScissorsResult.Win : RockPaperScissorsResult.Loss,
					RockPaperScissors.Paper => PlayerTwo == RockPaperScissors.Scissors ? RockPaperScissorsResult.Win : RockPaperScissorsResult.Loss,
					RockPaperScissors.Scissors => PlayerTwo == RockPaperScissors.Rock ? RockPaperScissorsResult.Win : RockPaperScissorsResult.Loss,
					_ => throw new System.ArgumentOutOfRangeException()
				};
			}

			private RockPaperScissorsResult GetResult(char desiredOutcome)
			{
				return desiredOutcome switch
				{
					'X' => RockPaperScissorsResult.Loss,
					'Y' => RockPaperScissorsResult.Draw,
					'Z' => RockPaperScissorsResult.Win,
					_ => throw new System.ArgumentOutOfRangeException()
				};
			}

			private RockPaperScissors GetDesiredHandForPlayerTwo(RockPaperScissorsResult desiredOutcome)
			{
				if (desiredOutcome == RockPaperScissorsResult.Draw) return PlayerOne;

				return PlayerOne switch
				{
					RockPaperScissors.Rock => desiredOutcome == RockPaperScissorsResult.Win ? RockPaperScissors.Paper : RockPaperScissors.Scissors, 
					RockPaperScissors.Paper => desiredOutcome == RockPaperScissorsResult.Win ? RockPaperScissors.Scissors : RockPaperScissors.Rock,
					RockPaperScissors.Scissors => desiredOutcome == RockPaperScissorsResult.Win ? RockPaperScissors.Rock : RockPaperScissors.Paper,
					_ => throw new System.ArgumentOutOfRangeException()
				};
			}
		}

		private enum RockPaperScissors
		{
			Rock = 1,
			Paper = 2,
			Scissors = 3
		}

		private enum RockPaperScissorsResult
		{
			Loss = 0,
			Draw = 3,
			Win = 6
		}
	}
}
