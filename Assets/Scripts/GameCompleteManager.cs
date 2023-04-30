using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteManager : MonoBehaviour
{

    static public bool pickerGameComplete = false;

    static public bool bongPillarComplete = false;

    static public bool lightGameComplete = false;

    static public bool deathAnimationPlay = false;


    [SerializeField]
    GameObject door1;

    [SerializeField]
    GameObject door2;




    void Update()
    {
        
        if(pickerGameComplete & bongPillarComplete & lightGameComplete) {
            

            deathAnimationPlay = true;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach(var enemy in enemies) {


                door1.SetActive(false);

                door2.SetActive(false);
            }
        }


    }
}
