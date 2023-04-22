using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class journal : MonoBehaviour
{
    [SerializeField]
    private GameObject journal_panel;

    public bool isEnabled;
    public static bool isPaused;
    private void Start()
    {
        isEnabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isEnabled)
            {
                journal_panel.SetActive(false);

                isEnabled = false;

                Time.timeScale = 1f;

                isPaused = false;
            }

            else
            {
                journal_panel.SetActive(true);

                isEnabled = true;

                Time.timeScale = 0f;

                isPaused = true;
            }
        }

        //if (isEnabled)
        //{

        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        journal_panel.SetActive(false);

        //        isEnabled = false;
        //    }

        //}

        //if (!isEnabled)
        //{
        //    if (Input.GetKeyDown(KeyCode.Q))
        //    {
        //        journal_panel.SetActive(true);

        //        isEnabled = true;
        //    }
        //}
    }
}