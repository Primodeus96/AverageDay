using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float MovementSpeed;
	
	public Vector2 LastMovement;
	
	private Animator Anim;
	private bool PlayerMovement;
	private Rigidbody2D PlayerRigidbody;
	
	


	// Use this for initialization
	void Start()
	{
		Anim = GetComponent<Animator>();
		PlayerRigidbody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		PlayerMovement = false;
        { 
			if (Input.GetAxisRaw("Horizontal") > 0.1f || Input.GetAxisRaw("Horizontal") < -0.1f)
			{
				PlayerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * MovementSpeed, PlayerRigidbody.velocity.y);
				PlayerMovement = true;
				LastMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);


			}

			if (Input.GetAxisRaw("Vertical") > 0.1f || Input.GetAxisRaw("Vertical") < -0.1f)
			{
				PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * MovementSpeed);
				PlayerMovement = true;
				LastMovement = new Vector2(0f, Input.GetAxisRaw("Vertical"));
			}



			if (Input.GetAxisRaw("Horizontal") < 0.1f && Input.GetAxisRaw("Horizontal") > -0.1f)
			{
				PlayerRigidbody.velocity = new Vector2(0f, PlayerRigidbody.velocity.y);
			}

			if (Input.GetAxisRaw("Vertical") < 0.1f && Input.GetAxisRaw("Vertical") > -0.1f)
			{
				PlayerRigidbody.velocity = new Vector2(PlayerRigidbody.velocity.x, 0f);
			}

			if (Input.GetKeyDown(KeyCode.Escape))
			{
				SceneManager.LoadScene("Menu");
			}
		}

		//Anim.SetFloat("Move X", Input.GetAxisRaw("Horizontal"));
		//Anim.SetFloat("Move Y", Input.GetAxisRaw("Vertical"));
		//Anim.SetBool("Player Moving", PlayerMovement);
		//Anim.SetFloat("Last Move X", LastMovement.x);
		//Anim.SetFloat("Last Move Y", LastMovement.y);
	}
}