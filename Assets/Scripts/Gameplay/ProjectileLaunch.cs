using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    //* v (final velocity), u (initial velocity), a (acceleration), t (time)
    // v = u + at

    // Start is called before the first frame update

    // public float initialVelocity = 10f;
    // public float acceleration = 9.81f;
    // public float time = 2f;
    // private bool launched = false;

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
        inputActions.Player.HoldForce.performed += ctx => CalculateForce();
    }

    // Click on ball and hold and drag to start calculating for force and direction of launch
    void CalculateForce()
    {
        Debug.Log("Calculating Force and Direction for Launch");
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
    }
}
