using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButt0n : MonoBehaviour
{
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
            ClickSound.Instance.GoSound(0);
            }
    }
    private void Option(){
        MenuUi.Instance.Option();
    }
    
}
