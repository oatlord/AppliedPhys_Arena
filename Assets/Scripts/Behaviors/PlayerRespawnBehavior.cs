using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnBehavior : MonoBehaviour
{
    public int lives = 3;
    public float respawnDelay = 0.5f;
    private GameObject cueBall;
    private Coroutine respawnCueballCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        cueBall = GameObject.FindGameObjectWithTag("CueBall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetCueballPosition()
    {
        cueBall.transform.position = new Vector3(0,0.54f,0);
        cueBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cueBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void CueballHit()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log("Lives remaining: " + lives);

            if (respawnCueballCoroutine == null)
            {
                respawnCueballCoroutine = StartCoroutine(RespawnCueballAfterDelay(respawnDelay));
            } else if (respawnCueballCoroutine != null)
            {
                StopCoroutine(respawnCueballCoroutine);
                respawnCueballCoroutine = StartCoroutine(RespawnCueballAfterDelay(respawnDelay));
            }

        }
        else
        {
            Debug.Log("Game Over!");
        }
    }

    IEnumerator RespawnCueballAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetCueballPosition();
    }
}
