using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 1;
	// Use this for initialization
	void Start () {
        speed = 3;
	}
	
	// Update is called once per frame
	void Update () {
        float value = Input.GetAxis("Horizontal");
        float value2 = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(value, value2) * Time.deltaTime * speed);
        SpriteRenderer sr = GetComponent<SpriteRenderer>(); if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
	}
}
