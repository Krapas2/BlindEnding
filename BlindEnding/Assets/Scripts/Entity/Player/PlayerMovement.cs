using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Parameters")]
    public float speed;
    public float acceleration;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        Move(moveInput);
    }

    void Move(Vector2 direction)
    {
        Vector2 desiredVelocity = direction * speed;

        rb.velocity = Vector2.Lerp(desiredVelocity, rb.velocity, Mathf.Pow(.5f, acceleration * Time.deltaTime));
    }
}
