using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DalekTurretScript : MonoBehaviour
{
    public LayerMask myLayerMask;
    [SerializeField] Transform RecoilPos, RecoverPos, ShootPos1, ShootPos2;
    Quaternion startRotation;

    [SerializeField] GameObject Projectile, CurrentTarget;

    [SerializeField] float TurretRange, maxAngle, ReloadTimer, projectileSpeed;

    float timer;

    public List<GameObject> targets;

    void Start()
    {
        timer = ReloadTimer;
        targets = new List<GameObject>();
        startRotation = this.transform.rotation;
    }
    void Update()
    {
        GunRotation();
    }

    void GunRotation()
    {
       
        if (targets.Count > 0)
        {
            RaycastHit GunRH;
            foreach (GameObject target in targets)
            {
                if (Physics.Raycast(this.transform.position, target.transform.position - this.transform.position, out GunRH, TurretRange))
                {
                    if (GunRH.collider.gameObject.tag == "Fighter")
                    {
                        
                        var TargetObject = target;
                        Vector3 toTarget = (TargetObject.transform.position + TargetObject.GetComponent<Boid>().velocity) - this.transform.position; //shooting slightly ahead of the target

                        Quaternion newRot = Quaternion.LookRotation(toTarget, transform.up);


                        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRot, Time.deltaTime * 10f);
                        Shooting();
                        return;
                    }

                }
            }
            
        }
        else
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, startRotation, Time.deltaTime * 1f);
        }
    }

    void Shooting()
    {
        if(timer<= 0f)
        {
            //DO SHOT
            GameObject RProjectile = Instantiate(Projectile, ShootPos1.transform.position, this.transform.rotation);
            GameObject LProjectile = Instantiate(Projectile, ShootPos2.transform.position, this.transform.rotation);
            RProjectile.GetComponent<Rigidbody>().velocity = this.transform.forward * projectileSpeed;
            LProjectile.GetComponent<Rigidbody>().velocity = this.transform.forward * projectileSpeed;

            timer = ReloadTimer;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fighter")
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fighter"&&targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }
}
