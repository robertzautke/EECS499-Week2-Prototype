using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

	public GameObject currentLevelController;
	
	Vector3 playerPosition;

	public float movementVelocity = 3f;
	public bool canJump = true;
	public int airTime = 0;
	public int temp_airTime = 10;

	public int force = 5;
	public float airTorque = 1f;
	public float groundTorque = 5f;

	public int airGravity = 2;
	public float groundGravity = 0.65f;

	public int temp_briefGravityTurnOn = 40;
	public int briefGravityTurnOn = 0;


	void Update () 
	{

		playerPosition = this.gameObject.transform.position;

/////////////// Movement Physics/Controls ////////////////////////////////////////////

		if(canJump)
		{
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;
		}

		if (Input.GetKey(KeyCode.W))
		{
			if (canJump) 
			{
				airTime = temp_airTime;
				if (airTime <= 0)
				{
					canJump = false;
					briefGravityTurnOn = temp_briefGravityTurnOn;
					airTime = 0;
				}
				else 
				{
					rigidbody2D.AddForce(new Vector2(0, force), ForceMode2D.Impulse);


					if (Input.GetKey(KeyCode.A))
					{
						rigidbody2D.AddTorque(airTorque);		
					}

					if (Input.GetKey(KeyCode.D))
					{
						rigidbody2D.AddTorque(-airTorque);
					}
					airTime--;
				}
			}

			if (Input.GetKey(KeyCode.A))
			{
				rigidbody2D.AddTorque(groundTorque);
				rigidbody2D.AddForce(new Vector2(-movementVelocity, 0), ForceMode2D.Force);

			}
			if (Input.GetKey(KeyCode.D))
			{
				rigidbody2D.AddTorque(-groundTorque);
				rigidbody2D.AddForce(new Vector2(+movementVelocity, 0), ForceMode2D.Force);

			}
		}
		else if (Input.GetKey(KeyCode.A))
		{
			rigidbody2D.AddForce(new Vector2(-movementVelocity, 0), ForceMode2D.Force);
		
		}
		else if (Input.GetKey(KeyCode.S))
		{
			rigidbody2D.AddForce(new Vector2(0, -movementVelocity), ForceMode2D.Force);

		}
		else if (Input.GetKey(KeyCode.D))
		{
			rigidbody2D.AddForce(new Vector2(+movementVelocity, 0), ForceMode2D.Force);

		}
		else 
		{
	
		}

		if (briefGravityTurnOn > 0)
		{
			briefGravityTurnOn--;
			this.GetComponent<Rigidbody2D>().gravityScale = airGravity;
		}
		else
		{
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;		
		}

		if(airTime > 0)
		{
			canJump = false;
			airTime--;
		}
		else if(airTime <= 0 && !canJump)
		{

			this.GetComponent<Rigidbody2D>().gravityScale = airGravity;
		}

//////////////////// Level Controls /////////////////////////////////////////

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{ 
			currentLevelController.GetComponent<currentLevelController>().nextLevel();
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentLevelController.GetComponent<currentLevelController>().previousLevel();		
		}
/////////////////////////////////////////////////////////////////////////////
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Floor")
		{
			canJump = true;
			this.GetComponent<Rigidbody2D>().gravityScale = groundGravity;
		}
	}
}
