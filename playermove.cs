using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{   [Header("动画相关")]
    public bool standing = true;//动画判断是否站立
    public float winkCD;//眨眼间隔
    public float winkCDTimer;
    [Header("平A")]
    Vector3 hit;
    public bool hited= false;
    public float attackCD;//平A间隔
    public float attackCDTimer;
    public GameObject attack;//平A碰撞体
    public bool attacking;//动画判断是否正在平A
    [Header("远程攻击")]
    public float vgotime=5f;//眩晕时间
    public float vertigoTimer = 0f;
    public bool vertigo=false;//是否眩晕中
    public GameObject Qjiantou;//Q技能范围
    public GameObject Qskill;//Q技能本体（如火焰，汁水，圆环等）
    public float QskillCD;//Q技能CD
    public float QCDTimer;
    public bool Qskilling=false;//判断是否在使用Q技能中
    [Header("冲刺技能")]
    public bool dashing= false;//判断是否在冲刺中
    public GameObject dashskill;//冲刺的箭头
    public Transform orientation;//位置
    public float dashForce;//冲刺力度
    public float dashDuration;
    public float dashCD;
    public float dashCDTimer;
    [Header("移动参数")]
    public float reco = 50f;//被击飞的力度
    public bool canmove=true;//能否移动
    Vector3 mpos;//鼠标位置
    public float realspeed;//用来计算是否正在走路
    public float setspeed;//设置的速度（初始跟movespeed一样，用来在movespeed改变后改回去）
     public float moveSpeed;//移动速度，受到眩晕后变成0，解除眩晕后恢复为setspeed
    public float rotateSpeed;//转角速度
    float rorateMultipliter = 1;
    Vector3 moveAmount;
    Vector3 targetDir;
    Rigidbody rb;
    public bool isskilling = false;//判断是否在冲刺中
    // Start is called before the first frame update
    void Start()
    {  setspeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   if(attackCDTimer<=0&&!vertigo)
        clickattack();
         Wink();
         realspeed = Mathf.Abs(Input.GetAxisRaw("Horizontal"))+Mathf.Abs(Input.GetAxisRaw("Vertical"));
        if(realspeed>0)
        {
            standing = false;
        }
        else
        standing = true;
         
        if(canmove)//行动
        {Vector3 moveDir =new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = moveDir * moveSpeed *Time.deltaTime;
        targetDir = Vector3.Lerp(transform.forward,moveDir,rotateSpeed*Time.deltaTime);
        
        if(!vertigo){
        if(!isskilling&&!Qskilling)//朝向
        transform.rotation = Quaternion.LookRotation(targetDir);
        else
        {Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo,10000000))
        {
            Vector3 target = hitInfo.point;
            
            target.y = transform.position.y;
            transform.LookAt(target);
        }
        }}}
        
        if(attackCDTimer>0)//平ACD
        attackCDTimer-=Time.deltaTime;
        if(QCDTimer>0)//技能cd
            QCDTimer-=Time.deltaTime;
        if(dashCDTimer>0)//冲刺CD
            dashCDTimer-=Time.deltaTime;
        if(vertigoTimer>0)
            vertigoTimer-=Time.deltaTime;    
        if(!vertigo){
        if(dashCDTimer<=0&& Input.GetKeyDown(KeyCode.LeftShift)&&!isskilling&&!Qskilling){
        dash();
        }
        if(QCDTimer<=0&& Input.GetKeyDown(KeyCode.Q)&&!isskilling&&!Qskilling){
        Qskill1();
        }
        if(Qskilling)
        {
        Qskill1();
        }
        if(isskilling)
        {   
            dash();
        }}
        if(hited)
        {   hit.y=0f;
            hit.Normalize();
            rb.AddForce(hit*reco*5f);
        }
        

    }
    private void Wink(){
       if(winkCDTimer>-1)
       winkCDTimer-=Time.deltaTime;
       if(winkCDTimer<=-1)
       {
        winkCDTimer=winkCD;
       }


    }
    private void clickattack(){//普攻
    if(Input.GetKeyDown(KeyCode.Mouse0)&&!isskilling&&!Qskilling&&!attacking)
    {   
        attacking=true;
        attack.gameObject.SetActive(true);
        Invoke(nameof(ResetAttack),0.75f);
    }
    }
    private void Qskill1(){//Q技能
     Qskilling = true;
     if(Qskilling)
       Qjiantou.gameObject.SetActive(true);
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {   
            Qskilling = false;
        Qjiantou.SetActive(false);
        return;
        }
     if(Input.GetKeyDown(KeyCode.Mouse0))  
     { canmove = false;
         Qskilling = false;
        Qjiantou.SetActive(false);
        Qskill.SetActive(true);
        QCDTimer = QskillCD;
         Invoke(nameof(ResetQ),1f);
     }
    }
    private void dash(){//冲刺
        isskilling = true;
        if(isskilling)
       dashskill.gameObject.SetActive(true);
       if(Input.GetKeyDown(KeyCode.Mouse1))
       {
        isskilling = false;
         dashskill.gameObject.SetActive(false);
         return;
       }
       if(Input.GetKeyDown(KeyCode.Mouse0))
       {dashing = true;
        canmove = false;
        isskilling = false;
        dashskill.gameObject.SetActive(false);
        dashCDTimer = dashCD;
         Vector3 forceToApply = orientation.forward*dashForce;
         delayedForceToApply = forceToApply;
         Invoke(nameof(DelayedDashForce),0.025f);

         Invoke(nameof(ResetDash),dashDuration*5);
        
       }

    }
    private void OnCollisionEnter(Collision other) {//受到攻击
        if(!vertigo)
        {   if(other.gameObject.tag=="Player")//攻击敌人时
        {
            bool isvertigo = other.gameObject.GetComponent<playermove>().vertigo;
            if(isvertigo == true)
            {
              float vtime =other.gameObject.GetComponent<playermove>().vertigoTimer;
              if(vtime>3)
              {data.instance.GetPoint(10);
              }
              else if(vtime>1)
              {
                data.instance.GetPoint(7);
              }
              else if(vtime>0)
              {
                data.instance.GetPoint(5);
              }
            }
        }
            else if((other.gameObject.tag == "attack"&&other.gameObject!=attack))
        {   Debug.Log("HIT");
            Vector3 recoilDrection = gameObject.transform.position - other.transform.position;
            Debug.Log(recoilDrection);
            recoilDrection.Normalize();
            hit = recoilDrection;
            rb.AddForce(recoilDrection*reco);
            hited = true;
            Invoke(nameof(Resethited),0.5f);
            return;
        }}
       else{
        if((other.gameObject.tag == "attack"||other.gameObject.tag=="Player"&&other.gameObject!=attack))
        {
             Debug.Log("HIT");
            Vector3 recoilDrection = gameObject.transform.position - other.transform.position;
            Debug.Log(recoilDrection);
            recoilDrection.Normalize();
            hit = recoilDrection;
            rb.AddForce(recoilDrection*reco);
            hited = true;
            Invoke(nameof(Resethited),0.5f);
            Resetvertigo();
            return;
        }
       }
       if(other.gameObject.tag=="wall")
       {
         Debug.Log("HIT");
            Vector3 recoilDrection = gameObject.transform.position - other.transform.position;
            Debug.Log(recoilDrection);
            recoilDrection.Normalize();
            hit = recoilDrection;
            rb.AddForce(recoilDrection*reco);
            hited = true;
            if(Mathf.Abs(hit.z)>Mathf.Abs(hit.x))
            {
                hit.z=0;
            }
            else
            {hit.x=0;}
            Invoke(nameof(Resethited),0.5f);
       }
        
    }
    private void OnTriggerEnter(Collider other) {//受到技能攻击
         if(other.gameObject.tag == "skill"&&other.gameObject!=Qskill)
        {   vertigoTimer = vgotime;
            vertigo = true;
            moveSpeed = 0;
            
            Invoke(nameof(Resetvertigo),5.0f);
        }
        
    }
    private Vector3 delayedForceToApply;
    private void DelayedDashForce(){
        rb.AddForce(delayedForceToApply,ForceMode.Impulse);
    }
    private void ResetQ(){//重置技能
    canmove = true;
    Qskill.SetActive(false);
    }
    private void ResetDash(){//重置冲刺
     canmove = true;
     dashing = false;
    }
    private void ResetAttack(){//重置平A
     attacking = false;
     attackCDTimer = attackCD;
     attack.gameObject.SetActive(false);
    }
    private void Resetvertigo(){//重置眩晕
        moveSpeed = setspeed;
        canmove = true;
        vertigo = false;
        vertigoTimer=0;
    }
    private void Resethited(){//重置被击退
        hited = false;
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position+moveAmount);
    }
}
