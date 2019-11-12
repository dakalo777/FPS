using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryControllerAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform head;

    private IWeapon weapon;
    [SerializeField] private Transform aimPosition;

    [SerializeField] private FieldOfView fov;
    [SerializeField] private float AI_tick;
    [SerializeField] private bool isActive;
    [SerializeField] private float rotationSpeed;


    private Quaternion desireRotation;
    private bool hasTarget = false;

    private void Start()
    {
        weapon = GetComponentInChildren<IWeapon>();
        StartCoroutine(Tick());

    }

    private void Update()
    {
        if (hasTarget)
        {
            var direction = target.position - head.transform.position;
            var newRotation = Quaternion.LookRotation(direction);
            newRotation.x = 0f;
            newRotation.z = 0f;

            head.rotation = Quaternion.Slerp(head.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);


        }
    }

    private IEnumerator Tick()
    {
        while (isActive)
        {
            hasTarget = false;
            if (fov.isTargetOnRange(target))
            {
                hasTarget = true;
                AttackPlayer();
            }

            yield return new WaitForSeconds(AI_tick);
        }
    }

    private void AttackPlayer()
    {
        if (weapon != null)
        {
            weapon.Use(aimPosition.position);            
        }


    }
}
