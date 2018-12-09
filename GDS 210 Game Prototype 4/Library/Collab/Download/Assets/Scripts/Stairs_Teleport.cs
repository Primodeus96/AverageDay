using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs_Teleport : MonoBehaviour
{

    public Transform teleportDes;
    public GameObject player;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D teleport)
    {
        if (gameObject.tag == "teleport")
        {
            player.transform.position = teleportDes.position; 
        }
    }
}
