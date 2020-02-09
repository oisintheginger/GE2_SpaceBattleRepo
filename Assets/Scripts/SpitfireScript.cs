using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitfireScript : MonoBehaviour
{
    public bool chosen;

    int currentTargetIndex = 0;
    genericMovementScript planeMovement;
    [SerializeField] List<GameObject> RunWayPosition;
    public GameObject planeEnterPoint;

    private void Awake()
    {
        planeMovement = this.GetComponent<genericMovementScript>();
        planeMovement.target = RunWayPosition[0];
        chosen = false;
    }
    void Update()
    {
        RunwayTakeoff();
    }

    void RunwayTakeoff()
    {
        if(Vector3.Distance(this.transform.position, planeMovement.target.transform.position)<20f)
        {
            if(currentTargetIndex<2)
            {
                currentTargetIndex++;
                planeMovement.target = RunWayPosition[currentTargetIndex];
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
