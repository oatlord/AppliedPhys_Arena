using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{

    //* v (final velocity), u (initial velocity), a (acceleration), t (time)
    // v = u + at

    // Start is called before the first frame update

    public float initialVelocity = 10f;
    public float acceleration = 9.81f;
    public float time = 2f;
    private bool launched = false;

    void FixedUpdate()
    {
        // Only launch once. Keep the finalVelocity calculation exactly as it was
        if (launched) return;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb == null) return;

        float finalVelocity = initialVelocity + (acceleration * time);

        rb.useGravity = true;

        Vector3 impulse = transform.forward.normalized * finalVelocity * rb.mass;

        rb.AddForce(impulse, ForceMode.Impulse);

        launched = true;
    }
}
