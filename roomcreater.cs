using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class roomcreater : MonoBehaviour
{   private int i;
    public int dir;
    public enum Direction {up,down,left,right};
    public Direction direction;

    [Header("房间信息")]
    public GameObject roomPrefabs;
    public static int roomNumber = 8;
    public static ROOM endRoom;
    public Color startColor,endColor;
   
    [Header("位置控制")]
    public Transform createrPoint;
    public float xOffset;
    public float yOffset;
    public static List<ROOM> rooms = new List<ROOM>();
    public LayerMask roomlayer;
    public ROOM beforeboss;
    public WallType wallType;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<roomNumber;i++)
        {
            rooms.Add(Instantiate(roomPrefabs,createrPoint.position,Quaternion.identity).GetComponent<ROOM>());

            //改变point位置
             changepointpos();
        }
        rooms[0].text.color = startColor;
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
       
       
       
        //找最远房间
        beforeboss = rooms[0];
        foreach(var room in rooms)
        {  
            if(Mathf.Abs(room.transform.position.x/xOffset)+Mathf.Abs(room.transform.position.y/yOffset)>=Mathf.Abs(beforeboss .transform.position.x/xOffset)+Mathf.Abs(beforeboss.transform.position.y/yOffset))
            {   if(Mathf.Abs(room.transform.position.x/17)!=Mathf.Abs(room.transform.position.y/8)||Mathf.Abs(room.transform.position.x/xOffset)+Mathf.Abs(room.transform.position.y/yOffset)>Mathf.Abs(beforeboss.transform.position.x/xOffset)+Mathf.Abs(beforeboss.transform.position.y/yOffset))
                beforeboss = room;
            }
           
        }
        
        



        if(Mathf.Abs(beforeboss.transform.position.x/xOffset)>=Mathf.Abs(beforeboss.transform.position.y/yOffset))
        {
            if(beforeboss.transform.position.x>0)
            {
            dir=3;}
            else
            {
            dir=2;}
        }
        else
        {
            if(beforeboss.transform.position.y>0)
            {
            dir =0;}
            else
            {
            dir =1;}
        }
         createrPoint.position = beforeboss.transform.position;
         endRoom = Instantiate(roomPrefabs,createrPoint.position,Quaternion.identity).GetComponent<ROOM>();
         
         rooms.Add(endRoom);
         
        finalchang(dir);
        i = 0;
        foreach(var room in rooms)
        {  
         setuproom(room,room.transform.position);
         i++;
         }
         
         setupendroom(beforeboss);
         endRoom.GetComponent<SpriteRenderer>().color = endColor;
         endRoom.text.color = endColor;
         rooms[roomNumber].sumend(rooms,roomNumber);

    }

    // Update is called once per frame
  
  
  
    void Update()
    {
        
    }
    
    
    
    
    public void changepointpos()
    {   


        do{
        direction = (Direction)Random.Range(0,4); 
        switch(direction)
        {
             case Direction.up:
                 createrPoint.position += new Vector3(0,yOffset,0);
                 break;
             case Direction.down:
             createrPoint.position += new Vector3(0,-yOffset,0);
             break;
             case Direction.left:
             createrPoint.position += new Vector3(-xOffset,0,0);
             break;
             case Direction.right:
             createrPoint.position += new Vector3(xOffset,0,0);
             break;
             
        }
        }while(Physics2D.OverlapCircle(createrPoint.position,0.2f,roomlayer));       

    }
   
   
   
   
    public void setuproom(ROOM newroom,Vector3 roomPosition){
        newroom.roomup= Physics2D.OverlapCircle(roomPosition + new Vector3(0,yOffset,0),0.2f,roomlayer);
        newroom.roomdown = Physics2D.OverlapCircle(roomPosition + new Vector3(0,-yOffset,0),0.2f,roomlayer);
        newroom.roomleft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset,0,0),0.2f,roomlayer);
        newroom.roomright = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset,0,0),0.2f,roomlayer);
        newroom.updateroom(xOffset,yOffset,i);
        if(newroom.transform.position!=beforeboss.transform.position)
        {
        switch(newroom.doornumber)
            {
                case 1:
                    if(newroom.roomup)
                    Instantiate(wallType.Wall_U,roomPosition,Quaternion.identity);
                    else if(newroom.roomdown)
                    Instantiate(wallType.Wall_D,roomPosition,Quaternion.identity);
                    else if(newroom.roomleft)
                    Instantiate(wallType.Wall_L,roomPosition,Quaternion.identity);
                    else if(newroom.roomright)
                    Instantiate(wallType.Wall_R,roomPosition,Quaternion.identity);
                    break;
                case 2:
                    if(newroom.roomup&&newroom.roomdown)
                    Instantiate(wallType.Wall_UD,roomPosition,Quaternion.identity);
                    else if(newroom.roomup&&newroom.roomleft)
                    Instantiate(wallType.Wall_UL,roomPosition,Quaternion.identity);
                    else if(newroom.roomup&&newroom.roomright)
                    Instantiate(wallType.Wall_UR,roomPosition,Quaternion.identity);
                    else if(newroom.roomdown&&newroom.roomleft)
                    Instantiate(wallType.Wall_DL,roomPosition,Quaternion.identity);
                    else if(newroom.roomdown&&newroom.roomright)
                    Instantiate(wallType.Wall_DR,roomPosition,Quaternion.identity);
                    else if(newroom.roomright&&newroom.roomleft)
                    Instantiate(wallType.Wall_RL,roomPosition,Quaternion.identity);
                    break;
                case 3:
                    if(newroom.roomup&&newroom.roomdown&&newroom.roomleft)
                    Instantiate(wallType.Wall_UDL,roomPosition,Quaternion.identity);
                    else if(newroom.roomup&&newroom.roomdown&&newroom.roomright)
                    Instantiate(wallType.Wall_UDR,roomPosition,Quaternion.identity);
                    else if(newroom.roomleft&&newroom.roomdown&&newroom.roomleft)
                    Instantiate(wallType.Wall_DRL,roomPosition,Quaternion.identity);
                    else if(newroom.roomleft&&newroom.roomup&&newroom.roomleft)
                    Instantiate(wallType.Wall_URL,roomPosition,Quaternion.identity);
                    break;
                case 4:
                    Instantiate(wallType.Wall_UDRL,roomPosition,Quaternion.identity);
                    break;
                    
            }
            }


    }
  
  
  
  
   public void finalchang(int dir){
   switch(dir){
    case 0:
    endRoom.transform.position += new Vector3(0,yOffset,0);
             break;
    case 1: 
   
   endRoom.transform.position += new Vector3(0,-yOffset,0);
             break;
    case 2:
  
    endRoom.transform.position += new Vector3(-xOffset,0,0);
             break;
    case 3:
   endRoom.transform.position += new Vector3(xOffset,0,0);
             break;
   }
   

   }
    
    
    
    
    
    public void setupendroom(ROOM beforeboss){
    if(dir==0)
    {
       beforeboss.roomup=true;
    
    }
    else if(dir==1)
    {
       beforeboss.roomdown=true;
    
    }
    else if(dir==2)
    {
       beforeboss.roomleft=true;
    }
    else if(dir==3)
    {
       beforeboss.roomright=true;
    }
      beforeboss.doornumber++;
     switch(beforeboss.doornumber)
            {
                case 1:
                    if(beforeboss.roomup)
                    Instantiate(wallType.Wall_U,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomdown)
                    Instantiate(wallType.Wall_D,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomleft)
                    Instantiate(wallType.Wall_L,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomright)
                    Instantiate(wallType.Wall_R,beforeboss.transform.position,Quaternion.identity);
                    break;
                case 2:
                    if(beforeboss.roomup&&beforeboss.roomdown)
                    Instantiate(wallType.Wall_UD,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomup&&beforeboss.roomleft)
                    Instantiate(wallType.Wall_UL,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomup&&beforeboss.roomright)
                    Instantiate(wallType.Wall_UR,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomdown&&beforeboss.roomleft)
                    Instantiate(wallType.Wall_DL,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomdown&&beforeboss.roomright)
                    Instantiate(wallType.Wall_DR,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomright&&beforeboss.roomleft)
                    Instantiate(wallType.Wall_RL,beforeboss.transform.position,Quaternion.identity);
                    break;
                case 3:
                    if(beforeboss.roomup&&beforeboss.roomdown&&beforeboss.roomleft)
                    Instantiate(wallType.Wall_UDL,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomup&&beforeboss.roomdown&&beforeboss.roomright)
                    Instantiate(wallType.Wall_UDR,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomleft&&beforeboss.roomdown&&beforeboss.roomleft)
                    Instantiate(wallType.Wall_DRL,beforeboss.transform.position,Quaternion.identity);
                    if(beforeboss.roomleft&&beforeboss.roomup&&beforeboss.roomleft)
                    Instantiate(wallType.Wall_URL,beforeboss.transform.position,Quaternion.identity);
                    break;
                case 4:
                    Instantiate(wallType.Wall_UDRL,beforeboss.transform.position,Quaternion.identity);
                    break;
            }
    }

[System.Serializable]
public class WallType{
    public GameObject Wall_L,Wall_R,Wall_U,Wall_D,
                      Wall_RL,Wall_DL,Wall_UL,Wall_UD,Wall_UR,Wall_DR,
                      Wall_UDL,Wall_UDR,Wall_URL,Wall_DRL,
                      Wall_UDRL;


}







    
}
