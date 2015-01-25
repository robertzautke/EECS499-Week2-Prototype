using UnityEngine;
using System.Collections;

public class endLevelHandler : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Application.LoadLevel("Level1");
        }
    }
}
