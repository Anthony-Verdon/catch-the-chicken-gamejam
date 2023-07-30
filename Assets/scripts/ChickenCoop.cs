using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenCoop : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private new Transform transform;
    [SerializeField] private Text ChickensSavedText;
    [SerializeField] private Text GameFinishText;

    private float distanceToPlayer;
    private int chickensSaved = 0;
    private GameObject Player;

    void Start()
    {
        Vector3 newPosition = new Vector3(width / 2, height / 2, 0);
        transform.position = newPosition;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (Player.GetComponent<Player>().GetisGameFinish()) {
            ChickensSavedText.text = "";
            GameFinishText.text = "You saved " + chickensSaved.ToString() + " chickens !";
        }
    }

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0) && playerClose()){
            chickensSaved += Player.GetComponent<Player>().GetChickensCaught();
            ChickensSavedText.text = "Chickens saved: " + chickensSaved.ToString();
            Player.GetComponent<Player>().SetChickensCaught(0);
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
