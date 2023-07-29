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

        Debug.Log("x:" + vectorMovement.x + "y:" + vectorMovement.y);
        if (vectorMovement.x < 0) {
        } else {

        }
        if (vectorMovement.y < 0) {
        } else {
            
        }
        /*
        normalize : avoid acceleration if you walk diagonally
        Time.deltaTime : avoid people who have a better frame rate to go faster
        */
        rigidbody.velocity = vectorMovement.normalized * Time.deltaTime * speed;
    }
}
