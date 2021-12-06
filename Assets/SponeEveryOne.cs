using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponeEveryOne : MonoBehaviour
{
    private float time=5f;
    private int hello=0;
    public List<Hihihihihihihi> sponers = new List<Hihihihihihihi>();
    void Start()
    {
        int a=0;
        foreach (var sponer in sponers){
            a++;
            sponer.Name = (a*5)+"초";
        }
    }

    void Update()
    {
        time+=Time.deltaTime;
        if(time >= 5f){
            time = 0f;
            
            hello++;
        }
    }
    // void Update(){
    //     time += Time.deltaTime;
    //     if(time>deley){
    //         Num--;
    //         time = 0f;
    //         AllPoolManager.Instance.GetObjPos(Index,SponePos).gameObject.SetActive(true);
    //         if(Num<=0) Destroy(gameObject);
    //     }
    // }
}
