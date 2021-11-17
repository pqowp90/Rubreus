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
            BangAniTrigger();
            gunDeley=0f;
            AllPoolManager.Instance.GetObjPos(3,casingOutlet).gameObject.SetActive(true);
            Bullet bullet = AllPoolManager.Instance.GetObjPos(1,shootingPos).GetComponent<Bullet>();
            bullet.damage = bulletDamage;
            bullet.speed = bulletSpeed;
            bullet.stunTime = stunTime;
            bullet.gameObject.SetActive(true);
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
