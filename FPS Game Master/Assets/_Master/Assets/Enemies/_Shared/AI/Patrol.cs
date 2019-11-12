using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    private NavMeshAgent agent;
    private FieldOfView fieldOfView;
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistanceToTarget;
    [SerializeField] private float tick;
    [SerializeField] private bool isActive;

    private void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Tick());
    }
    private IEnumerator Tick()
    {
        while (isActive)
        {
            if (fieldOfView.isTargetOnRange(target))
            {
                MoveToTarget();
            }
            yield return new WaitForSeconds(tick);
        }

    }

    private void MoveToTarget()
    {       
        var desirePosition = target.position;
        agent.SetDestination(desirePosition);          
    }

}
