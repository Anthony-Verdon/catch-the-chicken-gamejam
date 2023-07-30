using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private new Transform transform;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private const float speed = 225;
    private string state = "readyToRun";
    private float clock = 0;
    private float timeToRest;
    private float timeToWalk;
    private Vector2 direction = new Vector2();
    private int chickensAte = 0;

    private float distanceToPlayerSquared;
    private GameObject Player;
    private Transform NearestChicken;

    void Start()
    {
        timeToRest = Random.Range(1, 5);
        timeToWalk = Random.Range(5, 10);
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement() {
        //add to a clock time passed since last frame to avoid player with better frame rate to have faster fox
        clock += Time.deltaTime;
        if (state == "resting") {
            FoxRest();
        } else if (state == "readyToRun") {
            FoxReadyToRun();
        } else if (state == "running") {
            FoxRunning();
        }
    }

    private void FoxRest() {
        //set animation from walking to idle
        animator.SetBool("isRunning", false);
        if (clock < timeToRest && !playerClose())
            return ;
        //change fox state if his time to rest is passed
        state = "readyToRun";
    }

    private void FoxReadyToRun() {
        //run after the nearest chicken
        GameObject[] Chickens = GameObject.FindGameObjectsWithTag("Chicken");
        NearestChicken = findNearestChicken(convertGameObjectToTransform(Chickens));
        clock = 0;
        direction.x = Random.Range(-1, 2);
        direction.y = Random.Range(-1, 2);
        if (direction != Vector2.zero) {
            state = "running";
            animator.SetBool("isRunning", true);
        }
    }

    private void FoxRunning() {

        //if player is near, fox run away
        if (playerClose()) {
            direction.x = transform.position.x - Player.GetComponent<Transform>().position.x;
            direction.y = transform.position.y - Player.GetComponent<Transform>().position.y;
        } else if (NearestChicken){
            direction.x = NearestChicken.position.x - transform.position.x;
            direction.y = NearestChicken.position.y - transform.position.y;
        }

        //if fox approach the edge of the map, he goes backward
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

        //if fox goes to the left or right, flip is sprite into the good direction
        if (direction.x < 0) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }

        //change fox state if his time to walk is passed, else, make fox walk
        if (clock > timeToWalk && !playerClose()) {
            state = "resting";
            clock = 0;
            rigidbody.velocity = Vector2.zero;
        } else {
            rigidbody.velocity = direction.normalized * Time.deltaTime * speed;
        }
    }

    //compare distanceSquared is more efficient
    private bool playerClose() {
        distanceToPlayerSquared = Mathf.Pow(transform.position.x - Player.transform.position.x, 2) + Mathf.Pow(transform.position.y - Player.transform.position.y, 2);
        //9 = Math.Pow(3, 2)
        if (distanceToPlayerSquared < 9) {
            return (true);
        } else {
            return (false);
        }
    }

    private Transform findNearestChicken(Transform[] chickens) {
        Transform NearestChicken = null;
        float minDist = Mathf.Infinity;
        foreach (Transform T in chickens)
        {
            float dist = Mathf.Pow(T.position.x -transform.position.x, 2) + Mathf.Pow(T.position.y - transform.position.y, 2);
            if (dist < minDist)
            {
                NearestChicken = T;
                minDist = dist;
            }
        }
        return NearestChicken;
    }

    private Transform[] convertGameObjectToTransform(GameObject[] GameObjectArray) {
        int lenArray = GameObjectArray.Length;
        Transform[] TransformArray = new Transform[lenArray];
        for(int i = 0; i < lenArray; i++ ){
            TransformArray[i] = GameObjectArray[i].GetComponent<Transform>();
        }
        return TransformArray;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Chicken(Clone)") {
            chickensAte += 1;
            Destroy(other.gameObject);
        }
    }

    public int GetChickensAte() {
        return chickensAte;
    }
}
