using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    private Rigidbody2D RigidBody;
    private Animator Animator;
    private float horizontal;
    public float jumpForce;
    public float speed;
    private bool grounded;

    void Start()
    {
      RigidBody = GetComponent<Rigidbody2D>();
      Animator = GetComponent<Animator>();
      grounded = false;
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
        } else {
            grounded = false;
        }

        if(Input.GetKeyDown(KeyCode.W) && grounded){
            Jump();
            jumpForce +=10;
        }        

        if(Input.GetKeyDown(KeyCode.Space)){
            Shoot();
        }
    }

    private void Jump(){
       RigidBody.AddForce(Vector2.up * jumpForce);
    }

    private void Shoot(){
        GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
        //bullet.GetComponent<bullet>
    }

    private void FixedUpdate(){
        RigidBody.velocity = new Vector2(horizontal * speed, RigidBody.velocity.y);
    }
}
