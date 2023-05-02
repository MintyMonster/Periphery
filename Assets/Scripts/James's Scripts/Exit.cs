using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    //Made by James Sherlock

    //Takes the player back to the main menu when they colide with the trigger box
    private void OnTriggerEnter(Collider other) {
        SceneManager.LoadScene("Menu");
    }

}
