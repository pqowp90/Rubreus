using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP ;
using UnityEngine.EventSystems;
public class RangePotap : MonoBehaviour
{
    
    public int num;
    private SpriteRenderer mySpriteRenderer;
    private UnityEngine.Experimental.Rendering.Universal.Light2D myLight2D;
    public bool hihi=false;
    [SerializeField]
    public bool good = true;
    private Vector2 myPos;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myLight2D = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }
    private void OnEnable(){
        
        transform.localScale = new Vector3(1f,1f,1f)*GameManager.Instance.CurrentUser.potapList[num].range*2f;
        if(myLight2D == null)myLight2D=GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        myLight2D.pointLightOuterRadius = GameManager.Instance.CurrentUser.potapList[num].range;
    }
    
    private void Update(){
        if(!hihi) transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+new Vector3(0f,0f,10f);
        int layerMask = (1 << LayerMask.NameToLayer("Wall")) + (1 << LayerMask.NameToLayer("Range"));
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x/2f,layerMask);
        good=(cols.Length==1);
        
        if(good){
            if(hihi){
                mySpriteRenderer.enabled = false;
                myLight2D.enabled = false;
            }else{
                mySpriteRenderer.color=new Color(0f,0.490f,0.0459f,0.227451f);
                myLight2D.color=new Color(0f,0.490f,0.0459f,0);
            }
        }else{
            if(hihi){
                mySpriteRenderer.color=new Color(0.489f,0f,0f,0.227451f);
                myLight2D.color=new Color(0.489f,0f,0f,0f);
                mySpriteRenderer.enabled = true;
                myLight2D.enabled = true;
            }else{
                mySpriteRenderer.color=new Color(0.489f,0f,0f,0.227451f);
                myLight2D.color=new Color(0.489f,0f,0f,0f);
            }
        }
        
        if(hihi)return;
        
        if(Input.GetMouseButtonDown(1)){
            AllPoolManager.Instance.PoolObj(transform,9);
            GameManager.Instance.isMakeingPotap = false;
        }
        if(Input.GetMouseButtonDown(0)&&good&&!EventSystem.current.IsPointerOverGameObject()){
            GameManager.Instance.AddMoney(-GameManager.Instance.CurrentUser.potapList[num].cost);
            myPos = transform.position;
            GameManager.Instance.isMakeingPotap = false;
            hihi=true;
            mySpriteRenderer.enabled = false;
            myLight2D.enabled = false;
            MakePoTap();
        }
        
    }
    public void MakePoTap(){
        transform.position = myPos;
        Transform drone = AllPoolManager.Instance.GetObj(8);
        drone.position = new Vector3(500f,500f,0f);
        drone.GetComponent<Drone>().droneMove(num, transform.position);
        drone.gameObject.SetActive(true);
    }
}
