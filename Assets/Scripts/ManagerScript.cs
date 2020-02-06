using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] int generationDistance, gap, avoidanceVariable=1;
    public List<GameObject> obstacleObjects;
    public List<Vector3> viablePoints, nonViablePoints;
    Dictionary<string, Vector3> pointsDictionary;

    private void Awake()
    {
        obstacleObjects.Clear();
        foreach(GameObject obstacle in GameObject.FindGameObjectsWithTag("obstacle"))
        {
            obstacleObjects.Add(obstacle);
        }
        for (int x = 0; x<generationDistance; x+=gap)
        {
            for(int z =0; z<generationDistance; z+=gap)
            {
                Vector3 newPoint1 = new Vector3(x,0,z);
                Vector3 newPoint2 = new Vector3(-x, 0, z);
                Vector3 newPoint3 = new Vector3(x, 0, -z);
                Vector3 newPoint4 = new Vector3(-x, 0, -z);
                viablePoints.Add(newPoint1);
                viablePoints.Add(newPoint2);
                viablePoints.Add(newPoint3);
                viablePoints.Add(newPoint4);
                CheckPositions(newPoint1);
                CheckPositions(newPoint2);
                CheckPositions(newPoint3);
                CheckPositions(newPoint4);

            }
        }
        
    }

    void CheckPositions(Vector3 checkingPoints)
    {
        foreach(GameObject obstacle in obstacleObjects)
        {
            if(Vector3.Distance(new Vector3(obstacle.transform.position.x, 0, obstacle.transform.position.z), checkingPoints)< obstacle.transform.localScale.x * avoidanceVariable)
            {
                Debug.Log("Distance Checked");
                viablePoints.Remove(checkingPoints);
                nonViablePoints.Add(checkingPoints);
            }
            else
            {
                Debug.Log(checkingPoints + "This was reject");
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        foreach(Vector3 point in nonViablePoints)
        {
            Gizmos.DrawWireSphere(point, 1f);
        }
    }
}
