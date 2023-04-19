using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeenMeter : MonoBehaviour
{
    /// <summary>
    /// Enemy's "Seen" gauge
    /// Determines the enemy's speed towards player
    /// </summary>
    public float SeenGauge { get; set; }

    /// <summary>
    /// Boolean based on whether player has looked at enemy or not
    /// </summary>
    public bool Seen { get; set; } = false;

    /// <summary>
    /// Player for enemy to track upon glancing
    /// </summary>
    public Transform player { get; set; } = null;

    private EnemyAI ai;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<EnemyAI>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AddSpeed();
        if (Seen) agent.SetDestination(player.position);

        if (SeenGauge >= 0.1f)
        {
            animator.SetBool("isNoticed", true);
           
        }

        if (SeenGauge <= 0.2f)
        {
            animator.SetBool("isNoticed", false);
        }
        
    }

    /// <summary>
    /// Sets the speed to a specific amount -> SeenGauge / 20
    /// Max 80 Seen / 4 speed
    /// </summary>
    private void AddSpeed() 
        => ai.Speed = SeenGauge <= 80 && Seen ? SeenGauge / 20 : !Seen ? 0.5f : 5f;

    /// <summary>
    /// Adds 2 to the "Seen" gauge
    /// </summary>
    public void AddSeen()
        => SeenGauge += 2;

    /// <summary>
    /// Set the Seen gauge to value equating to lower speed
    /// </summary>
    /// <param name="amount"></param>
    public void SetSeen(float amount) 
        => SeenGauge = amount;

}
