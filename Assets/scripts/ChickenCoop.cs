using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenCoop : MonoBehaviour
{
    [SerializeField] private new Transform transform;
    [SerializeField] private Text ChickensSavedText;
    [SerializeField] private Text GameFinishText;
    [SerializeField] private GameObject Player;
    [SerializeField] private Button Menu, Play;

    private float distanceToPlayer;
    private int chickensSaved = 0;

    void Start()
    {
        Vector3 newPosition = new Vector3(Globals.MAP_WIDTH / 2, Globals.MAP_HEIGHT / 2, 0);
        transform.position = newPosition;
        Menu.gameObject.SetActive(false);
        Play.gameObject.SetActive(false);
    }

    void Update() {
        GameFinish();
    }

    private void GameFinish() {
        float timeLeft = Player.GetComponent<Player>().GetTimeLeft();
        if (timeLeft <= 0) {
            ChickensSavedText.text = "";
            GameFinishText.text = "You saved " + chickensSaved.ToString() + " chickens !\nThe fox ate "+ addAllChickensAte().ToString() + " chickens !";
            Menu.gameObject.SetActive(true);
            Play.gameObject.SetActive(true);
        } else if (timeLeft == 3) {
            GameFinishText.text = "                3...";
        } else if (timeLeft == 2) {
            GameFinishText.text = "                2..";
        } else if (timeLeft == 1) {
            GameFinishText.text = "                1.";
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

    private int addAllChickensAte() {
        int chickensAte = 0;
        GameObject[] Foxes = GameObject.FindGameObjectsWithTag("Fox");
        foreach (GameObject Fox in Foxes) {
            chickensAte += Fox.GetComponent<Fox>().GetChickensAte();
        }
        return chickensAte;
    }

}
