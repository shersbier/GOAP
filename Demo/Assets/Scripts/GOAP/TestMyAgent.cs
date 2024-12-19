using System.Collections;
using System.Collections.Generic;
using TestGOAP;
using UnityEngine;

public class TestMyAgent : MonoBehaviour
{
    public Transform resourcePoint; // 资源点位置
    public Transform basePoint;     // 基地位置
    private Agent agent;

    private void Start()
    {
        agent = GetComponent<Agent>();

        // 添加初始世界状态
        agent.worldState.SetState("HasTarget", false);
        agent.worldState.SetState("AtTarget", false);
        agent.worldState.SetState("AtResource", false);
        agent.worldState.SetState("AtBase", false);
        agent.worldState.SetState("HasResource", false);
        agent.worldState.SetState("DeliveredResource", false);

        // 创建目标：交付资源到基地
        Goal deliverResourceGoal = new Goal("DeliverResource", 1);
        deliverResourceGoal.AddTargetState("DeliveredResource", true);
        agent.AddGoal(deliverResourceGoal);

        // 配置动作
        SetupActions();
        
        agent.PlanAndExecute();
    }

    private void SetupActions()
    {
        // MoveToAction for Resource Point
        MoveToAction moveToResource = gameObject.AddComponent<MoveToAction>();
        moveToResource.target = resourcePoint;
        moveToResource.AddPrecondition("AtBase", false);
        moveToResource.AddEffect("AtResource", true);
        this.agent.actions.Add(moveToResource);
        // GatherResourceAction
        GatherResourceAction gatherResource = gameObject.AddComponent<GatherResourceAction>();
        gatherResource.AddPrecondition("AtResource", true);
        gatherResource.AddEffect("HasResource", true);
        this.agent.actions.Add(gatherResource);
        // MoveToAction for Base
        MoveToAction moveToBase = gameObject.AddComponent<MoveToAction>();
        moveToBase.target = basePoint;
        moveToBase.AddPrecondition("AtResource", false);
        moveToBase.AddEffect("AtBase", true);
        this.agent.actions.Add(moveToBase);
        // DropResourceAction
        DropResourceAction dropResource = gameObject.AddComponent<DropResourceAction>();
        dropResource.AddPrecondition("AtBase", true);
        dropResource.AddPrecondition("HasResource", true);
        dropResource.AddEffect("DeliveredResource", true);
        this.agent.actions.Add(dropResource);
    }

    private void Update()
    {
        // 每帧尝试规划和执行动作
     
    }
}
