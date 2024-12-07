using System.Numerics;

namespace Year2024
{
    public class Day07 : IDay
    {
        public object Sol1(string input)
        {
			BigInteger total = 0;
            string[] lines = input.Split("\n");

            foreach (string line in lines)
            {
                string[] split = line.Split(": ");
				BigInteger target = BigInteger.Parse(split[0]);
                List<BigInteger> values = split[1].Split(" ").Select(BigInteger.Parse).ToList();

                if (GetPossibleResults(values).Contains(target)) total += target;
            }

            return total;
        }

        private List<BigInteger> GetPossibleResults(List<BigInteger> values)
        {
			if (values.Count == 2) return new()
			{
				values[0] + values[1],
				values[0] * values[1]
			};
            List<BigInteger> results = new();

            List<BigInteger> tempResults = new()
			{
				values[0] + values[1],
				values[0] * values[1]
			};
			values = values.Skip(2).ToList();

            List<BigInteger> input1 = new List<BigInteger> { tempResults[0] };
            input1.AddRange(values);
			results.AddRange(GetPossibleResults(input1));

			List<BigInteger> input2 = new List<BigInteger> { tempResults[1] };
			input2.AddRange(values);
			results.AddRange(GetPossibleResults(input2));

			return results;
		}
		public object Sol2(string input)
		{
			BigInteger total = 0;
			string[] lines = input.Split("\n");

			foreach (string line in lines)
			{
				string[] split = line.Split(": ");
				BigInteger target = BigInteger.Parse(split[0]);
				List<BigInteger> values = split[1].Split(" ").Select(BigInteger.Parse).ToList();

				if (GetPossibleResults2(values).Contains(target)) total += target;
			}

			return total;
		}

		private List<BigInteger> GetPossibleResults2(List<BigInteger> values)
		{
			if (values.Count == 2) return new()
			{
				values[0] + values[1],
				values[0] * values[1],
				BigInteger.Parse(values[0].ToString() + values[1].ToString()),
			};
			List<BigInteger> results = new();

			List<BigInteger> tempResults = new()
			{
				values[0] + values[1],
				values[0] * values[1],
				BigInteger.Parse(values[0].ToString() + values[1].ToString()),
			};
			values = values.Skip(2).ToList();

			List<BigInteger> input1 = new List<BigInteger> { tempResults[0] };
			input1.AddRange(values);
			results.AddRange(GetPossibleResults2(input1));

			List<BigInteger> input2 = new List<BigInteger> { tempResults[1] };
			input2.AddRange(values);
			results.AddRange(GetPossibleResults2(input2));

			List<BigInteger> input3 = new List<BigInteger> { tempResults[2] };
			input3.AddRange(values);
			results.AddRange(GetPossibleResults2(input3));

			return results;
		}
	}
}