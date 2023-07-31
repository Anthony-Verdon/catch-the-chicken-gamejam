using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        if (sceneName == "GameScene" && SceneManager.GetActiveScene().name != "GameScene") {
            FindObjectOfType<AudioManager>().Play("gameSound");
            FindObjectOfType<AudioManager>().Stop("menuAndTutorial");
        } else if (sceneName != "GameScene" && SceneManager.GetActiveScene().name == "GameScene"){
            FindObjectOfType<AudioManager>().Play("menuAndTutorial");
            FindObjectOfType<AudioManager>().Stop("gameSound");
        }
    }
}