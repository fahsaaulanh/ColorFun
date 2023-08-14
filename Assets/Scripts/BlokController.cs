using UnityEngine;
using UnityEngine.Events;

public class BlokController : MonoBehaviour
{
    public static UnityAction<int, Color> OnBlokColorChange;

    Color selfColor;
    SpriteRenderer spriteRenderer;
    private int id;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selfColor = Color.white;
    }

    public void OnMouseDown()
    {
        if (GameManager.gameState == GameManager.GameState.none)
        {
            selfColor = GameManager.currentColor.color;
            spriteRenderer.color = selfColor;
            OnBlokColorChange?.Invoke(id, selfColor);
        }
    }

    public void SetId(int selfId)
    {
        id = selfId;
    }

    public void SetSelfColor(Color _selfColor)
    {
        spriteRenderer.color = _selfColor;
    }
}
