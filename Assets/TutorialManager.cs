using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public string instructionText;
        public RectTransform highlightTransform;
        public Button buttonToClick;
        public itemsToCheck itemToCheck;

        public int itemLimit;
        public bool checkLimitToProgress;
        public bool nextBtnActive;
        public bool blockOtherButtons;
    }

    public enum itemsToCheck
    {
        none,
        bean,
        coffee
    }

    public GameManager gamemanager;
    public CanvasGroup canvasGroup;
    public RectTransform highlightPanel;
    public GameObject welcomePanel;
    public GameObject tutorialTextBackground;
    public Button nextBtn;
    public TMP_Text instructionText;
    public bool tutorialFinished;
    public List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    //public GameObject overlayPanel;

    private int currentStepIndex = -1;

    void Start()
    {
        highlightPanel.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
        tutorialTextBackground.SetActive(false);
        //if(!gamemanager.finishedTutorial)
        //{
        //    welcomePanel.SetActive(true);
        //}
        //nextBtn.onClick.AddListener(NextStep);
    }

    void Update()
    {
        if (currentStepIndex > -1 && currentStepIndex < tutorialSteps.Count)
        {
            TutorialStep currStep = tutorialSteps[currentStepIndex];
            if (currStep.checkLimitToProgress)
            {
                if (currStep.itemToCheck == itemsToCheck.bean)
                {
                    if (gamemanager.beanCnt >= currStep.itemLimit)
                    {
                        NextStep();
                    }
                }
                else if (currStep.itemToCheck == itemsToCheck.coffee)
                {
                    if (gamemanager.coffee >= currStep.itemLimit)
                    {
                        NextStep();
                    }
                }
            }
        }
    }

    public void showWelcomePanel()
    {
        welcomePanel.SetActive(true);
    }

    public void StartTutorial()
    {
        highlightPanel.gameObject.SetActive(true);
        tutorialTextBackground.SetActive(true);
        instructionText.gameObject.SetActive(true);
        welcomePanel.SetActive(false);
        NextStep();
    }

    public void SkipTutorial()
    {
        welcomePanel.SetActive(false);
        tutorialFinished = true;
        gamemanager.finishedTutorial = true;
    }

    public void NextStep()
    {
        currentStepIndex++;
        Debug.Log("NextStep");
        Debug.Log("Showing step: " + (currentStepIndex + 1));
        if (currentStepIndex < tutorialSteps.Count)
        {
            ShowStep(tutorialSteps[currentStepIndex]);
        }
        else
        {
            EndTutorial();
        }
    }

    void ShowStep(TutorialStep step)
    {
        Debug.Log("ShowStep");
        instructionText.text = step.instructionText;
        if (step.highlightTransform)
        {
            highlightPanel.gameObject.SetActive(true);
            highlightPanel.position = step.highlightTransform.position;
            highlightPanel.sizeDelta = step.highlightTransform.sizeDelta;
        }
        else
        {
            highlightPanel.gameObject.SetActive(false);
        }
        if (step.buttonToClick)
        {
            // Remove any existing listeners
            step.buttonToClick.onClick.RemoveAllListeners();
            // Add new listener
            step.buttonToClick.onClick.AddListener(OnButtonClick);
        }
        if (step.blockOtherButtons)
        {
            DisableAllButtonRaycasts();
            EnableRaycastTarget(step.buttonToClick);
            if (step.nextBtnActive)
            {
                EnableRaycastTarget(nextBtn);
            }
        }
        else
        {
            EnableAllButtonRaycasts();
        }
        nextBtn.gameObject.SetActive(step.nextBtnActive);
        if (step.nextBtnActive)
        {
            EnableRaycastTarget(nextBtn);
        }
    }

    void OnButtonClick()
    {
        Debug.Log("OnButtonClick");
        tutorialSteps[currentStepIndex].buttonToClick.onClick.RemoveListener(OnButtonClick);
        NextStep();
    }

    void EndTutorial()
    {
        tutorialTextBackground.SetActive(false);
        instructionText.gameObject.SetActive(false);
        highlightPanel.gameObject.SetActive(false);
        EnableAllButtonRaycasts();
        tutorialFinished = true;
        gamemanager.finishedTutorial = true;
    }

    void DisableAllButtonRaycasts()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            button.GetComponent<Graphic>().raycastTarget = false;
            Graphic[] graphics = button.GetComponentsInChildren<Graphic>();
            foreach (Graphic graphic in graphics)
            {
                graphic.raycastTarget = false;
            }
        }
    }

    void EnableAllButtonRaycasts()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button button in allButtons)
        {
            button.GetComponent<Graphic>().raycastTarget = true;
            Graphic[] graphics = button.GetComponentsInChildren<Graphic>();
            foreach (Graphic graphic in graphics)
            {
                graphic.raycastTarget = true;
            }
        }
    }

    void EnableRaycastTarget(Button button)
    {
        if (button != null)
        {
            button.GetComponent<Graphic>().raycastTarget = true;
            Graphic[] graphics = button.GetComponentsInChildren<Graphic>();
            foreach (Graphic graphic in graphics)
            {
                graphic.raycastTarget = true;
            }
        }
    }
}
