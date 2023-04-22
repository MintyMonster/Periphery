using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchHandler : MonoBehaviour
{
    [Header("Torch")]
    [SerializeField] private Canvas generalCanvas;
    [SerializeField] private GameObject batteryEmpty;
    [SerializeField] private GameObject batteryOne;
    [SerializeField] private GameObject batteryTwo;
    [SerializeField] private GameObject batteryThree;
    [SerializeField] private GameObject batteryText;
    [SerializeField] private GameObject player;

    private Color batteryGreen = new Color(28, 255, 0);
    private Color batteryGrey = new Color(101, 101, 101);
    private Color batteryOrange = new Color(224, 113, 0);
    private Color batteryRed = new Color(255, 0, 0);

    void Update()
    {
        float batteryPercent = player.GetComponent<FirstPersonController>().torchBatteryPercent;
        batteryText.GetComponent<Text>().text = Mathf.RoundToInt(batteryPercent).ToString() + "%";

        //if(batteryPercent >= 50)
        //{
        //    batteryOne.GetComponent<Image>().color = Color.green;
        //    batteryTwo.GetComponent<Image>().color = Color.green;
        //    batteryThree.GetComponent<Image>().color = Color.green;
        //    batteryText.GetComponent<Text>().color = Color.green;
        //}

        //batteryThree.GetComponent<Image>().color = batteryPercent < 50 ? Color.grey : Color.green;
        //batteryTwo.GetComponent<Image>().color = batteryPercent < 10 ? Color.grey : batteryPercent < 50 && batteryPercent >= 10 ? Color.yellow : Color.green;
        //batteryOne.GetComponent<Image>().color = batteryPercent < 10 ? Color.red : batteryPercent < 50 && batteryPercent >= 10 ? Color.yellow : Color.green;
        //batteryText.GetComponent<Text>().color = batteryPercent < 10 ? Color.red : batteryPercent < 50 && batteryPercent >= 10 ? Color.yellow : Color.green;

        if (batteryPercent > 60)
        {
            batteryEmpty.SetActive(false);
            batteryOne.SetActive(false);
            batteryTwo.SetActive(false);
            batteryThree.SetActive(true);
        }

        if (batteryPercent > 30 && batteryPercent <= 60)
        {
            batteryEmpty.SetActive(false);
            batteryOne.SetActive(false);
            batteryTwo.SetActive(true);
            batteryThree.SetActive(false);
        }

        if (batteryPercent >0 && batteryPercent <= 30)
        {
            batteryEmpty.SetActive(false);
            batteryOne.SetActive(true);
            batteryTwo.SetActive(false);
            batteryThree.SetActive(false);
        }

        if (batteryPercent == 0)
        {
            batteryEmpty.SetActive(true);
            batteryOne.SetActive(false);
            batteryTwo.SetActive(false);
            batteryThree.SetActive(false);
        }
    }
}
