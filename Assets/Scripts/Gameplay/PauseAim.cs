using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseAim : MonoBehaviour
{
    // Script that pauses the input manager from detecting the angle of aim when the ball is launched and re-enables it when the ball comes to a full stop.
    public GameObject playerControl;
    private ProjectileLaunch projectileLaunchScript;
    // Start is called before the first frame update
    void Start()
    {
        projectileLaunchScript = playerControl.GetComponent<ProjectileLaunch>();
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileLaunchScript.GetLaunchState())
        {
            playerControl.gameObject.GetComponent<InputSystem_Actions>().Player.HoldForce.Disable();
            playerControl.gameObject.GetComponent<InputSystem_Actions>().Player.Look.Disable();
            Debug.Log("Aiming Paused");
        }
    }
}
