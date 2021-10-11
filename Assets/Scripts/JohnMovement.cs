/* aldov500 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class JohnMovement : MonoBehaviour{

    public GameObject   BulletPrefab;
    private Rigidbody2D RigidBody;
    private Animator    Animator;

    private float   horizontal;
    private bool    grounded;
    private bool    flyMode;

    private string jumpAnimation;
    private string runAnimation;
    private string dashAnimation;
    private string crunchAnimation;

    public float jumpForce;
    public float speed;
    public float movementSpeed;

    private bool movLeft;
    private bool movRight;
    private bool movUp;
    private bool movDown;
    
    void Start(){
        RigidBody       = GetComponent<Rigidbody2D>();
        Animator        = GetComponent<Animator>();
        movementSpeed   = 1000.0f;

        jumpAnimation   = "jump";
        runAnimation    = "running";
        dashAnimation   = "dash";
        crunchAnimation = "crunch";

    }

    void Update(){
        // Update is called once per frame on game
        ControlPlayer();
    }


    private void FixedUpdate(){
        // Update for Physics repeats 60 times (max frames) per second
        RigidBody.velocity = new Vector2(horizontal * speed, RigidBody.velocity.y);
    }

    private void ControlPlayer(){
        
        AnimatorCleanFlags();

        horizontal = Input.GetAxisRaw("Horizontal");
        
        if(PlayerIsRunning(horizontal)){
            Animator.SetBool("running",true);
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
            Animator.SetBool(crunchAnimation, true);
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

    private bool PlayerIsRunning(float direction){
        return direction != 0.0f;
    }
    private bool PlayerIsGrounded(){
        // Check if player is grounded
        //Debug.DrawRay(transform.position, Vector3.down*0.1f, Color.red);
        return Physics2D.Raycast(transform.position, Vector3.down, 0.1f);
    }

    private void AnimatorCleanFlags(){
        Animator.SetBool(jumpAnimation, false);
        Animator.SetBool(runAnimation, false);
        Animator.SetBool(dashAnimation, false);
        Animator.SetBool(crunchAnimation, false);
    }
    /*  Player actions */
    private void Jump(){
        Debug.Log("Jump");
        Animator.SetBool("jump", true);
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
        RigidBody.AddForce(Vector3.right * jumpForce * 10);
    }

    private void Shoot(){
         Debug.Log("Shot");
    }

}
