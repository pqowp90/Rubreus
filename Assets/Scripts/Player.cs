using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float bulletDamage, bulletSpeed;
    [SerializeField]
    private float speed=0f,lookAngle=0f,angle=0f,joomSpeed
        ,walkSpeed,runSpeed,cameraJoomIn,cameraJoomOut,joomInSpeed,myGunDeley;
    private float inputX,inputY,realInputX,realInputY,cameraJoom,gunDeley,beforeAngle,LerpAngle,turnSpeed;
    private Vector2 mousePos;
    private int anglePlayer,anglePlayer2;
    private Animator myAnimator;
    [SerializeField]
    private Camera myCamera;
    [SerializeField]
    private Transform playerTransform,angleTransform,playerCenter,casingOutlet,shootingPos;
    private Vector3 rote;
    [SerializeField]
    private bool Run,joom;
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    private Rigidbody2D myRigidbody2D;
    [SerializeField]
    private ParticleSystem myParticleSystem;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
        if(Input.GetMouseButton(0)&&gunDeley>=myGunDeley&&!myAnimator.GetBool("Run")){
            gunDeley=0f;
            AllPoolManager.Instance.GetObjPos(0,casingOutlet).gameObject.SetActive(true);
            Bullet bullet = AllPoolManager.Instance.GetObjPos(1,shootingPos).GetComponent<Bullet>();
            bullet.damage = bulletDamage;
            bullet.speed = bulletSpeed;
            bullet.gameObject.SetActive(true);
            myParticleSystem.Play();
        }
        gunDeley+=Time.deltaTime;




        joom=Input.GetMouseButton(1);
        myAnimator.SetBool("Joom",joom);
        bool isRun = Input.GetKey(KeyCode.LeftShift);
        if(isRun){

            joom=false;
            cameraJoom = cameraJoomIn;
            
            speed=runSpeed;
        }
        myAnimator.SetBool("Run",isRun);
        Run=isRun;
        
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
            cameraJoom = joom?cameraJoomOut:cameraJoomIn;
        }
        
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize,cameraJoom,Time.deltaTime*joomInSpeed);
        SmoothWalkLookMouse2();
        
    }
    private void FixedUpdate(){
        Move();
        float hi = Mathf.Abs(lookAngle-beforeAngle);
        //if(hi<1.2f)hi=0f;
        LerpAngle = Mathf.Lerp(LerpAngle,Mathf.Abs(hi),(hi<0.8f)?0.1f:0.5f);
        if(LerpAngle>2f)LerpAngle=2f;
        myAnimator.SetFloat("Turn",LerpAngle);
        beforeAngle=lookAngle;
    }
    private void LookMouseMode(){
        mousePos = Input.mousePosition;
        mousePos = myCamera.ScreenToWorldPoint(mousePos);
        mousePos = (Vector2)playerCenter.position-mousePos;
        mousePos.Normalize();
        lookAngle = Mathf.Atan2(mousePos.x,mousePos.y) * Mathf.Rad2Deg;
        lookAngle+=180f;
        playerTransform.localRotation = Quaternion.Euler(0f,lookAngle-16.37f,0f);
        angleTransform.localRotation = Quaternion.Euler(0f,0f,lookAngle-16.37f);
        if(inputX!=0||inputY!=0){
            myAnimator.SetBool("Stop",false);
        }else{
            myAnimator.SetBool("Stop",true);
        }
        
        // if(Mathf.Abs((lookAngle-beforeAngle))/2f>45f)
        //     beforeAngle = lookAngle;
        
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
            myAnimator.SetBool("Stop",true);
            realInputX=0f;
            realInputY=0f;
        }else{
            myAnimator.SetBool("Stop",false);
        }
        

        rote = (new Vector2(realInputX,realInputY)).normalized;
        transform.Translate((new Vector2(inputX,inputY)).normalized*speed*0.02f);
        angle = Mathf.Atan2(realInputX,realInputY) * Mathf.Rad2Deg;
    }
}
