using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_SlerpMovement : MonoBehaviour
{
    public Transform start;
    public Transform end;

    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;

    public void SetStartTime()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (start != null && end != null)
        {
            // The center of the arc
            Vector3 center = (start.position + end.position) * 0.5F;

            // move the center a bit downwards to make the arc vertical
            center -= new Vector3(0, 10, 0);

            // Interpolate over the arc relative to center
            Vector3 riseRelCenter = start.position - center;
            Vector3 setRelCenter = end.position - center;

            // The fraction of the animation that has happened so far is
            // equal to the elapsed time divided by the desired time for
            // the total journey.
            float fracComplete = (Time.time - startTime) / journeyTime;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;
        }
    }
}