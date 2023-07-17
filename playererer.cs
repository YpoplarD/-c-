using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playererer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Collider2D coll;
    public float jumpforc;
    public Animator runanima;
    public LayerMask ground;
    public int jumpnumber = 0;
    public int gamescore = 0;
    public Text score;
    private bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if(!isHurt){
        movement();}
        switchanima();
    }
    void movement()
    {   float hormove;
        float faced = Input.GetAxisRaw("Horizontal");
        hormove = Input.GetAxis("Horizontal");
        if(hormove!=0)
        {
            rb.velocity = new Vector2(hormove * speed, rb.velocity.y);
            runanima.SetFloat("running",Mathf.Abs(faced));
        }
        if(faced !=0)
        {
            transform.localScale = new Vector3(faced,1,1);
        }
        if(Input.GetButtonDown("Jump")&&jumpnumber<2)
        {   jumpnumber++;
            rb.velocity = new Vector2(rb.velocity.x,jumpforc);
           runanima.SetBool("idel",false);
           runanima.SetBool("jumping",true);
        }
    }
    void switchanima(){
        if(runanima.GetBool("jumping"))
        {
            if(rb.velocity.y<=0)
            {
                runanima.SetBool("jumping",false);
                runanima.SetBool("falling",true);
            }
        }else if (isHurt)
            { runanima.SetBool("hurt",true);
                if(Mathf.Abs(rb.velocity.x)<0.1f)
                {  runanima.SetBool("hurt",false);
                   runanima.SetBool("idle",true);
                    isHurt = false;
                }
            }
        
        if(runanima.GetBool("falling"))
        {   if(transform.position.y<-6.63f)
            {SceneManager.LoadScene(2);}
            if(rb.velocity.y==0&&coll.IsTouchingLayers(ground))
            {
                runanima.SetBool("falling",false);
                runanima.SetBool("idel",true);
                jumpnumber=0;
            }
            else if(rb.velocity.y>0)
            {
                runanima.SetBool("falling",false);
                runanima.SetBool("jumping",true);
            }
        }
        
        else if(runanima.GetBool("idel"))
        {
            if(rb.velocity.y<0)
            {
                runanima.SetBool("falling",true);
                runanima.SetBool("idel",false);
            }
        }
    }
 private void OnTriggerEnter2D(Collider2D collision) {
 if (collision.tag == "collection")
 {
       Destroy(collision.gameObject);
       gamescore+=100;
       score.text = gamescore.ToString();
 }   
 if (collision.tag == "collection2")
 {
       Destroy(collision.gameObject);
       gamescore+=300;
       score.text = gamescore.ToString();
 }   
 if (collision.tag == "door")
 {
    SceneManager.LoadScene(3);
    PlayerPrefs.SetString("Score",score.text);
 }
 }
//消灭敌人
private void OnCollisionEnter2D(Collision2D collision) {
    if(collision.gameObject.tag =="enemy")
   {  if(runanima.GetBool("falling"))
    {
          Destroy(collision.gameObject);
           rb.velocity = new Vector2(rb.velocity.x,jumpforc);
           gamescore+=50;
           score.text = gamescore.ToString();
           runanima.SetBool("idel",false);
           runanima.SetBool("jumping",true);
    }
    else if(transform.position.x < collision.gameObject.transform.position.x)
    {
        rb.velocity = new Vector2(-10,rb.velocity.y);
        isHurt = true;
        gamescore-=200;
        score.text = gamescore.ToString();
        if(gamescore<0)
        {
            SceneManager.LoadScene(2);
        }
    }
    else if(transform.position.x > collision.gameObject.transform.position.x)
    {
        rb.velocity = new Vector2(10,rb.velocity.y);
        isHurt = true;
        gamescore-=200;
        score.text = gamescore.ToString();
        if(gamescore<0)
        {
            SceneManager.LoadScene(2);
        }

    }
}
}

}
