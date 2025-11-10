using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public Tile tile;
    [SerializeField] private Transform cam;
    private Dictionary<Vector2, Tile> tileDict;
    [SerializeField] private GameObject player;
    private GameObject selectedMecha;
    private Mecha mechaScript;
    
    void Start()
    {
        MakeGrid();
        player.GetComponent<Mecha>().SetManager(this);
    }
    void MakeGrid()
    {
        tileDict = new Dictionary<Vector2, Tile>();
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(col, row), Quaternion.identity);
                spawnedTile.name = $"tile {row} {col}";
                var isOffset = (col % 2 == 0 && row % 2 != 0) || (col % 2 != 0 && row % 2 == 0);
                spawnedTile.Init(isOffset);


                tileDict[new Vector2(col, row)] = spawnedTile;
            }
        }
        Instantiate(player, new Vector3(0, 0), Quaternion.identity);
        cam.transform.position = new Vector3((float)width / 2 - .5f, (float)height / 2 - .5f, -10);
    }

    public Tile GetTile(Vector2 pos)
    {
        if (tileDict.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public void selectMecha(GameObject mecha)
    {
        if (selectedMecha != null)
        {
            mechaScript.Selection(false);
        }
        selectedMecha = mecha;
        mechaScript = selectedMecha.GetComponent<Mecha>();
    }

    public void moveMecha(Vector3 pos)
    {
        selectedMecha.transform.position = new Vector3((int)pos.x, (int)pos.y, 0);
    }
    public void moveMecha(Vector2 pos)
    {
        selectedMecha.transform.position = new Vector3((int)pos.x, (int)pos.y, 0);
    }
    
}
