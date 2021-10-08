using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class JohnMovement : MonoBehaviour
{
    public GameObject   BulletPrefab;
    private Rigidbody2D RigidBody;
    private Animator    Animator;

    private float   horizontal;
    private bool    grounded;
    private bool    flyMode;

    public float jumpForce;
    public float speed;
    public float movementSpeed;
    
    void Start()
    {
      RigidBody     = GetComponent<Rigidbody2D>();
      Animator      = GetComponent<Animator>();
      grounded      = false;
      flyMode       = false;
      movementSpeed = 1000.0f;
    }

    
    void Update()
    {
        // Update is called once per frame on game
        horizontal = Input.GetAxisRaw("Horizontal");
            
        if(PlayerIsRunning(horizontal)){
            Animator.SetBool("running",true);
        } else {
            Animator.SetBool("running",false);
        }
       
        if(PlayerIsGrounded()){
            grounded = true;
            flyMode =false;
        } else {
            grounded = false;
        }
        
        if(Input.GetKeyDown(KeyCode.Q) && Input.GetKeyDown(KeyCode.E) && grounded == false){
            flyMode = !flyMode;
        }
        
        if(flyMode == true){
            Fly(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }

        if(Input.GetKeyDown(KeyCode.A)){
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        if(Input.GetKeyDown(KeyCode.W)){
            if(grounded){
                Jump();
            }
        }

        if(Input.GetKeyDown(KeyCode.D)){
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if(Input.GetKeyDown(KeyCode.S)){
            Crunch();
            if(Input.GetKeyDown(KeyCode.A)){
                Dash();
               
            } else if(Input.GetKeyDown(KeyCode.D)){
                Dash();

            }
        }

        if(Input.GetKeyDown(KeyCode.F)){
            Melee();
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }


    }

    private void FixedUpdate(){
        // Update for Physics repeats 60 times (max frames) per second
        RigidBody.velocity = new Vector2(horizontal * speed, RigidBody.velocity.y);
    }

    private bool PlayerIsRunning(float direction){
        return direction != 0.0f;
    }
    private bool PlayerIsGrounded(){
        // Check if player is grounded
        //Debug.DrawRay(transform.position, Vector3.down*0.1f, Color.red);
        return Physics2D.Raycast(transform.position, Vector3.down, 0.1f);
    }

    /*  Player actions */
    private void Jump(){
        Debug.Log("Jump");
       RigidBody.AddForce(Vector2.up * jumpForce);
    }

    private void Melee(){
         Debug.Log("Melee");
    }

    private void Fly(Vector2 targetVelocity){
         Debug.Log("Fly");
         // Multiply the target by deltaTime to make movement speed consistent across different framerates
         RigidBody.velocity = (targetVelocity * movementSpeed) * Time.deltaTime; 
    }

    private void Crunch(){
        Debug.Log("Crunch");
    }

    private void Dash(){
         Debug.Log("Dash");
          Animator.SetBool("dash",true);
    }

    private void Shoot(){
         Debug.Log("Shot");
    }

   
}
