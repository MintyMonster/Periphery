using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPickUp : MonoBehaviour
{

    [SerializeField]
    private GameObject book;

    private void OnMouseOver() {

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            journal.canUse = true;
            Destroy(book);
        }
    }

}
