using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    //Component
    [SerializeField]
    private InputActionReference moveInput;
    [SerializeField]
    private InputActionReference jumpInput;
    [SerializeField]
    private InputActionReference lookInput;
    private Rigidbody rb;
    private Transform cam;
    //Settings
    public float speed;
    public float jumpForce;
    public float vSensitivy;
    public float hSensitivity;
    public float camLimit;
    //Data
    private Vector2 moveDir;
    private Vector2 lookDir;
    private float horRot;
    private float verRot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = moveInput.action.ReadValue<Vector2>().normalized;
        lookDir = lookInput.action.ReadValue<Vector2>();
        if (jumpInput.action.WasCompletedThisFrame())
        {
            Jump();
        }
        Look();

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = (transform.forward * moveDir.y + transform.right*moveDir.x)*speed;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce);
        
    }

    private void Look()
    {
        horRot = lookInput.action.ReadValue<Vector2>().x * hSensitivity;
        verRot += -lookInput.action.ReadValue<Vector2>().y * vSensitivy;

        verRot = Mathf.Clamp(verRot, -camLimit, camLimit);

        transform.Rotate(Vector3.up * horRot);
        cam.localRotation = Quaternion.Euler(verRot, 0f, 0f);
    }

}
