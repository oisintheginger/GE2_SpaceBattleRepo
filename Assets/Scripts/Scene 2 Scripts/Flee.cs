using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : SteerBehaviour
{
    public GameObject targetGameObject = null;
    public Vector3 target = Vector3.zero;

    public override Vector3 CalculateForce()
    {
        return -boid.SeekForce(target);
    }

    public void Update()
    {
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
    }
}
