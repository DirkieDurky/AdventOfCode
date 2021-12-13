namespace HelperFunctions
{
    class HelperFunctions
    {
        public static Int32[] Flatten(Int32[,] input)
        {
            // Step 1: get total size of 2D array, and allocate 1D array.
            Int32 size = input.Length;
            Int32[] result = new Int32[size];

            // Step 2: copy 2D array elements into a 1D array.
            Int32 write = 0;
            for (Int32 i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (Int32 z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }
            // Step 3: return the new array.
            return result;
        }
    }
}