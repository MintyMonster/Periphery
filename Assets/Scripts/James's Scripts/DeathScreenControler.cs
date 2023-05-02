using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenControler : MonoBehaviour
{
    //Made by James Sherlock


    [SerializeField]
    private Button menu;
    [SerializeField]
    private Button restart;

    //Listeners for the buttons
    void Start()
    {

        menu.onClick.AddListener(BackToMenu);
        restart.onClick.AddListener(ReStart);

    }

    //Loads the menu
    private void BackToMenu() {

        SceneManager.LoadScene("Menu");
        
    }

    //Reloads the scene like it was the first time you opened it
    private void ReStart() {

        FirstPersonController.firstBoot = true;
        SceneManager.LoadScene("Design_testing");
        
    }


}
