using System;

namespace Task_001
{
    public class BinaryTreeNode<TNode>
    {
        public TNode Value { get; private set; }

        public BinaryTreeNode(TNode value) { Value = value; }

        public BinaryTreeNode<TNode> Left { get; set; }

        public BinaryTreeNode<TNode> Right { get; set; }

        public int CompareTo(TNode other, Comparable<TNode> compare)
        {
            if (compare == null && other is IComparable<TNode>)
            {
                IComparable<TNode> value = other as IComparable<TNode>;
                return value.CompareTo(Value);
            }
            else if (compare != null)
                return compare.Invoke(other, Value);
            else
                throw new ComparatorHasNotBeenFoundException("Comparator has not been found check it and try again.");
        }
    }
}
