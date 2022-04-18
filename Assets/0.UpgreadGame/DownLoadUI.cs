using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DownLoadUI : MonoSingleton<DownLoadUI>
{
    private Canvas canvas;
    [SerializeField]
    private float distanse;
    [SerializeField]
    public List<Transform> panels;
    private bool onOff = false;
    private bool isMoving = false;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)&&isMoving == false){
            onOff = !onOff;
            if(onOff){
                OnPanels();
            }else{
                OffPanels();
            }
        }
    }
    public void OnPanels(){
        StopCoroutine("ShowUI");
        StopCoroutine("HideUI");
        StartCoroutine("ShowUI");
    }
    public void OffPanels(){
        StopCoroutine("HideUI");
        StopCoroutine("ShowUI");
        StartCoroutine("HideUI");
    }
    private IEnumerator ShowUI(){
        isMoving = true;
        for(int i=0;i<panels.Count;i++){
            panels[i].DOKill();
            panels[i].gameObject.SetActive(true);
            panels[i].DOLocalMove(AngleToDirection(360f / panels.Count * (panels.Count-i))*distanse, 0.2f);
            panels[i].DORotate(new Vector3(0f, 0f, (360f / panels.Count * (panels.Count-i))+90f), 0.2f);
            panels[i].GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.4f/panels.Count);
        }
        isMoving = false;
    }
    private IEnumerator HideUI(){
        isMoving = true;
        for(int i=0;i<panels.Count;i++){
            panels[i].DOKill();
            panels[i].DOLocalMove(Vector3.zero, 0.1f);
            panels[i].GetComponent<CanvasGroup>().DOFade(0, 0.2f);
            panels[i].DORotate(new Vector3(0f, 0f, 90f), 0.2f);
            StartCoroutine(SetActioveFalse(panels[i].gameObject));
            yield return new WaitForSeconds(0.4f/panels.Count);
        }
        isMoving = false;
    }
    private IEnumerator SetActioveFalse(GameObject panel){
        yield return new WaitForSeconds(0.2f);
        panel.gameObject.SetActive(false);
    }

    private Vector3 AngleToDirection(float angle)
    {
        Vector3 direction = Vector3.up;
 
        var quaternion = Quaternion.Euler(0, 0, angle);
        Vector3 newDirection = quaternion * direction;
 
        return newDirection;
    }

}
