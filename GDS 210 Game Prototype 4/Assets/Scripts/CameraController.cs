using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject FollowTarget;
	public float CameraMoveSpeed;

	private Vector3 TargetPosition;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		TargetPosition = new Vector3(FollowTarget.transform.position.x, FollowTarget.transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, TargetPosition, CameraMoveSpeed * Time.deltaTime);
	}
}
