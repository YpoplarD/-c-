using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallmap : MonoBehaviour
{
   GameObject mapsprite;
   private void OnEnable() {
    
    mapsprite=transform.parent.GetChild(0).gameObject;
    mapsprite.SetActive(false);

   }
   private void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Player"))
    {   cameracontroer.instance.ChangeTarget(transform);
        mapsprite.SetActive(true);


    }
   }
}
