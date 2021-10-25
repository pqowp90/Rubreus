using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    Transform target;
    void Start()	{
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        agent.SetDestination(target.position);
	}

    // Update is called once per frame
    void Update()
    {
        if(agent.destination!=target.transform.position){
            agent.SetDestination(target.position);
        }
        else{
            agent.SetDestination(transform.position);
        }
    }
}
