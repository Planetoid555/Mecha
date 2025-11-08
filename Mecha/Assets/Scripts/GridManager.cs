using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public Tile tile;
    [SerializeField] private Transform cam;
    void Start()
    {
        MakeGrid();
    }
    void MakeGrid()
    {
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(col, row), Quaternion.identity);
                spawnedTile.name = $"tile {row} {col}";
                var isOffset = (col % 2 == 0 && row % 2 != 0) || (col % 2 != 0 && row % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - .5f, (float)height / 2 - .5f, -10);
    }
}
