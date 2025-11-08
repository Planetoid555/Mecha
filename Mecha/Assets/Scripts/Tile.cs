using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, altColor;
    [SerializeField] private SpriteRenderer tileRender;
    [SerializeField] private GameObject highlight;
    public void Init(bool isOffset)
    {
        tileRender.color = isOffset ? altColor : baseColor;
    }
    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
    //void OnMouseDown()
    //{
    //    
    //}
}
