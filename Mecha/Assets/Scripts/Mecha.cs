using UnityEngine;

public class Mecha : MonoBehaviour
{
    private GridManager manager;
    public bool isSelected = false; //set to private once done debugging
    public GameObject mecha;
    [SerializeField] private int speed;
    public void SetManager(GridManager gridManager)
    {
        manager = gridManager;
    }
    public void Selection(bool state)
    {
        isSelected = state;
    }
    public int getSpeed()
    {
        return speed;
    }
    void OnMouseDown()
    {
        manager.selectMecha(mecha);
        isSelected = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSelected)
        {
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseScreenPosition.z = 0;
            manager.moveMecha(mouseScreenPosition);
        }
    }
}
