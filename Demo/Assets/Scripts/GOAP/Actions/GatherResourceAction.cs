using TestGOAP;
using UnityEngine;

public class GatherResourceAction : Action
{
    private bool hasGathered = false;

    private void Start()
    {
        AddPrecondition("AtResource", true); // 必须在资源点才能采集
        AddEffect("HasResource", true);      // 采集后获得资源
    }

    public override bool PerformAction()
    {
        if (hasGathered) return false;

        Debug.Log("Gathering resources...");
        hasGathered = true;

        // 模拟采集资源
        Invoke(nameof(FinishGathering), 2f);
        return true;
    }

    private void FinishGathering()
    {
        Debug.Log("Resources gathered!");
    }
}
