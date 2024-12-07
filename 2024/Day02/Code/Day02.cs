namespace Year2024
{
	public class Day02 : IDay
	{
		public object Sol1(string input)
		{
			String[] reports = input.Split("\n");
			int sum = 0;

			foreach (String report in reports)
			{
				int[] levels = report.Split(" ").Select(int.Parse).ToArray();

				bool ascending = levels[1] > levels[0];

				sum += CheckValidity(levels, ascending) ? 1 : 0;
			}

			return sum;
		}

		public bool CheckValidity(int[] levels, bool ascending)
		{
			for (int i = 1; i < levels.Length; i++)
			{
				if (ascending)
				{
					if (levels[i] < levels[i - 1])
					{
						return false;
					}
				}
				else
				{
					if (levels[i] > levels[i - 1])
					{
						return false;
					}
				}

				int difference = Math.Abs(levels[i] - levels[i - 1]);
				if (difference < 1 || difference > 3) return false;
			}

			return true;
		}

		public object Sol2(string input)
		{
			String[] reports = input.Split("\n");
			int sum = 0;

			foreach (String report in reports)
			{
				List<int> levels = report.Split(" ").Select(int.Parse).ToList();

				if (CheckValidityTotal(levels)) sum += 1;
			}

			return sum;
		}

		public bool CheckValidityTotal(List<int> levels)
		{
			if (CheckValidity2(levels)) return true;

			for (int i = 0; i < levels.Count; i++)
			{
				List<int> clone = new(levels);
				clone.RemoveAt(i);
				if (CheckValidity2(clone)) return true;
			}

			return false;
		}

		public bool CheckValidity2(List<int> levels)
		{
			bool ascending = levels[1] > levels[0];

			for (int i = 1; i < levels.Count; i++)
			{
				if (ascending)
				{
					if (levels[i] < levels[i - 1])
					{
						return false;
					}
				}
				else
				{
					if (levels[i] > levels[i - 1])
					{
						return false;
					}
				}

				int difference = Math.Abs(levels[i] - levels[i - 1]);
				if (difference < 1 || difference > 3) return false;
			}

			return true;
		}
	}
}