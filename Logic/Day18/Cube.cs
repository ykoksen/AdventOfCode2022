namespace Logic.Day18
{
    public record struct Cube(int X, int Y, int Z) : IComparable<Cube>
    {
        public int CompareTo(Cube other)
        {
            if (this == other)
                return 0;

            var xComp = X.CompareTo(other.X);

            if (xComp != 0)
                return xComp;

            var yComp = Y.CompareTo(other.Y);
            if (yComp != 0)
                return yComp;

            return Z.CompareTo(other.Z);
        }

        public IEnumerable<Cube> GetSides()
        {
            yield return this with { X = X - 1 };
            yield return this with { X = X + 1 };
            yield return this with { Y = Y - 1 };
            yield return this with { Y = Y + 1 };
            yield return this with { Z = Z - 1 };
            yield return this with { Z = Z + 1 };
        }

        public IEnumerable<Cube> GetAllPossibleAdjant()
        {
            foreach (var item in GetSides())
            {
                yield return item;
            }

            yield return this with { X = X - 1, Y = Y - 1 };
            yield return this with { X = X - 1, Y = Y + 1 };
            yield return this with { X = X + 1, Y = Y - 1 };
            yield return this with { X = X + 1, Y = Y + 1 };

            yield return this with { X = X - 1, Z = Z - 1 };
            yield return this with { X = X - 1, Z = Z + 1 };
            yield return this with { X = X + 1, Z = Z - 1 };
            yield return this with { X = X + 1, Z = Z + 1 };

            yield return this with { Z = Z - 1, Y = Y - 1 };
            yield return this with { Z = Z - 1, Y = Y + 1 };
            yield return this with { Z = Z + 1, Y = Y - 1 };
            yield return this with { Z = Z + 1, Y = Y + 1 };
        }

        public static bool operator <(Cube left, Cube right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Cube left, Cube right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(Cube left, Cube right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Cube left, Cube right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}

