using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeachingState
{
    Part1,
    Part2,
    Part3
}

public class TeachingManager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Object")]
    [SerializeField] GameObject teachingPoint;
    [SerializeField] GameObject part1, part2, part3;
    [SerializeField] GameObject[] supplyPoints;
    [SerializeField] GameObject[] targets;
    [SerializeField] Animator doorAnimator;


    TeachingState teachingState;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        AudioManager.Instance.PlayMusic("Main");
        UpdateTeachingState(TeachingState.Part1);
    }

    public void UpdateTeachingState(TeachingState newState)
    {
        teachingState = newState;

        switch(teachingState)
        {
            case TeachingState.Part1:
                part1.SetActive(true);
                teachingPoint.SetActive(true);
                break;
            case TeachingState.Part2:
                part1.SetActive(false);
                part2.SetActive(true);
                teachingPoint.SetActive(false);
                foreach(GameObject item in targets)
                {
                    item.SetActive(true);
                }
                foreach (GameObject item in supplyPoints)
                {
                    item.SetActive(true);
                }
                break;
            case TeachingState.Part3:
                doorAnimator.SetTrigger("Open");
                break;
        }
    }
}
