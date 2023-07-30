using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private int width, height;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private new Transform transform;
    [SerializeField] private Text ChickensCaughtText;
    [SerializeField] private Text TimeLeftText;
    [SerializeField] private int gameTime;
    

    private const float speed = 300;
    private bool notInIdleState;
    private Vector2 vectorMovement;
    private int ChickensCaught = 0;
    private float clock = 0;
    private bool isGameFinish = false;

    void Start() {
        Vector3 newPosition = new Vector3(width / 2, height / 2 - 2, 0);
        transform.position = newPosition;
    }

    void Update()
    {
        if (clock < gameTime) {
            UpdateMovement();
            UpdateLayer();
            clock += Time.deltaTime;
            int timeLeft =  gameTime - (int)clock;
            TimeLeftText.text = "Time left: " + timeLeft.ToString();
            ChickensCaughtText.text = "Chickens caught: " + ChickensCaught.ToString();
        } else {
            isGameFinish = true;
            TimeLeftText.text = "";
            ChickensCaughtText.text = "";
        }

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
            spriteRenderer.sortingOrder = 3;
        } else {
            spriteRenderer.sortingOrder = 1;
        }
    }

    public int GetChickensCaught() {
        return ChickensCaught;
    }

    public bool GetisGameFinish() {
        return isGameFinish;
    }

    public Vector2 GetDirection() {
        return vectorMovement;
    }

    public void SetChickensCaught(int nbChickens) {
        ChickensCaught = nbChickens;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (ChickensCaught < 5) {
            ChickensCaught += 1;
            Destroy(other.gameObject);
        }
    }
}
