using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum npcType { village=0,master,magician,craftsman,shop,tree};
public class Functiontalking : MonoBehaviour

{   public npcType name;
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;
    public GameObject nextbutton;
    [Header("文本文件")]
    public TextAsset textFile;
    public TextAsset text1;
    public TextAsset text2;
    public TextAsset text3;
    public TextAsset text4,text5,text6,text7;
    public int index=0;
    public float TtextSpeed;
    private float textSpeed;
    [Header("头像")]
    public Sprite face01,face02,face03;
    [Header("功能界面")]
    public GameObject  function;
    List<string> textList = new List<string>();


    bool textFinished;
    // Start is called before the first frame update
    private void Start() {
        if(name==npcType.village)
        {
            function = Chen_OldBook.instance.Body;
        }
        else if(name==npcType.master)
        {
            function = Chen_WeaponSkillUpGradeUI.instance.Body;
        }
        else if(name==npcType.tree)
        {
             function = Chen_SkillManage.Instance.Body;
        }
        
    }
    void Awake()
    {   
        switch(Chen_Save.instance.mainCityImage){
        case 0:
         GetTextFormFile(textFile);
         break;
        case 1:
        if((Chen_Save.instance.village1_1&&name==npcType.village)||(Chen_Save.instance.master1_1&&name==npcType.master))
        text1=textFile;
        GetTextFormFile(text1);
        break;
        case 2:
        GetTextFormFile(text2);
        break;
        case 3:
        if(Chen_Save.instance.magician1_2 &&name==npcType.magician)
        text3=textFile;
        GetTextFormFile(text3);
        break;
        case 4:
        GetTextFormFile(text4);
        break;
        case 5:
        GetTextFormFile(text5);
        break;
        case 6:
        GetTextFormFile(text6);
        break;
        case 7:
        textFile=text7;
        GetTextFormFile(text7);
        break;
        default:
        GetTextFormFile(textFile);
        break;
       }
    }
    private void OnEnable() {
    
       // textLabel.text = textList[index];
       // index++;
        StartCoroutine(SetTextUI());
    }
    // Update is called once per frame
    void Update()
    {   if(Input.GetKeyDown(KeyCode.F)&&index == textList.Count){//循环播放与关闭对话
        gameObject.SetActive(false);
        index = 0;
            GetTextFormFile(textFile);
      Chen_DataManage.instance.isTalking=false;
      Chen_DataManage.instance.canOpenUI = true;
      function.SetActive (true);
      if(name==npcType.village)
      {Chen_Save.instance.village1_1=true;
      if(Chen_Save.instance.mainCityImage==2)
      Chen_Save.instance.mainCityImage++;}
      else if(name==npcType.master)
      Chen_Save.instance.master1_1=true;
      else if(name==npcType.magician)
      Chen_Save.instance.magician1_2=true;
      else if(Chen_Save.instance.mainCityImage==4&&name==npcType.tree)
      Chen_Save.instance.mainCityImage++;
              return;
        }
        if(Input.GetKeyDown(KeyCode.F )&&textFinished==false)
           {
            textSpeed = 0;
           }
        if(Input.GetKeyDown(KeyCode.F )&&textFinished == true)
        {
            //textLabel.text = textList[index];
           // index++;
           StartCoroutine(SetTextUI());
           
        }
    }
    
    void GetTextFormFile(TextAsset file){//清空与获取文本
    textList.Clear();
    index = 0;

    var lineData = file.text.Split('\n');
    foreach(var line in lineData)
    {
        textList.Add(line);
    }
    }

     IEnumerator SetTextUI(){//逐字输出文本
       textFinished = false;
       textLabel.text = "";
     switch(textList[index])
     {
        case "猫娘\r":
              faceImage.sprite = face01;
              index=index+1;
              textSpeed = TtextSpeed;
              break;
        case "三角形\r":
              faceImage.sprite = face02;
              index=index+1;
              textSpeed = TtextSpeed;
              break;
        case "C\r":
              faceImage.sprite = face03;
              index=index+1;
              textSpeed = TtextSpeed;
              break;
     }
     nextbutton.SetActive(false);
     for(int i = 0;i<textList[index].Length;i++)
     {    
          textLabel.text+= textList[index][i];
          yield return new WaitForSeconds(textSpeed);
          }

     textFinished=true;
     nextbutton.SetActive(true);
     index++;
     }
}
