using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBullet : BulletBase
{
    [SerializeField]
    private float range;
    public LayerMask whatLayerMask;
    protected override void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.tag == "Enemy"){
            AllPoolManager.Instance.PoolObj(transform,1);
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range,whatLayerMask);
            if(cols.Length==0)return;
            foreach (Collider2D col in cols)
            {
                if(collider2D.gameObject.tag == "Enemy"){
                    col.GetComponent<Enemy1>().Damaged(damage,stunTime);
                }
            }
        }
    }
}
