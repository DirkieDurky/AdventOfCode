using HelperClasses;
using System.Drawing;

namespace Year2024
{
	public class Day06 : IDay
	{
		public object Sol1(string input)
		{
			HashSet<Point> visitedCells = new HashSet<Point>();
			char[] startPosChars = new char[] { '^', 'v', '<', '>' };
			Map<char> grid = new Map<char>(input.Split("\n").Select(x => x.ToCharArray()).ToArray());

			PosInfo pos = FindStart(startPosChars, grid);
			visitedCells.Add(pos.Pos);

			while (true)
			{
				int newX = pos.Pos.X + pos.Orientation.DeltaX;
				int newY = pos.Pos.Y + pos.Orientation.DeltaY;

				if (newY < 0 || newY >= grid.Height
				|| newX < 0 || newX >= grid.Width) return visitedCells.Count;

				while (grid[newY,newX] == '#')
				{
					pos.Orientation = Direction.RotateCW(pos.Orientation);

					newX = pos.Pos.X + pos.Orientation.DeltaX;
					newY = pos.Pos.Y + pos.Orientation.DeltaY;

					if (newY < 0 || newY >= grid.Height
					|| newX < 0 || newX >= grid.Width) return visitedCells.Count;
				}

				pos.Pos = new Point(newX, newY);
				visitedCells.Add(pos.Pos);
			}
		}

		private PosInfo FindStart(char[] startPosChars, Map<char> grid)
		{
			//Find start
			for (int y = 0; y < grid.Content.Length; y++)
			{
				for (int x = 0; x < grid.Content.GetLength(1); x++)
				{
					if (startPosChars.Contains(grid[y,x]))
					{
						return new PosInfo(x, y, grid[y,x] switch
						{
							'^' => Direction.Up,
							'v' => Direction.Down,
							'<' => Direction.Left,
							'>' => Direction.Right,
						});
					}
				}
			}

			throw new Exception("Nothing found");
		}

		private struct PosInfo
		{
			public Point Pos;
			public Direction Orientation;

			public PosInfo(int x, int y, Direction orientation)
			{
				Pos = new Point(x, y);
				Orientation = orientation;
			}
		}

		public object Sol2(string input)
		{
			int count = 0;
			char[] startPosChars = new char[] { '^', 'v', '<', '>' };
			Map<char> grid = new Map<char>(input.Split("\n").Select(x => x.ToCharArray()).ToArray());

			PosInfo pos = FindStart(startPosChars, grid);

			for (int y = 0; y < grid.Height; y++)
			{
				for (int x = 0; x < grid.Width; x++)
				{
					if (pos.Pos.X == x && pos.Pos.Y == y) continue;
					if (grid[y,x] == '#') continue;

					Map<char> newGrid = (Map<char>)grid.Clone();
					newGrid[y,x] = '#';

					if (PathGetsStuck(pos, newGrid)) count++;
				}
				Console.WriteLine($"Y:{y} {count}");
			}

			return count;
		}

		private bool PathGetsStuck(PosInfo pos, Map<char> grid)
		{
			HashSet<PosInfo> moveHistory = new HashSet<PosInfo>();
			moveHistory.Add(pos);

			while (true)
			{
				int newX = pos.Pos.X + pos.Orientation.DeltaX;
				int newY = pos.Pos.Y + pos.Orientation.DeltaY;

				if (newY < 0 || newY >= grid.Height
				|| newX < 0 || newX >= grid.Width) return false;

				while (grid[newY,newX] == '#')
				{
					pos.Orientation = Direction.RotateCW(pos.Orientation);

					newX = pos.Pos.X + pos.Orientation.DeltaX;
					newY = pos.Pos.Y + pos.Orientation.DeltaY;

					if (newY < 0 || newY >= grid.Height
					|| newX < 0 || newX >= grid.Width) return false;
				}

				pos.Pos = new Point(newX, newY);
				if (moveHistory.Contains(pos)) return true;
				moveHistory.Add(pos);
			}
		}
	}
}