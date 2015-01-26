using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

	public GameObject currentLevelController;
    public int health = 6;
	//Vector3 playerPosition;

    public Sprite face_smile;
    public Sprite face_OK;
    public Sprite face_frown;

	public Sprite face_smile_Stars;
	public Sprite face_OK_Stars;
	public Sprite face_frown_Stars;

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

	private int starTime;

	void Update () 
	{

		//playerPosition = this.gameObject.transform.position;

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

/////////////////////// Character Face Handler ///////////////////////////////

		if (this.rigidbody2D.angularVelocity > 2000)
		{
			starTime = 100;
		}
		if(starTime > 0)
		{ 
			starTime--;
		}
		if(this.GetComponent<SpriteRenderer>().sprite == face_smile && starTime > 0)
		{ 
			this.GetComponent<SpriteRenderer>().sprite = face_smile_Stars;
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_smile_Stars && starTime <= 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_smile;
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_OK && starTime > 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_OK_Stars;
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_OK_Stars && starTime <= 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_OK;
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_frown && starTime > 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_frown_Stars;
		}
		else if (this.GetComponent<SpriteRenderer>().sprite == face_frown_Stars && starTime <= 0)
		{
			this.GetComponent<SpriteRenderer>().sprite = face_frown;
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

        else if (other.gameObject.tag == "Enemy")
        {
            health--;

            if (health <= 4)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = face_OK;
            }

            if (health <= 2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = face_frown;
            }

            if (health <= 0)
            {
                health = 6;
                Application.LoadLevel(currentLevelController.GetComponent<currentLevelController>().currentApplicationLevelName);
            }
        }
	}
}
