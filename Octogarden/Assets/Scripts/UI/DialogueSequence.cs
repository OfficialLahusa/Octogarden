using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Scriptable Objects/DialogueSequence")]
public class DialogueSequence : ScriptableObject
{
    public List<DialogueLine> dialogueLines;
}
