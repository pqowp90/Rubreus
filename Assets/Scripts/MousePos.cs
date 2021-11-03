using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [SerializeField]
    private Transform playerPos;
    void Start(){
        //Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        Vector3 pos = (playerPos.position*4f+Camera.main.ScreenToWorldPoint(Input.mousePosition))/5f;
        transform.position = new Vector3(pos.x,pos.y,0f);
    }
}
