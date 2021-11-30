using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpone : MonoBehaviour
{
    [SerializeField]
    private Transform SponePos;
    private float time=0f;
    void Start()
    {
        
    }

    void Update(){
        time += Time.deltaTime;
        if(time>1f){
            time = 0f;
            AllPoolManager.Instance.GetObjPos(10,SponePos).gameObject.SetActive(true);
        }
    }
}
