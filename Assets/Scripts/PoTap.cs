using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoTap : MonoBehaviour
{
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
    void Start()
    {
        
        playerPos = FindObjectOfType<Player>().transform;
        InvokeRepeating("FindTarger",0f, 1f);
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
        Debug.Log("사라져라 뿅~!");
        Destroy(near.gameObject);
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
        if(Input.GetMouseButton(0)&&gunDeley>=myGunDeley){
            gunDeley=0f;
            AllPoolManager.Instance.GetObjPos(0,casingOutlet).gameObject.SetActive(true);
            AllPoolManager.Instance.GetObjPos(1,shootingPos).gameObject.SetActive(true);
            myParticleSystem.Play();
        }
        gunDeley+=Time.deltaTime;
    }
}
