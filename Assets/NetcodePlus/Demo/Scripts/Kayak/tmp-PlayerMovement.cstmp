using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("=== NORMAL PADDLE SETTINGS ===")]
    [SerializeField] private float normalPaddleTurnForce = 10f;
    [SerializeField] private float normalPaddleForwardForce = 10f;
    [SerializeField] private float normalRudderForce = 2f;
    //[SerializeField] private float rudderTime = 3f;
    //[SerializeField] private float normalPaddleWithdrawalFactor = 2f;
    //[SerializeField] private float normalStrokeTime = 1f;
    [SerializeField] private float normalSmoothForceSpeed = 1f;

    [Header("=== BACK PADDLE SETTINGS ===")]
    [SerializeField] private float backPaddleForce = 10f;

    [Header("=== WIDE PADDLE SETTINGS ===")]
    [SerializeField] private float widePaddleTurnForce = 20f;
    [SerializeField] private float widePaddleForwardForce = 5f;
    //[SerializeField] private float widePaddleWithdrawalFactor = 2f;
    //[SerializeField] private float wideStrokeTime = 1f;
    [SerializeField] private float wideSmoothForceSpeed = 1f;

    [Header("=== SIDE PADDLE SETTINGS ===")]
    [SerializeField] private float sidePaddleForce = 5f;
    //[SerializeField] private float sidePaddleWithdrawalFactor = 2f;
    //[SerializeField] private float sideStrokeTime = 1f;
    [SerializeField] private float sideSmoothForceSpeed = 1f;

    [Header("=== EDGE SETTINGS ===")]
    [SerializeField] private float edgeTurnInfluence = 5f;
    [SerializeField] private float edgeSideInfluence = 5f;

    [Header("=== STROKE STATES ===")]
    [SerializeField] private Stroke currentStroke = Stroke.none;
    [SerializeField] private Stroke lastStroke = Stroke.none;
    [SerializeField] private StrokeState currentStrokeState = StrokeState.Ready;
    public Stroke CurrentStroke { get { return currentStroke; } }
    public StrokeState CurrentStrokeState { get { return currentStrokeState; } }

    [Header("=== MOVING WATER SETTINGS ===")]
    [SerializeField] private bool isMovingWater = false;
    [SerializeField] private float movingWaterForce = 2f;
    [SerializeField] private float waterDirDownForce = 2f;
    [SerializeField] private float waterDownForceAngle = 10f;
    [SerializeField] private LayerMask whatIsWater;


    private float normalStrokeAxis;
    private float wideStrokeAxis;
    private float sideStrokeAxis;
    private float backStrokeAxis;
    private float rudderAxis;
    private float edgeAxis;

    public float NormalStrokeAxis { get { return normalStrokeAxis; } }
    public float WideStrokeAxis { get { return wideStrokeAxis; } }
    public float SideStrokeAxis { get { return sideStrokeAxis; } }
    public float BackStrokeAxis { get { return backStrokeAxis; } }
    public float RudderAxis { get { return rudderAxis; } }
    public float EdgeAxis { get { return edgeAxis; } }

    private float currentPaddleNormalForwardForce = 0f;
    private float currentPaddleWideForwardForce = 0f;
    private float currentPaddleSideForwardForce = 0f;
    private float currentPaddleBackForce = 0f;
    private float currentPaddleNormalRotateForce = 0f;
    private float currentPaddleWideRotateForce = 0f;

    float currentNrmlVel = 0;
    float currentWideVel = 0;
    float currentSideVel = 0;
    float currentBackVel = 0;
    float currentNrmlRotVel = 0;
    float currentWideRotVel = 0;

    private Rigidbody rb;
    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private Vector3 currentWaterUpDir;
    private Vector3 lastWaterUpDir;

    private Quaternion gravityAlignment = Quaternion.identity;



    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        currentStrokeState = StrokeState.Ready;
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerInput();
        SetStrokeState();

        AlignRotation();
    }

    private void FixedUpdate()
    {
        HandleBoatMovement();

        
    }

    private void AlignRotation()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5f, whatIsWater, QueryTriggerInteraction.Ignore))
        {
            currentWaterUpDir = lastWaterUpDir = hitInfo.normal;
        }

