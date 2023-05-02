using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Made by James Sherlock

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    [SerializeField] ParticleSystem pillarcomplete = null;

    public float time = 5f;
    public float timeStore = 5f;

    [HideInInspector]
    static public bool isLookedAt = false;

    private bool hasPlayed = false;

    [SerializeField]
    private Material gameCompleteMaterial;

    private void Update() {
        
        //Handles the time if you are looking at the pillar
        if(isLookedAt) {
           
            time -= Time.deltaTime;
            
        }
        if (!isLookedAt) {
            time = timeStore;
        }

        //When the timer gets to zero plays the sound and the particles
        //also resets the AI
        if (time <= 0f) {

            if(!hasPlayed) {
                source.PlayOneShot(clip);
                hasPlayed = true;
                pillarcomplete.Play();

                GameCompleteManager.bongPillarComplete = true;

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

                gameObject.GetComponent<MeshRenderer>().material = gameCompleteMaterial;

            }
        }
    }
}
