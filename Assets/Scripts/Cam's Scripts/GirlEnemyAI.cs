using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GirlEnemyAI : MonoBehaviour
{
    public bool CanMove { get; set; } = true;

    public float Speed { get; set; }
    private float walkRadius = 40.0f;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Set start params
        Speed = Random.Range(2f, 3f);
        agent.speed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {
            agent.speed = Speed;
            HandleDestinationReached();
        }

        // Animator stuff here for James
    }

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

    private void HandleDestinationReached()
    {
        if (!agent.pathPending)
            if (agent.remainingDistance <= agent.stoppingDistance)
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    HandleRandomRoam();
    }

    public void HandleRandomRoam() => agent.SetDestination(GetRandomPosition());
}
