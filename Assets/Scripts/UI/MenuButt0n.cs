using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButt0n : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private int index;
    [SerializeField]
    private GameObject gameObjectHi;
    private Animator animator;
    private bool mouseHi;
    private void Start(){
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D col){
        if(!MenuUi.Instance.isOption)
        if(col.tag == "mouse"){
            ClickSound.Instance.GoSound(2);
            mouseHi = true;
            animator.SetBool("hi",mouseHi);
        }

    }
    private void OnTriggerExit2D(Collider2D col){
        if(col.tag == "mouse"){
            mouseHi = false;
            animator.SetBool("hi",mouseHi);
        }
    }
    private void Update(){
        if(mouseHi&&Input.GetMouseButtonDown(0)){
            if(index==0)Option();
            else if(index==1)GameStart();
            else if(index==2)Quit();
            ClickSound.Instance.GoSound(0);
            }
    }
    public void PushButton(){
        GoGame();
    }
    private void GoGame(){
        LoadingScene.LoadScene(sceneName);
    }
    private void Option(){
        MenuUi.Instance.Option();
    }
    private void Quit(){
        Application.Quit();
    }
    private void GameStart(){
        gameObjectHi.SetActive(true);
    }
    
}
