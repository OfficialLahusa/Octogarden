using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Scriptable Objects/DialogueLine")]
public class DialogueLine : ScriptableObject
{
    public DialogueSpeaker speaker;
    public string text;
}
