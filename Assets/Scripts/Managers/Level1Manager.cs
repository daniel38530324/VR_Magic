using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  enum Level1State
{
    Part0,
    Part1,
    Part2,
    Part3,
    Part4,
}
public class Level1Manager : MonoBehaviour
{
    public static int currentState = 0;
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Space(20)]
    [SerializeField] Transform player;
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject success_UI, fail_UI;
    [SerializeField] Animator result_Animator;
    [SerializeField] BoxCollider doorTrigger;
    [SerializeField] GameObject state_Effect;
    [SerializeField] Transform[] returnPoints;
    [SerializeField] GameObject[] targets, barriers, returnCubes;

    Level1State level1State;
    bool success, fail;
    int desyoryNum = 0;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        UpdateLevel1State((Level1State)currentState);
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

    public void UpdateLevel1State(Level1State newState)
    {
        level1State = newState;

        switch (level1State)
        {
            case Level1State.Part0:
                player.GetComponent<CharacterController>().enabled = false;
                player.position = returnPoints[0].position;
                player.GetComponent<CharacterController>().enabled = true;
                desyoryNum = 0;
                break;
            case Level1State.Part1:
                player.GetComponent<CharacterController>().enabled = false;
                player.position = returnPoints[1].position;
                player.GetComponent<CharacterController>().enabled = true;
                for(int i = 0; i < 1; i++)
                {
                    targets[i].SetActive(false);
                    barriers[i].SetActive(false);
                    returnCubes[i].SetActive(false);
                }
                desyoryNum = 1;
                player.GetComponent<PlayerController>().attack.attackType = AttackType.FireBall;
                state_Effect.SetActive(true);
                break;
            case Level1State.Part2:
                player.GetComponent<CharacterController>().enabled = false;
                player.position = returnPoints[2].position;
                player.GetComponent<CharacterController>().enabled = true;
                for (int i = 0; i < 2; i++)
                {
                    targets[i].SetActive(false);
                    barriers[i].SetActive(false);
                    returnCubes[i].SetActive(false);
                }
                desyoryNum = 2;
                player.GetComponent<PlayerController>().attack.attackType = AttackType.FireBall;
                state_Effect.SetActive(true);
                break;
            case Level1State.Part3:
                player.GetComponent<CharacterController>().enabled = false;
                player.position = returnPoints[3].position;
                player.GetComponent<CharacterController>().enabled = true;
                for (int i = 0; i < 3; i++)
                {
                    targets[i].SetActive(false);
                    barriers[i].SetActive(false);
                    returnCubes[i].SetActive(false);
                }
                desyoryNum = 3;
                player.GetComponent<PlayerController>().attack.attackType = AttackType.FireBall;
                state_Effect.SetActive(true);
                break;
            case Level1State.Part4:
                player.GetComponent<CharacterController>().enabled = false;
                player.position = returnPoints[4].position;
                player.GetComponent<CharacterController>().enabled = true;
                for (int i = 0; i < 4; i++)
                {
                    targets[i].SetActive(false);
                    barriers[i].SetActive(false);
                    returnCubes[i].SetActive(false);
                }
                desyoryNum = 4;
                CheckLevel();
                player.GetComponent<PlayerController>().attack.attackType = AttackType.FireBall;
                state_Effect.SetActive(true);
                break;
        }
    }

    public void CheckLevel()
    {
        desyoryNum++;
        if (desyoryNum >= 4)
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
        success_UI.SetActive(false);
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
