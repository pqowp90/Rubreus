using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpone : MonoBehaviour
{
    [SerializeField]
    private int Index;
    [SerializeField]
    private float deley;
    [SerializeField]
    private Transform SponePos;
    private float time=0f;
    void Start()
    {
        
    }

    void Update(){
        time += Time.deltaTime;
        if(time>deley){
            time = 0f;
            AllPoolManager.Instance.GetObjPos(Index,SponePos).gameObject.SetActive(true);
        }
    }
}
