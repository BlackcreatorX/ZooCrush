
using UnityEngine;

public class AnimalPiece : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }

    private Vector3 originalScale;
    private bool isSelected;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Initialize(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void UpdateGridPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        float scale = isSelected ? 1.15f : 1f;

        transform.localScale = originalScale * scale;
    }
}
