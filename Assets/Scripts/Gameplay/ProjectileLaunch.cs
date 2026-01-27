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
    private Vector3 mousePosition;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        projectileRb = projectilePrefab.GetComponent<Rigidbody>();
    }

    // Click on ball and hold and drag to start calculating for force and direction of launch
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
        mouseDelta = inputActions.Player.Look.ReadValue<Vector2>();
        // projectilePrefab.transform.rotation = new Vector3(0, mouseDelta.y, 0);
        projectilePrefab.transform.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * 10f);

        if (inputActions.Player.HoldForce.IsPressed())
        {
            AddForce();
        } else if (inputActions.Player.HoldForce.WasReleasedThisFrame())
        {
            projectileRb.AddRelativeForce(launchForce * transform.right, ForceMode.Impulse);
            // launchForce = 0f; // Reset force after launch
            Debug.Log("Projectile Launched with Force: " + launchForce);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(projectilePrefab.transform.position, projectilePrefab.transform.forward * 5f);
    }
}
