using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.gameObject.name + " hit a " + this.gameObject.name);
        if (collider.gameObject.tag == "Player")
        {
            //Debug.Log(collider.gameObject.name + " hit a " + this.gameObject.name);
            player.Die();
        }
    }
}
