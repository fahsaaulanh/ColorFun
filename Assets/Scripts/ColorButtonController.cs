using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorButtonController : MonoBehaviour
{
    public static UnityAction OnActivateColorClick;
    [SerializeField] public ColorPalatteData.Colors palatteData;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite offSprite;

    public bool isActive;

    private void Start()
    {
        isActive = false;
        var button = GetComponent<Button>();
        button.onClick.AddListener(ActivateCurrentColor);
    }

    private void OnEnable()
    {
        OnActivateColorClick += CheckCurrentColorState;
    }

    private void OnDisable()
    {
        OnActivateColorClick -= CheckCurrentColorState;
    }

    private void ActivateCurrentColor()
    {
        GameManager.currentState = palatteData.state;
        GameManager.currentColor = palatteData;
        CheckCurrentColorState();
        OnActivateColorClick?.Invoke();
    }

    public void OnChangedColorPicker(Color _color)
    {
        palatteData.color = _color;
        if (isActive)
        {
            GameManager.currentColor = palatteData;
        }
    }

    private void CheckCurrentColorState()
    {
        if(GameManager.currentState == palatteData.state)
        {
            isActive = true;
            gameObject.GetComponent<Transform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            gameObject.GetComponent<Image>().sprite = activeSprite;
        }
        else
        {
            isActive = false;
            gameObject.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            gameObject.GetComponent<Image>().sprite = offSprite;
        }
    }
}
