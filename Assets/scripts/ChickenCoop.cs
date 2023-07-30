using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCoop : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private new Transform transform;

    private float distanceToPlayer;
    private int chickenRescued = 0;
    private GameObject Player;

    void Start()
    {
        Vector3 newPosition = new Vector3(width / 2, height / 2, 0);
        transform.position = newPosition;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseOver() {
        if(Input.GetMouseButtonDown(0) && playerClose()){
            chickenRescued += Player.GetComponent<Player>().GetChickensGrabbed();
            Player.GetComponent<Player>().SetChickensGrabbed(0);
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
