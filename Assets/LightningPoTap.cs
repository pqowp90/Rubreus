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
            gunDeley=0f;
            boundCount=0;
            poss.RemoveRange(0,poss.Count);
            poss.Add(shootingPos);
            NextTarget();

            for(int i=0;i<poss.Count;i++)
                Debug.Log(poss[i]);
            AllPoolManager.Instance.GetObjPos(3,shootingPos).gameObject.SetActive(true);
            myParticleSystem.Play();
        }
    }
    protected override private void FindTarger(Vector3 pos, ref Transform _targetTransform){
        Transform near=null;
        float nearRange, nearRange2=range;
        Collider2D[] cols = Physics2D.OverlapCircleAll(pos, range, whatLayerMask);
        if(cols.Length==0)return;
        foreach (Collider2D col in cols)
        {
            foreach(Transform tran in poss){
                
                if(tran == col.transform){
                    Debug.Log(tran);
                    Debug.Log("중복");
                    continue;
                }
            }
            nearRange=Vector3.Distance(playerPos.position , col.transform.position);
            if(nearRange2>nearRange){
                nearRange2 = nearRange; 
                near = col.transform;
            }
        }
        if(near==null)return;
        
        _targetTransform = near;
    }
    private void NextTarget(){
        newTarget=null;
        Debug.Log(boundCount);
        FindTarger(poss[boundCount].position,ref newTarget);
        poss.Add(newTarget);
        if(bound>boundCount||newTarget==null){
            boundCount++;
            NextTarget();
        }
        else{
            return;
        }
    }
    private void SetLinePos(){
        for(int i=0;i<poss.Count;i++){
            if(poss[i]==null)return;
            lineRenderer.positionCount = i+1;
            lineRenderer.SetPosition(i,poss[i].position);
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
