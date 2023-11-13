using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("Main");
        Level1Manager.currentState = 0;
        PlayerController.bossChase = false;
    }
}
