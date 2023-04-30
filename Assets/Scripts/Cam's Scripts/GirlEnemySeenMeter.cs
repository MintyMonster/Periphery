using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GirlEnemySeenMeter : MonoBehaviour
{

    public float SeenGauge { get; set; }
    public bool Seen { get; set; } = false;
    public Transform Player { get; set; } = null;

    private GirlEnemyAI ai;
    private NavMeshAgent agent;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<GirlEnemyAI>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AddSpeed();
        if (Seen) agent.SetDestination(Player.position);

        // Animator stuff for james
    }

    private void AddSpeed()
        => ai.Speed = SeenGauge <= 80 && Seen ? SeenGauge / 20 : !Seen ? 0.5f : 7f;

    public void AddSeen()
        => SeenGauge += 2;

    public void SetSeen(float amount)
        => SeenGauge = amount;
}
