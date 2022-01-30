using UnityEngine;

public class SkeletonAnchors : MonoBehaviour
{
    public Transform anchor1;
    public Transform anchor2;
    public Transform target;
    private Collider2D targetCollider;

    private int currentAnchor = 2;

    private float refreshRate;
    public float lastRefresh;

    private void Start()
    {
        refreshRate = 5f;
        lastRefresh = float.NegativeInfinity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > lastRefresh + refreshRate)
        {
            if (currentAnchor == 1)
            {
                target.position = anchor2.position;
                currentAnchor = 2;
            }
            else
            {
                target.position = anchor1.position;
                currentAnchor = 1;
            }
            lastRefresh = float.PositiveInfinity;
        }
    }
}
