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
            }
        }else{
            if(fire.isPlaying)fire.Stop();
        }

    }
    private IEnumerator Fire(){
        isFireing = true;
        if(!fire.isPlaying)fire.Play();
        yield return new WaitForSeconds(fireTime);
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
