using System;
using System.Collections.Generic;

namespace Distance
{

    class Node
    {
        public int Key { get; set; }
        public bool Visited { get; set; }
        //Supports two children nodes (so a binary tree)
        public Node Left { get; set; }
        public Node Right { get; set; }

    }

    class Program
    {
        public static void Main()
        {
            const int NODE_SIZE = 0;
            Random rand = new Random();
            Node root = new Node();
            root.Key = NODE_SIZE;
            List<int> values = new List<int>(NODE_SIZE);
            for (int i = 0; i < NODE_SIZE; i += 2)
            {
                Node left = new Node();
                left.Key = i;
                Node Right = new Node();
                Right.Key = i + 1;

            }


            root.Left = new Node();
            root.Right = new Node();
            root.Right.Key = 4;
            root.Left.Key = 1;
            root.Left.Left = new Node();
            root.Left.Left.Key = 2;
            root.Left.Left.Right = new Node();
            root.Left.Left.Right.Key = 3;
            root.Right.Left = new Node();
            root.Right.Left.Key = 5;
            root.Right.Left.Left = new Node();
            root.Right.Left.Left.Key = 6;
            root.Right.Right = new Node();
            root.Right.Right.Key = 8;
            root.Right.Right.Left = new Node();
            root.Right.Right.Left.Key = 7;
            Console.WriteLine("Distance: {0}", FindDistance(root, 1, 7));
        }

        public static int FindDistance(Node root, int v1, int v2) {
            if (root == null) {
                throw new ArgumentNullException("Root Node is null");
            }
            List<int> searchKeys = new List<int>();
            searchKeys.Add(v1);
            searchKeys.Add(v2);
            HashSet<int> visited = new HashSet<int>();
            Stack<Node> path = new Stack<Node>();
            path.Push(root);
            Stack<Node> firstFullPath = null;

            while(path.Count > 0) {
                Node current = path.Peek();
                if (current == null) {
                    throw new Exception("This is really broke, how did we get here");
                }
                if (searchKeys.Contains(current.Key) && !visited.Contains(current.Key)) {
                    if (firstFullPath == null) {
                        //Create an array of the elements and reverse it to preserve stack order
                        Node[] temp = path.ToArray();
                        Array.Reverse(temp);
                        firstFullPath = new Stack<Node>(temp);
                        searchKeys.Remove(current.Key);
                    } else {
                        return tracePaths(firstFullPath, path);
                    }
                }
                bool checkLeft = current.Left != null && !visited.Contains(current.Left.Key);
                bool checkRight = current.Right != null && !visited.Contains(current.Right.Key);
                if (!checkLeft && !checkRight) {
                    path.Pop();
                }
                if (checkLeft) {
                    path.Push(current.Left);
                }
                if (checkRight) {
                    path.Push(current.Right);
                }
                visited.Add(current.Key);
            }
            return -1;
        }

        private static int tracePaths(Stack<Node> p1, Stack<Node> p2) {
            int count = 0;
            Stack<Node> longer;
            Stack<Node> shorter;
            if(p1.Count > p2.Count) {
                longer = p1;
                shorter = p2;
            } else {
                longer = p2;
                shorter = p1;
            }
            //Trim the extra values until the stacks are the same length
            while (longer.Count > shorter.Count) {
                longer.Pop();
                count++;
            }

            //Pop boths stacks until the nodes at the same level are the the same node
            while(shorter.Peek() != longer.Peek()) {
                shorter.Pop();
                longer.Pop();
                count += 2;
            }

            return count;
        }
    }
}
