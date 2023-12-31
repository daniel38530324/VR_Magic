using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Space(20)]
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject success_UI, fail_UI;
    [SerializeField] Animator result_Animator;
    [SerializeField] CharacterController characterController;
    [SerializeField] BoxCollider doorTrigger;

    bool success, fail;
    int desyoryNum = 0;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
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

    public void CheckLevel()
    {
        desyoryNum++;
        if(desyoryNum >= 6)
        {
            doorAnimator.SetTrigger("Open");
            Success();
            doorTrigger.enabled = true;
        }
    }

    public void Success()
    {
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
        characterController.enabled = false;
        success = false;
        fail = true;
    }

    void SetSuccess()
    {
        if (GetNormalizedTime(result_Animator, "Success") >= 1)
        {
            success_UI.SetActive(false);
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
