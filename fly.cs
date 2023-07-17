using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public Transform highpoint,lowpoint;
    public float highy,lowy;
    private bool Faceleft = true;
    public float Speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        highy = highpoint.position.y;
        lowy = lowpoint.position.y;
        Destroy(highpoint.gameObject);
        Destroy(lowpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement(){
    if(Faceleft)
    {
        rb.velocity = new Vector2(rb.velocity.x,Speed);
        if(transform.position.y>highy)
        {
        
            Faceleft = false;
        }
    }
    else{
     rb.velocity = new Vector2(rb.velocity.x,-Speed);
    if(transform.position.y<lowy)
        {
            
            Faceleft = true;
        }
    }
}
}