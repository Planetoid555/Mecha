using UnityEngine;

public class PilotController : MonoBehaviour
{
    public float xSens;
    public float ySens;
    public Transform pilotCamera;

    [SerializeField] float pilotSpeed;
    [SerializeField] float jumpForce;

    private Transform transPilot;
    private Rigidbody rigidPilot;
    private bool canMove; //is pilot movement enabled
    private bool isGrounded;

    float xRotate;
    float yRotate;
    float horizontalInput;
    float verticalInput;
    float playerHeight;
    float raycastDistance; //For Grounded Check, maybe rename later
    bool doJump;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transPilot = GetComponent<Transform>();
        rigidPilot = GetComponent<Rigidbody>();
        canMove = true;
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;
        isGrounded = true;
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
        if(canMove)
        {
            pilotCamera.rotation = Quaternion.Euler(xRotate, yRotate, 0);
            transPilot.rotation = Quaternion.Euler(0, yRotate, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        IsGrounded();
        Vector3 move = (transPilot.forward * verticalInput + transPilot.right * horizontalInput).normalized;
        rigidPilot.linearVelocity = new Vector3(move.x * pilotSpeed, rigidPilot.linearVelocity.y, move.z * pilotSpeed);
        if (doJump && isGrounded)
        {
            Vector3 vel = rigidPilot.linearVelocity;
            vel.y = jumpForce;
            rigidPilot.linearVelocity = vel;
        }
        doJump = false;
    }

    public void SetPilotMovement(bool moveState)
    {
        canMove = moveState;
    }

    private void IsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, raycastDistance); 
    }
}