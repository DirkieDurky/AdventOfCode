using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace HelperClasses;

class Map<T> : ICloneable
{
    public T[,] Content;
    public int Height;
    public int Width;

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Content = new T[Width, Height];
    }

    public Map(T[,] startContent)
    {
        Height = startContent.GetLength(1);
        Width = startContent.GetLength(0);
        Content = startContent;
    }

    public Map(T[][] startContent)
	{
		//Convert jagged array to 2D array
		try
		{
			int FirstDim = startContent.Length;
			int SecondDim = startContent.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

			var result = new T[FirstDim, SecondDim];
			for (int i = 0; i < FirstDim; ++i)
				for (int j = 0; j < SecondDim; ++j)
					result[i, j] = startContent[i][j];

			Content = result;
		}
		catch (InvalidOperationException)
		{
			throw new InvalidOperationException("The given jagged array is not rectangular.");
		}
		Height = Content.GetLength(1);
		Width = Content.GetLength(0);
	}

    public T this[int x, int y]
    {
        get { return Content[x, y]; }
        set { Content[x, y] = value; }
    }

    public void Print()
    {
        for (int y = 0; y < Height; y++)
        {
            StringBuilder line = new();
            for (int x = 0; x < Width; x++)
            {
                line.Append(Content[y, x]);
            }
            Console.WriteLine(line);
        }
        Console.WriteLine();
    }

    public object Clone() => new Map<T>((T[,])Content.Clone());

    // public object DeepClone()
    // {
    //     MemoryStream mem = new MemoryStream();
    //     System.Runtime.Serialization.Formatters.Binary.BinaryFormatter form = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
    //     form.Serialize(mem, this);
    //     mem.Position = 0;
    //     return form.Deserialize(mem);
    // }

    public override bool Equals(object? other) =>
       other != null && GetType() == other.GetType() && Equals((Map<T>)other);

    public bool Equals(Map<T> other)
    {
        return Content.Rank == other.Content.Rank &&
            Enumerable.Range(0, Content.Rank).All(dimension => Content.GetLength(dimension) == other.Content.GetLength(dimension)) &&
            Content.Cast<T>().SequenceEqual(other.Content.Cast<T>());
    }

    public override int GetHashCode()
    {
        int hashCode = 0;

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                hashCode = (hashCode * 397) ^ (Content[x, y] != null ? Content[x, y]!.GetHashCode() : 0);
            }
        }

        return hashCode;
    }
}

class CharMap : Map<char>, ICloneable
{
    public CharMap(int width, int height) : base(width, height)
    {
        //Empty
    }

    public CharMap(char[,] startContent) : base(startContent)
    {
        //Empty
    }

    public CharMap(string[] newContent) : base(newContent[0].Length, newContent.Length)
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Content[x, y] = newContent[y][x];
            }
        }
    }

    public new object Clone() => new CharMap((char[,])Content.Clone());

    public void Fill(string[] newContent)
    {
        if (newContent.Length != Height
            || newContent.Any(x => x.Length != newContent[0].Length)
            || newContent[0].Length != Width)
        {
            throw new Exception("New content size doesn't match map size");
        }

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                Content[x, y] = newContent[y][x];
            }
        }
    }
}
