using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

     void OnTriggerEnter(Collider other) 
        {
           Destroy(gameObject);
        }

    void Update()
    { 
        transform.Rotate(0f, 0f, 80f * Time.deltaTime); 
    }

}