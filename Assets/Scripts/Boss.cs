using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Idle,
    Chase,
    Attack,
    Attack2,
    Attack3,
    Damage,
    PowerUp,
    Die,
}

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] float playerChangeRange, playerAttackRange;

    private BossState bossState;
    private Animator animator;
    private bool isNavMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateEnemyState(BossState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        IsInRange();
    }

    public void UpdateEnemyState(BossState newState)
    {
        bossState = newState;

        switch (bossState)
        {
            case BossState.Idle:
                animator.CrossFadeInFixedTime("Idle", 0.1f);
                isNavMeshAgent = false;
                break;
            case BossState.Chase:
                animator.CrossFadeInFixedTime("Walk", 0.1f);
                isNavMeshAgent = true;
                break;
            case BossState.Attack:
                animator.CrossFadeInFixedTime("Attack1", 0.1f);
                isNavMeshAgent = false;
                break;
            case BossState.Attack2:
                animator.CrossFadeInFixedTime("Attack2", 0.1f);
                isNavMeshAgent = false;
                break;
            case BossState.Attack3:
                animator.CrossFadeInFixedTime("Attack3", 0.1f);
                isNavMeshAgent = false;
                break;
            case BossState.Damage:
                animator.CrossFadeInFixedTime("Impact1", 0.1f);
                isNavMeshAgent = false;
                break;
            case BossState.PowerUp:
                animator.CrossFadeInFixedTime("PowerUp", 0.1f);
                isNavMeshAgent = false;
                break;
            case BossState.Die:
                animator.CrossFadeInFixedTime("Death", 0.1f);
                isNavMeshAgent = false;
                break;
        }
    }

    void IsInRange()
    {
        float playerDistanceSqr = (player.transform.position - transform.position).sqrMagnitude;
        if ((playerDistanceSqr <= playerChangeRange * playerChangeRange) && (playerDistanceSqr > playerAttackRange * playerAttackRange))
        {
            if (bossState == BossState.Idle || bossState == BossState.Attack || bossState == BossState.Attack2)
            {
                UpdateEnemyState(BossState.Chase);
            }
        }
        else if (playerDistanceSqr <= playerAttackRange * playerAttackRange)
        {
            if (bossState == BossState.Chase)
            {
                UpdateEnemyState(BossState.Attack);
            }
        }
        else
        {
            if ((!(bossState == BossState.Idle)) && (!(bossState == BossState.Damage)) && (!(bossState == BossState.Die)))
            {
                UpdateEnemyState(BossState.Idle);
            }
        }
    }

}
