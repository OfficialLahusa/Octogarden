using UnityEngine;

public class MeleeIndicator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private static readonly float _timeToLiveSeconds = 0.25f;
    private float _lifetime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lifetime += Time.deltaTime;

        if (_lifetime >= _timeToLiveSeconds)
        {
            Destroy(gameObject);
        }
        else
        {
            float alpha = 1f - (_lifetime / _timeToLiveSeconds);
            _spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
        }
    }
}
