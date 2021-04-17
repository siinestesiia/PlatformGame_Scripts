using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   private Rigidbody rb;             
   private float impulsoSalto = 8f; 
   private bool isGrounded; //variable para evitar los multiples saltos.
   private bool saltando; //variable que chequea si la barra espaciadora es presionada.
 
   private Vector3 startPoint; //Contiene posicion inicial
   private float horizontalInput;   //contiene el valor del movimiento horizontal.
   

   private void EnAire()
   {
     if(saltando)
     {
        rb.AddForce(Vector3.up * impulsoSalto, ForceMode.VelocityChange);
        saltando = false;
     } 
   }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        startPoint = this.transform.position;
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            saltando = true;
        }
        
        
        horizontalInput = Input.GetAxis("Horizontal") * 4; //la variable toma el eje horizontal de movimiento, que puede ser negativo o positivo (A o D) y lo multiplica * 4.
                                                           // en Edit -> Project settings -> Input Manager, se puede ver los nombres de los axis (y modificar efectos).
       
        if(transform.position.y <= -13)
        {
            GameManager.Instance.Lives --;
            transform.position = startPoint; //el componente transform es igual a la posicion inicial
        }

    }

    void FixedUpdate() 
    {
        EnAire(); 
        //se toma la velocidad del rigidbody. se crea un nuevo vector y se especifican los ejes. para evitar que la capsula no caiga lento, en el eje "y" hay que poner su valor normal y no "0". o sea, rb.velocity.y
        rb.velocity = new Vector3(horizontalInput, rb.velocity.y, 0); 
    }                                                                  
                                                                         
    private void OnTriggerEnter(Collider Coin) 
    {
        GameManager.Instance.Coins ++;
    }


    private void OnCollisionEnter(Collision collision) 
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision) 
    {
        isGrounded = false;
    }
}

/* Commentarios sobre el proyecto que no necesariamente estan en el codigo.

   - Otra manera de agregar fuerza para el salto seria: rb.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
   
   - Para evitar que el jugador salte muchas veces, se declaró la variable tipo boolean "isGrounded" y se agregaron dos funciones "OnCollisionenter" y "OnCollisionExit".
     Luego se agregó el "if(!isGrounded)" dentro de FixedUpdate para evitar que se ejecute el salto.    

     - Otra manera de evitar que el personaje salte varias veces: declarar variable "[SerializedField] private Transform GroundCheckTransform = null;",
     luego crear un empty object y ponerlo en el slot del inspector como "GroundCheckTransform". Al empty Object emparentarlo al objeto Player y ubicarlo sobre
     el borde inferior donde colisiona con el suelo. En FixedUpdate poner un if(Physics.OverLapSphere(groundCheckTransform.position, 0.1f).Length == 1){return;}

   -El axis Horizontal se pude determinar derecha izquierda si es > o < a 0. es util para animaciones.  
*/
