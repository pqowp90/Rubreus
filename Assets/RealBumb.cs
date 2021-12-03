using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RealBumb : MonoBehaviour
{
    [SerializeField]
    private Transform body1;
    [SerializeField]
    private Transform lightPos;
    private float myrotate=0f;
    private IEnumerator RotateBody1(){
        while(true){
            yield return new WaitForSeconds(2f);
            body1.DORotate(new Vector3(0f,0f,myrotate),0.6f);
            myrotate+=30f;
        }
    }
    private IEnumerator Lightning(){
        while(true){
            yield return new WaitForSeconds(Random.Range(0f,2f));
            lightPos.position = transform.position+new Vector3(Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f),0f);
            AllPoolManager.Instance.GetObjPos(14, lightPos).gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateBody1());
        StartCoroutine(Lightning());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
