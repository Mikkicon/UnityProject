using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOrc : MonoBehaviour
{
    public float speed = 1;
    Rigidbody2D myBody = null;
    private Vector3 pointA;
    public Vector3 pointB;
    public float Speed = 0.03f;
    public float Timeout = 1f;
    public bool going_to_a;
    private Vector3 my_pos;
    private float value;
    private Vector3 rabit_pos;
    private float _timeToWait;
    Animator animator;
    SpriteRenderer sr; 


    public enum Mode
    {
        GoToA,
        GoToB,
        GoToRabit,
        Attack //...
    }
    Mode mode = Mode.GoToB;





    private void Start()
    {
        pointA = this.transform.position;
        myBody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>(); 


    }





    float getDirection()
    {
        my_pos = this.transform.position;
        if (mode == Mode.GoToA)
        {

            return 1;

        }
        else if (mode == Mode.GoToB)
        {
            return -1;
        }

        return 0; //No movement 
    }






    private void Update()
    {

        value = this.getDirection();
        my_pos = this.transform.position;

       
            Vector2 vel = myBody.velocity;
            animator.SetBool("Walk", true);
            //print(animator.GetBool("Walk"));
            vel.x = value * speed;
            myBody.velocity = vel;
        


        if (mode == Mode.GoToA){
            //print("mode == Mode.GoToA");
            if (isArrived(pointA)){
                //print("isArrived(pointA) == true");

                mode = Mode.GoToB;
                my_pos = Vector3.MoveTowards(my_pos, pointB, Speed);

            }

            my_pos = Vector3.MoveTowards(my_pos, pointA, Speed);

        }else if (mode == Mode.GoToB){
            //print("mode == Mode.GoToB");
            if (isArrived(pointB)){
                //print("isArrived(pointB) == true");
                mode = Mode.GoToA;
            }
            my_pos = Vector3.MoveTowards(my_pos, pointB, Speed);
        }


        transform.position = my_pos;
        sr = GetComponent<SpriteRenderer>(); 
        if (value < 0){
            sr.flipX = true;
        }else if (value > 0){
            sr.flipX = false;
        }

        rabit_pos = HeroRabit.lastRabit.transform.position;
        if (HeroRabit.lastRabit.transform.position.x > Mathf.Min(pointA.x, pointB.x)
            && HeroRabit.lastRabit.transform.position.x < Mathf.Max(pointA.x, pointB.x)){
            Rabitreachable();
        }else if (mode == Mode.GoToRabit|| mode == Mode.Attack){
            mode = Mode.GoToA;
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);

        }


    }






    private void Rabitreachable()
    {
        print("entered rabitreachable");
        mode = Mode.GoToRabit;
        //my_pos = Vector3.MoveTowards(my_pos, rabit_pos, Speed);

        if(Mathf.Abs(my_pos.x - rabit_pos.x) > 3f) {
            print("Mathf.Abs(my_pos.x - rabit_pos.x) < 0.2f"+(Mathf.Abs(my_pos.x - rabit_pos.x) < 0.2f));

            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);

            sr = GetComponent<SpriteRenderer>(); 
            if (my_pos.x - rabit_pos.x> 0)
            {
                sr.flipX = false;
            }
            else if(my_pos.x - rabit_pos.x < 0)
            {
                sr.flipX = true;
            }
            my_pos = Vector3.MoveTowards(my_pos, rabit_pos, Speed);

        }else if(Mathf.Abs(my_pos.x - rabit_pos.x) < 3f){
            print("Mathf.Abs(my_pos.x - rabit_pos.x) < 3f" + (Mathf.Abs(my_pos.x - rabit_pos.x) < 3f));
            mode = Mode.Attack;
            animator.SetBool("Run", false);
            animator.SetBool("Attack", true);
            animator.SetTrigger("Attack");

        }
        transform.position = my_pos;

    }




    private bool isArrived(Vector3 dest)
    {
        return Mathf.Abs(dest.x - transform.position.x) < 0.5f;
    }

}
