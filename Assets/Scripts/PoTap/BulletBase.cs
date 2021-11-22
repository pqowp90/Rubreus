using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField]
    public float speed,damage;
    public float stunTime;


    protected void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*speed);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.tag == "Enemy"){
            AllPoolManager.Instance.PoolObj(transform,1);
            collider2D.GetComponent<Enemy1>().Damaged(damage,stunTime);
        }
    }
}
