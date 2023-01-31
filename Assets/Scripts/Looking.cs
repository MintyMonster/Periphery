using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{

    [SerializeField]
    private float range = 100f;

    [SerializeField]
    private Camera eyeCam;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyeCam.transform.position, eyeCam.transform.forward, out hit, range)) {

            if(hit.transform.name == "Pillar") {
                Target.isLookedAt = true;
            }
            if(hit.transform.name != "Pillar") {
                Target.isLookedAt = false;
            }
        }


    }


}