/*        float upAlignmentSpeed = 10f;

        Vector3 fromUp = gravityAlignment * transform.up;
        Vector3 toUp = currentWaterUpDir;
        float dot = Mathf.Clamp(Vector3.Dot(fromUp, toUp), -1f, 1f);
        float angle = Mathf.Acos(dot) * Mathf.Deg2Rad;
        float maxAngle = upAlignmentSpeed * Time.deltaTime;

        Quaternion newAlignment = Quaternion.FromToRotation(fromUp, toUp) * gravityAlignment;

        transform.localRotation = Quaternion.SlerpUnclamped(
                gravityAlignment, newAlignment, maxAngle / angle
            );*/

    }

    void HandlePlayerInput()
    {
        normalStrokeAxis = playerControls.Player.NormalStroke.ReadValue<float>();
        wideStrokeAxis = playerControls.Player.WideStroke.ReadValue<float>();
        sideStrokeAxis = playerControls.Player.SideStroke.ReadValue<float>();
        backStrokeAxis = playerControls.Player.BackStroke.ReadValue<float>();
        edgeAxis = playerControls.Player.EdgeAxis.ReadValue<float>();
        rudderAxis = playerControls.Player.RudderAxis.ReadValue<float>();

        if (playerControls.Player.Escape.triggered)
        {
            Application.Quit();
        }
    }

    void SetStrokeState()
    {

        if (normalStrokeAxis != 0) currentStroke = lastStroke = Stroke.normal;
        if (wideStrokeAxis != 0) currentStroke = lastStroke = Stroke.wide;
        if (sideStrokeAxis != 0) currentStroke = lastStroke = Stroke.side;
        if (backStrokeAxis != 0) currentStroke = lastStroke = Stroke.back;

        if (normalStrokeAxis == 0) ResetNormalStrokeForces();
        if (wideStrokeAxis == 0) ResetWideStrokeForces();
        if (sideStrokeAxis == 0) ResetSideStrokeForces();

            // If no input set stroke to none.
            if (normalStrokeAxis == 0 && wideStrokeAxis == 0 && sideStrokeAxis == 0 && backStrokeAxis == 0)
        {
            currentStroke = Stroke.none;
            currentStrokeState = StrokeState.Ready;
        }


        if (normalStrokeAxis != 0 && (wideStrokeAxis != 0 || sideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            currentStroke = Stroke.normal;
        }
        else if (wideStrokeAxis != 0 && (normalStrokeAxis != 0 || sideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            currentStroke = Stroke.wide;
        }
        else if (sideStrokeAxis != 0 && (normalStrokeAxis != 0 || wideStrokeAxis != 0 || backStrokeAxis != 0))
        {
            currentStroke = Stroke.side;
        }
        else if(backStrokeAxis != 0 && (normalStrokeAxis != 0 || wideStrokeAxis != 0 || sideStrokeAxis != 0))
        {
            currentStroke = Stroke.back;
        }

        // If we're ready for the next stroke and player is performing a stroke, then set state to midstroke.
        if (currentStrokeState == StrokeState.Ready && currentStroke != Stroke.none)
        {
            currentStrokeState = StrokeState.MidStroke;
        }
    }

    #region Boat Movement
    void ResetNormalStrokeForces()
    {
        currentPaddleNormalForwardForce = Mathf.SmoothDamp(currentPaddleNormalForwardForce, 0, ref currentNrmlVel, normalSmoothForceSpeed);
        if (currentPaddleNormalForwardForce != 0 && currentPaddleNormalForwardForce < 0.1) currentPaddleNormalForwardForce = 0;

        currentPaddleNormalRotateForce = Mathf.SmoothDamp(currentPaddleNormalRotateForce, 0, ref currentNrmlRotVel, normalSmoothForceSpeed);
        if (currentPaddleNormalRotateForce != 0 && currentPaddleNormalRotateForce < 0.1) currentPaddleNormalRotateForce = 0;
    }

    void ResetWideStrokeForces()
    {
        currentPaddleWideForwardForce = Mathf.SmoothDamp(currentPaddleWideForwardForce, 0, ref currentWideVel, wideSmoothForceSpeed);
        if (currentPaddleWideForwardForce != 0 && currentPaddleWideForwardForce < 0.1) currentPaddleWideForwardForce = 0;

        currentPaddleWideRotateForce = Mathf.SmoothDamp(currentPaddleWideRotateForce, 0, ref currentWideRotVel, wideSmoothForceSpeed);
        if (currentPaddleWideRotateForce != 0 && currentPaddleWideRotateForce < 0.1) currentPaddleWideRotateForce = 0;
    }

    void ResetSideStrokeForces()
    {
        currentPaddleSideForwardForce = Mathf.SmoothDamp(currentPaddleSideForwardForce, 0, ref currentSideVel, sideSmoothForceSpeed);
        if (currentPaddleSideForwardForce != 0 && currentPaddleSideForwardForce < 0.1) currentPaddleSideForwardForce = 0;
    }

    void HandleBoatMovement()
    {
        // Apply downforce towards water normal
        
            rb.AddForce(-currentWaterUpDir * waterDirDownForce);
            
        

        // If the water is moving we can control the speed here.
        if (isMovingWater)
        {
            rb.AddForce(Vector3.forward * movingWaterForce * Time.deltaTime);
        }

        // Depending on the stroke, add the relevant motions
        if (currentStrokeState == StrokeState.MidStroke)
        {
            switch (currentStroke)
            {
                case Stroke.none:
                    break;
                case Stroke.normal:

                    currentPaddleNormalRotateForce = Mathf.SmoothDamp(currentPaddleNormalRotateForce, normalPaddleTurnForce, ref currentNrmlRotVel, normalSmoothForceSpeed);
                    rb.AddRelativeTorque(Vector3.up * -normalStrokeAxis * currentPaddleNormalRotateForce * Time.deltaTime);

                    currentPaddleNormalForwardForce = Mathf.SmoothDamp(currentPaddleNormalForwardForce, normalPaddleForwardForce, ref currentNrmlVel, normalSmoothForceSpeed);
                    rb.AddForce(transform.forward * currentPaddleNormalForwardForce * Time.deltaTime);

                    break;

                case Stroke.wide:

                    currentPaddleWideRotateForce = Mathf.SmoothDamp(currentPaddleWideRotateForce, widePaddleTurnForce, ref currentNrmlRotVel, wideSmoothForceSpeed);
                    rb.AddRelativeTorque(Vector3.up * -wideStrokeAxis * currentPaddleWideRotateForce * Time.deltaTime);

                    currentPaddleWideForwardForce = Mathf.SmoothDamp(currentPaddleWideForwardForce, widePaddleForwardForce, ref currentWideVel, wideSmoothForceSpeed);
                    rb.AddForce(transform.forward * currentPaddleWideForwardForce * Time.deltaTime);
                    break;

                case Stroke.side:
                    currentPaddleSideForwardForce = Mathf.SmoothDamp(currentPaddleSideForwardForce, sidePaddleForce, ref currentSideVel, sideSmoothForceSpeed);
                    rb.AddForce(transform.right * sideStrokeAxis * currentPaddleSideForwardForce * Time.deltaTime);
                    break;

                case Stroke.back:
                    rb.AddForce(-transform.forward * backStrokeAxis * backPaddleForce * Time.deltaTime);
                    break;
            }
        }

        // Handle boat ruddering
        if (currentStrokeState == StrokeState.Ruddering)
        {
            rb.AddRelativeTorque(Vector3.up * -rudderAxis * normalRudderForce * Time.deltaTime);
        }

        // Handle boat edging
        if (rb.velocity.magnitude > .15f && (edgeAxis > 0.1 || edgeAxis < -0.1))
        {
            rb.AddRelativeTorque(Vector3.up * edgeAxis * edgeTurnInfluence * Time.deltaTime);
            rb.AddForce(transform.right * edgeAxis * edgeSideInfluence * Time.deltaTime);
        }
    }
    #endregion
}


