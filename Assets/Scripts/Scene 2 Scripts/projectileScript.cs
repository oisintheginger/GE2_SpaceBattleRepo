using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    float timer = 3f;
    public bool isFighter = false;
    private void Update()
    {
        if(timer<=0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Fighter"&&!isFighter)
        {

            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Dalek" && isFighter)
        {
            FindObjectOfType<myGameManagement>().DalekHealth -= 10f;
            Destroy(this.gameObject);
        }
    }

}
