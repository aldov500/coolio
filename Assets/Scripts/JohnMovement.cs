using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    private Rigidbody2D RigidBody;
    private Animator Animator;

    private float horizontal;
    private bool grounded;
    private bool movementXY;

    public float jumpForce;
    public float speed;
    public float movementSpeed;
    void Start()
    {
      RigidBody = GetComponent<Rigidbody2D>();
      Animator  = GetComponent<Animator>();
      grounded  = false;
      movementXY = false;
      movementSpeed = 1000.0f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal < 0.0f){
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        } else if(horizontal > 0.0f){
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Animator.SetBool("running",horizontal != 0.0f);
        Debug.DrawRay(transform.position, Vector3.down*0.1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f)){
            grounded = true;
            movementXY =false;
        } else {
            grounded = false;
        }

        if(movementXY == true){
            Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            MoveXY(targetVelocity);
        }

        if(Input.GetKeyDown(KeyCode.W) && grounded){
            Jump();
        }        

        if(Input.GetKeyDown(KeyCode.Q) && grounded == false){
            movementXY = true;
        }  

        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

     // Update for Physics
    private void FixedUpdate(){
        RigidBody.velocity = new Vector2(horizontal * speed, RigidBody.velocity.y);
    }

    void MoveXY(Vector2 targetVelocity){        
        RigidBody.velocity = (targetVelocity * movementSpeed) * Time.deltaTime; // Multiply the target by deltaTime to make movement speed consistent across different framerates
    }

    /*  Player actions */
    private void Jump(){
       RigidBody.AddForce(Vector2.up * jumpForce);
    }

    private void Melee(){

    }

    private void Fly(){

    }

    private void Crunch(){

    }

    private void Dash(){

    }

    private void Shoot(){
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
    }

   
}
