using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private Vector3 offset = new Vector3(0, 1, -10); 
    
    void Update()
    {
        this.transform.position = GameObject.FindWithTag("Player").transform.position + offset;
    }
}