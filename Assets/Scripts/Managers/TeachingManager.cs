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
    [SerializeField] GameObject success_UI, fail_UI;
    [SerializeField] Animator result_Animator;
    [SerializeField] BoxCollider doorTrigger;

    bool success, fail, successCount = true;
    TeachingState teachingState;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateTeachingState(TeachingState.Part1);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Main");
        
    }

    private void Update()
    {
        if (success)
        {
            //SetSuccess();
        }
        else if (fail)
        {
            SetFail();
        }
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
                Success();
                break;
            case TeachingState.Part3:
                doorAnimator.SetTrigger("Open");
                Success();
                successCount = false;
                doorTrigger.enabled = true;
                break;
        }
    }

    public void Success()
    {
        if (!successCount) { return; }
        success_UI.SetActive(true);
        result_Animator.CrossFadeInFixedTime("Success", 0.1f);
        AudioManager.Instance.PlaySound("Success");
        success = true;
        fail = false;
    }

    public void Fail()
    {
        fail_UI.SetActive(true);
        result_Animator.CrossFadeInFixedTime("Fail", 0.1f);
        AudioManager.Instance.Stop();
        AudioManager.Instance.PlaySound("Fail");
        success = false;
        fail = true;
    }

    void SetSuccess()
    {
        if (GetNormalizedTime(result_Animator, "Success") >= 1)
        {
            success_UI.SetActive(false);
            success = false;
        }
    }

    void SetFail()
    {
        if (GetNormalizedTime(result_Animator, "Fail") >= 1)
        {
            GameManager.instance.ReturnScene();
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
}
