using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteManager : MonoBehaviour
{
    //Made by James Sherlock


    //All bool relating to completing the game
    [HideInInspector]
    static public bool pickerGameComplete = false;
    [HideInInspector]
    static public bool bongPillarComplete = false;
    [HideInInspector]
    static public bool lightGameComplete = false;
    [HideInInspector]
    static public bool deathAnimationPlay = false;
    [HideInInspector]
    static public bool girldeathAnimationPlay = false;

    //The front doors that need to be disabled
    [SerializeField]
    private GameObject door1;
    [SerializeField]
    private GameObject door2;

    //Resets these bools on start so the enemies arent dead if you replay the game
    private void Start()
    {
        deathAnimationPlay = false;
        girldeathAnimationPlay = false;
    }

    //Simplely checks if all the puzzles are comlete and if so kills the enemies and opens the door
    void Update()
    {
        
        if(pickerGameComplete & bongPillarComplete & lightGameComplete) {
            
            deathAnimationPlay = true;

            girldeathAnimationPlay = true;

            door1.SetActive(false);

            door2.SetActive(false);
        }
    }

}
