using UnityEngine;

public class Mecha : MonoBehaviour
{
    private GridManager manager;
    public bool isSelected = false; //set to private once done debugging
    public void SetManager(GridManager gridManager)
    {
        manager = gridManager;
    }
    public void Selection(bool state)
    {
        isSelected = state;
    }
    void OnMouseDown()
    {
        manager.selectMecha(gameObject);
        isSelected = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSelected)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            manager.moveMecha(mouseScreenPosition);
        }
    }
}
