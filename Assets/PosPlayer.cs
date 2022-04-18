using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosPlayer : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
