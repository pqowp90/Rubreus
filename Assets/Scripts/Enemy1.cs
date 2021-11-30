using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    public int reincarnationsNum=0;
    private NavMeshAgent agent;
    [SerializeField]
    Transform target;
    [SerializeField]
    private float maxHp,hp;
    private HpBar hpBar;
    private bool stun;
    [SerializeField]
    private float stunDeley;
    void Awake()	{
        hpBar = GetComponent<HpBar>();
		agent = GetComponent<NavMeshAgent>();
        
        //agent.SetDestination(target.position);
	}
    private IEnumerator StartNav(){
        yield return new WaitForSeconds(0.1f);
        agent.enabled = true;
		agent.updateRotation = false;
		agent.updateUpAxis = false;
    }
    private void OnEnable(){
        StartCoroutine(StartNav());
        hp=maxHp;
    }
    void Update()
    {
        if(agent.enabled==false)return;
        if(target == null)
            target=GameManager.Instance.player;
        if(stunDeley>0)
            stunDeley-=Time.deltaTime;
        if(stunDeley>3)stunDeley=3;
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
}
