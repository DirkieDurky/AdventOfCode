namespace Advent_of_Code.HelperClasses
{
    class HelperFunctions
    {
        public static int[] Flatten(int[,] input)
        {
            int size = input.Length;
            int[] result = new int[size];

            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }
            return result;
        }

        public static List<int> AllIndexesOf(string str, string value)
        {
            List<int> indexes = new List<int>();
            if (string.IsNullOrEmpty(value)) return indexes;
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}
