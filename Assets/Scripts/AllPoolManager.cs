
using UnityEngine;

public class AllPoolManager : MonoSingleton<AllPoolManager>
{
    [SerializeField]
    private GameObject[] objects;
    [SerializeField]
    private const int count=0;
    [SerializeField]
    private Transform[] mothersOfObjects;
    
    void Awake()
    {
        for(int index=0;index<objects.Length;index++){
            mothersOfObjects[index] = new GameObject(objects[index].name+"'s Mother").transform;
            mothersOfObjects[index].transform.SetParent(transform);
        }
    }

    public Transform GetObj(int index){
        Transform obj;
        if(mothersOfObjects[index].transform.childCount>0){
            
            obj = mothersOfObjects[index].GetChild(0);
            obj.SetParent(null);
            
        }else {
            obj = Instantiate(objects[index], new Vector3(100f,100f,0f),Quaternion.identity).transform;
        }
        obj.gameObject.SetActive(false);
        return obj;
    }
    public Transform GetObjPos(int index, Transform Pos){
        Transform obj;
        obj = GetObj(index);
        obj.position = Pos.position;
        obj.rotation = Pos.rotation;
        return obj;
    }
    public Transform GetObjTransform(int index, Transform Trans){
        Transform obj;
        obj = GetObj(index);
        obj.SetParent(Trans);
        obj.localPosition = Vector3.zero;
        obj.localRotation = Quaternion.identity;
        return obj;
    }
    public void PoolObj(Transform obj,int index){
        obj.gameObject.SetActive(false);
        obj.SetParent(mothersOfObjects[index]);
    }
}
