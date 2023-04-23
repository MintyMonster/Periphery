using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private bool hasPlayed = false;

    [SerializeField]
    private Material gameCompleteMaterial;

    private void Update() {
        
        if(isLookedAt) {
           
            time -= Time.deltaTime;
            
        }
        if (!isLookedAt) {
            time = timeStore;
        }

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
                });

                gameObject.GetComponent<MeshRenderer>().material = gameCompleteMaterial;

            }
        }
    }
}
