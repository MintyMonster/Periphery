using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomPicker : MonoBehaviour
{
    //Made by James Sherlock

    [SerializeField]
    private GameObject[] pillars;

    [SerializeField]
    private int pillarSelect;

    [SerializeField]
    private float stayLit;
    [SerializeField]
    private float stayLitCounter;
    [SerializeField]
    private float waitBetweenLight;
    [SerializeField]
    private float waitBetweenCounter;
    [SerializeField]
    private bool shouldBeLit;
    [SerializeField]
    private bool shouldBeDark;

    public List<int> activeSequence;

    private int positionInSequence;

    private bool gameActive;

    private int inputInSequence;

    public int gameCompleteNumber = 0;

    private bool hasSoundPlayed = false;

    private bool gameStarted = false;

    [SerializeField]
    private Material  gameCompleteMaterial;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip clip;


    //Calls the StartGame function if your in the right room
    //Only calls it once
    private void OnTriggerEnter(Collider other) {
        if (!gameStarted)
        {
            StartGame();
        }

    }

    // Update is called once per frame
    void Update() {

        //This handles the order and the lighting of the pillars
        //This can only run is the game is active
        if(gameCompleteNumber != 5) {
            if (shouldBeLit) {

                stayLitCounter -= Time.deltaTime;

                if (stayLitCounter < 0) {

                    pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = false;

                    shouldBeLit = false;

                    shouldBeDark = true;
                    waitBetweenCounter = waitBetweenLight;

                    positionInSequence++;
                }

            }
            if (shouldBeDark) {

                waitBetweenCounter -= Time.deltaTime;

                if (positionInSequence >= activeSequence.Count) {
                    shouldBeDark = false;
                    gameActive = true;
                }
                else {
                    if (waitBetweenCounter < 0) {


                        pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = true;

                        stayLitCounter = stayLit;
                        shouldBeLit = true;
                        shouldBeDark = false;
                    }

                }

            }
        }
       
        //this handles when the game is completed 
        if (gameCompleteNumber == 5) {
            GameCompleteManager.pickerGameComplete = true;
            foreach(var pillar in pillars) {
                pillar.GetComponent<MeshRenderer>().material = gameCompleteMaterial;

                pillar.GetComponent<Light>().enabled = false;

            }

            //this handles the sound and the AI
            if (!hasSoundPlayed) {
                source.PlayOneShot(clip);
               
                // AI reset
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                enemies.ToList().ForEach(x =>
                {
                    x.GetComponent<SeenMeter>().Seen = false;
                    x.GetComponent<EnemyAI>().HandleRandomRoam();
                    x.GetComponent<SeenMeter>().SeenGauge = 0;
                    SeenMeter.hasPlayed = false;
                    GirlEnemySeenMeter.hasPlayedGirl = false;
                });
                hasSoundPlayed = true;
            }

            gameActive = false;
        }


    }

    //Starts the puzzle by picking one of the pillars and uses that as the first pillar in the order
    //also sets gamestarted to true meaning you can start the puzzle multiple times
    public void StartGame() {

        positionInSequence = 0;
        inputInSequence = 0;
        pillarSelect = Random.Range(0, pillars.Length);

        activeSequence.Add(pillarSelect);

        pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = true;

        stayLitCounter = stayLit;
        shouldBeLit = true;
        gameStarted = true;

    }


    //checks the order of the pillars being pressed if the order is correct allows the game to continue
    //if the pillar is order tell you are wrong
    public void pillarLookedAt(int whichPillar) {

        if (gameActive) {

            if (activeSequence[inputInSequence] == whichPillar) {

                Debug.Log("Correct");

                inputInSequence++;

                if(inputInSequence >= activeSequence.Count) {

                    positionInSequence = 0;
                    inputInSequence = 0;

                    pillarSelect = Random.Range(0, pillars.Length);

                    activeSequence.Add(pillarSelect);

                    gameCompleteNumber++;

                    pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = true;

                    stayLitCounter = stayLit;
                    shouldBeLit = true;

                    gameActive = false;
                }
            }
            else {

                Debug.Log("Wrong");

            }
        }

       

    }

}
