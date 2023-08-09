using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Space(20)]
    [SerializeField] Animator doorAnimator;

    int desyoryNum = 0;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }

    public void CheckLevel()
    {
        desyoryNum++;
        if(desyoryNum >= 6)
        {
            doorAnimator.SetTrigger("Open");
        }
    }

}
