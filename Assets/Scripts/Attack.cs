using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Null,
    FireBall,
    WaterBall,
    LightningBall
}


public class Attack : MonoBehaviour
{
    public AttackType attackType = AttackType.Null;
    [SerializeField] GameObject[] spawnBullet;

    GameObject bullet;
    bool stay;

    // Update is called once per frame
    void Update()
    {
        if(bullet == null)
        {
            stay = false;
        }

        if (stay)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
        }


    }

    public void Generate()
    {
        if (stay) return;
        if (attackType == AttackType.Null) return;

        if (attackType == AttackType.FireBall)
        {
            bullet = Instantiate(spawnBullet[0], transform.position, transform.rotation);
            AudioManager.Instance.PlaySound("FireBall");
        }
        else if (attackType == AttackType.WaterBall)
        {
            bullet = Instantiate(spawnBullet[1], transform.position, transform.rotation);
            AudioManager.Instance.PlaySound("WaterBall");
        }
        else if (attackType == AttackType.LightningBall)
        {
            bullet = Instantiate(spawnBullet[2], transform.position, transform.rotation);
            AudioManager.Instance.PlaySound("LightingBall");
        }

        stay = true;

    }

    public void Shoot()
    {
        if (!stay) return;
        StartCoroutine(ShootHandle());
    }

    IEnumerator ShootHandle()
    {
        yield return new WaitForSeconds(0.3f);
        stay = false;
        bullet.GetComponent<Bullet>().shoot = true;
    }
}
