using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP ;
public class RangePotap : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private UnityEngine.Experimental.Rendering.Universal.Light2D myLight2D;
    public bool hihi=false;
    [SerializeField]
    public bool good = true;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myLight2D = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }
    private void Update(){
        int layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Range"));
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x/2f,layerMask);
        good=(cols.Length==1);
        if(hihi)return;
        if(good){
            mySpriteRenderer.color=new Color(0f,0.490f,0.0459f,0.403f);
            myLight2D.color=new Color(0f,0.490f,0.0459f,0);
        }else{
            mySpriteRenderer.color=new Color(0.489f,0f,0f,0.403f);
            myLight2D.color=new Color(0.489f,0f,0f,0f);
        }
        if(Input.GetMouseButtonDown(0)&&good)
            hihi=true;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0f,0f,10f);
    }
}
