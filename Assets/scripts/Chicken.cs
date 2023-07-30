using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private new Transform transform;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private string state = "readyToWalk";
    private float clock = 0;
    private float timeToRest;
    private float timeToWalk;
    private Vector2 direction = new Vector2();
    private float distanceToPlayerSquared;
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
        if ((transform.position.x < 2) || (transform.position.x > Globals.MAP_WIDTH - 2)) {
            direction.x = Globals.MAP_WIDTH / 2 - transform.position.x;
        }
        if ((transform.position.y < 2) || (transform.position.y > Globals.MAP_HEIGHT - 2)) {
            direction.y = Globals.MAP_HEIGHT / 2 - transform.position.y;
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
            rigidbody.velocity = direction.normalized * Time.deltaTime * Globals.CHICKEN_SPEED;
        }
    }

    //compare distanceSquared is more efficient
    private bool playerClose() {
        if (Player == null) {
            return (false);
        }
        distanceToPlayerSquared = Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2);
        //9 = Math.Pow(3, 2)
        if (distanceToPlayerSquared < 9) {
            return (true);
        } else {
            return (false);
        }
    }
}
