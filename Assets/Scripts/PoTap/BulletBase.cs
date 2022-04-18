using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    [SerializeField]
    public float speed,damage;
    public float stunTime;
    [SerializeField]
    private int maxWall = 1;
    private int wallHiNum;
    private void OnEnable(){
        wallHiNum=0;
    }
    protected virtual void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*speed);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.layer == 6){
            GameObject munji = AllPoolManager.Instance.GetObjPos(31,transform).gameObject;
            munji.SetActive(true);
            munji.transform.rotation = Quaternion.identity;
            wallHiNum++;
            if(wallHiNum>=2)AllPoolManager.Instance.PoolObj(transform,1);
            damage/=2f;
            speed/=2f;
            
        }
        if(collider2D.gameObject.tag == "Enemy"){
            AllPoolManager.Instance.PoolObj(transform,1);
            collider2D.GetComponent<Enemy1>().Damaged(damage,stunTime);
        }
    }
}
