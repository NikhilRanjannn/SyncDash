using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float horizontalSpeed = 7f;
    public float jumpForce = 7f;

    [Header("Speed Progression")]
    public float startSpeed = 5f;
    public float maxSpeed = 50f;
    public float timeToMaxSpeed = 15f;
    private float speedTimer = 0f;
    private float forwardSpeed;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckDist = 0.3f;
    public LayerMask groundLayer;

    [Header("Networking")]
    public NetworkSimulator network;
    public float sendRate = 0.04f;

    private Rigidbody rb;
    private bool jumpInput;
    private float horizontalInput;
    private float sendTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        forwardSpeed = startSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;

       
        // PC CONTROL (A / D or Left / Right)
        // -------------------------------
        float keyboardInput = Input.GetAxisRaw("Horizontal");  // A/D or arrows

        
        // MOBILE TILT CONTROL
        
        float tiltInput = 0f;

        if (SystemInfo.supportsAccelerometer)
        {
            tiltInput = Input.acceleration.x;   // tilt left/right
        }

        // Combined control (PC or mobile)
        // If keyboard is pressed, use keyboard, otherwise use tilt.
        horizontalInput = Mathf.Abs(keyboardInput) > 0.1f ? keyboardInput : tiltInput;

        // Ground Check
        bool isGrounded = Physics.Raycast(
            groundCheck.position,
            Vector3.down,
            groundCheckDist,
            groundLayer
        );

        // Jump (space or screen tap)
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            jumpInput = true;

        
        // NETWORK SYNC
       
        sendTimer += Time.deltaTime;
        if (sendTimer >= sendRate)
        {
            sendTimer = 0f;

            SyncPacket pkt = new SyncPacket(
                transform.position,
                rb.velocity,
                horizontalInput,
                jumpInput,
                Time.time
            );

            network.Send(pkt);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;

        // Speed progression
        speedTimer += Time.deltaTime;
        float t = Mathf.Clamp01(speedTimer / timeToMaxSpeed);
        forwardSpeed = Mathf.Lerp(startSpeed, maxSpeed, t);

        Vector3 vel = rb.velocity;

        // Constant forward speed
        vel.z = forwardSpeed;

        // Horizontal movement from tilt or A/D
        vel.x = horizontalInput * horizontalSpeed;

        // Jump
        if (jumpInput)
        {
            vel.y = jumpForce;
            jumpInput = false;
        }

        rb.velocity = vel;
    }
}
