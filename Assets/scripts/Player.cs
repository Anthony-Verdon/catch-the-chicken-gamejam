using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    private Vector2 vectorMovement;
    private const float speed = 300;

    void Update()
    {
        vectorMovement.x = Input.GetAxisRaw("Horizontal");
        vectorMovement.y = Input.GetAxisRaw("Vertical");
        rigidbody.velocity = vectorMovement.normalized * Time.deltaTime * speed;
    }
}
