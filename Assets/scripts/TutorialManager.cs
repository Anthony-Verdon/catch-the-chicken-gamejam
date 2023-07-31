using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Text TutorialTips;
    [SerializeField] private Button Play;
    [SerializeField] private GameObject Player, Chicken, Fox, ChickenCoop;

    private string tutorialStep = "start";
    private int movementMade = 0;
    private Vector3 PlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        Play.gameObject.SetActive(false);
        PlayerPosition = Player.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        changeStep();
        UpdateText();
    }

    private void changeStep() {
        if (tutorialStep == "start" && Input.GetKeyDown("space")) {
            tutorialStep = "learnToMove";
        } else if (tutorialStep == "learnToMove") {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {
                movementMade++;
            }
            if (movementMade != 0 && Input.GetKeyDown("space")) {
                tutorialStep = "catchChickens";
                Instantiate(Chicken, new Vector3(PlayerPosition.x + 2, PlayerPosition.y, 0), Quaternion.identity);
            }
        }  else if (tutorialStep == "catchChickens" && Player.GetComponent<Player>().GetChickensCaught() != 0 && Input.GetKeyDown("space")) {
            tutorialStep = "saveChicken";
        } else if (tutorialStep == "saveChicken" && ChickenCoop.GetComponent<ChickenCoop>().GetChickensSaved() != 0 && Input.GetKeyDown("space")) {
            tutorialStep = "learnAboutTheFox";
            Instantiate(Fox, new Vector3(PlayerPosition.x + 2, PlayerPosition.y, 0), Quaternion.identity);
        } else if (tutorialStep == "learnAboutTheFox" &&  Input.GetKeyDown("space")) {
            tutorialStep = "rules";
        } else if (tutorialStep == "rules" &&  Input.GetKeyDown("space")) {
            tutorialStep = "end";
        } else if (tutorialStep == "end") {
            Play.gameObject.SetActive(true);
        }
    }

    private void UpdateText() {
        if (tutorialStep == "start") {
            TutorialTips.text = "Welcome in [title] game tutorial !\nYou will learn everything you need to play.\nTo start the tutorial, press spacebar.";
        } else if (tutorialStep == "learnToMove") {
            TutorialTips.text = "In this game, you can move with key arrows !\nPress spacebar to continue after this.";
        } else if (tutorialStep == "catchChickens") {
            TutorialTips.text = "Oh no ! Your chickens escaped !\nYour goal is to catch a maximum of chickens. Run on them to catch them.\nPress spacebar to continue after this.";
        } else if (tutorialStep == "saveChicken") {
            TutorialTips.text = "It's good to catch them, but it's better to place them in a safe area.\nPress E near the ChickenCoop to save them.\nPress spacebar to continue after this.";
        } else if (tutorialStep == "learnAboutTheFox") {
            TutorialTips.text = "The reason they escaped is because a fox attack them.\nCatch your chickens before the fox ate them !\nPress spacebar to continue after this.";
        } else if (tutorialStep == "rules") {
            TutorialTips.text = "Two rules are : you can carry a maximum of fives chickens at the same time.\nAfter, you must place them into the ChickenCoop to catch some more.\n. The second rule is you have only 60 seconds to save them all !\nGood luck ! Press spacebar to continue after this.";
        } else if (tutorialStep == "end") {
            TutorialTips.text = "Click on the button to start the game, or return to the menu.";
        }
    }
}
