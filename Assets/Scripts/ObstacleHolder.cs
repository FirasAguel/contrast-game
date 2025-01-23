using UnityEngine;
using System.Collections;

public class ObstacleHolder : MonoBehaviour {

    public Manager manager;

    void Start()
    {
        manager = FindObjectOfType<Manager>().GetComponent<Manager>() ;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            manager.Score += 0.5f;
            manager.ScoreText.text = manager.Score.ToString();
        }
    }

}
