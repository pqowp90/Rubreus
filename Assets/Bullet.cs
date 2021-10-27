using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime*speed);
    }
    void OnTriggerEnter2D(Collider2D collider2D){
        Debug.Log("ddd");
        if(collider2D.gameObject.tag == "Enemy"){
            AllPoolManager.Instance.PoolObj(transform,1);
            Destroy(collider2D.gameObject);
        }
    }
}
