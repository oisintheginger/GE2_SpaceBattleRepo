using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pilotMovementScript : MonoBehaviour
{
    Animator pilotAnim;
    ManagerScript gameManager;

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


    public bool seekEnabled = false, arriveEnabled = false, isWandering=false, isPatrolling =false, isFleeing = false;
    public GameObject target;
    public List<Vector3> patrolPoints;
    public float patrolRange, regenerateTime = 60f;
    [SerializeField] int currentPoint, approachDistance;

    public GameObject selectedPlane;

    public float timer = 0;

    private void Awake()
    {
        pilotAnim = this.gameObject.GetComponent<Animator>();
        gameManager = FindObjectOfType<ManagerScript>();
    }

    private void Start()
    {
        patrolPoints = thisPatrol(gameManager.viablePoints);
        target.transform.position = patrolPoints[0];
        InvokeRepeating("regeneratePatrol", Time.time, regenerateTime);
    }

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

        if(gameManager.AlertPilots==true)
        {
            pilotAnim.SetTrigger("Alert");
            maxSpeed = 4f;
            if (Vector3.Distance(this.transform.position, target.transform.position)<1f)
            {

                pilotAnim.SetTrigger("Climb");
                
            }
        }
    }

    void DestroyWait(float timeToWait)
    {

        timer += Time.deltaTime;
        if (timer >= timeToWait)
        {
            timer = 0;
        }
        selectedPlane.GetComponent<genericMovementScript>().seekEnabled=true;
        selectedPlane.GetComponent<Animator>().SetBool("Active", true);
        this.gameObject.SetActive(false);
        
    }

    Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        if (seekEnabled)
        {
            force += Seek(target);
        }
        if(isFleeing)
        {
            force += Flee(target);
        }
        if (arriveEnabled)
        {
            force += Arrive(target);
        }
        if (isWandering)
        {
            Wander();
        }
        if (isPatrolling)
        {
            target.transform.position = patrolPoints[currentPoint];
            Patrol();

        }

        return force;
    }

    void Wander()
    {
        if(Vector3.Distance(this.transform.position, target.transform.position)<4f)
        {
            bool breakWhile = false;
            while(!breakWhile)
            {
                Vector3 newPoint = gameManager.viablePoints[(int)Random.Range(1, gameManager.viablePoints.Count)];
                if (Vector3.Distance(this.transform.position, newPoint)<patrolRange)
                {
                    Debug.Log("While loop distance check success");
                    target.transform.position = newPoint;
                    breakWhile=true;
                }
                else
                {
                    breakWhile = false;
                }
            }
            
        }
    }

    void Patrol()
    {
        if(Mathf.Abs(Vector3.Distance(this.transform.position, target.transform.position))<approachDistance)
        {
            if (patrolPoints.Capacity - 1 <= currentPoint)
            {
                currentPoint=0;
            }
            else
            {
                currentPoint++;
            }
        }
        
    }

    List<Vector3> thisPatrol(List<Vector3> viablePoints)
    {
        List<Vector3> thePatrolPoints = new List<Vector3>(5);

        int p = 0;
        while (p < thePatrolPoints.Capacity)
        {
            Vector3 newPoint = viablePoints[(int)Random.Range(1, viablePoints.Count)];
            if (Vector3.Distance(this.transform.position, newPoint) < patrolRange)
            {
                thePatrolPoints.Add(newPoint);
                p++;
            }
        }


        return thePatrolPoints;
    }

    void regeneratePatrol()
    {
        patrolPoints = thisPatrol(gameManager.viablePoints);
    }

    Vector3 Seek(GameObject target)
    {
        Vector3 toTarget = target.transform.position - this.gameObject.transform.position;
        Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
        return desiredVelocity - velocity;
    }

    Vector3 Flee(GameObject target)
    {
        Vector3 toTarget = -target.transform.position + this.gameObject.transform.position;
        Vector3 desiredVelocity = toTarget.normalized * maxSpeed;
        return desiredVelocity - velocity;
    }

    Vector3 Arrive(GameObject target)
    {
        Vector3 correctedTarget = new Vector3(target.transform.position.x, 
                                                this.transform.position.y, 
                                                target.transform.position.z);

        Vector3 toTarget = correctedTarget - this.gameObject.transform.position;
        float dist = toTarget.magnitude;
        
        float rampSpeed = ((dist / slowingDistance)) * maxSpeed ;
        float Clamped = Mathf.Min(rampSpeed, maxSpeed);
        
        Vector3 desiredVelocity = Clamped * (toTarget.normalized);
        
        return desiredVelocity - velocity;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        //Gizmos.DrawWireSphere(target.transform.position, slowingDistance);

        Gizmos.color = Color.red;
        foreach (Vector3 p in patrolPoints)
        {
            Gizmos.DrawWireSphere(p, 1);
        }
    }
}
