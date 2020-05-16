using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myGameManagement : MonoBehaviour
{
    public float DalekHealth = 1000f;
    private void Start()
    {
        DalekHealth = 1000f;
    }
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
