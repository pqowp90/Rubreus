using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Penal : MonoBehaviour
{
    public int num;
    public Image potapImage;
    public void MakePoTap(){

        Transform drone = AllPoolManager.Instance.GetObj(8);
        drone.position = new Vector3(500f,500f,0f);
        drone.GetComponent<Drone>().droneMove(num, GameManager.Instance.player.position);
        drone.gameObject.SetActive(true);
    }
}
