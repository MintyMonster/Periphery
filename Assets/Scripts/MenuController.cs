using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

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


    private void Start() {

        play.onClick.AddListener(PlayLevel);
        exit.onClick.AddListener(ExitGame);
        controls.onClick.AddListener(() => SwitchToPannel(mainMenuPannel, ControlsPannel));
        contolsback.onClick.AddListener(() => SwitchToPannel(ControlsPannel, mainMenuPannel));


    }

    private void PlayLevel() {
        FirstPersonController.firstBoot = true;
        SceneManager.LoadScene("Design_testing");
    }


    private void SwitchToPannel(GameObject currentPannel, GameObject newPannel) {
        currentPannel.SetActive(false);
        newPannel.SetActive(true);
    }

    private void ExitGame() {
        Application.Quit();
    }

}
