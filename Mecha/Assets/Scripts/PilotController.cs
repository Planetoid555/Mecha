using UnityEngine;

public class PilotController : MonoBehaviour
{
    public float xSens;
    public float ySens;
    public Transform pilotCamera;

    [SerializeField] float pilotSpeed;

    private Transform transPilot;
    private Rigidbody rigidPilot;
    private bool canMove; //is pilot movement enabled

    float xRotate;
    float yRotate;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transPilot = GetComponent<Transform>();
        rigidPilot = GetComponent<Rigidbody>();
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
        pilotCamera.rotation = Quaternion.Euler(xRotate, yRotate, 0);
        transPilot.rotation = Quaternion.Euler(0, yRotate, 0);
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            MovePilot();
        }
    }

    private void MovePilot()
    {
        moveDirection = transPilot.forward * verticalInput + transPilot.right * horizontalInput;
        rigidPilot.AddForce(moveDirection.normalized * pilotSpeed, ForceMode.Force);
    }

    public void SetPilotMovement(bool moveState)
    {
        canMove = moveState;
    }
}