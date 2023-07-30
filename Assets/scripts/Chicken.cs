using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private new Transform transform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private const float speed = 150;
    private string state = "readyToWalk";
    private float clock = 0;
    private float timeToRest;
    private float timeToWalk;
    private Vector2 direction = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        timeToRest = Random.Range(3, 10);
        timeToWalk = Random.Range(3, 10);
    }

    // Update is called once per frame
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
        if (clock < timeToRest)
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
        //if chicken approach the edge of the map, he goes backward
        if ((transform.position.x < 2 && direction.x < 0)
            || (transform.position.x > width - 2 && direction.x > 0)) {
            direction.x = -direction.x;
        }
        if ((transform.position.y < 2 && direction.y < 0)
            || (transform.position.y > height - 2 && direction.y > 0)) {
            direction.y = -direction.y;
        }

        //if chicken goes to the left or right, flip is sprite into the good direction
        if (direction.x < 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        //change chicken state if his time to walk is passed, else, make chicken walk
        if (clock > timeToWalk) {
            state = "resting";
            clock = 0;
            rigidbody.velocity = Vector2.zero;
        } else {
            rigidbody.velocity = direction.normalized * Time.deltaTime * speed;
        }
    }
}
