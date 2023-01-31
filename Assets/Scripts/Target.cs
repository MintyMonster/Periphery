using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip clip;

    [SerializeField] ParticleSystem pillarcomplete = null;

    public float time = 5f;

    public float timeStore = 5f;

    static public bool isLookedAt = false;

    private void Update() {
        
        if(isLookedAt) {
           
         time -= Time.deltaTime;
            
        }
        if (!isLookedAt) {
            time = timeStore;
        }

        Debug.Log(isLookedAt);

        if (time <= 0f) {

            source.PlayOneShot(clip);

        }
    }

    

}
