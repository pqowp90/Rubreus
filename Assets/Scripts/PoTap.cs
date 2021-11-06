using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoTap : PoTapBase
{
    
    protected override void Start()
    {
        base.Start();
    }
    private void FireGun(){
        if(targetTransform!=null&&gunDeley>=myGunDeley){
            gunDeley=0f;
            AllPoolManager.Instance.GetObjPos(3,casingOutlet).gameObject.SetActive(true);
            AllPoolManager.Instance.GetObjPos(1,shootingPos).gameObject.SetActive(true);
            myParticleSystem.Play();
        }
    }
    void Update()
    {
        LookAtTarget();
        FireGun();
        TimeGo();
    }
}
