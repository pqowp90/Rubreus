using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoTapBase : MonoBehaviour
{
    [SerializeField]
    protected float bulletDamage, bulletSpeed,stunTime;
    private Animator animator;
    public LayerMask whatLayerMask;
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
        animator = GetComponent<Animator>();
        playerPos = FindObjectOfType<Player>().transform;
        StartCoroutine(RepeatingFind());
    }
    protected void BangAniTrigger(){
        animator.SetTrigger("bang");
    }
    private IEnumerator RepeatingFind(){
        yield return new WaitForSeconds(Random.Range(0f,2f));
        while(true){
            FindTarger(transform.position, ref targetTransform);
            yield return new WaitForSeconds(0.5f);
        }
    }
    protected virtual private void LookAtTarget(){
        if(targetTransform==null)return;
        Vector3 targetPos = targetTransform.position;
        targetPos-=transform.position;
        float lookAngle = Mathf.Atan2(targetPos.y,targetPos.x)* Mathf.Rad2Deg;
        chukTransform.eulerAngles = new Vector3(0f,0f,lookAngle);
    }
    protected private Transform FindTarger(Vector3 pos, ref Transform _targetTransform){
        Transform near=null;
        
        float nearRange, nearRange2=range;
        Collider2D[] cols = Physics2D.OverlapCircleAll(pos, range,whatLayerMask);
        if(cols.Length==0)return null;
        foreach (Collider2D col in cols)
        {
            nearRange=Vector3.Distance(pos , col.transform.position);
            if(nearRange2>nearRange){
                nearRange2 = nearRange; 
                near = col.transform;
            }
        }
        if(near==null)return null;
        
        _targetTransform = near;
        return near;
    }
    
    protected void TimeGo(){
        gunDeley+=Time.deltaTime;
    }

}
