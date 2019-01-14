using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text playerHealth;
    public GameObject player;
    private UnitController controller;

    private void Start()
    {
        controller = player.GetComponent<UnitController>();
    }

    private void Update()
    {
        playerHealth.text = "Player Health: " + controller.health;
    }
}
