using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueScreen : MonoBehaviour
{
    [SerializeField]
    GameObject mainCharacter;
    [SerializeField]
    GameObject oldMan;
    [SerializeField]
    GameObject father;
    [SerializeField]
    GameObject mother;

    [SerializeField]
    DialogueBox rightDialogueBox;

    private List<DialogueLine> _remainingDialogueLines;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            NextLine();
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SetDialogueSequence(Resources.Load<DialogueSequence>("Scriptable Objects/Dialogues/Sample Sequence/Sample Sequence"));
        }
    }

    public void SetDialogueSequence(DialogueSequence dialogueSequence)
    {
        _remainingDialogueLines = new List<DialogueLine>(dialogueSequence.dialogueLines);

        NextLine();
    }

    public void NextLine()
    {
        if (_remainingDialogueLines.Count == 0)
        {
            Time.timeScale = 1f;
            Hide();
        }
        else
        {
            Time.timeScale = 0f;
            Unhide();
            SetDialogueLine(_remainingDialogueLines[0]);
            _remainingDialogueLines.RemoveAt(0);
        }
    }

    private void SetDialogueLine(DialogueLine dialogueLine)
    {
        mainCharacter.SetActive(dialogueLine.speaker == DialogueLine.Speaker.MainCharacter);
        oldMan.SetActive(dialogueLine.speaker == DialogueLine.Speaker.OldMan);
        father.SetActive(dialogueLine.speaker == DialogueLine.Speaker.Father);
        mother.SetActive(dialogueLine.speaker == DialogueLine.Speaker.Mother);
        rightDialogueBox.SetDialogue(dialogueLine);
    }

    private void Hide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Unhide()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
