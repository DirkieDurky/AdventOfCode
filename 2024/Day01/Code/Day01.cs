namespace Year2024
{
    public class Day01 : IDay
    {
        public object Sol1(string input)
        {
            String[] lines = input.Split("\n");
            int sum = 0;
            List<int> leftList = new();
            List<int> rightList = new();

			foreach (String line in lines)
            {
                String[] valuesStr = line.Split("   ");
				List<int> values = valuesStr.Select(int.Parse).ToList();
                leftList.Add(values[0]);
                rightList.Add(values[1]);
			}

            leftList.Sort();
            rightList.Sort();

            for (int i=0;i<leftList.Count;i++)
            {
				sum += Math.Abs(leftList[i] - rightList[i]);
            }

			return sum;
        }

        public object Sol2(string input)
		{
			String[] lines = input.Split("\n");
			int sum = 0;
			List<int> leftList = new();
			List<int> rightList = new();

			foreach (String line in lines)
			{
				String[] valuesStr = line.Split("   ");
				List<int> values = valuesStr.Select(int.Parse).ToList();
				leftList.Add(values[0]);
				rightList.Add(values[1]);
			}

			leftList.Sort();
			rightList.Sort();

			for (int i = 0; i < leftList.Count; i++)
			{
				sum += rightList.Where(x=>x==leftList[i]).Count() * leftList[i];
			}

			return sum;
		}
    }
}