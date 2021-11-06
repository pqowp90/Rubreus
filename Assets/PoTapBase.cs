using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoTapBase : MonoBehaviour
{
    [SerializeField]
    protected float myGunDeley,range;
    protected float gunDeley;
    [SerializeField]
    protected Transform casingOutlet,shootingPos,chukTransform;
    [SerializeField]
    protected ParticleSystem myParticleSystem;
    protected Transform targetTransform,playerPos;
    protected virtual void Start()
    {
        
        playerPos = FindObjectOfType<Player>().transform;
        InvokeRepeating("FindTarger",Random.Range(0f,1f), 0.5f);
    }
    protected private void LookAtTarget(){
        if(targetTransform==null)return;
        Vector3 targetPos = targetTransform.position;
        targetPos-=transform.position;
        float lookAngle = Mathf.Atan2(targetPos.y,targetPos.x)* Mathf.Rad2Deg;
        chukTransform.eulerAngles = new Vector3(0f,0f,lookAngle);
    }
    protected private void FindTarger(){
        Transform near=null;
        float nearRange, nearRange2=range;
        Debug.Log("dd");
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range);
        if(cols.Length==0)return;
        foreach (Collider2D col in cols)
        {
            if(col.tag!="Enemy")continue;
            nearRange=Vector3.Distance(playerPos.position , col.transform.position);
            if(nearRange2>nearRange){
                nearRange2 = nearRange; 
                near = col.transform;
            }
        }
        if(near==null)return;
        targetTransform = near;
    }
    
    protected void TimeGo(){
        gunDeley+=Time.deltaTime;
    }

}
