using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class score : MonoBehaviour
{   public Text score1;
    // Start is called before the first frame update
    void Start()
    {
        score1.text = PlayerPrefs.GetString("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
