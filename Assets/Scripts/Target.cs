using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    [SerializeField] GameObject barrier;
    // Start is called before the first frame update
    private void OnDestroy()
    {
        Destroy(barrier);
        level1Manager.CheckLevel();
    }
}
