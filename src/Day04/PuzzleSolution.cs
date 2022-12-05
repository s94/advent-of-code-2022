namespace AdventOfCode.Day04
{
	public sealed class PuzzleSolution
	{
		private readonly Task<string[]> _puzzleInput = PuzzleUtilities.GetPuzzleInput("Day04/puzzle-input.txt");

		[Fact]
		public async Task CampCleanupPartOne()
		{
			IEnumerable<CleanupSectionAssignment> assignmentList = await GetAssignmentList();

			int totalCount = assignmentList.Where(x => x.IsAssignmentRangeAlreadyCovered()).Count();

			totalCount.Should().Be(536);
		}

		[Fact]
		public async Task CampCleanupPartTwo()
		{
			IEnumerable<CleanupSectionAssignment> assignmentList = await GetAssignmentList();

			int totalCount = assignmentList.Where(x => x.DoesAssignmentRangeHaveAnyOverlap()).Count();

			totalCount.Should().Be(845);
		}

		private async Task<IEnumerable<CleanupSectionAssignment>> GetAssignmentList()
		{
			List<CleanupSectionAssignment> cleanupSectionAssignmentList = new();
			foreach (string cleanupSectionAssignment in await _puzzleInput)
			{
				string[] assignmentPairs = cleanupSectionAssignment.Split(',');
				string[] firstAssignmentRange = assignmentPairs[0].Split('-');
				string[] secondAssignmentRange = assignmentPairs[1].Split('-');
				cleanupSectionAssignmentList.Add(new CleanupSectionAssignment {
					ElfOneAssignmentStart = int.Parse(firstAssignmentRange[0]),
					ElfOneAssignmentEnd = int.Parse(firstAssignmentRange[1]),
					ElfTwoAssignmentStart = int.Parse(secondAssignmentRange[0]),
					ElfTwoAssignmentEnd = int.Parse(secondAssignmentRange[1])
				});
			}

			return cleanupSectionAssignmentList;
		}

		private class CleanupSectionAssignment
		{
			public int ElfOneAssignmentStart { get; set; }
			public int ElfOneAssignmentEnd { get; set; }
			public int ElfTwoAssignmentStart { get; set; }
			public int ElfTwoAssignmentEnd { get; set; }

			public bool IsAssignmentRangeAlreadyCovered()
			{
				if (ElfOneAssignmentStart >= ElfTwoAssignmentStart && ElfOneAssignmentEnd <= ElfTwoAssignmentEnd)
					return true;
				else if (ElfTwoAssignmentStart >= ElfOneAssignmentStart && ElfTwoAssignmentEnd <= ElfOneAssignmentEnd)
					return true;
				else
					return false;
			}

			public bool DoesAssignmentRangeHaveAnyOverlap()
			{
				if (IsAssignmentRangeAlreadyCovered())
					return true;
				else if (ElfOneAssignmentEnd >= ElfTwoAssignmentStart && ElfOneAssignmentEnd <= ElfTwoAssignmentEnd)
					return true;
				else if (ElfOneAssignmentStart >= ElfTwoAssignmentStart && ElfOneAssignmentStart <= ElfTwoAssignmentEnd)
					return true;
				else if (ElfTwoAssignmentStart >= ElfOneAssignmentStart && ElfTwoAssignmentStart <= ElfOneAssignmentEnd)
					return true;
				else
					return false;
			}
		}
	}
}
