using System.Collections;
using System.Collections.Generic;
using Astar;
using UnityEngine;
using Grid = UnityEngine.Grid;

namespace Astar
{
    
    public class AStarAlgorithm
    {
        //网格地图
        public AStar_Grid AStarGrid;
        //节点路径库
        public List<Node> path;
        
        public AStarAlgorithm(AStar_Grid aStarGrid)
        {
            this.AStarGrid = aStarGrid;
        }

        // 寻路核心函数
        public List<Node> FindPath(Vector2 start,Vector2 end)
        {
           Node startNode = this.AStarGrid.GetNodeFromWorldPosition(start);
           Node endNode = this.AStarGrid.GetNodeFromWorldPosition(end);
           
           if (startNode == null || endNode == null)
           {
               return null;
           }

           startNode.gCost = 0;
           startNode.hCost = Distance(startNode, endNode);
           startNode.parent = null;
           
           List<Node> openList = new List<Node> { startNode };
           /*
            * 用hashSet的意义
            * 时间复杂度0（1）的查找操作
            * hashset不允许重复的元素
            * 内部使用哈希表实现，内存更少
            * 避免排序开销
            */
           HashSet<Node> closeList = new HashSet<Node>();

           while (openList.Count > 0)
           {
               Node current = openList[0];
               for (int i = 0; i < openList.Count; i++)
               {
                   //总代价边界条件对比
                   if (openList[i].fCost < current.fCost || (Mathf.Approximately(openList[i].fCost, current.fCost) && openList[i].hCost < current.hCost))
                   {
                       current = openList[i];
                   }
               }
               openList.Remove(current);
               closeList.Add(current);
               //终点条件
               if (current == endNode)
               {
                   return ReconstructPath(current);
               }
                //找邻居点了
                List<Node> neighbors = GetNeighbors(current);
               foreach (var node in neighbors)
               {
                   if (closeList.Contains(node))
                   {
                       continue;
                   }
                    //起点到当前邻点的代价
                   float tentativeGCost = current.gCost + Distance(node, current);
                   if (tentativeGCost < node.gCost || !openList.Contains(node))
                   {
                       node.parent = current;
                       node.gCost = tentativeGCost;
                       //终点到当前邻点的距离代价
                       node.hCost = Distance(node, endNode);
                       if (!openList.Contains(node))
                       {
                           openList.Add(node);
                       }
                   }

               }
               
           }

           return null;
        }

        // 从终点开始，通过父节点，还原重构路径
        private List<Node> ReconstructPath(Node endNode)
        {
            List<Node> path = new List<Node>();
            Node current = endNode;
            while (current != null)
            {
                path.Add(current);
                current = current.parent;
            }
            path.Reverse();
            return path;
        }

        //找8位邻居
        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();
            
            //可空值类型的写法
            Vector2Int? gridPosition = this.AStarGrid.GetWorldPosition(node.position);
            if (gridPosition == null) return neighbors;

            int x = gridPosition.Value.x;
            int y = gridPosition.Value.y;

            //找邻近的节点，8个点，麻烦的是边界条件
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1 ; dy++)
                {
                    if (dx == 0 && dy== 0)
                    {
                        continue;
                    }

                    Vector2Int neighborPos = new Vector2Int(x + dx, y + dy);
                    if (neighborPos.x < 0 || neighborPos.x >= this.AStarGrid.width || neighborPos.y < 0 || neighborPos.y >= this.AStarGrid.height)
                    {
                        continue;
                    }
                    neighbors.Add(this.AStarGrid.nodes[neighborPos.x, neighborPos.y]);
                }
            }

            return neighbors;
        }


        //计算距离代价，非常简单粗暴的计算两点直线距离
        private float Distance(Node a , Node b)
        {
            return Vector2.Distance(a.position, b.position);
        }
    }
}
