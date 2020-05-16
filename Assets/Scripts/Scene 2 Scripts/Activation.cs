using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Fighter")
        {
            myGameManagement.thisGM.OnDalekApproach();
        }
    }
}
