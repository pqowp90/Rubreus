using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bumb : BulletBase
{
    
    [SerializeField]
    private AnimationCurve curve;
    public LayerMask whatLayerMask;
    public Vector2 targetPos;
    public float range;
    private float time;
    public void OnEnableBumb(){
        time=speed;
        transform.DOMove(targetPos,speed).OnComplete(()=>{Booooom();});
        // Sequence mySequence = DOTween.Sequence();
        // mySequence.Append(transform.DOMove(targetPos,speed));
        // mySequence.Join(transform.DOScale(1.5f,speed/2f));
        // mySequence.Insert(speed/2f, transform.DOScale(1f,speed/2f)).OnComplete(()=>{Booooom();});
    }
    private void Booooom(){
        AllPoolManager.Instance.GetObjPos(13, transform).gameObject.SetActive(true);
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range,whatLayerMask);
        if(cols.Length!=0){
        foreach (Collider2D col in cols)
            {
                col.GetComponent<Enemy1>().Damaged(damage,stunTime);
            }
        }
        AllPoolManager.Instance.PoolObj(transform,12);
    }
    protected override void Update(){
        if(time>0f)time -= Time.deltaTime;
        transform.localScale=new Vector3(1f,1f,1f)*curve.Evaluate(time/speed);
        Debug.Log(time/speed);
        return;
    }
    protected override void OnTriggerEnter2D(Collider2D collider2D){
        return;
    }
    
}
