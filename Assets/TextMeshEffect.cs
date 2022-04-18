using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextMeshEffect : MonoBehaviour
{
    public bool isMoney;
    private Text text;
    private void Start(){
        text = GetComponent<Text>();
        
    }
    private void OnEnable(){
        if(text == null){
            text = GetComponent<Text>();
        }
        transform.localPosition = Vector3.zero;
        transform.DOLocalMoveY(transform.localPosition.y+1000, 0.5f);
        text.DOFade(0f, 0.5f);
    }
}
