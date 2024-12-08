using HelperClasses;
using System.Drawing;
using System.Text;

namespace Year2024
{
	public class Day08 : IDay
	{
		public object Sol1(string input)
		{
			Map<char> grid = new Map<char>(input.Split("\n").Select(x => x.ToCharArray()).ToArray());
			Dictionary<char, List<Point>> antennaLocations = new();

			//Fill antennaLocations
			for (int y = 0; y < grid.Height; y++)
			{
				for (int x = 0; x < grid.Width; x++)
				{
					if (grid[y, x] == '.') continue;
					if (!antennaLocations.ContainsKey(grid[y, x]))
					{
						antennaLocations[grid[y, x]] = new() { new Point(x, y) };
					}
					else
					{
						antennaLocations[grid[y, x]].Add(new Point(x, y));
					}
				}
			}

			HashSet<Point> antinodeLocations = new();
			foreach (KeyValuePair<char, List<Point>> antennaLocation in antennaLocations)
			{
				foreach (Point location in antennaLocation.Value)
				{
					foreach (Point location2 in antennaLocation.Value)
					{
						if (location == location2) continue;
						Point diff = new Point(location.X - location2.X, location.Y - location2.Y);

						Point locationPlusDiff = new Point(location.X + diff.X, location.Y + diff.Y);
						if (locationPlusDiff.X >= 0 && locationPlusDiff.X < grid.Width
							&& locationPlusDiff.Y >= 0 && locationPlusDiff.Y < grid.Height)
							antinodeLocations.Add(locationPlusDiff);
						Point locationMinusDiff = new Point(location2.X - diff.X, location2.Y - diff.Y);
						if (locationMinusDiff.X >= 0 && locationMinusDiff.X < grid.Width
							&& locationMinusDiff.Y >= 0 && locationMinusDiff.Y < grid.Height)
							antinodeLocations.Add(locationMinusDiff);
					}
				}
			}

			//foreach (Point location in antinodeLocations)
			//{
			//	grid[location.Y, location.X] = '#';
			//}

			//grid.Print();

			return antinodeLocations.Count;
		}
		public object Sol2(string input)
		{
			Map<char> grid = new Map<char>(input.Split("\n").Select(x => x.ToCharArray()).ToArray());
			Dictionary<char, List<Point>> antennaLocations = new();

			//Fill antennaLocations
			for (int y = 0; y < grid.Height; y++)
			{
				for (int x = 0; x < grid.Width; x++)
				{
					if (grid[y, x] == '.') continue;
					if (!antennaLocations.ContainsKey(grid[y, x]))
					{
						antennaLocations[grid[y, x]] = new() { new Point(x, y) };
					}
					else
					{
						antennaLocations[grid[y, x]].Add(new Point(x, y));
					}
				}
			}

			HashSet<Point> antinodeLocations = new();
			foreach (KeyValuePair<char, List<Point>> antennaLocation in antennaLocations)
			{
				foreach (Point location in antennaLocation.Value)
				{
					foreach (Point location2 in antennaLocation.Value)
					{
						if (location == location2) continue;
						Point diff = new Point(location.X - location2.X, location.Y - location2.Y);

						antinodeLocations.Add(location);

						Point locationPlusDiff = new Point(location.X + diff.X, location.Y + diff.Y);
						while (locationPlusDiff.X >= 0 && locationPlusDiff.X < grid.Width
							&& locationPlusDiff.Y >= 0 && locationPlusDiff.Y < grid.Height)
						{
							antinodeLocations.Add(locationPlusDiff);
							locationPlusDiff = new Point(locationPlusDiff.X + diff.X, locationPlusDiff.Y + diff.Y);
						}

						Point locationMinusDiff = new Point(location2.X - diff.X, location2.Y - diff.Y);
						while (locationMinusDiff.X >= 0 && locationMinusDiff.X < grid.Width
							&& locationMinusDiff.Y >= 0 && locationMinusDiff.Y < grid.Height)
						{
							antinodeLocations.Add(locationMinusDiff);
							locationMinusDiff = new Point(locationMinusDiff.X - diff.X, locationMinusDiff.Y - diff.Y);
						}
					}
				}
			}

			//foreach (Point location in antinodeLocations)
			//{
			//	grid[location.Y, location.X] = '#';
			//}

			//grid.Print();

			return antinodeLocations.Count;
		}
	}
}