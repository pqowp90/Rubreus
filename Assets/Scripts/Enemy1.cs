using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    protected Animator animator;
    public int reincarnationsNum=0;
    protected NavMeshAgent agent;
    [SerializeField]
    Transform target;
    [SerializeField]
    protected float maxHp,hp;
    protected HpBar hpBar;
    protected bool stun;
    [SerializeField]
    protected float stunDeley;
    protected Vector2 pastPos,nowPos;
    protected Vector3 nowAngle;
    protected bool playerChase=false, isAttacking=false, hiTarger=false;
    [SerializeField]
    protected float attackRange, attackDeley, damage;
    [SerializeField]
    protected Transform pos;
    [SerializeField]
    protected Vector2 boxSize;
    protected virtual void Awake()	{
        animator = GetComponent<Animator>();
        hpBar = GetComponent<HpBar>();
		agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetRotate", 0f, 0.1f);
        //agent.SetDestination(target.position);
	}
    protected void SetRotate(){
        if(target == null)return; 
        nowPos = transform.position;
        nowAngle = new Vector3(0f,0f,GameManager.GetAngle(nowPos,(playerChase)?(Vector2)target.position:pastPos)+((playerChase)?-90f:90f));
        pastPos = transform.position;
    }
    protected IEnumerator StartNav(){
        yield return new WaitForSeconds(0.1f);
        agent.enabled = true;
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }
    protected void OnEnable(){
        stunDeley = 0f;
        playerChase=false;
        isAttacking=false;
        agent.updateRotation = true;
        StartCoroutine(StartNav());
        hp=maxHp;
    }
    protected virtual void Attact(){
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Bumb"));

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, transform.rotation.z, layerMask);

        foreach(Collider2D collider2D in collider2Ds){
            if(collider2D.tag == "Bumb"){
                collider2D.GetComponent<RealBumb>();
            }
            if(collider2D.tag == "Player"){
                collider2D.GetComponent<Player>().Damaged(damage);
            }
        }
    }
    protected virtual IEnumerator AttactAni(){
        isAttacking = true;
        animator.SetTrigger("Attack");
        stunDeley = attackDeley;
        yield return new WaitForSeconds(attackDeley);
        isAttacking = false;
    }
    protected void FixedUpdate(){
        if(agent.enabled==false)return;


        if(target != null){
            if(Vector2.Distance(transform.position, target.position)<=attackRange){
                hiTarger = true;
                if(!isAttacking)
                    StartCoroutine(AttactAni());
                else
                    return;
            }
            else hiTarger = false;
        }

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.eulerAngles), Quaternion.Euler(nowAngle), 0.1f);

        if(target == null){
            animator.SetFloat("Walk",0f);
            if(GameManager.Instance.isBumbCharging){
                target=GameManager.Instance.bumbPos;
            }
        }else{
            animator.SetFloat("Walk",1f);
        }
        
        if(stunDeley>0)
            stunDeley-=Time.deltaTime;
        if(stunDeley>3)stunDeley=3;
        if(target==null)return;
        if(agent.destination!=target.transform.position&&stunDeley<=0){
            agent.SetDestination(target.position);
        }
        else{
            agent.SetDestination(transform.position);
        }
        
    }
    
    public void Damaged(float damage, float stunTime){
        stunDeley += stunTime;
        hp -= damage;
        hpBar.SetHp(hp, maxHp);
        if(hp<=0){
            reincarnationsNum++;
            hpBar.DestroyHpbar();
            AllPoolManager.Instance.PoolObj(transform,10);
        }
    }
    protected void OnTriggerStay2D(Collider2D collider2D){
        if(collider2D.gameObject.layer != 8||playerChase)return;
        int layerMask = (1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Wall"));
        RaycastHit2D ray;
        Vector3 vec =  collider2D.transform.position-transform.position;
        vec.Normalize();
        ray = Physics2D.Raycast(transform.position, vec,Mathf.Infinity,layerMask);
        //Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position,vec*10f, new Color(0,1,0));
        if(ray){
            if(collider2D.gameObject == ray.transform.gameObject){
                target=GameManager.Instance.player;
                playerChase = true;
            }
        }
    }
    protected void OnTriggerExit2D(Collider2D collider2D){
        if(collider2D.gameObject.layer != 8)return;
        target = null;
        playerChase = false;
    }
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
        
    }
}
