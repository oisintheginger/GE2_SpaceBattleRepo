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
                viablePoints.Add(newPoint1);
                viablePoints.Add(newPoint2);
                viablePoints.Add(newPoint3);
                viablePoints.Add(newPoint4);
            }
        }
    }

    List<Vector3> CheckPositions(List<Vector3> checkingPoints)
    {
        List<Vector3> checkedPositions = new List<Vector3>();
        foreach(GameObject obstacle in obstacleObjects)
        {
            foreach(Vector3 positions in checkingPoints)
            {
                //if (positions.x-)
            }
        }
        return checkedPositions;
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
