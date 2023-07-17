using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;
    public GameObject nextbutton;
    [Header("文本文件")]
    public TextAsset textFile;
    public TextAsset text2;
    public int index=0;
    public float TtextSpeed;
    private float textSpeed;
    [Header("头像")]
    public Sprite face01,face02,face03;
    List<string> textList = new List<string>();


    bool textFinished;
    // Start is called before the first frame update
    void Awake()
    {   
        GetTextFormFile(textFile);
      
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
            GetTextFormFile(text2);
            Chen_DataManage.instance.canOpenUI = true;
      Chen_DataManage.instance.isTalking=false;
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
