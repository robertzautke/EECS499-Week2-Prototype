using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	public GameObject Player;

	public Vector3 cameraPosition = new Vector3(0,0,-1);
	void Update () {
		cameraPosition.x = Player.transform.position.x;
		cameraPosition.y = Player.transform.position.y;
		this.gameObject.transform.position = cameraPosition;
	}
}
