using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericMovementScript : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float mass = 1.0f;
    public float maxSpeed;
    public float maxForce;
    public float speed = 0f;
    public float slowingDistance;
    public float brakeSpeed;
    [Range(0.0f, 10f)]
    public float Banking = 3f, bankSpeed = 3f;

    public bool seekEnabled = false, arriveEnabled = false, isWandering = false, isPatrolling = false, isFleeing = false;
    public GameObject target;

    [SerializeField] int currentPoint, approachDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        force = CalculateForce();

        acceleration = force / mass;

        velocity = velocity + acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
        speed = velocity.magnitude;

        if (speed > 0.1f)
        {
            transform.forward = velocity;

            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * Banking), Time.deltaTime * bankSpeed);
            transform.LookAt(transform.position + velocity, tempUp);
        }
    }

    Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        if (seekEnabled)
        {
            force += Seek(target);
        }
        if (arriveEnabled)
        {
            force += Arrive(target);
        }
        if (isFleeing)
        {
            //force += Flee(target);
        }
        
        if (isWandering)
        {
            //Wander();
        }
        if (isPatrolling)
        {
            //target.transform.position = patrolPoints[currentPoint];
            //Patrol();

        }

        return force;
    }

    Vector3 Seek(GameObject target)
    {
        Vector3 toTarget = target.transform.position - this.gameObject.transform.position;
        Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
        return desiredVelocity - velocity;
    }

    Vector3 Arrive(GameObject target)
    {
        Vector3 correctedTarget = new Vector3(target.transform.position.x,
                                                target.transform.position.y,
                                                target.transform.position.z);

        Vector3 toTarget = correctedTarget - this.gameObject.transform.position;
        float dist = toTarget.magnitude;

        float rampSpeed = ((dist / slowingDistance)) * maxSpeed;
        float Clamped = Mathf.Min(rampSpeed, maxSpeed);

        Vector3 desiredVelocity = Clamped * (toTarget.normalized);

        return desiredVelocity - velocity;

    }
}
