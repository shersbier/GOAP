using TestGOAP;
using UnityEngine;

public class MoveToAction : Action
{
    public Transform target; // 要移动到的目标位置
    public float moveSpeed = 5f;

    private void Start()
    {
        AddPrecondition("HasTarget", true); // 移动前需要有目标
        AddEffect("AtTarget", true);        // 移动后会到达目标
    }

    public override bool PerformAction()
    {
        if (target == null) return false;

        // 移动到目标位置
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

        // 如果接近目标位置，返回成功
        return Vector3.Distance(transform.position, target.position) < 0.1f;
    }
}
