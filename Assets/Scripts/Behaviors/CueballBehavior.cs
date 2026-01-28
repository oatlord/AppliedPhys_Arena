using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueballBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("CueBall hit a hazard!");
            SendMessageUpwards("CueballHit");
        }
    }
}
