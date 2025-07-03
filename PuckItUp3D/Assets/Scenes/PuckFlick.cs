using UnityEngine;

public enum PuckTeam
{
    TeamA,
    TeamB
}

[RequireComponent(typeof(Rigidbody))]
public class PuckFlick : MonoBehaviour
{
    public PuckTeam team;                 // Assign TeamA or TeamB in Inspector
    public bool settled = false;          // True when permanently settled
    public bool isWaitingToSettle = false;// True when waiting inside settle zone

    [HideInInspector] public Rigidbody rb;
  //  [HideInInspector] public Coroutine settleCoroutine;

    public float forceMultiplier = 0.001f; // Adjust for feel of flick

    private Vector3 swipeStartPos;
    private float swipeStartTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if (settled) return;

        swipeStartPos = Input.mousePosition;
        swipeStartTime = Time.time;
    }

    void OnMouseUp()
    {
        if (settled) return;

        Vector3 swipeEndPos = Input.mousePosition;
        float swipeEndTime = Time.time;

        Vector3 swipeVector = swipeEndPos - swipeStartPos;
        float swipeDuration = swipeEndTime - swipeStartTime;

        if (swipeDuration < 0.05f) swipeDuration = 0.05f;

        float swipeSpeed = swipeVector.magnitude / swipeDuration;
        Vector3 direction = new Vector3(swipeVector.x, 0f, swipeVector.y).normalized;
        Vector3 force = direction * swipeSpeed * forceMultiplier;

        rb.AddForce(force, ForceMode.Impulse);
    }
}
