using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMove : MonoBehaviour
{
    public float moveSpeed = 200f;
    private Vector2 movement = new Vector2();
    private Rigidbody2D _r2d;
    // Start is called before the first frame update
    void Start()
    {
        _r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        _r2d.velocity = movement * moveSpeed;
    }
}
