using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponeFirstMonsters : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;
    private bool hi;
    void Start()
    {
        foreach (var gameobj in gameObjects)
        {
            gameobj.SetActive(true);
        }
    }
    void Update(){
        if(!hi)
            if(GameManager.Instance.isBumbCharging){
                hi=true;
                foreach (var gameobj in gameObjects)
                {
                    Destroy(gameobj);
                }
            }
    }
}
