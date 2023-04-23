using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prisms;

    [HideInInspector]
    static public bool prismOneBeingLookedAt = false;

    static public bool prismTwoBeingLookedAt = false;

    static public bool prismThreeBeingLookedAt = false;

    static public bool prismFourBeingLookedAt = false;

    [SerializeField]
    private GameObject[] lightBeams;

    private float prismOnePostion = 0;

    private float prismTwoPostion = 0;

    private float prismThreePostion = 0;

    private float prismFourPostion = 0;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip turingSound;

    [SerializeField]
    private AudioClip inPostionSound;

    [SerializeField]
    private Material gameCompleteMaterial;
    
    // Update is called once per frame
    void Update()
    {

        
        //
        if(Input.GetKeyDown(KeyCode.E)) {
            if (prismOneBeingLookedAt) {

                if(prismOnePostion != 7) {
                    prisms[0].transform.Rotate(0, 45f, 0);

                    ++prismOnePostion;
                    source.PlayOneShot(turingSound);
                }

            }
            if (prismTwoBeingLookedAt) {

                if(prismTwoPostion != 5) {
                    prisms[1].transform.Rotate(0, 45f, 0);
                    ++prismTwoPostion;
                    source.PlayOneShot(turingSound);
                }
               

            }
            if (prismThreeBeingLookedAt) {

                if(prismThreePostion != 2) {
                    prisms[2].transform.Rotate(0, 45f, 0);
                    ++prismThreePostion;
                    source.PlayOneShot(turingSound);
                }

            }
            if (prismFourBeingLookedAt) {

                if(prismFourPostion != 6) {
                    prisms[3].transform.Rotate(0, 45, 0);
                    ++prismFourPostion;
                    source.PlayOneShot(turingSound);
                }
                

            }
        }
        //
        if (prismOnePostion == 7) {
            lightBeams[1].SetActive(true);
            prisms[1].GetComponent<MeshRenderer>().material = gameCompleteMaterial;
            
        }
        else {
            lightBeams[1].SetActive(false);
        }
        //
        if(prismTwoPostion == 5 & prismOnePostion == 7) {
            lightBeams[2].SetActive(true);
            prisms[2].GetComponent<MeshRenderer>().material = gameCompleteMaterial;
        }
        else {
            lightBeams[2].SetActive(false);
        }
        //
        if(prismThreePostion == 2 & prismTwoPostion == 5 & prismOnePostion == 7) {
            lightBeams[3].SetActive(true);
            prisms[3].GetComponent<MeshRenderer>().material = gameCompleteMaterial;
        }
        else {
            lightBeams[3].SetActive(false);
        }
        //
        if(prismFourPostion == 6 & prismThreePostion == 2 & prismTwoPostion == 5 & prismOnePostion == 7) {
            GameCompleteManager.lightGameComplete = true;
            source.PlayOneShot(inPostionSound);
        }

    }

   


}
