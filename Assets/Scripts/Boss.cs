using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    [SerializeField] private GameObject player, weapon;
    [SerializeField] private float playerChangeRange, playerAttackRange;
    [SerializeField] Material material;
    

    private BossState bossState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int health = 15;
    private AttackType damageType = AttackType.WaterBall;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateBossState(BossState.Idle);
        material.color = new Color(0.7529412f, 0, 0.09143171f);
    }

    // Update is called once per frame
    void Update()
    {
        IsInRange();
        SetAttack();
        SetDamage();
        SetDie();
        SetNavMeshAgent();
        SetPowerUp();
    }

    public void UpdateBossState(BossState newState)
    {
        bossState = newState;

        switch (bossState)
        {
            case BossState.Idle:
                animator.CrossFadeInFixedTime("Idle", 0.1f);
                break;
            case BossState.Chase:
                animator.CrossFadeInFixedTime("Walk", 0.1f);
                break;
            case BossState.Attack:
                animator.CrossFadeInFixedTime("Attack1", 0.1f);
                break;
            case BossState.Attack2:
                animator.CrossFadeInFixedTime("Attack2", 0.1f);
                break;
            case BossState.Attack3:
                animator.CrossFadeInFixedTime("Attack3", 0.1f);
                break;
            case BossState.Damage:
                animator.CrossFadeInFixedTime("Impact1", 0.1f);
                break;
            case BossState.PowerUp:
                animator.CrossFadeInFixedTime("PowerUp", 0.1f);
                break;
            case BossState.Die:
                animator.CrossFadeInFixedTime("Death", 0.1f);
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
                UpdateBossState(BossState.Chase);
            }
        }
        else if (playerDistanceSqr <= playerAttackRange * playerAttackRange)
        {
            if (bossState == BossState.Chase)
            {
                UpdateBossState(BossState.Attack);
            }
        }
        else
        {
            if ((!(bossState == BossState.Idle)) && (!(bossState == BossState.Damage)) && (!(bossState == BossState.Die)))
            {
                UpdateBossState(BossState.Idle);
            }
        }
    }

    void SetAttack()
    {
        if (bossState == BossState.Attack)
        {
            if (GetNormalizedTime(animator, "Attack1") >= 1)
            {

                UpdateBossState(BossState.Attack2);

            }
        }
        else if (bossState == BossState.Attack2)
        {
            if (GetNormalizedTime(animator, "Attack2") >= 1)
            {

                UpdateBossState(BossState.Attack3);

            }
        }
        else if (bossState == BossState.Attack3)
        {
            if (GetNormalizedTime(animator, "Attack3") >= 1)
            {

                UpdateBossState(BossState.Attack);

            }
        }
    }

    void SetNavMeshAgent()
    {
        if (bossState == BossState.Chase)
        {
            navMeshAgent.destination = player.transform.position;
        }
        else if (bossState == BossState.Attack || bossState == BossState.Attack2 || bossState == BossState.Attack3)
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
        if (bossState == BossState.Damage)
        {
            if (GetNormalizedTime(animator, "Impact1") >= 1)
            {
                if(health == 10 || health == 5)
                {
                    UpdateBossState(BossState.PowerUp);
                }
                else
                {
                    Debug.Log(health);
                    UpdateBossState(BossState.Idle);
                }
                
            }
        }
    }

    void SetPowerUp()
    {
        if (bossState == BossState.PowerUp)
        {
            if (GetNormalizedTime(animator, "PowerUp") >= 1)
            {
                if (health == 10)
                {
                    material.color = new Color(0, 0.6496316f, 0.7529412f);
                }
                else if(health == 5)
                {
                    material.color = new Color(0.6066381f, 0, 0.7529412f);
                }
                UpdateBossState(BossState.Idle);
            }
        }
    }

    void SetDie()
    {
        if (bossState == BossState.Die)
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
        if (other.CompareTag(damageType.ToString()))
        {
            health--;
            if (health > 0)
            {
                UpdateBossState(BossState.Damage);
            }
            else
            {
                
                UpdateBossState(BossState.Die);
            }

        }
    }
}
