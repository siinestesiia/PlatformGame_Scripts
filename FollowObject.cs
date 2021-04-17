using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject objectToFollow;
    private Vector3 offset = new Vector3(0, 1, -10); //It can be initialized like so: public Vector3 offset = new Vector3(x, y, z);
    

    void Update()
    {
        //this.transform.position = objectToFollow.transform.position + offset; //The offset is added to the position of the object to follow.
        this.transform.position = GameObject.FindWithTag("Player").transform.position + offset;
    }
}

/*
    This script will follow a GameObject. We'll put an offset so the camera'll be away from the player.
    Another way to do the offset is in the update. Instead of "+ offset;" --> "+ new Vector(x, y, z);" 
*/