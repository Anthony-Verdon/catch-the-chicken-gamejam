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
        Debug.Log(animator.GetCurrentAnimatorStateInfo(1));
        if (vectorMovement != Vector2.zero) {
            animator.SetFloat("X", vectorMovement.x);
            animator.SetFloat("Y", vectorMovement.y);
        } else {        
                if (currentState == "playerRunFront") {
                    ChangeAnimationState("playerIdleFront");
                } else if (currentState == "playerRunBack") {
                    ChangeAnimationState("playerIdleBack");
                } else if (currentState == "playerRunLeft") {
                    ChangeAnimationState("playerIdleLeft");
                } else if (currentState == "playerRunRight") {
                    ChangeAnimationState("playerIdleRight");
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
