
using System.Collections.Generic;
using UnityEngine;

public class MatchDetector : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;

    public List<AnimalPiece> FindMatches()
    {
        HashSet<AnimalPiece> matches = new HashSet<AnimalPiece>();

        // DETECCIÓN HORIZONTAL
        for (int y = 0; y < boardManager.Height; y++)
        {
            int x = 0;

            while (x < boardManager.Width)
            {
                int startX = x;
                int type = boardManager.GetPieceType(x, y);

                while (x < boardManager.Width &&
                       boardManager.GetPieceType(x, y) == type)
                {
                    x++;
                }

                int length = x - startX;

                if (length >= 3)
                {
                    for (int i = startX; i < x; i++)
                    {
                        matches.Add(boardManager.GetPiece(i, y));
                    }
                }
            }
        }

        // DETECCIÓN VERTICAL
        for (int x = 0; x < boardManager.Width; x++)
        {
            int y = 0;

            while (y < boardManager.Height)
            {
                int startY = y;
                int type = boardManager.GetPieceType(x, y);

                while (y < boardManager.Height &&
                       boardManager.GetPieceType(x, y) == type)
                {
                    y++;
                }

                int length = y - startY;

                if (length >= 3)
                {
                    for (int i = startY; i < y; i++)
                    {
                        matches.Add(boardManager.GetPiece(x, i));
                    }
                }
            }
        }

        return new List<AnimalPiece>(matches);
    }

    [ContextMenu("Test Match Detection")]
    private void TestMatchDetection()
    {
        List<AnimalPiece> matches = FindMatches();

        Debug.Log($"ZooCrush: {matches.Count} piezas detectadas.");
    }
}
