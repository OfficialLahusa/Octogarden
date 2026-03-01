using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [SerializeField]
    TMP_Text actorText;
    [SerializeField]
    TMP_Text contentText;

    private RectTransform _rectTransform;
    private float _initialPosY;
    private float _currentOffsetY;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _initialPosY = _rectTransform.anchoredPosition.y;
        _currentOffsetY = 0f;
    }

    void Update()
    {
        _currentOffsetY += 2000f * Time.unscaledDeltaTime; // Move up at a speed of 2000 units per second
        if (_currentOffsetY > 0)
        {
            _currentOffsetY = 0f; // Limit the upward movement
        }

        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _initialPosY + _currentOffsetY);
    }

    public void SetDialogue(DialogueLine dialogueLine)
    {
        actorText.text = dialogueLine.speaker switch
        {
            DialogueLine.Speaker.MainCharacter => "You",
            DialogueLine.Speaker.OldMan => "Master Monty",
            DialogueLine.Speaker.Father => "Lord of the Sea",
            DialogueLine.Speaker.Mother => "Lady of the Sea",
            _ => "Unknown"
        };
        contentText.text = dialogueLine.text;

        _currentOffsetY = -2000f;
    }
}
