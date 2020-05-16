using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class myGameManagement : MonoBehaviour
{
    public float DalekHealth , dH;
    public GameObject Cam1, Cam2, Cam3, Cam4;
    public GameObject particleemmitter;
    public GameObject dalekShip;
    float timer = 3f;
    public enum SceneStage
    {
        OnApproach,
        FightersAttack,
        Death,
        ReturnToEarth
    }
    public static myGameManagement thisGM;
    void Awake()
    {
        thisGM = this;
        dH = DalekHealth;
    }
    private void Update()
    {
        if (DalekHealth < dH  && DalekHealth >= dH - 100f)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
        }
        else if (DalekHealth < dH - 100f && DalekHealth >= dH - 200) 
        {
            Cam2.SetActive(false);
            Cam3.SetActive(true);

        }
        else if (DalekHealth < dH - 200 && DalekHealth >= dH - 300) 
        {
            Cam3.SetActive(false);
            Cam4.SetActive(true);

        }

        if(DalekHealth<=0f)
        {
            particleemmitter.SetActive(true);
            dalekShip.SetActive(false);
            timer -= Time.deltaTime;
            if(timer<=0f)
            {
                Debug.Log(1122233333);
                Application.Quit();
            }
        }
    }

    
    public event Action FightersAttack;
    public void OnDalekApproach()
    {
        if(FightersAttack != null)
        {
            FightersAttack();
        }
    }

}
