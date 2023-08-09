using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Attack attack;
    [SerializeField] Transform resurrectionPoint;
    [SerializeField] GameObject damage_Image;
    [SerializeField] GameObject[] state_Effect;
    [SerializeField] GameObject[] health_Image;
    int health = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fire"))
        {
            attack.attackType = AttackType.FireBall;
            state_Effect[0].SetActive(true);
            state_Effect[1].SetActive(false);
            state_Effect[2].SetActive(false);
        }
        else if(other.CompareTag("Water"))
        {
            attack.attackType = AttackType.WaterBall;
            state_Effect[0].SetActive(false);
            state_Effect[1].SetActive(true);
            state_Effect[2].SetActive(false);
        }
        else if(other.CompareTag("Lightning"))
        {
            attack.attackType = AttackType.LightningBall;
            state_Effect[0].SetActive(false);
            state_Effect[1].SetActive(false);
            state_Effect[2].SetActive(true);
        }

        if(other.CompareTag("Pitfall"))
        {
            GameManager.instance.ReturnScene();
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

        if(other.CompareTag("Level1"))
        {
            GameManager.instance.ChangeScene("Game 1");
        }

        if (other.CompareTag("Level2"))
        {
            GameManager.instance.ChangeScene("Game 2");
        }

    }

    void CheckHealth()
    {
        health--;
        StartCoroutine(Damage());
        if (health == 2)
        {
            health_Image[0].SetActive(false);
        }
        else if(health == 1)
        {
            health_Image[1].SetActive(false);
        }
        if(health <= 0)
        {
            GameManager.instance.ReturnScene();
        }
    }

    IEnumerator Damage()
    {
        damage_Image.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        damage_Image.SetActive(false);
    }
}
