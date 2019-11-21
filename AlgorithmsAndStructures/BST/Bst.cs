using System;
using System.IO;

namespace AlgorithmsAndStructures.BST
{
    class Bst
    {
        public class Node
        {
            public long Value { get; set; }
            public Node Parent { get; set; }
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }

            public Node(long value)
            {
                Value = value;
            }
        }

        private Node root;

        public Bst()
        {
            root = null;
        }

        public void Insert(long value)
        {
            Node node = root;
            Node newNode = new Node(value);
            while (node != null)
            {
                newNode.Parent = node;
                if (node.Value > newNode.Value)
                {
                    node = node.LeftChild;
                }
                else if (node.Value < newNode.Value)
                {
                    node = node.RightChild;
                }
                else
                {
                    return;
                }
            }

            if (newNode.Parent == null)
                root = newNode;
            else if (newNode.Parent.Value > newNode.Value)
                newNode.Parent.LeftChild = newNode;
            else if (newNode.Parent.Value < newNode.Value)
                newNode.Parent.RightChild = newNode;
        }

        public void Delete(long value)
        {
            Node node = root;
            while (node != null)
            {
                if (node.Value > value)
                {
                    node = node.LeftChild;
                }
                else if (node.Value < value)
                {
                    node = node.RightChild;
                }
                else
                {
                    break;
                }
            }

            if (node == null)
                return;

            if (node.LeftChild == null && node.RightChild == null)
            {
                Node parent = node.Parent;
                if (node.Parent == null)
                {
                    root = null;
                }
                else if(parent.Value > value)
                {
                    parent.LeftChild = null;
                }
                else
                {
                    parent.RightChild = null;
                }
            } 
            else if (node.LeftChild == null || node.RightChild == null)
            {
                Node child = node.LeftChild ?? node.RightChild;
                Node parent = node.Parent;
                if (node.Parent == null)
                {
                    root = child;
                }
                else if (node.Parent.Value > value)
                {
                    parent.LeftChild = child;
                }
                else
                {
                    parent.RightChild = child;
                }

                child.Parent = parent;
            }
            else
            {
                Node leastNode = node.RightChild;
                while (leastNode.LeftChild != null)
                    leastNode = leastNode.LeftChild;
                node.Value = leastNode.Value;
                if (leastNode.Parent.LeftChild == leastNode)
                {
                    leastNode.Parent.LeftChild = leastNode.RightChild;
                }
                else
                {
                    leastNode.Parent.RightChild = leastNode.RightChild;
                }
            }
        }

        public bool Exists(long value)
        {
            Node node = root;
            while(node != null)
            {
                if (node.Value > value)
                {
                    node = node.LeftChild;
                }
                else if (node.Value < value)
                {
                    node = node.RightChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Elements(Node node)
        {
            if (node == null)
                return;
            Elements(node.LeftChild);
            Console.WriteLine(node.Value);
            Elements(node.RightChild);
        }

        public Node Next(long value)
        {
            Node node = root;
            if (node == null)
                return null;
            while (true)
            {
                if (node.Value > value && node.LeftChild != null)
                    node = node.LeftChild;
                else if (node.Value < value && node.RightChild != null)
                    node = node.RightChild;
                else 
                    break;
                
            }

            if (node.Value > value)
            {
                return node;
            }

            if (node.RightChild != null)
            {
                Node leastNode = node.RightChild;
                while (leastNode.LeftChild != null)
                    leastNode = leastNode.LeftChild;
                return leastNode;
            }

            while (node != null && node.Value <= value)
                node = node.Parent;
            return node;
        }

        public Node Prev(long value)
        {
            Node node = root;
            if (node == null)
                return null;
            while (true)
            {
                if (node.Value > value && node.LeftChild != null)
                    node = node.LeftChild;
                else if (node.Value < value && node.RightChild != null)
                    node = node.RightChild;
                else
                    break;

            }

            if (node.Value < value)
            {
                return node;
            }

            if (node.LeftChild != null)
            {
                Node leastNode = node.LeftChild;
                while (leastNode.RightChild != null)
                    leastNode = leastNode.RightChild;
                return leastNode;
            }

            while (node != null && node.Value >= value)
                node = node.Parent;
            return node;
        }
    }

    class SolutionBst
    {
        private static void Test()
        {
            Bst bst = new Bst();
            bst.Insert(2);
            bst.Insert(5);
            bst.Insert(3);
            Console.WriteLine(bst.Exists(2) ? "true" : "false");
            Console.WriteLine(bst.Exists(4) ? "true" : "false");
            Console.WriteLine(bst.Next(4)?.Value.ToString() ?? "none");
            Console.WriteLine(bst.Prev(4)?.Value.ToString() ?? "none");
            bst.Delete(5);
            Console.WriteLine(bst.Next(4)?.Value.ToString() ?? "none");
            Console.WriteLine(bst.Prev(4)?.Value.ToString() ?? "none");
            
        }

        public static void Solve()
        {
            using (var input = new StreamReader("bstsimple.in"))
            {
                using (var output = new StreamWriter("bstsimple.out"))
                {
                    Bst bst = new Bst();
                    while (true)
                    {
                        string[] line = input.ReadLine()?.Trim().Split();
                        if (line == null)
                            break;
                        switch (line[0])
                        {
                            case "insert":
                                bst.Insert(long.Parse(line[1]));
                                break;
                            case "delete":
                                bst.Delete(long.Parse(line[1])); 
                                break;
                            case "exists":
                                output.WriteLine(bst.Exists(long.Parse(line[1])) ? "true" : "false");
                                break;
                            case "next":
                                output.WriteLine(bst.Next(long.Parse(line[1]))?.Value.ToString() ?? "none");
                                break;
                            case "prev":
                                output.WriteLine(bst.Prev(long.Parse(line[1]))?.Value.ToString() ?? "none");
                                break;
                        }
                    }
                }
            }
        }
    }
}
