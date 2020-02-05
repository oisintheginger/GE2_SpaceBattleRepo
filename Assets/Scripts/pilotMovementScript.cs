using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilotMovementScript : MonoBehaviour
{
    Animator pilotAnim;

    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float mass = 1.0f;
    public float maxSpeed;
    public float maxForce;
    public float speed = 0f;
    public float slowingDistance;
    public float brakeSpeed;
    [Range(0.0f,10f)]
    public float Banking=3f, bankSpeed=3f;


    public bool seekEnabled = false, arriveEnabled = false;
    public GameObject target;

    private void Awake()
    {
        pilotAnim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        force = CalculateForce();

        acceleration = force / mass;

        velocity = velocity + acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
        speed = velocity.magnitude;

        if (speed > 1f)
        {
            transform.forward = velocity;

            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * Banking), Time.deltaTime * bankSpeed);
            transform.LookAt(transform.position + velocity, tempUp);
        }
    }
    
    public Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        if (seekEnabled)
        {
            force += Seek(target);
        }
        if(arriveEnabled)
        {
            force += Arrive(target);
        }
        
        return force;
    }

    public Vector3 Seek(GameObject target)
    {
        Vector3 toTarget = target.transform.position - this.gameObject.transform.position;
        Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
        return desiredVelocity - velocity;
    }
    public Vector3 Arrive(GameObject target)
    {
        Vector3 correctedTarget = new Vector3(target.transform.position.x, 
                                                this.transform.position.y, 
                                                target.transform.position.z);

        Vector3 toTarget = correctedTarget - this.gameObject.transform.position;
        float dist = toTarget.magnitude;
        
        float rampSpeed = ((dist / slowingDistance)) * maxSpeed ;
        float Clamped = Mathf.Min(rampSpeed, maxSpeed);
        Debug.Log(speed);
        
        Vector3 desiredVelocity = Clamped * (toTarget.normalized);
        
        return desiredVelocity - velocity;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(target.transform.position, slowingDistance);
    }
    /* Arrive Functionality pseudocode
     toTarget = target - velocity
     dist = |toTarget|
     rampedSpeed= (distance/slowingDistance)*maxSpeed
     Clamped = min(rampedSpeed),max(MaxSpeed);
     
     desiredVelocity = clamped*(toTarget/distance); or clamped*(toTarget.normalized)

    return desiredVelocity;
    */
}
