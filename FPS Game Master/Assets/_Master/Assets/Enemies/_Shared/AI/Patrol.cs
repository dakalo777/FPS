using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    private NavMeshAgent agent;
    private FieldOfView fieldOfView;
    private Transform target;
    [SerializeField] private bool isActive;
    private EnemieHealth health;

    private void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemieHealth>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (fieldOfView.isTargetOnRange(target) && isActive && health.CurrentHealth > 0)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        var desirePosition = target.position;
        agent.SetDestination(desirePosition);

    }

}
