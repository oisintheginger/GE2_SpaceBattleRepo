using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteerBehaviour : MonoBehaviour
{
    public float weighting = 1f;
    public Vector3 force;


    [HideInInspector]
    public Boid boid;

    private void Awake()
    {
        boid = this.GetComponent<Boid>();

    }

    public abstract Vector3 CalculateForce();
}
