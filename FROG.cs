using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FROG : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Transform leftpoint,rightpoint;
    public float leftx,rightx;
    private bool Faceleft = true;
    public float Speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }  
    void Movement(){
    if(Faceleft)
    {
        rb.velocity = new Vector2(-Speed,rb.velocity.y);
        if(transform.position.x<leftx)
        {
            transform.localScale = new Vector3(-1,1,1);
            Faceleft = false;
        }
    }
    else{
    rb.velocity = new Vector2(Speed,rb.velocity.y);
    if(transform.position.x>rightx)
        {
            transform.localScale = new Vector3(1,1,1);
            Faceleft = true;
        }
    }

    }
}
