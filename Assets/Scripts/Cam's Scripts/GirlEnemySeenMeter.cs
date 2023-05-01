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
    public static bool hasPlayedGirl;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<GirlEnemyAI>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        hasPlayedGirl = false;
    }

    // Update is called once per frame
    void Update()
    {
        AddSpeed();
        if (Seen) agent.SetDestination(Player.position);

        // Animator stuff for james
        if (SeenGauge >= 0.1f)
        {
            animator.SetBool("isNoticed", true);
            if (!hasPlayedGirl)
            {
                source.PlayOneShot(clip);
                hasPlayedGirl = true;
            }
        }

        if (SeenGauge <= 0.2f)
        {
            animator.SetBool("isNoticed", false);
        }
    }

    private void AddSpeed()
        => ai.Speed = SeenGauge <= 80 && Seen ? SeenGauge / 20 : !Seen ? 0.5f : 7f;

    public void AddSeen()
        => SeenGauge += 2;

    public void SetSeen(float amount)
        => SeenGauge = amount;
}
