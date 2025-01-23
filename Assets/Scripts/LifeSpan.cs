using UnityEngine;
using System.Collections;

public class LifeSpan : MonoBehaviour {

    public float lifeSpan = 10;

    void Start ()
    {
        DestroyObject(this.gameObject, lifeSpan);
	}
}
