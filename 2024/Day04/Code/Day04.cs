using System.Collections;
using System.Text;

namespace Year2024
{
	public class Day04 : IDay
	{
		public object Sol1(string input)
		{
			String[] grid = input.Split("\n");
			StringBuilder lastChars = new StringBuilder(4);
			int sum = 0;

			for (int y = 0; y < grid.Length; y++)
			{
				for (int x = 0; x < grid[y].Length; x++)
				{
					if (grid[y][x] == 'X')
					{
						if (CheckMatch(grid, x, y, -1, 0)) sum += 1;
						if (CheckMatch(grid, x, y, 1, 0)) sum += 1;
						if (CheckMatch(grid, x, y, 0, -1)) sum += 1;
						if (CheckMatch(grid, x, y, 0, 1)) sum += 1;
						if (CheckMatch(grid, x, y, -1, -1)) sum += 1;
						if (CheckMatch(grid, x, y, 1, -1)) sum += 1;
						if (CheckMatch(grid, x, y, -1, 1)) sum += 1;
						if (CheckMatch(grid, x, y, 1, 1)) sum += 1;
					}
				}
			}

			return sum;
		}

		public bool CheckMatch(String[] grid, int x, int y, int xDiff, int yDiff)
		{
			String pattern = "XMAS";

			for (int i = 0; i < 4; i++)
			{
				if (x < 0 || y < 0 || y >= grid.Length || x >= grid[y].Length) return false;
				if (grid[y][x] != pattern[i]) return false;

				x += xDiff;
				y += yDiff;
			}

			return true;
		}

		public object Sol1Attempt(string input)
		{
			String[] grid = input.Split("\n");
			StringBuilder lastChars = new StringBuilder(4);
			int sum = 0;

			//Horizontal
			for (int y = 0; y < grid.Length; y++)
			{
				for (int x = 0; x < grid[y].Length; x++)
				{
					if (lastChars.Length >= 4) lastChars.Remove(0, 1);
					lastChars.Append(grid[y][x]);

					if (lastChars.Equals(new StringBuilder("XMAS"))) sum += 1;
					if (lastChars.Equals(new StringBuilder("SAMX"))) sum += 1;
				}
			}

			//Vertical
			for (int y = 0; y < grid.Length; y++)
			{
				for (int x = 0; x < grid[y].Length; x++)
				{
					if (lastChars.Length >= 4) lastChars.Remove(0, 1);
					lastChars.Append(grid[x][y]);

					if (lastChars.Equals(new StringBuilder("XMAS"))) sum += 1;
					if (lastChars.Equals(new StringBuilder("SAMX"))) sum += 1;
				}
			}

			return sum;
		}
		public object Sol2(string input)
		{
			String[] grid = input.Split("\n");
			StringBuilder lastChars = new StringBuilder(4);
			int sum = 0;

			for (int y = 1; y < grid.Length - 1; y++)
			{
				for (int x = 1; x < grid[y].Length - 1; x++)
				{
					if (grid[y][x] == 'A')
					{
						if (
							(
							(grid[y - 1][x - 1] == 'M' && grid[y + 1][x + 1] == 'S')
							|| (grid[y - 1][x - 1] == 'S' && grid[y + 1][x + 1] == 'M')
							)
							&&
							(
							(grid[y - 1][x + 1] == 'M' && grid[y + 1][x - 1] == 'S')
							|| (grid[y - 1][x + 1] == 'S' && grid[y + 1][x - 1] == 'M')
							)
							)
						{
							sum += 1;
						}
					}
				}
			}

			return sum;
		}

		public bool CheckMatch2(String[] grid, int x, int y, int xDiff, int yDiff)
		{
			String pattern = "MAS";

			for (int i = 0; i < 4; i++)
			{
				if (x < 0 || y < 0 || y >= grid.Length || x >= grid[y].Length) return false;
				if (grid[y][x] != pattern[i]) return false;

				x += xDiff;
				y += yDiff;
			}

			return true;
		}
	}
}