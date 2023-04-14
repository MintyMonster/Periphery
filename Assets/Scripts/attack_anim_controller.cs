using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_anim_controller : MonoBehaviour
{
   private Animator animator;

// Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    private void OnTriggerStay(Collider other)
    {
        animator.SetBool("inRange", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("inRange", false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
