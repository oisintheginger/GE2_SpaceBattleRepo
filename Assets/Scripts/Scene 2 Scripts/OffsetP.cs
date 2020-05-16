using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetP : SteerBehaviour
{
    public Boid leader;
    Vector3 targetPos;
    Vector3 worldTarget;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - leader.transform.position;
        offset = Quaternion.Inverse(leader.transform.rotation) * offset;
    }

    public override Vector3 CalculateForce()
    {
        worldTarget = leader.transform.TransformPoint(offset);
        float dist = Vector3.Distance(worldTarget, transform.position);
        float time = dist / boid.maxSpeed;
        targetPos = worldTarget + (leader.velocity * time);
        force = boid.ArriveForce(targetPos);
        return force;
    }
}

