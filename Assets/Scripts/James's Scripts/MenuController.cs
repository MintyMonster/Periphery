using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Made by James Sherlock

    [SerializeField]
    private Button play;
    [SerializeField]
    private Button controls;
    [SerializeField]
    private Button exit;
    [SerializeField]
    private Button contolsback;
    [SerializeField]
    private GameObject mainMenuPannel;
    [SerializeField]
    private GameObject ControlsPannel;

    //sets variables on awake to avoid errors and the game not working.
    private void Awake() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameCompleteManager.bongPillarComplete = false;
        GameCompleteManager.lightGameComplete = false;
        GameCompleteManager.pickerGameComplete = false;
    }

    //Listeners for all the buttons
    private void Start() {

        play.onClick.AddListener(PlayLevel);
        exit.onClick.AddListener(ExitGame);
        controls.onClick.AddListener(() => SwitchToPannel(mainMenuPannel, ControlsPannel));
        contolsback.onClick.AddListener(() => SwitchToPannel(ControlsPannel, mainMenuPannel));

    }

    //Loads you into the first level
    private void PlayLevel() {
        FirstPersonController.firstBoot = true;
        SceneManager.LoadScene("Design_testing");
    }

    //Opens and Closes the controls menu
    private void SwitchToPannel(GameObject currentPannel, GameObject newPannel) {
        currentPannel.SetActive(false);
        newPannel.SetActive(true);
    }

    //Quits the game
    private void ExitGame() {
        Application.Quit();
    }

}
