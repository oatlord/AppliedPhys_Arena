using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileLaunch : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    public GameObject projectilePrefab;
    public float launchForceRiseSpeed = 20f;

    private float launchForce = 0f;
    private Rigidbody projectileRb;
    private Vector2 mouseDelta;

    private bool ballMoving = false;
    private bool launchPerformed = false;
    private Coroutine postBallMoveCoroutine;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        projectileRb = projectilePrefab.GetComponent<Rigidbody>();
    }

    void AddForce()
    {
        launchForce += Time.deltaTime * launchForceRiseSpeed; // Increase force over time while holding
        launchForce = Mathf.Clamp(launchForce, 0f, 100f); // Clamp to max force
        Debug.Log("Current Launch Force: " + launchForce);
    }

    void SubtractForce()
    {
        launchForce -= Time.deltaTime * launchForceRiseSpeed; // Decrease force over time while holding
        launchForce = Mathf.Clamp(launchForce, 0f, 100f); // Clamp to max force
        Debug.Log("Current Launch Force: " + launchForce);
    }

    void Update()
    {
        // Debug.Log("Velocity: " + projectileRb.velocity.magnitude);
        // Debug.Log("Ball Moving: " + ballMoving);
        // Debug.Log("Launch Force: " + launchForce);
        // Debug.Log("Launch Performed: " + launchPerformed);

        mouseDelta = inputActions.Player.Look.ReadValue<Vector2>();
        projectilePrefab.transform.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * 10f);

        if (inputActions.Player.HoldForce.IsPressed() && !launchPerformed)
        {
            AddForce();
        } else if (inputActions.Player.HoldForce.WasReleasedThisFrame() && !launchPerformed)
        {
            projectileRb.AddRelativeForce(launchForce * transform.forward, ForceMode.Impulse);
            Debug.Log("Projectile Launched with Force: " + launchForce);
            launchPerformed = true;
        }

        if (projectileRb.velocity.magnitude > 0.1f)
        {
            ballMoving = true;
        }
        else
        {
            ballMoving = false;
        }

        // Debug.Log("Input Actions Player Enabled: " + inputActions.Player.enabled);

        if (launchPerformed && ballMoving)
        {
            if (inputActions.Player.enabled)
            {
                inputActions.Player.Disable();
            }

            if (postBallMoveCoroutine == null) 
            {
                postBallMoveCoroutine = StartCoroutine(PostBallMoveSequence());
            } else 
            {
                StopCoroutine(postBallMoveCoroutine);
                postBallMoveCoroutine = StartCoroutine(PostBallMoveSequence());
            }
        }
    }

    IEnumerator PostBallMoveSequence()
    {
        yield return new WaitUntil(() => projectileRb.velocity.magnitude < 0.1f);
        Debug.Log("Ball Stopped Moving. Resetting Launch.");
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before resetting
        ResetLaunch();
    }

    private void ResetLaunch()
    {
        launchForce = 0f;
        launchPerformed = false;
        ballMoving = false;
        projectileRb.transform.rotation = Quaternion.Euler(0f,0f,0f);
        projectileRb.velocity = Vector3.zero;
        projectileRb.angularVelocity = Vector3.zero;
        Debug.Log("Ball Launch Reset");

        if (inputActions.Player.enabled == false)
        {
            inputActions.Player.Enable();
        }
    }

    public bool GetBallState()
    {
        return ballMoving;
    }

    public bool GetLaunchState()
    {
        return launchPerformed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(projectilePrefab.transform.position, projectilePrefab.transform.forward * 5f);
    }
}
