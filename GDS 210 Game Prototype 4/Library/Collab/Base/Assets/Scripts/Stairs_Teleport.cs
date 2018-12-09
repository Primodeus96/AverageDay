using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs_Teleport : MonoBehaviour
{

    public Transform teleportDes;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTrigger2D(Collider2D teleport)
    {
        if (gameObject.tag == "teleport")
        {
            transform.position = teleportDes.position; 
        }
    }
}
