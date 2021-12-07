using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SponeFirstMonsters : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;
    void Start()
    {
        foreach (var gameobj in gameObjects)
        {
            gameobj.SetActive(true);
        }
    }
}
