using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoTap : MonoBehaviour
{
    [SerializeField]
    private Transform chukTransform;
    [SerializeField]
    private float myGunDeley;
    private float gunDeley;
    [SerializeField]
    private Transform casingOutlet,shootingPos;
    [SerializeField]
    private float range;
    private Transform playerPos;
    [SerializeField]
    private ParticleSystem myParticleSystem;
    private Transform targetTransform;
    void Start()
    {
        
        playerPos = FindObjectOfType<Player>().transform;
        InvokeRepeating("FindTarger",Random.Range(0f,1f), 0.5f);
    }
    private void LookAtTarget(){
        if(targetTransform==null)return;
        Vector3 targetPos = targetTransform.position;
        targetPos-=transform.position;
        float lookAngle = Mathf.Atan2(targetPos.y,targetPos.x)* Mathf.Rad2Deg;
        chukTransform.eulerAngles = new Vector3(0f,0f,lookAngle);
    }
    private void FindTarger(){
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
        // }
        // Transform near = cols[0].transform;
        // float nearRange = Vector3.Distance(playerPos.position , near.position);
        // foreach (Collider2D col in cols)
        // {
        //     if(col.tag!="Enemy")continue;
        //     float nearRange2=Vector3.Distance(playerPos.position , col.transform.position);
        //     if(nearRange<nearRange2){
        //         near = col.transform;
        //         nearRange = nearRange2;
        //     }
             
        // }
        // Destroy(near.gameObject);
    }
    void Update()
    {
        if(targetTransform!=null&&gunDeley>=myGunDeley){
            gunDeley=0f;
            AllPoolManager.Instance.GetObjPos(3,casingOutlet).gameObject.SetActive(true);
            AllPoolManager.Instance.GetObjPos(1,shootingPos).gameObject.SetActive(true);
            myParticleSystem.Play();
        }
        gunDeley+=Time.deltaTime;
        LookAtTarget();
    }
}
