using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrismController : MonoBehaviour
{
    //Made by James Sherlock

    [SerializeField]
    private GameObject[] prisms;

    [HideInInspector]
    static public bool prismOneBeingLookedAt = false;
    [HideInInspector]
    static public bool prismTwoBeingLookedAt = false;
    [HideInInspector]
    static public bool prismThreeBeingLookedAt = false;
    [HideInInspector]
    static public bool prismFourBeingLookedAt = false;

    [SerializeField]
    private GameObject[] lightBeams;
    [SerializeField]
    private GameObject logo;

    private float prismOnePostion = 0;
    private float prismTwoPostion = 0;
    private float prismThreePostion = 0;
    private float prismFourPostion = 0;

    private bool hasDone = false;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip turingSound;
    [SerializeField]
    private AudioClip inPostionSound;

    [SerializeField]
    private Material gameCompleteMaterial;
    [SerializeField]
    private Material logoMaterial;
    
    void Update()
    {
        
        //checks if the prisms are being looked at and handles the rotations of the prisms
        //does this for each of the prisms
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            if (prismOneBeingLookedAt) {

                if(prismOnePostion != 4) {
                    prisms[0].transform.Rotate(0, 20f, 0);

                    ++prismOnePostion;
                    source.PlayOneShot(turingSound);
                }

            }
            if (prismTwoBeingLookedAt) {

                if(prismTwoPostion != 5) {
                    prisms[1].transform.Rotate(0, 25f, 0);
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

                if(prismFourPostion != 5) {
                    prisms[3].transform.Rotate(0, 21f, 0);
                    ++prismFourPostion;
                    source.PlayOneShot(turingSound);
                }
                

            }
        }
        //These handle if the prisms are in the right postion and turns the lightbeams on if they are
        //also changes the material to relect if the prism is complete
        if (prismOnePostion == 4) {
            lightBeams[1].SetActive(true);
            prisms[1].GetComponent<MeshRenderer>().material = gameCompleteMaterial;
            
        }
        else {
            lightBeams[1].SetActive(false);
        }
        //
        if(prismTwoPostion == 5 & prismOnePostion == 4) {
            lightBeams[2].SetActive(true);
            prisms[2].GetComponent<MeshRenderer>().material = gameCompleteMaterial;
        }
        else {
            lightBeams[2].SetActive(false);
        }
        //
        if(prismThreePostion == 2 & prismTwoPostion == 5 & prismOnePostion == 4) {
            lightBeams[3].SetActive(true);
            prisms[3].GetComponent<MeshRenderer>().material = gameCompleteMaterial;
        }
        else {
            lightBeams[3].SetActive(false);
        }
        //
        if(prismFourPostion == 5 & prismThreePostion == 2 & prismTwoPostion == 5 & prismOnePostion == 4) {
            GameCompleteManager.lightGameComplete = true;


            logo.GetComponent<SpriteRenderer>().material = logoMaterial;

            // AI reset
            if (!hasDone) {
                source.PlayOneShot(inPostionSound);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                enemies.ToList().ForEach(x =>
                {
                    x.GetComponent<SeenMeter>().Seen = false;
                    x.GetComponent<EnemyAI>().HandleRandomRoam();
                    x.GetComponent<SeenMeter>().SeenGauge = 0;
                    SeenMeter.hasPlayed = false;
                    GirlEnemySeenMeter.hasPlayedGirl = false;
                });
                hasDone = true;
            }
            
        }

    }

   


}
