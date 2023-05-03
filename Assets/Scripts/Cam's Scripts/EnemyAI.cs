using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Possible snares?

    /// <summary>
    /// Boolean based on whether the agent is allowed to move or not
    /// </summary>
    public bool CanMove { get; set; } = true;

    /// <summary>
    /// Agent speed
    /// </summary>
    public float Speed { get; set; }

    private float walkRadius = 40.0f;
    private NavMeshAgent agent;
    // Added by James for animation
    private Animator animator;

    private bool hasPlayed;

    [SerializeField]
    private AudioSource slowstepSource;

    [SerializeField]
    private AudioSource faststepSource;

    [SerializeField]
    private AudioSource deathsoundSource;

    [SerializeField]
    private AudioClip deathSound;

    // Start is called before the first frame updated
    void Start()
    {
        
        // Added by James for animation
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator.SetBool("dead", false);

        // Set start params
        Speed = Random.Range(0.5f, 0.7f);
        agent.speed = Speed; // random start speed. Adds "Game depth"
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            agent.speed = Speed;

            HandleDestinationReached();
            AgentAhead();
        }


        // Made by James Pennell
        // Triggers animations and aduio.
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

        if (GameCompleteManager.deathAnimationPlay)
        {
            animator.SetBool("dead", true);

            animator.SetBool("isNoticed", true);

            if (!hasPlayed)
            {
                deathsoundSource.PlayOneShot(deathSound);
                hasPlayed = true;
            }

            gameObject.GetComponent<EnemyAI>().enabled = false;

            gameObject.GetComponent<SeenMeter>().enabled = false;

            gameObject.GetComponent<NavMeshAgent>().enabled = false;

            faststepSource.enabled = false;
            slowstepSource.enabled = false;
            FirstPersonController.canDie = false;
        }

    }

    /// <summary>
    /// Random roam handler
    /// </summary>
    public void HandleRandomRoam()
    {
        agent.SetDestination(GetRandomPosition());
    }

    /// <summary>
    /// Checks if the agent has reached it's destination or not
    /// </summary>
    private void HandleDestinationReached()
    {
        if (!agent.pathPending)
            if (agent.remainingDistance <= agent.stoppingDistance)
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    HandleRandomRoam();
    }

    /// <summary>
    /// Detects if another enemy is ahead, re-paths to avoid collision
    /// </summary>
    private void AgentAhead()
    {
        RaycastHit hit;
        if (!(this.gameObject.GetComponent<SeenMeter>().Seen))
        {
            if (Physics.Raycast(this.transform.position, Vector3.forward, out hit, 1f)
            || Physics.Raycast(this.transform.position, Vector3.back, out hit, 1f)
            || Physics.Raycast(this.transform.position, Vector3.left, out hit, 1f)
            || Physics.Raycast(this.transform.position, Vector3.right, out hit, 1f))
                if (hit.transform.tag.ToLower().Equals("enemy"))
                    HandleRandomRoam();
        }
    }

    /// <summary>
    /// Picks random destination based on NavMesh area
    /// </summary>
    /// <returns>(Vector3) final position</returns>
    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPos = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
            finalPos = hit.position;

        return finalPos;
    }
}
