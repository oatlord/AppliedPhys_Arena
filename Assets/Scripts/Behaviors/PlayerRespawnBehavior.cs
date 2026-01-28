using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerRespawnBehavior : MonoBehaviour
{
    public int lives = 3;
    public float respawnDelay = 0.5f;
    public TMP_Text livesText;
    private GameObject cueBall;
    private Coroutine respawnCueballCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        cueBall = GameObject.FindGameObjectWithTag("CueBall");
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
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
        if (lives > 1)
        {
            lives--;
            if (livesText != null)
            {
                livesText.text = "Lives: " + lives.ToString();
            }
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
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator RespawnCueballAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetCueballPosition();
    }
}
