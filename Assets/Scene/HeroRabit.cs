using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    public float speed = 3;
    Rigidbody2D myBody = null;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;
    Transform heroParent = null;
    public static HeroRabit lastRabit = null;
    void Awake()
    {
        lastRabit = this;
    }
    // Use this for initialization
    void Start()
    {      
        float diff = Time.deltaTime;
        myBody = this.GetComponent<Rigidbody2D>();
        //print(transform.position);
        LevelController.current.setStartPosition(transform.position);
        //class LevelController
        this.heroParent = this.transform.parent;
        this.heroParent = null;

    }

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться 
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float value = Input.GetAxis("Horizontal");
        //float value2 = Input.GetAxis("Vertical");
        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            animator.SetBool("Run", true);
            vel.x = value * speed;
            //vel.y = value2 * 10;
            myBody.velocity = vel;
        }
        else
        {
            animator.SetBool("Run", false);
            //animator.SetBool("IdleMoving", true);
        }
        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
            //print("isGrounded");
        }
        else
        {
            isGrounded = false;
            //print("is not Grounded");

        }

            //Перевіряємо чи ми опинились на платформі 
            if(hit.transform != null&& hit.transform.GetComponent<MovingPlatform>() != null){ //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }

        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
        }

        Debug.DrawLine(from, to, Color.red);
        //print(Input.GetButtonDown("Jump"));
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
            //print(" JumpActive");

        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають 
            //print("Input.GetButton('Jump')"+Input.GetButton("Jump"));
            if (Input.GetButton("Jump")){
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime){
                    //print(" JumpActive \n DAROVA ");
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime); 
                    myBody.velocity = vel;
                }
            }else
            {
                this.JumpActive = false;
                this.JumpTime = 0;

            }
                
        }
        //print("Jump" + animator.GetBool("Jump"));

            if (this.isGrounded)
            {
                animator.SetBool("Jump", false);
            //print("Jump on ground "+animator.GetBool("Jump"));
            }
            else
            {
                animator.SetBool("Jump", true);
            //print("Jump not on ground" + animator.GetBool("Jump"));

            }

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
