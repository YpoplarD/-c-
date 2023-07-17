using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ROOM : MonoBehaviour
{    public GameObject doorright,doorleft,doorup,doordown;
     public bool roomleft,roomright,roomup,roomdown;
     public int stepToStart;
 public int doornumber;
 public int Mapnumber;
 public bool item=false;
 public GameObject sub,div,mul;
 public Text text;
 public static int enemynumber=0;
 public bool flag=false;
 public int rand;
 bool door = true;
 public Transform tran;
  public GameObject enemy1,enemy2,enemy3,enemy4,enemy5,enemy6,enemy7,enemy8,enemy9,boss;
    public static bool bosscome;
    // Start is called before the first frame update
    void Start()
    {
        bosscome = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemynumber==0)
        {
         doorleft.SetActive(false);
        doordown.SetActive(false);
        doorright.SetActive(false);
        doorup.SetActive(false);
        door = true;
        }
        else
        {if(door)
       { doorleft.SetActive(roomleft);
        doordown.SetActive(roomdown);
        doorright.SetActive(roomright);
        doorup.SetActive(roomup);
        MusicMgr.GetInstance().PlaySound("关门", false);
        door=false;
       }
        }
        
    }
    public void updateroom(float xOffset,float yOffset,int i)
    {   if(i<=roomcreater.roomNumber/2)
         Mapnumber = UnityEngine.Random.Range(5,9);
         else
         Mapnumber = UnityEngine.Random.Range(8,11);
    if(transform.position.x==0&&transform.position.y==0)
        Mapnumber = 0;

        text.text = Mapnumber.ToString();
         stepToStart = (int)(Mathf.Abs(transform.position.x/xOffset)+Mathf.Abs(transform.position.y)/yOffset);
        if(roomup)
        doornumber++;
        if(roomdown)
        doornumber++;
        if(roomleft)
        doornumber++;
        if(roomright)
        doornumber++;
    }
    public void sumend(List<ROOM> room,int roomnumber)
    {    int sum=0;
          for(int i = 0; i < roomnumber;i++)
     {
          sum += Convert.ToInt32(room[i].text.text);
          Mapnumber = sum;
     }
          text.text = sum.ToString();

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Player"))
        {   
            cameracontroer.instance.ChangeTarget(transform);
         smallmap.instance.ChangeTarget(transform);
           
            if(!flag)
            {        if(transform.position != roomcreater.endRoom.transform.position)
                     {
                     for(int i = 0;i<Mapnumber;i++)
                     {  rand = UnityEngine.Random.Range(1,10);
                        switch(rand){
                        case 1:
                        GameObject.Instantiate(enemy1,transform.position,Quaternion.identity);
                        break;
                        case 2:
                        GameObject.Instantiate(enemy2,transform.position,Quaternion.identity);
                        break;
                        case 3:
                        GameObject.Instantiate(enemy3,transform.position,Quaternion.identity);
                        break;
                        case 4:
                        GameObject.Instantiate(enemy4,transform.position,Quaternion.identity);
                        break;
                        case 5:
                        GameObject.Instantiate(enemy5,transform.position,Quaternion.identity);
                        break;
                        case 6:
                        GameObject.Instantiate(enemy6,transform.position,Quaternion.identity);
                        break;
                        case 7:
                        GameObject.Instantiate(enemy7,transform.position,Quaternion.identity);
                        break;
                        case 8:
                        GameObject.Instantiate(enemy8,transform.position,Quaternion.identity);
                        break;
                        case 9:
                        GameObject.Instantiate(enemy9,transform.position,Quaternion.identity);
                        break;
                        
                        }
                           enemynumber++;}}
                           else
                           {
                            GameObject.Instantiate(boss,transform.position,Quaternion.identity);
                            Boss.HP = 15*roomcreater.endRoom.Mapnumber;
                            bosscome = true;
                            enemynumber++;
                           }
                    flag=true;
                    item = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(item&&enemynumber==0&&(transform.position!=roomcreater.rooms[0].transform.position)&&(transform.position!=roomcreater.endRoom.transform.position))
             {rand = UnityEngine.Random.Range(0,10);
              if(rand>=0&&rand<=4)
              GameObject.Instantiate(sub,transform.position,Quaternion.identity);
              else if(rand >4&&rand<=6)
              GameObject.Instantiate(div,transform.position,Quaternion.identity);
              else 
              GameObject.Instantiate(mul,transform.position,Quaternion.identity);
              item = false;
        }
    }

}
