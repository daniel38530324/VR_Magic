using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool shoot;

    [SerializeField] float speed = -5;
    [SerializeField] GameObject explosion;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            timer += Time.deltaTime;
            transform.Translate(0, speed * Time.deltaTime, 0);
        }

        if(timer >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            AudioManager.Instance.PlaySound("Boom");
            Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
            Instantiate(explosion, closestPoint, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Collider"))
        {
            AudioManager.Instance.PlaySound("Boom");
            Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
            Instantiate(explosion, closestPoint, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySound("Boom");
            Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
            Instantiate(explosion, closestPoint, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
