using System.Collections.Generic;
using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public Tile tile;
    [SerializeField] private Transform cam;
    private Dictionary<Vector2, Tile> tileDict;
    [SerializeField] private GameObject mecha;
    private GameObject selectedMecha;
    private Mecha mechaScript;
    private GameObject spawnedMecha;
    
    void Start()
    {
        MakeGrid();
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
        spawnedMecha = Instantiate(mecha, new Vector3(0, 0), Quaternion.identity);
        spawnedMecha.GetComponent<Mecha>().SetManager(this);
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

    public void selectMecha(GameObject selected)
    {
        if (selectedMecha != null)
        {
            mechaScript.Selection(false);
        }
        selectedMecha = selected;
        mechaScript = selectedMecha.GetComponent<Mecha>();
    }

    public void moveMecha(Vector3 pos)
    {
        int manhattan = (int) Math.Sqrt((selectedMecha.transform.position.x - pos.x) * (selectedMecha.transform.position.x - pos.x) + (selectedMecha.transform.position.y - pos.y) * (selectedMecha.transform.position.y - pos.y));
        if (mechaScript.getSpeed() > manhattan)
        {
            selectedMecha.transform.position = new Vector3((int)(pos.x + .5), (int)(pos.y + .5), pos.z);
        }
    }
}
