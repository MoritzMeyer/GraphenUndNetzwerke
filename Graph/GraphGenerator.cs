using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Tests")]
[assembly: InternalsVisibleTo("GraphSearch")]

namespace GraphCollection
{
    public static class GraphGenerator
    {
        public static Graph<BucketNode> BucketGraph(int capacityBucket1, int capacityBucket2, BucketNode root = null)
        {
            if (root == null)
            {
                root = new BucketNode("0-0", capacityBucket1, capacityBucket2, 0, 0);
            }

            // Initialize graph and root node.            
            root = new BucketNode(root.Caption, root.Capacity1, root.Capacity2, root.Content1, root.Content2);
            Queue<BucketNode> notProcessedNodes = new Queue<BucketNode>(GetPossibleChildNodes(root));
            root.Neighbors.AddRange(notProcessedNodes);

            Graph<BucketNode> graph = new Graph<BucketNode>(root);

            while(notProcessedNodes.Count > 0)
            {
                BucketNode actualNode = notProcessedNodes.Dequeue();
                graph.AddNode(actualNode);

                IEnumerable<BucketNode> childNodes = GetPossibleChildNodes(actualNode);

                foreach(BucketNode node in childNodes)
                {
                    // if graph doesn't contains node, add node as neigbor of actual and add to list of not processed nodes.
                    if(!graph.Contains(node))
                    {
                        actualNode.AddNeighbor(node);
                        notProcessedNodes.Enqueue(node);
                    }
                    else
                    {
                        // if graph contains node, get this node instance and add it to actual node as neighbor.
                        BucketNode childNodeInstance = (BucketNode)graph.GetNodeInstance(node);
                        actualNode.AddNeighbor(childNodeInstance);
                    }
                }
            }

            return graph;
        }

        #region failure
        //private static void GenerateBucketGraph(ref Graph graph, ref BucketNode root)
        //{
        //    // Get all child nodes for root node.
        //    List<BucketNode> rootChildNodes = GetPossibleChildNodes(root).ToList();
        //    bool allChildsExistsWithinGraph = true;
        //    for (int i = 0; i < rootChildNodes.Count(); i++)
        //    {
        //        if (graph.Contains(rootChildNodes.ElementAt(i)))
        //        {
        //            rootChildNodes[i] = (BucketNode)graph.GetNodeInstance(rootChildNodes.ElementAt(i));
        //        }
        //        else
        //        {
        //            allChildsExistsWithinGraph = false;
        //        }
        //    }

        //    // Add neighbors to the root node.
        //    foreach (Node node in rootChildNodes)
        //    {
        //        if (!root.AddNeighbor(node))
        //        {
        //            throw new ArgumentException("A child Node couldn't be added to the root neigbours list.");
        //        }
        //    }

        //    if (!allChildsExistsWithinGraph)
        //    {
        //        foreach(BucketNode node in rootChildNodes)
        //        {
        //            if (!graph.Contains(node))
        //            {
        //                GenerateBucketGraph(ref graph, ref node);
        //            }
        //        }
        //    }
        //}
        #endregion

        #region GetPossibleChildNodes
        /// <summary>
        /// Generates a list with all possible child nodes for one bucket node.
        /// </summary>
        /// <param name="root">the bucket node for which the child nodes should be generated.</param>
        /// <returns>A list with possible child nodes.</returns>
        public static IEnumerable<BucketNode> GetPossibleChildNodes(BucketNode root)
        {
            // List with possibleChildNodes.
            List<BucketNode> childNodes = new List<BucketNode>();

            // 1. Check if Bucket 1 can be filled.
            if (root.Capacity1 > root.Content1)
            {
                childNodes.Add(
                    new BucketNode(
                        $"{root.Capacity1}-{root.Content2}",
                        root.Capacity1,
                        root.Capacity2,
                        root.Capacity1,
                        root.Content2));
            }

            // 2. Check if Bucket 2 can be filled.
            if (root.Capacity2 > root.Content2)
            {
                childNodes.Add(
                    new BucketNode(
                        $"{root.Content1}-{root.Capacity2}",
                        root.Capacity1,
                        root.Capacity2,
                        root.Content1,
                        root.Capacity2));
            }

            // 3. Check if bucket 1 can be emptied
            if (root.Content1 > 0)
            {
                childNodes.Add(
                    new BucketNode(
                        $"0-{root.Content2}",
                        root.Capacity1,
                        root.Capacity2,
                        0,
                        root.Content2));
            }

            // 4. Check if bucket 2 can be emptied
            if (root.Content2 > 0)
            {
                childNodes.Add(
                    new BucketNode(
                        $"{root.Content1}-0",
                        root.Capacity1,
                        root.Capacity2,
                        root.Content1,
                        0));
            }

            // 5. Check if bucket 2 can be filled from bucket 1
            if (root.Capacity2 > root.Content2 && root.Content1 > 0)
            {
                // Calculate free space for bucket 2
                int freeSpace = root.Capacity2 - root.Content2;

                if (root.Content1 <= freeSpace)
                {
                    int newContent2 = root.Content1 + root.Content2;
                    childNodes.Add(
                        new BucketNode(
                            $"0-{newContent2}",
                            root.Capacity1,
                            root.Capacity2,
                            0,
                            newContent2));
                }
                else
                {
                    int remainingContent1 = root.Content1 - freeSpace;
                    childNodes.Add(
                        new BucketNode(
                            $"{remainingContent1}-{root.Capacity2}",
                            root.Capacity1,
                            root.Capacity2,
                            remainingContent1,
                            root.Capacity2));
                }                
            }

            // 6. Check if bucket 1 can be filled from bucket 2
            if (root.Capacity1 > root.Content1 && root.Content2 > 0)
            {
                // Calculate free space for bucket 2
                int freeSpace = root.Capacity1 - root.Content1;

                if (root.Content2 <= freeSpace)
                {
                    int newContent1 = root.Content1 + root.Content2;
                    childNodes.Add(
                        new BucketNode(
                            $"{newContent1}-0",
                            root.Capacity1,
                            root.Capacity2,
                            newContent1,
                            0));
                }
                else
                {
                    int remainingContent2 = root.Content2 - freeSpace;
                    childNodes.Add(
                        new BucketNode(
                            $"{root.Capacity1}-{remainingContent2}",
                            root.Capacity1,
                            root.Capacity2,
                            root.Capacity1,
                            remainingContent2));
                }
            }

            return childNodes;
        }
        #endregion
    }
}