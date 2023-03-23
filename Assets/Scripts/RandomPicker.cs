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

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldBeLit) {

            stayLitCounter -= Time.deltaTime;
        
            if(stayLitCounter < 0) {



                pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = true;

                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLight;

                positionInSequence++;
            }
            
        }
        if(shouldBeDark) {

            waitBetweenCounter -= Time.deltaTime;

            if(positionInSequence >= activeSequence.Count) {
                shouldBeDark = false;
            } else {
                if(waitBetweenCounter < 0) {

                    pillarSelect = Random.Range(0, pillars.Length);

                    activeSequence.Add(pillarSelect);

                    pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = false;

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }

            }

        }


    }

    public void StartGame() {

        positionInSequence = 0;

        pillarSelect = Random.Range(0, pillars.Length);

        activeSequence.Add(pillarSelect);

        pillars[activeSequence[positionInSequence]].GetComponent<Light>().enabled = false;

        stayLitCounter = stayLit;
        shouldBeLit = true;
    }

    public void pillarLookedAt(int whichPillar) {

        if(pillarSelect == whichPillar) {

            Debug.Log("Correct");

        } else {
            Debug.Log("Wrong");
        }

    }

}
