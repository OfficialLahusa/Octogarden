using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Scriptable Objects/DialogueLine")]
public class DialogueLine : ScriptableObject
{
    public enum Speaker
    {
        MainCharacter,
        OldMan,
        Mother,
        Father
    }
    public Speaker speaker;
    public string text;
}
