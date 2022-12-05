namespace AdventOfCode
{
    public static class TreeExtensions
    {
        public static IEnumerable<Tree<T>> GetAllChildren<T>(this IEnumerable<Tree<T>> trees)
        {
            foreach (var tree in trees)
            {
                foreach (var child in tree.Children)
                {
                    yield return child;
                }
            }
        }
    }
}