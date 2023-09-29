using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum BossState
{
    Idle,
    Chase,
    Attack,
    Attack2,
    Attack3,
    Attack4,
    Damage,
    PowerUp,
    Die,
}

public class Boss : MonoBehaviour
{
    [SerializeField] Level3Manager level3Manager;
    [SerializeField] private GameObject player, weapon, weapon2, sword_Fire, sword_Water, sword_Lighting, slashPoint;
    [SerializeField] private float playerChangeRange, playerAttackRange;
    [SerializeField] Material material, floor;
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject hint_UI;
    [SerializeField] GameObject[] slash_Effect;
    [SerializeField] GameObject[] aura_Effect;
    [SerializeField] GameObject[] attackRange;
    [SerializeField] GameObject[] slashShock_Effect;
    [Header("¦å¶q")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Text hpText;
    [SerializeField] Image hpImage;
    [SerializeField] Sprite[] hpSprites;

    private BossState bossState;
    private Animator animator;
    private float playerDistanceSqr;
    private float verticalVelocity;
    private NavMeshAgent navMeshAgent;
    private int health = 15;
    private AttackType damageType = AttackType.WaterBall;
    private bool isChase, attack3State;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        UpdateBossState(BossState.Idle);
        material.color = new Color(0.7529412f, 0, 0.09143171f);
        floor.color = Color.red;
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        hpSlider.value = health;
        hpImage.sprite = hpSprites[0];
        hpText.color = new Color(0.9960785f, 0.3921569f, 0.1294118f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChase)
        {
            IsInRange();
            SetAttack();          
            SetNavMeshAgent();
            SetVerticalVelocity();
        }
        else
        {
            if(navMeshAgent.enabled == true)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.enabled = false;
            }
        }

