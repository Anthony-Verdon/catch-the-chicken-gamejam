using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;

    private const float speed = 300;
    private Vector2 vectorMovement;

    void Update()
    {
        vectorMovement.x = Input.GetAxisRaw("Horizontal");
        vectorMovement.y = Input.GetAxisRaw("Vertical");
        if (vectorMovement != Vector2.zero) {
            animator.SetFloat("X", vectorMovement.x);
            animator.SetFloat("Y", vectorMovement.y);
        } else {
            float lastX = animator.GetFloat("X");
            float lastY = animator.GetFloat("Y");
            animator.SetFloat("X", lastX / 2);
            animator.SetFloat("Y", lastY / 2);
        }
        /*
        normalize : avoid acceleration if you walk diagonally
        Time.deltaTime : avoid people who have a better frame rate to go faster
        */
        rigidbody.velocity = vectorMovement.normalized * Time.deltaTime * speed;
    }
}
