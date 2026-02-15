using UnityEngine;

public class MechaController : MonoBehaviour
{
    public float xSens;
    public float ySens;

    [SerializeField] float mechaSpeed;

    private Transform transMecha;
    private Rigidbody rigidMecha;
    private bool canMove; //is mecha movement enabled

    float xRotate;
    float yRotate;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    private void Start()
    {
        transMecha = GetComponent<Transform>();
        rigidMecha = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void Update()
    {
        //Input Maxxing
        float xMouse = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens;
        float yMouse = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        yRotate += xMouse;
        xRotate -= yMouse;
        xRotate = Mathf.Clamp(xRotate, -90f, 90f);
        transMecha.rotation = Quaternion.Euler(0, yRotate, 0);
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            MoveMecha();
        }
    }

    private void MoveMecha()
    {
        moveDirection = (transMecha.forward * verticalInput + transMecha.right * horizontalInput).normalized;

        Vector3 velocity = moveDirection * mechaSpeed;
    
        velocity.y = rigidMecha.linearVelocity.y;
    
        rigidMecha.linearVelocity = velocity;;
    }

    public void SetMechaMovement(bool moveState)
    {
        canMove = moveState;
    }
}