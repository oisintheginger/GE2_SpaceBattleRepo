using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] int generationDistance, gap;
    public List<GameObject> obstacleObjects;
    public List<Vector3> viablePoints;

    private void Awake()
    {
        for(int x = 0; x<generationDistance; x+=gap)
        {
            for(int z =0; z<generationDistance; z+=gap)
            {
                Debug.Log(x);
                Vector3 newPoint1 = new Vector3(x,0,z);
                Vector3 newPoint2 = new Vector3(-x, 0, z);
                Vector3 newPoint3 = new Vector3(x, 0, -z);
                Vector3 newPoint4 = new Vector3(-x, 0, -z);
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
            if(Vector3.Distance(new Vector3(obstacle.transform.position.x, 0, obstacle.transform.position.z), checkingPoints)> obstacle.transform.localScale.x)
            {
                viablePoints.Add(checkingPoints);
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        foreach(Vector3 point in viablePoints)
        {
            //Gizmos.DrawWireSphere(point, 1f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
