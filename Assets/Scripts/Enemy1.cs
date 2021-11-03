using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1 : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    Transform target;
    [SerializeField]
    private float maxHp,hp;
    private HpBar hpBar;
    void Awake()	{
        hpBar = GetComponent<HpBar>();
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.SetDestination(target.position);
	}
    private void OnEnable(){
        hp=maxHp;
    }
    void Update()
    {
        if(agent.destination!=target.transform.position){
            agent.SetDestination(target.position);
        }
        else{
            agent.SetDestination(transform.position);
        }
    }
    public void Damaged(float damage){
        hp -= damage;
        hpBar.SetHp(hp, maxHp);
        if(hp<=0){
            hpBar.DestroyHpbar();
            Destroy(gameObject);
        }
    }
}
