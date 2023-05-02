using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneLoad : MonoBehaviour
{
    //Made by James Sherlock


    public float time = 2f;
    public float timeStore = 2f;

    //Resets the timer
    private void Awake() {
        time = timeStore;
    }

    //Reloads to the main scene
    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0f) {
            SceneManager.LoadScene("Design_testing");
        }


    }
}
