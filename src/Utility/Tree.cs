using System.Collections;

namespace AdventOfCode
{
    public class Tree<T> : IEnumerable<Tree<T>>
    {
        public Tree<T> Parent { get; set; }
        public LinkedList<Tree<T>> Children { get; set; } = new LinkedList<Tree<T>>();
        public T Data { get; set; }

        public Tree(T data)
        {
            Data = data;
        }

        public int CalcDistance(Tree<T> target)
        {
            var distance = 0;

            var visited = new HashSet<Tree<T>>();
            var reachable = new List<Tree<T>>() { this };

            while (!reachable.Any(x => x == target))
            {
                var newReachable = new List<Tree<T>>();

                foreach (var t in reachable.Except(visited))
                {
                    if (t.Parent != null)
                    {
                        newReachable.Add(t.Parent);
                    }

                    newReachable.AddRange(t.Children);
                }

                visited.AddRange(reachable);
                reachable = newReachable;
                distance++;
            }

            return distance;
        }

        public IEnumerator<Tree<T>> GetEnumerator()
        {
            return AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return AsEnumerable().GetEnumerator();
        }

        private IEnumerable<Tree<T>> AsEnumerable()
        {
            yield return this;

            foreach (var child in Children)
            {
                foreach (var t in child.AsEnumerable())
                {
                    yield return t;
                }
            }
        }
    }
}