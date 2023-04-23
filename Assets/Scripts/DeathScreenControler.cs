using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenControler : MonoBehaviour
{

    [SerializeField]
    private Button menu;

    [SerializeField]
    private Button restart;

    // Start is called before the first frame update
    void Start()
    {

        menu.onClick.AddListener(BackToMenu);

        restart.onClick.AddListener(ReStart);

    }

    private void BackToMenu() {

        SceneManager.LoadScene("Menu");
        
    }

    private void ReStart() {

        FirstPersonController.firstBoot = true;
        SceneManager.LoadScene("Design_testing");
        
    }


}