        SetDamage();
        SetPowerUp();
        SetDie();
    }

    public void UpdateBossState(BossState newState)
    {
        bossState = newState;

        switch (bossState)
        {
            case BossState.Idle:
                animator.CrossFadeInFixedTime("Idle", 0.1f);
                DisableWeapon();
                DisableAttackRange();
                break;
            case BossState.Chase:
                animator.CrossFadeInFixedTime("Walk", 0.1f);
                DisableAttackRange();
                break;
            case BossState.Attack:
                animator.CrossFadeInFixedTime("Attack1", 0.1f);
                transform.LookAt(player.transform.position);
                player.GetComponent<PlayerController>().repelDistance = -1;
                DisableAttackRange();
                break;
            case BossState.Attack2:
                animator.CrossFadeInFixedTime("Attack2", 0.1f);
                transform.LookAt(player.transform.position);
                player.GetComponent<PlayerController>().repelDistance = -2;
                DisableAttackRange();
                break;
            case BossState.Attack3:
                animator.CrossFadeInFixedTime("Attack3", 0.1f);
                transform.LookAt(player.transform.position);
                player.GetComponent<PlayerController>().repelDistance = -3;
                break;
            case BossState.Attack4:
                transform.LookAt(player.transform.position);
                animator.CrossFadeInFixedTime("Attack4", 0.1f);
                player.GetComponent<PlayerController>().repelDistance = -3;
                break;
            case BossState.Damage:
                animator.CrossFadeInFixedTime("Impact1", 0.1f);
                break;
            case BossState.PowerUp:
                animator.CrossFadeInFixedTime("PowerUp", 0.1f);
                DisableAttackRange();
                break;
            case BossState.Die:
                animator.CrossFadeInFixedTime("Death", 0.1f);
                DisableAttackRange();
                break;
        }
    }

    void IsInRange()
    {
        playerDistanceSqr = (player.transform.position - transform.position).sqrMagnitude;
        if ((playerDistanceSqr <= playerChangeRange * playerChangeRange) && (playerDistanceSqr > playerAttackRange * playerAttackRange))
        {
            if (bossState == BossState.Idle)
            {
                int num = Random.Range(0, 7);
                if (num < 4)
                {
                    UpdateBossState(BossState.Chase);
                }
                else if(num >= 4)
                {
                    int num2 = Random.Range(0, 2);
                    if(num2 == 0)
                    {
                        UpdateBossState(BossState.Attack3);
                    }
                    else
                    {
                        UpdateBossState(BossState.Attack4);
                    }  
                }
            }
        }
        else if (playerDistanceSqr <= playerAttackRange * playerAttackRange)
        {
            if (bossState == BossState.Chase || bossState == BossState.Idle)
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
                if ((playerDistanceSqr <= playerChangeRange * playerChangeRange) && (playerDistanceSqr > 5 * 5))
                {
                    UpdateBossState(BossState.Idle);
                }
                else if (playerDistanceSqr <= 5 * 5)
                {
                    UpdateBossState(BossState.Attack2);
                }
            }
        }
        else if (bossState == BossState.Attack2)
        {
            if (GetNormalizedTime(animator, "Attack2") >= 1)
            {
                if ((playerDistanceSqr <= playerChangeRange * playerChangeRange) && (playerDistanceSqr > playerAttackRange * playerAttackRange))
                {
                    UpdateBossState(BossState.Idle);
                }
                else if (playerDistanceSqr <= playerAttackRange * playerAttackRange)
                {
                    UpdateBossState(BossState.Attack3);
                }
            }
        }
        else if (bossState == BossState.Attack3)
        {
            if(attack3State)
            {
                Move((transform.forward * 40));
            } 

            if (GetNormalizedTime(animator, "Attack3") >= 1)
            {
                if ((playerDistanceSqr <= playerChangeRange * playerChangeRange) && (playerDistanceSqr > playerAttackRange * playerAttackRange))
                {
                    UpdateBossState(BossState.Idle);
                }
                else if (playerDistanceSqr <= playerAttackRange * playerAttackRange)
                {
                    UpdateBossState(BossState.Attack);
                }
            }
        }
        else if (bossState == BossState.Attack4)
        {
            if (GetNormalizedTime(animator, "Attack4") >= 1)
            {
                if ((playerDistanceSqr <= playerChangeRange * playerChangeRange) && (playerDistanceSqr > playerAttackRange * playerAttackRange))
                {
                    UpdateBossState(BossState.Idle);
                }
                else if (playerDistanceSqr <= playerAttackRange * playerAttackRange)
                {
                    UpdateBossState(BossState.Attack);
                }
            }
        }
    }

    void SetNavMeshAgent()
    {
        if (bossState == BossState.Chase)
        {
            transform.LookAt(player.transform.position);
            navMeshAgent.enabled = true;
            if (navMeshAgent.isOnNavMesh)
            {
                navMeshAgent.destination = player.transform.position;
                Move(navMeshAgent.desiredVelocity.normalized * 3.5f);
            }
            navMeshAgent.velocity = characterController.velocity;
        }
        else if (bossState == BossState.Attack || bossState == BossState.Attack2 || bossState == BossState.Attack3)
        {
            if (navMeshAgent.enabled == true)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.enabled = false;
            }
            //transform.LookAt(player.transform.position);
        }
        else
        {
            if (navMeshAgent.enabled == true)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.enabled = false;
            }
        }
    }

    void Move(Vector3 motion)
    {
        characterController.Move((motion + (Vector3.up * verticalVelocity))* Time.deltaTime);
    }

    void SetVerticalVelocity()
    {
        if (verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
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
                    UpdateBossState(BossState.Idle);
                }
                
            }
        }
    }

    void SetPowerUp()
    {
        if (bossState == BossState.PowerUp)
        {
            if (GetNormalizedTime(animator, "PowerUp") >= 0.5f && GetNormalizedTime(animator, "PowerUp") < 1)
            {
                if (health == 10)
                {
                    material.color = new Color(0, 0.6496316f, 0.7529412f);
                    sword_Fire.SetActive(false);
                    sword_Water.SetActive(true);
                    floor.color = new Color(0, 0.7059705f, 1);
                    damageType = AttackType.LightningBall;
                    aura_Effect[0].SetActive(false);
                    aura_Effect[1].SetActive(true);
                    hpImage.sprite = hpSprites[1];
                    hpText.color = new Color(0.4470589f, 0.9960785f, 0.9960785f);
                }
                else if(health == 5)
                {
                    material.color = new Color(0.6066381f, 0, 0.7529412f);
                    sword_Water.SetActive(false);
                    sword_Lighting.SetActive(true);
                    floor.color = new Color(0.6134043f, 0, 1);
                    damageType = AttackType.FireBall;
                    aura_Effect[1].SetActive(false);
                    aura_Effect[2].SetActive(true);
                    hpImage.sprite = hpSprites[2];
                    hpText.color = new Color(0.9960785f, 0.4980392f, 0.9960785f);
                }
                
            }
            else if (GetNormalizedTime(animator, "PowerUp") >= 1)
            {
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
                level3Manager.Success();
                Destroy(gameObject, 5);
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

    public void SetChase(bool chase)
    {
        if(chase)
        {
            isChase = true;
        }
        else
        {
            isChase = false;
            if(bossState == BossState.PowerUp || bossState == BossState.Damage || bossState == BossState.Die) { return; }

            UpdateBossState(BossState.Idle);
        }
    }

    public void EnableWeapon()
    {
        if(health > 10)
        {
            slash_Effect[0].SetActive(true);
        }
        else if(health <= 10 && health > 5)
        {
            slash_Effect[1].SetActive(true);
        }
        else if (health <= 5)
        {
            slash_Effect[2].SetActive(true);
        }

        if (bossState == BossState.Attack2)
        {
            weapon2.SetActive(true);
            return;
        }
        weapon.SetActive(true);
    }

    public void DisableWeapon()
    {
        weapon.SetActive(false);
        weapon2.SetActive(false);
        slash_Effect[0].SetActive(false);
        slash_Effect[1].SetActive(false);
        slash_Effect[2].SetActive(false);
    }

    public void EnableAttack3()
    {
        AudioManager.Instance.PlaySound("Attack3");
        attack3State = true;
        weapon.SetActive(true);
        if (health > 10)
        {
            slash_Effect[0].SetActive(true);
        }
        else if (health <= 10 && health > 5)
        {
            slash_Effect[1].SetActive(true);
        }
        else if (health <= 5)
        {
            slash_Effect[2].SetActive(true);
        }
    }

    public void DisableAttack3()
    {
        attack3State = false;
        weapon.SetActive(false);
        slash_Effect[0].SetActive(false);
        slash_Effect[1].SetActive(false);
        slash_Effect[2].SetActive(false);
    }

    public void EnableAttackRange()
    {
        attackRange[0].SetActive(true);
    }

    public void DisableAttackRange()
    {
        attackRange[0].SetActive(false);
    }

    public void EnableSlash()
    {
        AudioManager.Instance.PlaySound("Attack4");
        if (health > 10)
        {
            Instantiate(slashShock_Effect[0], slashPoint.transform.position, slashPoint.transform.rotation);
        }
        else if (health <= 10 && health > 5)
        {
            Instantiate(slashShock_Effect[1], slashPoint.transform.position, slashPoint.transform.rotation);
        }
        else if (health <= 5)
        {
            Instantiate(slashShock_Effect[2], slashPoint.transform.position, slashPoint.transform.rotation);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(damageType.ToString()))
        {
            if (bossState == BossState.PowerUp || bossState == BossState.Die) { return; }

            health--;
            hpSlider.value = health;

            if (health > 0)
            {
                if (health == 10 || health == 5)
                {
                    UpdateBossState(BossState.Damage);
                }

                if (bossState == BossState.Attack || bossState == BossState.Attack2 || bossState == BossState.Attack3 || bossState == BossState.Attack4)
                {
                    return;
                }
                else
                {
                    UpdateBossState(BossState.Damage);
                }
            }
            else
            {
                UpdateBossState(BossState.Die);
                aura_Effect[0].SetActive(false);
                aura_Effect[1].SetActive(false);
                aura_Effect[2].SetActive(false);
            }

        }
        else if (other.CompareTag("FireBall") || other.CompareTag("WaterBall") || other.CompareTag("LightningBall"))
        {
            if (hint_UI.activeInHierarchy) { return; }
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
