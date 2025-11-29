using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FormationFollower : MonoBehaviour
{
    private NavMeshAgent agent;
    /// <summary>
    /// Cannot be Vector3.zero because it's the target position
    /// </summary>
    private Vector3 localTargetPos = Vector3.zero;
    private Transform target;

    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform target, Vector3 localPos)
    {
        localTargetPos = localPos;
        this.target = target;
    }

  
    void Update()
    {
        if (CheckAttributes())
        {
            agent.destination = target.TransformPoint(localTargetPos);
            agent.speed = target.GetComponent<NavMeshAgent>().speed;
        }
        else
            Debug.LogError("Some attributes may not be correct");
    }

    private bool CheckAttributes()
    {
        return localTargetPos != null && localTargetPos != Vector3.zero && target && agent && target.GetComponent<NavMeshAgent>();
    }
}
