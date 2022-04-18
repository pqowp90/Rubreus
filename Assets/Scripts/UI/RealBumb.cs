    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RealBumb : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform body1;
    [SerializeField]
    private Transform lightPos;
    private float myrotate=0f;
    private float speed=2f;
    private IEnumerator RotateBody1(){
        while(true){
            yield return new WaitForSeconds(speed);
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
    public void Startpeeeeeeezzz(){
        StartCoroutine(Peeeeeeezzz());
    }
    public void StartBBBBBBBBBBBBBBBBBBaaaaaaaaaaaaaaaannnnnnnnnngggggggggggg(){
        StartCoroutine(BBBBBBBBBBBBBBBBBBaaaaaaaaaaaaaaaannnnnnnnnngggggggggggg());
    }
    public IEnumerator Peeeeeeezzz(){
        if(!GameManager.Instance.gameEnd){
            GameManager.Instance.gameEnd = true;
            speed = 100f;
            GameManager.Instance.look.enabled=false;
            GameManager.Instance.look.transform.position = transform.position;
            
            yield return new WaitForSeconds(2f);
            GameManager.Instance.GameClear(false);
            yield return new WaitForSeconds(2f);
            GoRobby();
        }
    }
    public IEnumerator BBBBBBBBBBBBBBBBBBaaaaaaaaaaaaaaaannnnnnnnnngggggggggggg(){
        if(!GameManager.Instance.gameEnd){
            GameManager.Instance.gameEnd = true;
            speed = 0.2f;
            GameManager.Instance.look.enabled=false;
            GameManager.Instance.look.transform.position = transform.position;
            
            yield return new WaitForSeconds(2f);
            GameManager.Instance.GameClear(true);
            yield return new WaitForSeconds(2f);
            GoRobby();
        }
    }
    public void GoRobby(){
        LoadingScene.LoadScene("MainRobby");
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.Instance.BumbHello();
        StartCoroutine(RotateBody1());
        StartCoroutine(Lightning());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
