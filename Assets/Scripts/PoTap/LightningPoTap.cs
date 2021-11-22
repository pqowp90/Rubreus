using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightningPoTap : PoTapBase
{
    [SerializeField]
    private int bound;
    [SerializeField]
    private int boundCount;
    [SerializeField]
    public LineRenderer lineRenderer;
    //Transform[] poss = new Transform[10];
    [SerializeField]
    List<Transform> poss=new List<Transform>();
    private AudioSource audioSource;
    protected Transform newTarget;
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }
    private void FireLightning(){
        if(targetTransform!=null&&gunDeley>=myGunDeley){
            FindTarger(transform.position, ref targetTransform);
            if(targetTransform == null)return;
            gunDeley=0f;
            boundCount=0;
            poss.RemoveRange(0,poss.Count);
            poss.Add(shootingPos);
            poss.Add(targetTransform);
            boundCount=0;
            FindLightningTarger(poss[1].position,range);
            audioSource.Play();
            StartCoroutine(Zizizizi());
            animator.SetTrigger("zizi");
            LookAtTarget();
        }
    }
    protected override private void LookAtTarget(){
        if(targetTransform==null)return;
        Vector3 targetPos = targetTransform.position;
        targetPos-=transform.position;
        float lookAngle = Mathf.Atan2(targetPos.y,targetPos.x)* Mathf.Rad2Deg;
        chukTransform.DOLocalRotate(new Vector3(0f,0f,lookAngle), 0.4f);
        
    }
    private void  FindLightningTarger(Vector2 pos, float _range){
        
        if(boundCount>bound)return;
        float nearRange, nearRange2=_range;
        Transform near=null;
        bool hi=false;
        Collider2D[] cols = Physics2D.OverlapCircleAll(pos, _range, whatLayerMask);
        if(cols.Length==0)return;
        foreach (Collider2D col in cols){
            hi=false;
            foreach (Transform tran in poss){
                if(tran == col.transform){
                    hi=true;
                    break;
                }
            }
            if(hi)continue;
            nearRange=Vector2.Distance(pos , col.transform.position);             
            if(nearRange2>=nearRange){
                nearRange2 = nearRange;
                near = col.transform;
            }    
        }
        if(near == null){
            return;
        }
        poss.Add(near);
        boundCount++;
        FindLightningTarger(near.position,range/3*1);
    }
    
    private void SetLinePos(){
        
        lineRenderer.positionCount = poss.Count;
        for(int i=0;i<poss.Count;i++){
            if(poss[i]==null){
                lineRenderer.positionCount = i;
                return;
            }
            
            Vector2 pos = poss[i].position;
            lineRenderer.SetPosition(i,pos);

            //poss[i].GetComponent<Enemy1>().Damaged(damage);
        }
        
    }
    
    private IEnumerator Zizizizi(){
        for(int i=0;i<poss.Count;i++){
            Enemy1 ene = poss[i].GetComponent<Enemy1>();
            if(ene==null)continue;
            ene.Damaged(bulletDamage/i+1,stunTime);
        }
        SetLinePos();
        for(int i=0;i<10;i++){
            lineRenderer.SetPosition(0,poss[0].position);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        FireLightning();
        TimeGo();
    }
}
