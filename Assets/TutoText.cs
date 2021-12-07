using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoText : MonoBehaviour
{
    [SerializeField]
    private Color color,color2;
    private TextMesh textMesh;
    private bool isHi;
    private void OnTriggerEnter2D(Collider2D col){
        if(col.tag != "Player")return;
        isHi = true;
    }
    private void OnTriggerExit2D(Collider2D col){
        if(col.tag != "Player")return;
        isHi = false;
    }
    private void Start(){
        textMesh = GetComponentInChildren<TextMesh>();
    }
    private void Update(){
        textMesh.color = Color.Lerp(textMesh.color, (isHi)?color:color2, 0.1f);
    }
}
