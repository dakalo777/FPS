using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;
    private Transform target;
    [SerializeField] private float detectionRadius;
    [SerializeField] private float visionAngle;
    private void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(this.transform.position, (target.position - this.transform.position).normalized * detectionRadius);
        }

        UnityEditor.Handles.color = new Color32(25, 25, 25, 75);
        UnityEditor.Handles.DrawSolidDisc(this.transform.position, transform.up, detectionRadius);

        UnityEditor.Handles.color = new Color32(0, 255, 0, 50);
        UnityEditor.Handles.DrawSolidArc(this.transform.position, transform.up, transform.forward, visionAngle, detectionRadius);
        UnityEditor.Handles.DrawSolidArc(this.transform.position, transform.up, transform.forward, -visionAngle, detectionRadius);



        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, transform.forward * detectionRadius);
    }



    public bool isTargetOnRange(Transform target)
    {     
        this.target = target;
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(this.transform.position, detectionRadius, overlaps,targetMask);
        for (int i = 0; i < overlaps.Length; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionToTarget = (target.position - this.transform.position).normalized;
                    directionToTarget.y *= 0;

                    float angle = Vector3.Angle(this.transform.forward, directionToTarget);

                    if (angle <= visionAngle)
                    {
                        Ray ray = new Ray(this.transform.position, target.position - this.transform.position);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, detectionRadius))
                        {
                            if (hit.transform != null)
                            {
                                if (hit.transform == target)
                                {                                    
                                    return true;
                                }
                            }
                        }

                    }
                }
            }
        }

        return false;
    }


}
