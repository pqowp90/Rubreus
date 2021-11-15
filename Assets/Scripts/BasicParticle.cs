
using UnityEngine;

public class BasicParticle : MonoBehaviour
{
    [SerializeField]
    private float lifeTime;
    private float timeTime;
    [SerializeField]
    private int index;
    void Start()
    {
        //Invoke("Pool",lifeTime);
    }
    void OnEnable(){
        timeTime=lifeTime;
    }
    private void Update(){
        timeTime-=Time.deltaTime;
        if(timeTime<=0){
            timeTime=10f;
            AllPoolManager.Instance.PoolObj(transform,index);
        }
    }
}
