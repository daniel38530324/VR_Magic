using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turrent : MonoBehaviour
{
    [SerializeField] Level2Manager level2Manager;
    [SerializeField] Transform player, spawnPoint;
    [SerializeField] GameObject enemyBullet;
    [SerializeField] float range = 30;
    [SerializeField] AttackType damageType = AttackType.FireBall;

    float timer = 0;
    bool trigger = false;

    // Update is called once per frame
    void Update()
    {
        if(trigger)
        {
            //transform.LookAt(player);
            Vector3 lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(lookPos);

            timer += Time.deltaTime;
            if(timer >= 1.5f)
            {
                timer = 0;
                Instantiate(enemyBullet, spawnPoint.position, spawnPoint.rotation);
                AudioManager.Instance.PlaySound("Laser");
            }
        }

        if((player.transform.position - transform.position).sqrMagnitude <= range)
        {
            trigger = true;
        }
        else
        {
            trigger = false;
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(damageType.ToString()))
        {
            level2Manager.CheckLevel();
            Destroy(gameObject.transform.parent.gameObject);
            
        }
    }
}
