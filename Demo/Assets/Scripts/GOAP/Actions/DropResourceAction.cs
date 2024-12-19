using TestGOAP;
using UnityEngine;

public class DropResourceAction : Action
{
    private bool hasDropped = false;

    private void Start()
    {
        AddPrecondition("AtBase", true);      // 必须在基地才能交付资源
        AddPrecondition("HasResource", true); // 必须有资源才能交付
        AddEffect("DeliveredResource", true); // 交付后完成目标
    }

    public override bool PerformAction()
    {
        if (hasDropped) return false;

        Debug.Log("Dropping resources at base...");
        hasDropped = true;

        // 模拟交付资源
        Invoke(nameof(FinishDropping), 2f);
        return true;
    }

    private void FinishDropping()
    {
        Debug.Log("Resources delivered!");
    }
}
