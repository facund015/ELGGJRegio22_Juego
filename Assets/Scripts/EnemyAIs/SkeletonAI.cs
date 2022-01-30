using UnityEngine;
using Pathfinding;

public class SkeletonAI : MonoBehaviour
{

    private Animator animator;

    public Transform target;

    private Seeker seeker;
    private Path path;

    private float speed = 1f;

    private float nextWaypointDistance = 0.5f;
    private int currentWaypoint = 0;

    private float repathRate = 0.1f;
    private float lastRepath = float.NegativeInfinity;

    private bool reachedEndOfPath;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        animator = GetComponent<Animator>();
    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);


        // Path pooling. To avoid unnecessary allocations paths are reference counted.
        // Calling Claim will increase the reference count by 1 and Release will reduce
        // it by one, when it reaches zero the path will be pooled and then it may be used
        // by other scripts. The ABPath.Construct and Seeker.StartPath methods will
        // take a path from the pool if possible. See also the documentation page about path pooling.
        p.Claim(this);
        if (!p.error)
        {
            if (path != null) path.Release(this);
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        }
        else
        {
            p.Release(this);
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        if (Time.time > lastRepath + repathRate && seeker.IsDone())
        {
            speed = 2.5f;
            lastRepath = Time.time;
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }        


        if (path == null)
        {
            // We have no path to follow yet, so don't do anything
            return;
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame. Amogus
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 velocity = dir * speed;
        velocity.y = 0;

        transform.position += velocity * Time.deltaTime;

        if (velocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            animator.SetFloat("Speed", 1f);
        }
        else if (velocity.x <= -0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

    }
}
