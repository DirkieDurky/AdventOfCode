namespace Year2024
{
	public class Day05 : IDay
	{
		public object Sol1(string input)
		{
			int sum = 0;

			string[] split = input.Split("\n\n");
			List<string> orderingRules = split[0].Split("\n").ToList(); ;
			List<string> updates = split[1].Split("\n").ToList();

			foreach (string updatePages in updates)
			{
				List<int> pages = updatePages.Split(",").Select(int.Parse).ToList();

				if (IsValid(pages, orderingRules))
				{
					sum += pages[(int)Math.Floor(pages.Count / 2d)];
				}
			}

			return sum;
		}

		private bool IsValid(List<int> pages, List<string> orderingRules)
		{
			foreach (string rule in orderingRules)
			{
				int[] rulePages = rule.Split("|").Select(int.Parse).ToArray();

				if (pages.Contains(rulePages[0])
					&& pages.Contains(rulePages[1]))
				{
					if (pages.IndexOf(rulePages[1]) < pages.IndexOf(rulePages[0])) return false;
				}
			}

			return true;
		}

		public object Sol2(string input)
		{
			int sum = 0;

			string[] split = input.Split("\n\n");
			List<string> orderingRules = split[0].Split("\n").ToList(); ;
			List<string> updates = split[1].Split("\n").ToList();

			foreach (string updatePages in updates)
			{
				List<int> pages = updatePages.Split(",").Select(int.Parse).ToList();

				if (!IsValid(pages, orderingRules))
				{
					//Console.Write(String.Join(",", pages));
					List<int> fixedPages = Fix(pages, orderingRules);
					//Console.WriteLine(" has been changed to " + String.Join(",", fixedPages));
					sum += fixedPages[(int)Math.Floor(pages.Count / 2d)];
				}

			}

			return sum;
		}

		private List<int> Fix(List<int> pages, List<string> orderingRules)
		{
			foreach (string rule in orderingRules)
			{
				int[] rulePages = rule.Split("|").Select(int.Parse).ToArray();

				if (pages.Contains(rulePages[0])
					&& pages.Contains(rulePages[1]))
				{
					if (pages.IndexOf(rulePages[1]) < pages.IndexOf(rulePages[0]))
					{
						//Swap values
						int tmp = rulePages[0];
						pages[pages.IndexOf(rulePages[0])] = rulePages[1];
						pages[pages.IndexOf(rulePages[1])] = tmp;

						return Fix(pages, orderingRules);
					}
				}
			}

			return pages;
		}
	}
}