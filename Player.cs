using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   private Vector3 startPoint; 
   private Rigidbody rb;             
   private float jumpImpulse = 8f; 
   private float horizontalInput;   
   private bool isGrounded; 
   private bool isJumping; 
 
   
   private void OnAir()
   {
     if(isJumping)
     {
        rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
        isJumping = false;
     } 
   }


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        startPoint = this.transform.position;
    }

    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * 4; 
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            isJumping = true;
        }
       
        if(transform.position.y <= -13)
        {
            GameManager.Instance.Lives --;
            transform.position = startPoint; 
        }
    }

    void FixedUpdate() 
    {
        OnAir(); 
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