using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirePotap : PoTapBase
{
    [SerializeField]
    private ParticleSystem fire;
    [SerializeField]
    private float fireTime;
    private bool isFireing;
    private float lookAngle;
    protected override void Start()
    {
        base.Start();
        gunDeley = myGunDeley;
    }
    private void FireGun(){
        
        if(targetTransform!=null){
            if(gunDeley>=myGunDeley){
                if(targetTransform.GetComponent<Enemy1>().reincarnationsNum!=targetReincarnations)return;
                gunDeley = 0f;
                
                LookAtTarget();
                StartCoroutine(Fire());
                gunDeley=0f;
                falldown.LosePotapHp();
                
            }
        }else{
            if(fire.isPlaying)fire.Stop();
        }

    }
    private IEnumerator Fire(){
        falldown.LosePotapHp();
        isFireing = true;
        if(!fire.isPlaying)fire.Play();
        int hi=-20;
        for(int j=0;j<15;j++){
            yield return new WaitForSeconds(fireTime/15f);
            hi=-20;
            for(int i=0;i<4;i++){
                BulletBase bullet = AllPoolManager.Instance.GetObjPos(30,shootingPos).GetComponent<BulletBase>();
                bullet.damage = bulletDamage;
                bullet.speed = bulletSpeed;
                bullet.stunTime = stunTime;
                bullet.transform.rotation=chukTransform.rotation;
                bullet.transform.eulerAngles += new Vector3(0f,0f,90f+hi);
                hi+=10;
                bullet.gameObject.SetActive(true);
            }
        }
        
        isFireing = false;
        fire.Stop();
    }
    protected override private void LookAtTarget(){
        if(targetTransform==null)return;
        Vector3 targetPos = targetTransform.position;
        targetPos-=transform.position;
        lookAngle = Mathf.LerpAngle(lookAngle, Mathf.Atan2(targetPos.y,targetPos.x)* Mathf.Rad2Deg, 0.1f);
        chukTransform.eulerAngles = new Vector3(0f,0f,lookAngle-90f);
    }
    
    void Update()
    {
        FireGun();
        TimeGo();
        if(isFireing){
            LookAtTarget();
        }
    }
}
