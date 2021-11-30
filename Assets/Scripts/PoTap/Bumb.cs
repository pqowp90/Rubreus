using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bumb : BulletBase
{
    public LayerMask whatLayerMask;
    public Vector2 targetPos;
    public float range;
    private void OnEnable(){
        transform.DOMove(targetPos,speed);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(1.5f,speed/2f));
        mySequence.Append(transform.DOScale(1f,speed/2f)).OnComplete(()=>{Booooom();});
    }
    private void Booooom(){
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range,whatLayerMask);
        if(cols.Length==0)return;
        foreach (Collider2D col in cols)
        {

        }
        AllPoolManager.Instance.PoolObj(transform,12);
    }
    protected override void Update(){
        return;
    }
    protected override void OnTriggerEnter2D(Collider2D collider2D){
        return;
    }
}
