using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAI : MonoBehaviour
{
    public LayerMask lm;
    public GameObject dalekship;
    public Collider[] gms;
    [SerializeField] float time;
    public float reloadTime;
    public  GameObject projectile;
    private void Start()
    {
        time = reloadTime;
    }


    private void Update()
    {
        gms = Physics.OverlapSphere(this.transform.position + transform.forward * 200f, 50f);
        toShoot();
    }

    void toShoot()
    {
        if(gms.Length>0)
        {
            shooting();
        }
    }

    void shooting()
    {
        if (time <= 0f)
        {
           
            GameObject P = Instantiate(projectile, this.transform.position,Quaternion.identity);
            P.GetComponent<Rigidbody>().velocity = (dalekship.transform.position-this.transform.position).normalized * 100f;
            time = reloadTime;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + this.transform.forward * 200f, 50f);
    }
}
