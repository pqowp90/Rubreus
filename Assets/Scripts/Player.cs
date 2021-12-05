using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float bulletDamage, bulletSpeed;
    [SerializeField]
    private float reloadTime;
    [SerializeField]
    private float speed=0f,lookAngle=0f,angle=0f,joomSpeed
        ,walkSpeed,runSpeed,cameraJoomIn,cameraJoomOut,joomInSpeed,myGunDeley;
    private float inputX,inputY,realInputX,realInputY,cameraJoom,gunDeley,beforeAngle,LerpAngle,turnSpeed;
    private Vector2 mousePos;
    private int anglePlayer,anglePlayer2;
    private Animator myAnimator;
    [SerializeField]
    private float maxHp,hp;
    public static int bullet,maxBullet;
    public int realMaxBullet;
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
    private IEnumerator playingCrt;
    private bool wallHi,isReloading;
    private int weightAni;
    private float _weightAni;
    [SerializeField]
    private GameObject gunLight;
    [SerializeField]
    private Transform effectTransform;
    void Start()
    {
        
        hp = maxHp;
        GameManager.Instance.playerUi.SetHpUi(hp, maxHp);
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        SetMaxBullet(0);
    }
    public float GetMaxHp(){
        return maxHp;
    }
    public void SetMaxBullet(int a){
        if(a!=0)realMaxBullet = a;
        Player.maxBullet = realMaxBullet;
        Player.bullet = realMaxBullet;
    }
    private IEnumerator Reloading(){
        //if(wallHi)yield return null;
        GameManager.Instance.playerUi.OnUI(0f);
        playingCrt = Reloading();
        isReloading = true;
        weightAni=1;
        myAnimator.Play("Rifle_Reload_2",-1,0f);
        yield return new WaitForSeconds(1.4f);
        Player.bullet = realMaxBullet;
        GameManager.Instance.playerUi.UpdateGunUi();
        GameManager.Instance.playerUi.OnUI(1.1f);
        yield return new WaitForSeconds(0.6f);
        isReloading = false;
        weightAni=0;
        // if(wallHi==false)
        //     weightAni=0;
        // else{
        //     myAnimator.Play("Rifle_Look_45U_Additive",-1,0f);
        // }
    }
    private IEnumerator LookUp(){//오류있어서 안씀
        weightAni=1;
        myAnimator.Play("Rifle_Look_45U_Additive",-1,0f);
        yield return new WaitUntil(()=>!wallHi);
        weightAni=0;
    }
    private void StopAllCrt(){
        if(playingCrt!=null)
            StopCoroutine(playingCrt);
        weightAni=0;
        
    }

    void Update()
    {
        _weightAni = Mathf.Lerp(_weightAni,(float)weightAni,0.1f);
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex("TopMove"), _weightAni);
        if(Input.GetKeyDown(KeyCode.R)){
            StopAllCrt();
            StartCoroutine(Reloading());
        }
        if(Input.GetMouseButton(0)&&gunDeley>=myGunDeley&&!myAnimator.GetBool("Run")
            &&!EventSystem.current.IsPointerOverGameObject()&& _weightAni<0.1f){
            
            if(Player.bullet > 0){
                GameManager.Instance.playerUi.OnUI(1.5f);
                gunDeley=0f;
                AllPoolManager.Instance.GetObjPos(0,casingOutlet).gameObject.SetActive(true);
                BulletBase bullet = AllPoolManager.Instance.GetObjPos(1,shootingPos).GetComponent<BulletBase>();
                Player.bullet--;
                bullet.damage = bulletDamage;
                bullet.speed = bulletSpeed;
                bullet.gameObject.SetActive(true);
                myParticleSystem.Play();
                GameManager.Instance.playerUi.UpdateGunUi();
            }else{
                StopAllCrt();
                StartCoroutine(Reloading());
            }
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
    private void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.layer != 6)return;
        //if(wallHi||isReloading)return;
        //wallHi=true;
        gunLight.SetActive(false);
        //GameManager.Instance.playerUi.OnUI(0f);
        //StopAllCrt();
        //StartCoroutine(LookUp());
    }
    private void OnTriggerExit2D(Collider2D collider2D){
        if(collider2D.gameObject.layer != 6)return;
        //wallHi=false;
        gunLight.SetActive(true);
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
    public void Damaged(float damage){
        
        effectTransform.localPosition = new Vector3(Random.Range(-0.3f,0.3f),1f,Random.Range(-0.1f,0.1f));
        AllPoolManager.Instance.GetObjPos(Random.Range(16,19), effectTransform).gameObject.SetActive(true);
        CinemachineShake.Instance.ShakeCamera(3, 0.2f);
        if(hp-damage<=0){
            hp = 0f;
            Die();
        }else
            hp -= damage;
        GameManager.Instance.playerUi.SetHpUi(hp, maxHp);
        
    }
    private void Die(){

    } 
}
