using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : SteerBehaviour
{
    public Boid targetBoid;

    public Vector3 targetPosition;

    public override Vector3 CalculateForce()
    {

        float dist = Vector3.Distance(targetBoid.transform.position, transform.position);
        float time = dist / boid.maxSpeed;

        targetPosition = targetBoid.transform.position + (targetBoid.velocity * time);

        return boid.SeekForce(targetPosition);
    }
}
