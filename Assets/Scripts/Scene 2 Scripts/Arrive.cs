using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : SteerBehaviour
{
    public Vector3 targetPosition = Vector3.zero;
    public float slowingDistance = 15.0f;

    public GameObject targetGameObject = null;

    public override Vector3 CalculateForce()
    {
        Vector3 force = boid.ArriveForce(targetPosition, slowingDistance);
        return force;
    }

    public void Update()
    {
        if (targetGameObject != null)
        {
            targetPosition = targetGameObject.transform.position;
        }
    }
}
