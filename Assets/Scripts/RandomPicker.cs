using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPicker : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update() {
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
            } else {
                if (waitBetweenCounter < 0) {

                    //pillarSelect = Random.Range(0, pillars.Length);

                    //activeSequence.Add(pillarSelect);

                    pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = true;

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }

            }

        }

        if (gameCompleteNumber == 4) {
            GameCompleteManager.pickerGameComplete = true;
        }


    }

    public void StartGame() {

        positionInSequence = 0;
        inputInSequence = 0;
        pillarSelect = Random.Range(0, pillars.Length);

        activeSequence.Add(pillarSelect);

        pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = true;

        stayLitCounter = stayLit;
        shouldBeLit = true;
        
    }

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
