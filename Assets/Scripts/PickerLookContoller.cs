using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerLookContoller : MonoBehaviour
{

    public int thisPillarNumber;

    private RandomPicker theRP;


    // Start is called before the first frame update
    void Start()
    {
        theRP = FindObjectOfType<RandomPicker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {
        if (Input.GetKeyDown(KeyCode.E)) {
            theRP.pillarLookedAt(thisPillarNumber);
        }
        
    }
}
