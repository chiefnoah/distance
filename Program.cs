using System;
using System.Collections.Generic;

namespace Distance
{

    class Node
    {
        public int Key { get; set; }
        //Supports two children nodes (so a binary tree)
        public Node Left { get; set; }
        public Node Right { get; set; }

    }

    class Program
    {

        public static void Main()
        {
            Node root = new Node();
            root.Key = 0;
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
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
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

            //Pretty standard implementation of preorder depth-first search
            while(path.Count > 0) {
                Node current = path.Peek();
                if (current == null) {
                    //How did this get here? There should never be nulls in the stack
                    throw new Exception("This is really broke, how did we get here");
                }
                //If we found a node we haven't checked yet and it matches one of the search criteria...
                if (searchKeys.Contains(current.Key) && !visited.Contains(current.Key)) {
                    //... and haven't already found one of the search criteria
                    if (firstFullPath == null) {
                        //Create an array of the elements and reverse it to preserve stack order
                        Node[] temp = path.ToArray();
                        Array.Reverse(temp);
                        //And create a copy of the current stack, for later use
                        firstFullPath = new Stack<Node>(temp);
                        //Remove the discovered search key so we don't look for it again
                        searchKeys.Remove(current.Key); 
                    } else {
                        //We found the other one!
                        return tracePaths(firstFullPath, path);
                    }
                }
                //Does the current node have children that haven't been checked yet? 
                bool checkLeft = current.Left != null && !visited.Contains(current.Left.Key);
                bool checkRight = current.Right != null && !visited.Contains(current.Right.Key);
                if (!checkLeft && !checkRight) {
                    //No? Remove it from the stack
                    path.Pop();
                }
                //Add children nodes to the stack for searching
                if (checkLeft) {
                    path.Push(current.Left);
                }
                if (checkRight) {
                    path.Push(current.Right);
                }
                visited.Add(current.Key);
            }
            //This means one or more of the search keys weren't found :(
            return -1;
        }

        private static int tracePaths(Stack<Node> p1, Stack<Node> p2) {
            
            //Loop through the stacks and remove any sibling nodes that aren't directly
            //part of the path from target node to the root node
            LinkedList<Node> temp1 = new LinkedList<Node>();
            LinkedList<Node> temp2 = new LinkedList<Node>();
            while(p1.Count > 1) {
                Node current = p1.Pop();
                Node parent = p1.Peek();
                temp1.AddFirst(current); //Add at the beginning of the LL to preserve stack order
                //If the current node isn't a direct child of the next at the top of the stack
                //that next node isn't actually part of the path, remove it
                if (current != parent.Left && current != parent.Right) {
                    p1.Pop();
                }
            }
            temp1.AddFirst(p1.Pop());
            while (p2.Count > 1)
            {
                Node current = p2.Pop();
                Node parent = p2.Peek();
                temp2.AddFirst(current);
                //If the current node isn't a direct child of the next at the top of the stack
                //that next node isn't actually part of the path, remove it
                if (current != parent.Left && current != parent.Right)
                {
                    p2.Pop();
                }
            }
            temp2.AddFirst(p2.Pop());
            int count = 0;
            LinkedList<Node> longer;
            LinkedList<Node> shorter;
            if(temp1.Count > temp2.Count) {
                longer = temp1;
                shorter = temp2;
            } else {
                longer = temp2;
                shorter = temp1;
            }
            //Trim the extra values until the stacks are the same length
            while (longer.Count > shorter.Count) {
                longer.RemoveLast();
                count++;
            }

            //Pop boths stacks until the nodes at the same level are the the same node
            while(shorter.Last.Value != longer.Last.Value) {
                shorter.RemoveLast();
                longer.RemoveLast();
                count += 2;
            }

            return count;
        }
    }
}
