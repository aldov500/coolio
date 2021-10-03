using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Vector2 Direction;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rigidbody.velocity = Vector2.right * speed;
    }

    public void setDirection(Vector2 direction){
        Direction = direction;
    }
}
