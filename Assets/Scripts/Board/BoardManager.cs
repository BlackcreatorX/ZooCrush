
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board Settings")]
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float cellSize = 0.9f;
    public int Width => width;
    public int Height => height;

    [Header("Animal Prefabs")]
    [SerializeField] private GameObject[] animalPrefabs;

    private GameObject[,] board;
    private int[,] boardTypes;

    private void Start()
    {
        GenerateBoard();
    }

    private void GenerateBoard()
    {
        board = new GameObject[width, height];
        boardTypes = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int animalIndex;

                do
                {
                    animalIndex = Random.Range(0, animalPrefabs.Length);
                }
                while (CreatesMatch(x, y, animalIndex));

                boardTypes[x, y] = animalIndex;

                Vector2 position = new Vector2(
                    x * cellSize - (width - 1) * cellSize / 2f,
                    y * cellSize - (height - 1) * cellSize / 2f
                );

                GameObject animal = Instantiate(
                    animalPrefabs[animalIndex],
                    position,
                    Quaternion.identity,
                    transform
                );

                animal.name = $"Animal_{x}_{y}";

                board[x, y] = animal;
                AnimalPiece piece = animal.GetComponent<AnimalPiece>();
                piece.Initialize(x, y);
            }
        }

        Debug.Log("ZooCrush: Board generated!");
    }

    private bool CreatesMatch(int x, int y, int type)
    {
        // Check horizontal matches
        if (x >= 2)
        {
            if (boardTypes[x - 1, y] == type &&
                boardTypes[x - 2, y] == type)
            {
                return true;
            }
        }

        // Check vertical matches
        if (y >= 2)
        {
            if (boardTypes[x, y - 1] == type &&
                boardTypes[x, y - 2] == type)
            {
                return true;
            }
        }

        return false;
    }
    
public void SwapPieces(AnimalPiece pieceA, AnimalPiece pieceB)
{
    int ax = pieceA.X;
    int ay = pieceA.Y;

    int bx = pieceB.X;
    int by = pieceB.Y;

    // Swap references
    board[ax, ay] = pieceB.gameObject;
    board[bx, by] = pieceA.gameObject;

    // Swap logical types
    int tempType = boardTypes[ax, ay];
    boardTypes[ax, ay] = boardTypes[bx, by];
    boardTypes[bx, by] = tempType;

    // Update piece coordinates
    pieceA.UpdateGridPosition(bx, by);
    pieceB.UpdateGridPosition(ax, ay);

    // Update visual positions
    pieceA.transform.position = GetWorldPosition(bx, by);
    pieceB.transform.position = GetWorldPosition(ax, ay);
}

private Vector2 GetWorldPosition(int x, int y)
{
    return new Vector2(
        x * cellSize - (width - 1) * cellSize / 2f,
        y * cellSize - (height - 1) * cellSize / 2f
    );
}
public AnimalPiece GetPiece(int x, int y)
{
    if (x < 0 || x >= width || y < 0 || y >= height)
        return null;

    if (board[x, y] == null)
        return null;

    return board[x, y].GetComponent<AnimalPiece>();
}
public int GetPieceType(int x, int y)
{
    return boardTypes[x, y];
}
public void SwapBack(AnimalPiece pieceA, AnimalPiece pieceB)
{
    SwapPieces(pieceA, pieceB);
}
}
