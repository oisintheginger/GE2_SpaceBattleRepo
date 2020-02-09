using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] int generationDistance, gap;
    [SerializeField] float avoidanceVariable = 1f;
    public List<GameObject> camerasList;
    public List<GameObject> obstacleObjects, pilotsList, planesList;
    public List<Transform> shotPoints;
    public List<Vector3> viablePoints, nonViablePoints;
    Dictionary<string, Vector3> pointsDictionary;

    public bool AlertPilots = false;
    private void Awake()
    {
        findPlanes();
        foreach(GameObject Pilot in GameObject.FindGameObjectsWithTag("Pilot"))
        {
            pilotsList.Add(Pilot);
        }
        foreach(GameObject obstacle in GameObject.FindGameObjectsWithTag("obstacle"))
        {
            obstacleObjects.Add(obstacle);
        }
        Vector3 thisT = this.gameObject.transform.position;
        for (int x = 0; x<generationDistance; x+=gap)
        {
            for(int z =0; z<generationDistance; z+=gap)
            {
                Vector3 newPoint1 = new Vector3(x+thisT.x,0,z+thisT.z);
                viablePoints.Add(newPoint1);
                CheckPositions(newPoint1);

            }
        }
        AssignPlanes();
    }

    private void Update()
    {
        if(AlertPilots == true)
        {
            foreach(GameObject pilot in pilotsList)
            {
                var pScript = pilot.GetComponent<pilotMovementScript>();
                pScript.target = pScript.selectedPlane.GetComponent<SpitfireScript>().planeEnterPoint;
                pScript.isWandering = false;
                pScript.isPatrolling = false;
                pScript.arriveEnabled = true;

            }
        }
    }

    void CheckPositions(Vector3 checkingPoints)
    {
        foreach(GameObject obstacle in obstacleObjects)
        {
            if(Vector3.Distance(new Vector3(obstacle.transform.position.x, 0, obstacle.transform.position.z), checkingPoints)< obstacle.transform.localScale.x * avoidanceVariable)
            {
                viablePoints.Remove(checkingPoints);
                nonViablePoints.Add(checkingPoints);
            }
        }
        
    }

    void findPlanes()
    {
        foreach(GameObject plane in GameObject.FindGameObjectsWithTag("Plane"))
        {
            planesList.Add(plane);
        }
    }

    void AssignPlanes()
    {
        foreach(GameObject pilot in pilotsList)
        {
            pilot.GetComponent<pilotMovementScript>().selectedPlane = planesList[pilotsList.IndexOf(pilot)];
            planesList[pilotsList.IndexOf(pilot)].GetComponent<SpitfireScript>().chosen = true;
        }
    }

    

    private void OnDrawGizmos()
    {
        foreach(Vector3 point in nonViablePoints)
        {
            //Gizmos.DrawWireSphere(point, 1f);
        }
        foreach(Vector3 point in viablePoints)
        {
            //Gizmos.DrawWireSphere(point, 1f);
        }
    }
}
