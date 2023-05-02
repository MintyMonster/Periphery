using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPickUp : MonoBehaviour
{
    //Made by James Sherlock

    [SerializeField]
    private GameObject book;

    //Allows you to pick up and use the journal
    private void OnMouseOver() {

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            journal.canUse = true;
            Destroy(book);
        }
    }

}
