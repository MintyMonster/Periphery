using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerLookContoller : MonoBehaviour 
{
    //Made by James Sherlock

    [HideInInspector]
    public int thisPillarNumber;

    private RandomPicker theRP;

    //Finds the RandomPicker
    void Start()
    {
        theRP = FindObjectOfType<RandomPicker>();
    }

    //Sends the number of which pillar was clicked on to the randompicker script
    private void OnMouseOver() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) { // Left mouse button AKA 'A' on Wii controller
            theRP.pillarLookedAt(thisPillarNumber);
        }
        
    }
}
