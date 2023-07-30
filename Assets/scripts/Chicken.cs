using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private new Transform transform;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private const float speed = 150;
    private string state = "readyToWalk";
    private float clock = 0;
    private float timeToRest;
    private float timeToWalk;
    private Vector2 direction = new Vector2();

    private float distanceToPlayer;
    private GameObject Player;

    void Start()
    {
        timeToRest = Random.Range(3, 10);
        timeToWalk = Random.Range(3, 10);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement() {
        //add to a clock time passed since last frame to avoid player with better frame rate to have faster chickens
        clock += Time.deltaTime;
        if (state == "resting") {
            ChickenRest();
        } else if (state == "readyToWalk") {
            ChickenReadyToWalk();
        } else if (state == "walking") {
            ChickenWalking();
        }
    }

    private void ChickenRest() {
        //set animation from walking to idle
        animator.SetBool("isWalking", false);
        if (clock < timeToRest && !playerClose())
            return ;
        //change chicken state if his time to rest is passed
        state = "readyToWalk";
    }

    private void ChickenReadyToWalk() {
        //generate new direction in X and Y axis, and reset clock
        direction.x = Random.Range(-1, 2);
        direction.y = Random.Range(-1, 2);
        clock = 0;
        //if direction.x == 0 and direction.y == 0, than chicken is still ready to walk
        if (direction != Vector2.zero) {
            state = "walking";
            animator.SetBool("isWalking", true);
        }
    }

    private void ChickenWalking() {

        //if player is near, chicken run away
        if (playerClose()) {
            direction.x = transform.position.x - Player.GetComponent<Transform>().position.x;
            direction.y = transform.position.y - Player.GetComponent<Transform>().position.y;
        }

        //if chicken approach the edge of the map, he goes backward
        if (transform.position.x < 2) {
            direction.x = transform.position.x - 0;
        } else if (transform.position.x > width - 2) {
            direction.x = transform.position.x - width;
        }
        if (transform.position.y < 2) {
            direction.y = transform.position.y - 0;
        } else if (transform.position.y > height - 2) {
            direction.y = transform.position.y - height;
        }

        //if chicken goes to the left or right, flip is sprite into the good direction
        if (direction.x < 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        //change chicken state if his time to walk is passed, else, make chicken walk
        if (clock > timeToWalk && !playerClose()) {
            state = "resting";
            clock = 0;
            rigidbody.velocity = Vector2.zero;
        } else {
            rigidbody.velocity = direction.normalized * Time.deltaTime * speed;
        }
    }

    private bool playerClose() {
        distanceToPlayer = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2));
        if (distanceToPlayer < 3) {
            return (true);
        } else {
            return (false);
        }
    }
}
