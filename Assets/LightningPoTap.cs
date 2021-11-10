using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected Transform newTarget;
    protected override void Start()
    {
        base.Start();
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
            NextTarget();

            // for(int i=0;i<poss.Count;i++)
            //     Debug.Log(poss[i]);
            AllPoolManager.Instance.GetObjPos(3,shootingPos).gameObject.SetActive(true);
            myParticleSystem.Play();
        }
    }
    private void FindLightningTarger(Vector2 pos){
        if(boundCount>bound)return;
        float nearRange, nearRange2=range;
        Transform near=null;
        bool hi=false;
        Collider2D[] cols = Physics2D.OverlapCircleAll(pos, range, whatLayerMask);
        if(cols.Length==0)return;
        foreach (Collider2D col in cols)
        {
            hi=false;
            Debug.Log("심판시작 "+col);
            foreach (Transform tran in poss)
            {
                if(tran == col.transform){
                    Debug.Log("넌 겹친다 "+col);
                    hi=true;
                    break;
                }
            }
            if(hi)continue;
            nearRange=Vector2.Distance(pos , col.transform.position);                     
            if(nearRange2>=nearRange){
                nearRange2 = nearRange; 
                near = col.transform;
                Debug.Log("통과"+nearRange);
            }else{
                Debug.Log("탈락"+nearRange);
            }
            
        }
        if(near == null){
            Debug.Log("대상을 찾을수 없음.");
            return;
        }
        poss.Add(near);
        Debug.Log(boundCount);
        boundCount++;
        FindLightningTarger(near.position);
    }
    private void NextTarget(){
        boundCount=0;
        FindLightningTarger(poss[1].position);
    }
    private void SetLinePos(){
        for(int i=0;i<poss.Count;i++){
            if(poss[i]==null)return;
            lineRenderer.positionCount = i+1;
            Vector2 pos = poss[i].position;
            lineRenderer.SetPosition(i,pos);
        }
        
    }
    void Update()
    {
        LookAtTarget();
        FireLightning();
        TimeGo();
        SetLinePos();
    }
}
