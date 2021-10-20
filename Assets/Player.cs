using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed=0f,lookAngle=0f,angle=0f,joomSpeed,walkSpeed,runSpeed,cameraJoomIn,cameraJoomOut,cameraJoom3;
    private float inputX,inputY,realInputX,realInputY;
    private Vector2 mousePos;
    private int anglePlayer,anglePlayer2;
    private Animator myAnimator;
    [SerializeField]
    private Camera myCamera;
    [SerializeField]
    private Transform playerTransform,angleTransform;
    private Vector3 rote;
    private bool Run,joom;
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    private Rigidbody2D myRigidbody2D;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        joom=Input.GetMouseButton(1);
        myAnimator.SetBool("Joom",joom);
        if(Input.GetKey(KeyCode.LeftShift)){
            myAnimator.SetBool("Run",true);
            Run=true;
            joom=false;
            vcam.m_Lens.OrthographicSize = cameraJoom3;
            speed=runSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            myAnimator.SetBool("Run",false);
            Run=false;
            
        }
        if(inputX!=0||inputY!=0){
            if(Run){
                playerTransform.localRotation = Quaternion.Euler(0f,angle,0f);
            }else{
                LookMouseMode();
            }
        }            
        else{
            myAnimator.SetBool("Run",false);
            LookMouseMode();
        }
        if(!Run){
            speed=joom?joomSpeed:walkSpeed;
            vcam.m_Lens.OrthographicSize = joom?cameraJoomOut:cameraJoomIn;
        }
        
        SmoothWalkLookMouse2();
        
    }
    private void FixedUpdate(){
        Move();
    }
    private void LookMouseMode(){
        mousePos = Input.mousePosition;
        mousePos = myCamera.ScreenToWorldPoint(mousePos);
        mousePos = (Vector2)transform.position-mousePos;
        mousePos.Normalize();
        lookAngle = Mathf.Atan2(mousePos.x,mousePos.y) * Mathf.Rad2Deg;
        lookAngle+=180f;
        playerTransform.localRotation = Quaternion.Euler(0f,lookAngle,0f);
        angleTransform.localRotation = Quaternion.Euler(0f,0f,lookAngle);
    }
    private void SmoothWalkLookMouse(){//하.. 이제안씀 블랜드트리 2d라니
        angle = Mathf.Atan2(inputY,inputX) * Mathf.Rad2Deg;
        anglePlayer = (int)(((angle+180f)/360f)*8);
        anglePlayer2 = (int)(((lookAngle+22.5f)/360f)*8);
        if(anglePlayer2==8)
            anglePlayer2=0;
        anglePlayer+=anglePlayer2;
        anglePlayer%=8;
        if(inputX==0&&inputY==0){
            anglePlayer=8;
            if(myAnimator.GetBool("Move")){
                myAnimator.SetBool("Move",false);
                myAnimator.SetTrigger("Oh");
            }
        }
        else{
            if(!myAnimator.GetBool("Move")){
                myAnimator.SetBool("Move",true);
                myAnimator.SetTrigger("Oh");
            }
        }

        myAnimator.SetInteger("Angle22",anglePlayer);
    }
    private void SmoothWalkLookMouse2(){
        Vector3 lookV;
        lookV = angleTransform.TransformDirection(rote.x,rote.y,0f);
        myAnimator.SetFloat("Blend1",lookV.x);
        myAnimator.SetFloat("Blend2",lookV.y);
    }
    
    private void Move(){
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        realInputX = Mathf.Lerp(realInputX,inputX,20f*Time.deltaTime);
        realInputY = Mathf.Lerp(realInputY,inputY,20f*Time.deltaTime);
        if(inputX==0&&inputY==0){
            realInputX=0f;
            realInputY=0f;
        }

        rote = (new Vector2(realInputX,realInputY)).normalized;
        transform.Translate((new Vector2(inputX,inputY)).normalized*speed*Time.deltaTime);
        angle = Mathf.Atan2(realInputX,realInputY) * Mathf.Rad2Deg;
    }
}
