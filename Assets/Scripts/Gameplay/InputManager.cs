
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private InputActionReference clickAction;

    [Header("References")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private Camera mainCamera;

    private AnimalPiece selectedPiece;

    private void OnEnable()
    {
        clickAction.action.performed += OnClick;
        pointAction.action.Enable();
        clickAction.action.Enable();
    }

    private void OnDisable()
    {
        clickAction.action.performed -= OnClick;
        pointAction.action.Disable();
        clickAction.action.Disable();
    }

    private void OnClick(
        InputAction.CallbackContext context)
    {
        Vector2 screenPosition =
            pointAction.action.ReadValue<Vector2>();

        HandleSelection(screenPosition);
    }

    private void HandleSelection(Vector2 screenPosition)
    {
        Vector2 worldPosition =
            mainCamera.ScreenToWorldPoint(screenPosition);

        Collider2D hit =
            Physics2D.OverlapPoint(worldPosition);

        if (hit == null)
        {
            DeselectPiece();
            return;
        }

        AnimalPiece clickedPiece =
            hit.GetComponent<AnimalPiece>();

        if (clickedPiece == null)
            return;

        if (selectedPiece == null)
        {
            SelectPiece(clickedPiece);
            return;
        }

        if (clickedPiece == selectedPiece)
        {
            DeselectPiece();
            return;
        }

        int distanceX =
            Mathf.Abs(clickedPiece.X - selectedPiece.X);

        int distanceY =
            Mathf.Abs(clickedPiece.Y - selectedPiece.Y);

        if (distanceX + distanceY == 1)
        {
            boardManager.SwapPieces(
                selectedPiece,
                clickedPiece
            );

            DeselectPiece();
        }
        else
        {
            SelectPiece(clickedPiece);
        }
    }

    private void SelectPiece(AnimalPiece piece)
    {
        DeselectPiece();

        selectedPiece = piece;
        selectedPiece.SetSelected(true);
    }

    private void DeselectPiece()
    {
        if (selectedPiece != null)
        {
            selectedPiece.SetSelected(false);
            selectedPiece = null;
        }
    }
}
