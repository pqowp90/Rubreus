using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoTap : PoTapBase
{
    private Falldown falldown;
    protected override void Start()
    {
        base.Start();
        falldown = GetComponent<Falldown>();
        
    }
    private void FireGun(){
        
        if(targetTransform!=null&&gunDeley>=myGunDeley){

            if(targetTransform.GetComponent<Enemy1>().reincarnationsNum!=targetReincarnations)return;
            falldown.LosePotapHp();
            BangAniTrigger();
            gunDeley=0f;
            AllPoolManager.Instance.GetObjPos(3,casingOutlet).gameObject.SetActive(true);
            BulletBase bullet = AllPoolManager.Instance.GetObjPos(1,shootingPos).GetComponent<BulletBase>();
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
