using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public float speed;
    public Transform[] waypointList;

    private Transform target;
    private int targetIndex;
    void Start()
    {
        target = waypointList[0];
        targetIndex = 0;
        if (target.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // graphics.flipX = target.position.x < transform.position.x;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate((dir.normalized) * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            targetIndex = (targetIndex + 1) % waypointList.Length;
            target = waypointList[targetIndex];
            //graphics.flipX = ( (target.position - transform.position).normalized.x < 0.3);
            if (target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
