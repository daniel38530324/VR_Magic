using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Attack2,
    Damage,
    Die
}

public class Enemy : MonoBehaviour
{
    [SerializeField] Level2Manager level2Manager;
    [SerializeField] private GameObject player, weapon;
    [SerializeField] float playerChangeRange, playerAttackRange;
    [SerializeField] AttackType damageType = AttackType.FireBall;
    [SerializeField] GameObject hint_UI;

    private EnemyState enemyState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isNavMeshAgent;
    private int health = 2;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateEnemyState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        IsInRange();
        SetAttack();
        SetNavMeshAgent();
        SetDamage();
        SetDie();
    }

    public void UpdateEnemyState(EnemyState newState)
    {
        enemyState = newState;

        switch(enemyState)
        {
            case EnemyState.Idle:
                animator.CrossFadeInFixedTime("Idle", 0.1f);
                isNavMeshAgent = false;
                break;
            case EnemyState.Chase:
                animator.CrossFadeInFixedTime("Walk Forward In Place", 0.1f);
                isNavMeshAgent = true;
                break;
            case EnemyState.Attack:
                animator.CrossFadeInFixedTime("EnemyAttack1", 0.1f);
                isNavMeshAgent = false;
                break;
            case EnemyState.Attack2:
                animator.CrossFadeInFixedTime("EnemyAttack2", 0.1f);
                isNavMeshAgent = false;
                break;
            case EnemyState.Damage:
                animator.CrossFadeInFixedTime("Take Damage", 0.1f);
                isNavMeshAgent = false;
                break;
            case EnemyState.Die:
                animator.CrossFadeInFixedTime("Die", 0.1f);
                isNavMeshAgent = false;
                break;
        }
    }

    void IsInRange()
    {
        float playerDistanceSqr = (player.transform.position - transform.position).sqrMagnitude;
        if((playerDistanceSqr <= playerChangeRange * playerChangeRange) && (playerDistanceSqr > playerAttackRange * playerAttackRange))
        {
            if(enemyState == EnemyState.Idle || enemyState == EnemyState.Attack || enemyState == EnemyState.Attack2)
            {
                UpdateEnemyState(EnemyState.Chase);
            }
        }
        else if(playerDistanceSqr <= playerAttackRange * playerAttackRange)
        {
            if (enemyState == EnemyState.Chase)
            {
                UpdateEnemyState(EnemyState.Attack);
            }
        }
        else
        {
            if((!(enemyState == EnemyState.Idle)) && (!(enemyState == EnemyState.Damage)) && (!(enemyState == EnemyState.Die)))
            {
                UpdateEnemyState(EnemyState.Idle);
            }
        }
    }

    void SetAttack()
    {
        if (enemyState == EnemyState.Attack)
        {
            if (GetNormalizedTime(animator, "EnemyAttack1") >= 1)
            {
                
                 UpdateEnemyState(EnemyState.Attack2);
               
            }
        }
        else if(enemyState == EnemyState.Attack2)
        {
            if (GetNormalizedTime(animator, "EnemyAttack2") >= 1)
            {

                UpdateEnemyState(EnemyState.Attack);

            }
        }
    }

    void SetNavMeshAgent()
    {
        if (enemyState == EnemyState.Chase)
        {
            navMeshAgent.destination = player.transform.position;
        }
        else if (enemyState == EnemyState.Attack || enemyState == EnemyState.Attack2)
        {
            navMeshAgent.ResetPath();
            transform.LookAt(player.transform.position);
        }
        else
        {
            navMeshAgent.ResetPath();
        }
    }

    void SetDamage()
    {
        if (enemyState == EnemyState.Damage)
        {
            if (GetNormalizedTime(animator, "TakeDamage") >= 1)
            {
                UpdateEnemyState(EnemyState.Idle);
            }
        }
    }

    void SetDie()
    {
        if (enemyState == EnemyState.Die)
        {
            if (GetNormalizedTime(animator, "Die") >= 1)
            {
                Destroy(gameObject, 1);
            }
        }
    }

    float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }

    }

    public void EnableWeapon()
    {
        weapon.SetActive(true);
    }

    public void DisableWeapon()
    {
        weapon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(damageType.ToString()))
        {
            if (enemyState == EnemyState.Damage || enemyState == EnemyState.Die) { return; }

            health--;
            if (health > 0)
            {
                UpdateEnemyState(EnemyState.Damage);
            }
            else
            {
                level2Manager.CheckLevel();
                UpdateEnemyState(EnemyState.Die);
            }
            
        }
        else if (other.CompareTag("FireBall") || other.CompareTag("WaterBall") || other.CompareTag("LightningBall"))
        {
            if(hint_UI.activeInHierarchy) { return; }
            StartCoroutine(SetHint());
        }
    }

    IEnumerator SetHint()
    {
        hint_UI.SetActive(true);
        yield return new WaitForSeconds(2f);
        hint_UI.SetActive(false);
    }
}
