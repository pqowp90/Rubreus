using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoungPotap : PoTapBase
{
    
    protected override void Start()
    {
        base.Start();
    }
    private void FireGun(){
        if(targetTransform!=null&&gunDeley>=myGunDeley){
            if(targetTransform.GetComponent<Enemy1>().reincarnationsNum!=targetReincarnations)return;
            BangAniTrigger();
            gunDeley=0f;
            Bumb bullet = AllPoolManager.Instance.GetObjPos(12,shootingPos).GetComponent<Bumb>();
            bullet.damage = bulletDamage;
            bullet.speed = bulletSpeed;
            bullet.stunTime = stunTime;
            bullet.targetPos=targetTransform.position;
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
