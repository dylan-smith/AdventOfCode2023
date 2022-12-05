namespace AdventOfCode;

public class HashableList<T> : List<T>
{
    public HashableList(IEnumerable<T> seed) : base(seed)
    {
    }

    public override bool Equals(object obj)
    {
        var state = (IEnumerable<T>)obj;
        return this.SequenceEqual(state);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 19;

            foreach (var x in this)
            {
                hash = (hash * 31) + x.GetHashCode();
            }

            return hash;
        }
    }
}