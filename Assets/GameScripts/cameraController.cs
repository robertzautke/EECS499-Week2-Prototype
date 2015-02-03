using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public GameObject Player;

	void Start() {
		Screen.orientation = ScreenOrientation.Landscape;
	}

	public static cameraController Instance
	{
		get { return instance; }
	}
	private static cameraController instance = null;

	void Awake() {
     if (instance != null && instance != this) {
         Destroy(this.gameObject);
         return;
     } else {
         instance = this;
     }
     DontDestroyOnLoad(this.gameObject);
	}

	public Vector3 cameraPosition = new Vector3(0,0,-1);
	void Update () {

		if(this.GetComponent<AudioSource>().loop == false)
		{ 
			this.GetComponent<AudioSource>().loop = true;
		}

		if(Player != null)
		{ 
			cameraPosition.x = Player.transform.position.x;
			cameraPosition.y = Player.transform.position.y;
			this.gameObject.transform.position = cameraPosition;
		}
		else
		{ 
			Player = GameObject.Find("Box(Sprite)Player");

		}
	}
}
