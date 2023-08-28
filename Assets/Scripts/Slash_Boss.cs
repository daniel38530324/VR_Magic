using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash_Boss : MonoBehaviour
{
    [SerializeField] float speed = -6;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
