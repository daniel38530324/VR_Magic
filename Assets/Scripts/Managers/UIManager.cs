using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] Image startButton;
    [SerializeField] Image exitButton, tutorialButton, level1Button, level2Button, level3Button;
    [Header("Txet")]
    [SerializeField] Text startText;
    [SerializeField] Text exitText, tutorialText, level1Text, level2Text, level3Text;
    [Header("Sprite")]
    [SerializeField] Sprite enterSprite;
    [SerializeField] Sprite exitSprite;

    public void PointEnter(string name)
    {
        if(name == "startButton")
        {
            startButton.sprite = enterSprite;
            startButton.color = new Color(0, 0.9f, 0);
            startText.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else if(name == "exitButton")
        {
            exitButton.sprite = enterSprite;
            exitText.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else if (name == "tutorialButton")
        {
            tutorialButton.sprite = enterSprite;
            tutorialButton.color = new Color(0, 0.9f, 0);
            tutorialText.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else if (name == "level1Button")
        {
            level1Button.sprite = enterSprite;
            level1Text.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else if (name == "level2Button")
        {
            level2Button.sprite = enterSprite;
            level2Text.color = new Color(0.85f, 0.85f, 0.85f);
        }
        else if (name == "level3Button")
        {
            level3Button.sprite = enterSprite;
            level3Text.color = new Color(0.85f, 0.85f, 0.85f);
        }
    }

    public void PointExit(string name)
    {
        if (name == "startButton")
        {
            startButton.sprite = exitSprite;
            startButton.color = new Color(0, 1, 0);
            startText.color = new Color(0, 1, 0);
        }
        else if (name == "exitButton")
        {
            exitButton.sprite = exitSprite;
            exitText.color = new Color(1, 0, 0);
        }
        else if (name == "tutorialButton")
        {
            tutorialButton.sprite = exitSprite;
            tutorialButton.color = new Color(0, 1, 0);
            tutorialText.color = new Color(0, 1, 0);
        }
        else if (name == "level1Button")
        {
            level1Button.sprite = exitSprite;
            level1Text.color = new Color(0, 0.5094261f, 1);
        }
        else if (name == "level2Button")
        {
            level2Button.sprite = exitSprite;
            level2Text.color = new Color(1, 0.5058824f, 0);
        }
        else if (name == "level3Button")
        {
            level3Button.sprite = exitSprite;
            level3Text.color = new Color(1, 0, 0);
        }
    }
}
