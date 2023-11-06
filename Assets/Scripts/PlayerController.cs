using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float repelDistance = -3;

    public Attack attack;
    [SerializeField] Transform resurrectionPoint;
    [SerializeField] GameObject damage_Image;
    [SerializeField] int health = 3;
    [SerializeField] CharacterController characterController;
    [SerializeField] Boss boss;
    [SerializeField] GameObject[] state_Effect;
    [SerializeField] GameObject[] health_Image;
    [SerializeField] GameObject[] alives;
    [Header("¦å¶q")]
    [SerializeField] Slider hpSlider;
    [SerializeField] Image hpImage, hpImage2;

    public UnityEvent FailHandle;

    float drag = 0.3f;
    Vector3 dampingVelocity;
    Vector3 repelDirection;
    private bool repel;

    // Start is called before the first frame update
    void Start()
    {
        if (hpSlider)
        {
            hpSlider.value = health;
            hpImage.color = new Color(0.9056604f, 0.9056604f, 0.9056604f);
            hpImage2.color = new Color(0.9056604f, 0.9056604f, 0.9056604f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(repel)
        {
            characterController.Move(repelDirection * repelDistance * Time.deltaTime);
            repelDirection = Vector3.SmoothDamp(repelDirection, Vector3.zero, ref dampingVelocity, drag);
            if (repelDirection.sqrMagnitude < 0.2f)
            {
                repelDirection = Vector3.zero;
                repel = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fire"))
        {
            attack.attackType = AttackType.FireBall;
            state_Effect[0].SetActive(true);
            state_Effect[1].SetActive(false);
            state_Effect[2].SetActive(false);
            if(hpSlider)
            {
                hpImage.color = new Color(1, 0.4531737f, 0.2122642f);
                hpImage2.color = new Color(1, 0.4531737f, 0.2122642f);
            }
        }
        else if(other.CompareTag("Water"))
        {
            attack.attackType = AttackType.WaterBall;
            state_Effect[0].SetActive(false);
            state_Effect[1].SetActive(true);
            state_Effect[2].SetActive(false);
            if (hpSlider)
            {
                hpImage.color = new Color(0.4470589f, 0.9960785f, 0.9960785f);
                hpImage2.color = new Color(0.4470589f, 0.9960785f, 0.9960785f);
            }
        }
        else if(other.CompareTag("Lightning"))
        {
            attack.attackType = AttackType.LightningBall;
            state_Effect[0].SetActive(false);
            state_Effect[1].SetActive(false);
            state_Effect[2].SetActive(true);
            if (hpSlider)
            {
                hpImage.color = new Color(0.9960785f, 0.4980392f, 0.9960785f);
                hpImage2.color = new Color(0.9960785f, 0.4980392f, 0.9960785f);
            }
        }

        if(other.CompareTag("Pitfall"))
        {
            alives[0].SetActive(false);
            alives[1].SetActive(false);
            alives[2].SetActive(false);
            alives[3].SetActive(false);
            alives[4].SetActive(false);
            alives[5].SetActive(false);
            state_Effect[0].SetActive(false);
            state_Effect[1].SetActive(false);
            state_Effect[2].SetActive(false);
            FailHandle.Invoke();
        }

        if(other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            CheckHealth();
        }
        if (other.CompareTag("EnemyWeapon"))
        {
            CheckHealth();
        }
        if (other.CompareTag("BossWeapon"))
        {
            CheckHealth_Boss();
            repelDirection = (other.transform.position - transform.position);
            repel = true;
        }

        if (other.CompareTag("Slash"))
        {
            CheckHealth_Boss();
            repelDirection = (other.transform.position - transform.position);
            repel = true;
        }

        if (other.CompareTag("BossPlace"))
        {
            boss.SetChase(true);       
        }

        if (other.CompareTag("Level1"))
        {
            GameManager.instance.ChangeScene("Level1");
        }

        if (other.CompareTag("Level2"))
        {
            GameManager.instance.ChangeScene("Level2");
        }

        if (other.CompareTag("Level3"))
        {
            GameManager.instance.ChangeScene("Level3");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("BossPlace"))
        {
            boss.SetChase(false);
        }
    }

    void CheckHealth()
    {
        if (health <= 0) { return; }

        health--;
        StartCoroutine(Damage());
        AudioManager.Instance.PlaySound("Damage");
        hpSlider.value = health;

        if (health <= 0)
        {
            alives[0].SetActive(false);
            alives[1].SetActive(false);
            alives[2].SetActive(false);
            alives[3].SetActive(false);
            alives[4].SetActive(false);
            alives[5].SetActive(false);
            state_Effect[0].SetActive(false);
            state_Effect[1].SetActive(false);
            state_Effect[2].SetActive(false);
            FailHandle.Invoke();
        }
    }

    void CheckHealth_Boss()
    {
        if (health <= 0) { return; }

        health--;
        StartCoroutine(Damage());
        AudioManager.Instance.PlaySound("Damage");

        hpSlider.value = health;

        if (health <= 0)
        {
            alives[0].SetActive(false);
            alives[1].SetActive(false);
            alives[2].SetActive(false);
            alives[3].SetActive(false);
            alives[4].SetActive(false);
            alives[5].SetActive(false);
            state_Effect[0].SetActive(false);
            state_Effect[1].SetActive(false);
            state_Effect[2].SetActive(false);
            FailHandle.Invoke();
        }
        
    }

    IEnumerator Damage()
    {
        damage_Image.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damage_Image.SetActive(false);
    }
}
