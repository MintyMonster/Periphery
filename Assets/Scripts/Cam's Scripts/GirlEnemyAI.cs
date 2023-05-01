using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GirlEnemyAI : MonoBehaviour
{
    public bool CanMove { get; set; } = true;
    public bool CanAggro { get; set; } = true;

    private bool hasPlayed;

    public float Speed { get; set; }
    public int Seconds { get; set; } = 3;
    private float walkRadius = 40.0f;
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField]
    private AudioSource slowstepSource;

    [SerializeField]
    private AudioSource faststepSource;

    [SerializeField]
    private AudioSource deathsoundSource;

    [SerializeField]
    private AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("dead", false);

        // Set start params
        Speed = Random.Range(2f, 3f);
        agent.speed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            agent.speed = Speed;
            HandleDestinationReached();
        }

        // Animator stuff here for James
        if (Speed >= 0.51f)
        {
            animator.SetBool("speed", false);
        }

        if (Speed <= 0.52f)
        {
            animator.SetBool("speed", true);
        }

        if (Speed == 4f)
        {
            animator.SetBool("fast", true);
            if (animator.GetBool("fast") == true)
            {
                faststepSource.enabled = true;
                slowstepSource.enabled = false;
            }
            else
            {
                faststepSource.enabled = false;
                slowstepSource.enabled = true;
            }
        }

        // Killing girl handler
        if (GameCompleteManager.girldeathAnimationPlay)
        {
            animator.SetBool("dead", true);

            animator.SetBool("isNoticed", true);

            if (!hasPlayed)
            {
                deathsoundSource.PlayOneShot(deathSound);
                hasPlayed = true;
            }

            gameObject.GetComponent<GirlEnemyAI>().enabled = false;

            gameObject.GetComponent<GirlEnemySeenMeter>().enabled = false;

            gameObject.GetComponent<NavMeshAgent>().enabled = false;

            faststepSource.enabled = false;
            slowstepSource.enabled = false;
        }
    }

    // Get a random position in the map for the enemy
    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
            finalPos = hit.position;

        return finalPos; // Return a random position in the house
    }

    // When it reaches a destination, pick a new one
    private void HandleDestinationReached()
    {
        if (!agent.pathPending)
            if (agent.remainingDistance <= agent.stoppingDistance)
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    HandleRandomRoam();
    }

    // Aggro cooldown for when it de-aggros from the player
    public void AggroCooldown()
    {
        CanAggro = false;
        HandleRandomRoam();
        StartCoroutine(AggroEnumurator(Seconds));
    }

    // Wait for time
    private IEnumerator AggroEnumurator(int secs)
    {
        yield return new WaitForSeconds(secs);
        CanAggro = true;
    }

    // Set new destination
    public void HandleRandomRoam() => agent.SetDestination(GetRandomPosition());
}
