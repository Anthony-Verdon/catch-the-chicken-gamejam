using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private new Transform transform;

    private const float speed = 300;
    private bool notInIdleState;
    private Vector2 vectorMovement;
    private int ChickensGrabbed = 0;

    void Update()
    {
       UpdateMovement();
       UpdateLayer();
    }

    private void UpdateMovement() {
        //Get input for movement on axis X and Y
        vectorMovement.x = Input.GetAxisRaw("Horizontal");
        vectorMovement.y = Input.GetAxisRaw("Vertical");

        //adapt animation state by updating paramaters in the animator
        if (vectorMovement != Vector2.zero) {
            animator.SetFloat("X", vectorMovement.x);
            animator.SetFloat("Y", vectorMovement.y);
            notInIdleState = true;
        } else if (notInIdleState){
            float lastX = animator.GetFloat("X");
            float lastY = animator.GetFloat("Y");
            animator.SetFloat("X", lastX / 2);
            animator.SetFloat("Y", lastY / 2);
            notInIdleState = false;
        }

        //update movement speed
        //normalize : avoid acceleration if you walk diagonally
        //Time.deltaTime : avoid people who have a better frame rate to go faster
        rigidbody.velocity = vectorMovement.normalized * Time.deltaTime * speed;
    }

    private void UpdateLayer() {
        //probably a better of doing this
        if (transform.position.y < 7.5) {
            spriteRenderer.sortingOrder = 2;
        } else {
            spriteRenderer.sortingOrder = 1;
        }
    }

    public int GetChickensGrabbed() {
        return ChickensGrabbed;
    }

    public void SetChickensGrabbed(int nbChickens) {
        ChickensGrabbed = nbChickens;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (ChickensGrabbed < 5) {
            ChickensGrabbed += 1;
            Destroy(other.gameObject);
        }
    }
}
