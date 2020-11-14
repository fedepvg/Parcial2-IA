using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public delegate void OnTargetFound(GameObject target);
	public OnTargetFound onTargetFoundAction;

    public void FindVisibleTargets()
	{
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					if (onTargetFoundAction != null)
						onTargetFoundAction(target.gameObject);
				}
			}
		}
	}	
}
