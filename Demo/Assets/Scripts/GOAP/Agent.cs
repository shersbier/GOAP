using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGOAP
{
    public class Agent : MonoBehaviour
    {
        public List<Action> actions;  // 所有可用的动作
        private List<Goal> goals;      // 角色的目标列表
        public WorldState worldState; // 当前世界状态

        private void Awake()
        {
            actions = new List<Action>(GetComponents<Action>());
            goals = new List<Goal>();
            worldState = new WorldState();
        }

        // 添加目标
        public void AddGoal(Goal goal)
        {
            goals.Add(goal);
        }

        // 规划和执行
        public void PlanAndExecute()
        {
            // 找到优先级最高的目标
            Goal highestPriorityGoal = goals.OrderByDescending(goal => goal.Priority).FirstOrDefault();
            if (highestPriorityGoal == null) return;

            // 使用 A* 搜索路径
            var plan = PlanUsingAStar(highestPriorityGoal);
            if (plan == null || plan.Count == 0)
            {
                //Debug.Log("No plan found for goal: " + highestPriorityGoal.Name);
                return;
            }

            // 执行计划
            ExecutePlan(plan);
        }

        // 使用 A* 搜索计划
        private List<Action> PlanUsingAStar(Goal goal)
        {
            var openList = new List<PlanNode>();        // 开放列表（待检查的节点）
            var closedList = new HashSet<WorldState>(); // 关闭列表（已检查的状态）

            // 初始化起点节点
            var startNode = new PlanNode(worldState, null, null, 0, CalculateHeuristic(worldState, goal));
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // 找到开放列表中 f(n) 最小的节点
                PlanNode currentNode = openList.OrderBy(node => node.TotalCost).First();
                openList.Remove(currentNode);
              
                // 如果达成目标，构建路径
                if (IsGoalSatisfied(currentNode.State, goal))
                {
                    return ReconstructPlan(currentNode);
                }

                // 将当前状态加入关闭列表
                closedList.Add(currentNode.State);
                //Debug.Log("当前关闭列表的节点状态: " + PrintState(currentNode.State)); 
               
                // 遍历所有可能的动作
                foreach (var action in actions)
                {
                    if (CanPerformAction(currentNode.State, action))
                    {
                       // Debug.Log("当前关闭列表的节点状态: " + action.name);
                      
                        // 执行动作后的新状态
                        WorldState newState = ApplyActionEffects(currentNode.State, action);

                        // 如果新状态已经在关闭列表中，跳过
                        bool isExist = false;
                        foreach (WorldState state in closedList)
                        {
                            //存在键值不相等的情况
                            foreach (KeyValuePair<string, bool> s in state.StateDic)
                            {
                                if (newState.StateDic[s.Key] != s.Value)
                                {
                                    isExist = true;
                                    break;
                                }
                            }
                        }
                        if (!isExist) continue;

                        // 动作的代价
                        float newCost = currentNode.Cost + action.Cost;

                        // 如果新状态不在开放列表，或者更优的路径被发现
                        var existingNode = openList.FirstOrDefault(node => node.State.Equals(newState));
                        if (existingNode == null || newCost < existingNode.Cost)
                        {
                            var heuristic = CalculateHeuristic(newState, goal);
                            var newNode = new PlanNode(newState, currentNode, action, newCost, heuristic);

                            if (existingNode != null)
                            {
                                openList.Remove(existingNode);
                            }

                            openList.Add(newNode);
                            //Debug.Log("当前关闭列表的节点状态: " + action.GetType() );
                        }
                    }
                }
            }

            // 如果没有找到路径，返回空
            return null;
        }

        // 辅助方法：打印世界状态
        private string PrintState(WorldState state)
        {
            var sb = new System.Text.StringBuilder();
            foreach (var kvp in state.StateDic)
            {
                sb.Append($"{kvp.Key}: {kvp.Value}, ");
            }
            return sb.ToString();
        }
        
        
        // 检查是否满足目标
        private bool IsGoalSatisfied(WorldState state, Goal goal)
        {
            foreach (var target in goal.TargetState)
            {
                if (!state.HasState(target.Key) || state.StateDic[target.Key] != target.Value)
                {
                    return false;
                }
            }

            return true;
        }

        // 检查动作是否可执行
        private bool CanPerformAction(WorldState state, Action action)
        {
            foreach (var precondition in action.Preconditions)
            {
                if (state.HasState(precondition.Key) && state.StateDic[precondition.Key] != precondition.Value)
                {
                    Debug.Log("当前世界状态下是否满足前置条件: "  + state.HasState(precondition.Key) + "前置条件的值"+ state.StateDic[precondition.Key]+ "="+precondition.Value);
                    return false;
                }
            }
            
            return true;
        }

        // 应用动作的效果
        private WorldState ApplyActionEffects(WorldState state, Action action)
        {
            var newState = state.Clone();
            foreach (var effect in action.Effects)
            {
                newState.SetState(effect.Key, effect.Value);
            }

            return newState;
        }

        // 计算启发式函数
        private float CalculateHeuristic(WorldState state, Goal goal)
        {
            float heuristic = 0;
            foreach (var target in goal.TargetState)
            {
                if (!state.HasState(target.Key) || state.StateDic[target.Key] != target.Value)
                {
                    heuristic += 1; // 每个未满足的目标 +1
                }
            }

            return heuristic;
        }

        // 重建计划路径
        private List<Action> ReconstructPlan(PlanNode node)
        {
            var plan = new List<Action>();
            while (node.Parent != null)
            {
                plan.Insert(0, node.Action); // 从尾部向头部插入动作
                node = node.Parent;
            }

            return plan;
        }

        // 执行计划
        private void ExecutePlan(List<Action> plan)
        {
            foreach (var action in plan)
            {
                if (!action.PerformAction())
                {
                    Debug.LogError("Action failed: " + action.name);
                    break;
                }
            }
        }
    }
}
