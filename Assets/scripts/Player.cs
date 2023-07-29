using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;

    private const float speed = 300;
    private Vector2 vectorMovement;
    private string currentState = "playerIdleFront";

    void Update()
    {
        vectorMovement.x = Input.GetAxisRaw("Horizontal");
        vectorMovement.y = Input.GetAxisRaw("Vertical");

        //d = x:1, a = x:-1, w = y:1, s = y:-1
        //Debug.Log("x:" + vectorMovement.x + "y:" + vectorMovement.y);
        if (vectorMovement.x == 0 && vectorMovement.y == 0) {
            if (currentState == "playerRunFront") {
                ChangeAnimationState("playerIdleFront");
            } else if (currentState == "playerRunBack") {
                ChangeAnimationState("playerIdleBack");
            } else if (currentState == "playerRunLeft") {
                ChangeAnimationState("playerIdleLeft");
            } else if (currentState == "playerRunRight") {
                ChangeAnimationState("playerIdleRight");
            } 
        } else {
            if (vectorMovement.x < 0) {
                ChangeAnimationState("playerRunLeft");
            } else if (vectorMovement.x != 0) {
                ChangeAnimationState("playerRunRight");
            }
            if (vectorMovement.y < 0) {
                ChangeAnimationState("playerRunFront");
            } else if (vectorMovement.y != 0) {
                ChangeAnimationState("playerRunBack");
            }
        }
        /*
        normalize : avoid acceleration if you walk diagonally
        Time.deltaTime : avoid people who have a better frame rate to go faster
        */
        rigidbody.velocity = vectorMovement.normalized * Time.deltaTime * speed;
    }

    void ChangeAnimationState(string newState) {
        if (currentState ==  newState) {
            return ;
        } else {
            animator.Play(newState);
            currentState = newState;
        }
    }
}
