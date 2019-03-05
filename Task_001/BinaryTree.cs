using System;
using System.Collections.Generic;
using System.Collections;

namespace Task_001
{
    public class BinaryTree<T> : IEnumerable<T>
    {
        BinaryTreeNode<T> root;

        public BinaryTreeNode<T> Root { get { return root; } }

        #region Constructors

        public BinaryTree()
        {
        }

        public BinaryTree(IComparable<T>[] nodes)
        {
            foreach (var node in nodes)
                Add(node);
        }

        public BinaryTree(T[] nodes, Comparable<T> compare)
        {
            if (compare == null)
                throw new ComparatorHasNotBeenFoundException("Comparator has not been found check it and try again.");

            foreach (var node in nodes)
                Add(node, compare);
        }

        #endregion

        #region Counter

        public int Counter { get; private set; }

        #endregion

        #region Event 

        public delegate void AddNode(string message);

        public event AddNode AddNodeEvent;

        #endregion

        #region Adding a new node of tree

        public void Add(IComparable<T> value)
        {
            Add((T)value, null);
        }

        public void Add(T value, Comparable<T> compare)
        {
            if (root == null)
                root = new BinaryTreeNode<T>(value);
            else
                AddToNode(root, value, compare);
            Counter++;

            if (AddNodeEvent != null)
                AddNodeEvent.Invoke($"Node {Counter} has added to Binary Tree with value {value}");
        }

        void AddToNode(BinaryTreeNode<T> node, T value, Comparable<T> compare)
        {
            if (node.CompareTo(value, compare) < 0)
            {
                if (node.Left == null) node.Left = new BinaryTreeNode<T>(value);
                else AddToNode(node.Left, value, compare);
            }
            else
            {
                if (node.Right == null) node.Right = new BinaryTreeNode<T>(value);
                else AddToNode(node.Right, value, compare);
            }
        }

        #endregion

        #region Enumerator

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTrav();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region InOrder Traversal

        private IEnumerator<T> InOrderTrav()
        {
            if (root != null)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> current = root;

                bool goLeftNext = true;

                stack.Push(current);

                while (stack.Count > 0)
                {
                    if (goLeftNext)
                    {
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }

                    yield return current.Value; // Back node from the stack.

                    if (current.Right != null)
                    {
                        current = current.Right;
                        goLeftNext = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }
        #endregion

        #region PreOrder Traversal

        public IEnumerable<T> PreOrderTrav
        {
            get
            {
                if (root != null)
                {
                    Queue<BinaryTreeNode<T>> queue = new Queue<BinaryTreeNode<T>>();
                    Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();

                    BinaryTreeNode<T> current = root;
                    bool goLeftNext = true;

                    stack.Push(current);
                    queue.Enqueue(current);

                    while (queue.Count != Counter)
                    {
                        if (goLeftNext)
                        {
                            while (current.Left != null)
                            {
                                current = current.Left;
                                stack.Push(current);
                                queue.Enqueue(current);
                            }
                            current = stack.Pop();
                        }

                        if (current.Right != null)
                        {
                            queue.Enqueue(current.Right);
                            stack.Push(current.Right);
                            current = current.Right;
                            goLeftNext = true;
                        }
                        else
                        {
                            current = stack.Pop();
                            goLeftNext = false;
                        }
                    }

                    while (queue.Count > 0)
                        yield return queue.Dequeue().Value; // Back node from the stack. 
                }
            }
        }

        #endregion

        #region PostOrder Traversal

        public IEnumerable<T> PostOrderTrav
        {
            get
            {
                if (root != null)
                {
                    Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                    BinaryTreeNode<T> current = root;

                    BinaryTreeNode<T> rightNode = null;
                    BinaryTreeNode<T> parentNode = null;

                    bool goLeftNext = true;

                    stack.Push(current);

                    while (stack.Count > 0)
                    {
                        if (goLeftNext)
                        {
                            while (current.Left != null || current.Right != null)
                            {
                                if (current.Left != null)
                                    current = current.Left;

                                else if (current.Right != null)
                                    current = current.Right;

                                stack.Push(current);
                            }
                        }

                        yield return stack.Pop().Value; // Back node from the stack.

                        if (stack.Count > 0)
                        {
                            parentNode = stack.Peek();
                            rightNode = stack.Peek().Right;
                        }
                        else
                            continue;

                        if (rightNode != null && rightNode != current)
                        {
                            current = rightNode;
                            stack.Push(current);
                            goLeftNext = true;
                        }
                        else
                        {
                            current = parentNode;
                            goLeftNext = false;
                        }
                    }
                }
            }
        }

        #endregion

        #region Deleting tree

        public void Clear()
        {
            root = null;
            Counter = 0;
        }

        #endregion

        #region Deleting node

        public bool Remove(IComparable<T> node)
        {
            return Remove((T)node, null);
        }

        public bool Remove(T value, Comparable<T> compare)
        {
            BinaryTreeNode<T> current;
            BinaryTreeNode<T> parent;

            current = FindValue(value, out parent, compare);

            if (current == null)
                return false;

            Counter--;
            // 1st. Remoting node has no right node.
            if (current.Right == null)
            {
                if (parent == null)
                    root = current.Left;
                else
                {
                    int result = parent.CompareTo(current.Value, compare);

                    if (result < 0)
                        parent.Left = current.Left;
                    else if (result > 0)
                        parent.Right = current.Left;
                }
            }
            // 2nd. Remoting node has right node which has no left node.
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                    root = current.Right;
                else
                {
                    int result = parent.CompareTo(current.Value, compare);

                    if (result < 0)
                        parent.Left = current.Right;
                    else if (result > 0)
                        parent.Right = current.Left;
                }
            }
            // 3rd. Remoting node has right node which has left node.
            else
            {
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                leftmostParent.Left = leftmost.Right;

                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                    root = leftmost;

                else
                {
                    int result = parent.CompareTo(current.Value, compare);

                    if (result < 0)
                        parent.Left = leftmost;

                    else if (result > 0)
                        parent.Right = leftmost;
                }
            }
            return true;
        }

        #endregion

        #region Finding a value

        public bool Contains(IComparable<T> node)
        {
            return Contains((T)node, null);
        }

        public bool Contains(T value, Comparable<T> compare)
        {
            BinaryTreeNode<T> parent;
            return FindValue(value, out parent, compare)!=null;
        }

        BinaryTreeNode<T> FindValue(T value, out BinaryTreeNode<T> parent, Comparable<T> compare)
        {
            BinaryTreeNode<T> current = root;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value, compare);
                if (result < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result > 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                    break;
            }
            return current;
        }

        #endregion
    }
}