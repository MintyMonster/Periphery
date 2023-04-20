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

    public Vector3 rotation;

    private void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {

        
        //
        if(Input.GetKeyDown(KeyCode.E)) {
            if (prismOneBeingLookedAt) {

                prisms[0].transform.Rotate(0, 45f, 0); ; 

            }
            if (prismTwoBeingLookedAt) {

                prisms[1].transform.Rotate(0, 0, 90f);

            }
            if (prismThreeBeingLookedAt) {

                prisms[2].transform.Rotate(0, 0, 90f);

            }
            if (prismFourBeingLookedAt) {

                prisms[3].transform.Rotate(0, 0, 90f);

            }
        }
        //
        if (prisms[0].transform.rotation.eulerAngles.y == 90f) {
            lightBeams[0].SetActive(true);
            Debug.Log("FuckYou");
        }
        else {
            lightBeams[0].SetActive(false);
        }

    }

   


}
