using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour {
    public float Speed;
    public float LifeTime;
    public bool Direction;
    private Vector3 pos;

    private void Start()
    {
        StartCoroutine(DestroyLater());
        GetComponent<SpriteRenderer>().flipX = Direction;
        //pos =
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(Speed * (Direction ? 1 : -1), 0, 0);
    }

    private IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HeroRabit>() != null)
        {
            other.GetComponent<HeroRabit>().HitRabbit();
            Destroy(gameObject);
        }
    }
    public void launch(float direction){
        float last_carrot = 0;
        //fix the time of last launch
        last_carrot = Time.time;
        //check launch time
        if (Time.time - last_carrot > 2.0f)
        { //Launch carrot
            //pos = Vector3.MoveTowards(pos,new Vector3(pos.x + direction,0,-2), Speed);
            FixedUpdate();
        }
    }
}
