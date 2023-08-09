using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingPoint : MonoBehaviour
{
    [SerializeField] TeachingManager teachingManager;
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
        if(other.CompareTag("Player"))
        {
            teachingManager.UpdateTeachingState(TeachingState.Part2);
        }
    }
}
