using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

     void OnTriggerEnter(Collider other) //En vez de usar collision se usa trigger para evitar que la moneda frene al player.
        {
           Destroy(gameObject);
        }

    void Update()
    { 
        transform.Rotate(0f, 0f, 80f * Time.deltaTime); //aplica rotacion en el eje z.
    }

}

