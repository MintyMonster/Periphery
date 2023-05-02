using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
    //Made by James Sherlock

    [SerializeField]
    private float range = 100f;

    [SerializeField]
    private Camera eyeCam;

    //This Handles all the Raycasting for two of the Puzzles 
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyeCam.transform.position, eyeCam.transform.forward, out hit, range)) {

            //
            if(hit.transform.name == "Pillar") {
                Target.isLookedAt = true;
            }
            if(hit.transform.name != "Pillar") {
                Target.isLookedAt = false;
            }
            //
            if(hit.transform.name == "Prism") {
                PrismController.prismOneBeingLookedAt = true;

            }
            if (hit.transform.name != "Prism") {
                PrismController.prismOneBeingLookedAt = false;
            }
            //
            if (hit.transform.name == "Prism2") {
                PrismController.prismTwoBeingLookedAt = true;

            }
            if (hit.transform.name != "Prism2") {
                PrismController.prismTwoBeingLookedAt = false;
            }
            //
            if (hit.transform.name == "Prism3") {
                PrismController.prismThreeBeingLookedAt = true;

            }
            if (hit.transform.name != "Prism3") {
                PrismController.prismThreeBeingLookedAt = false;
            } 
            //
            if (hit.transform.name == "Prism4") {
                PrismController.prismFourBeingLookedAt = true;

            }
            if (hit.transform.name != "Prism4") {
                PrismController.prismFourBeingLookedAt = false;
            }

        }

        
    }


}
