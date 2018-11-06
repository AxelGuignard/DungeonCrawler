using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Move : MonoBehaviour {
    public GameObject onTile;
    private float speed;
    private bool moving;
    private bool rotating;
    private float startTime;
    private Vector3 startPosition;
    private Vector3 endPosition;

    // Use this for initialization
    void Start () {
        speed = 13.0F;
        moving = false;
        startTime = 0;
        startPosition = new Vector3();
        endPosition = new Vector3();
        
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            onTile = hit.collider.gameObject;
            transform.position = new Vector3(onTile.transform.position.x, 1.5f, onTile.transform.position.z);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (moving)
        {
            RaycastHit hit = new RaycastHit();

            if (transform.position != endPosition)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracDistance = distCovered / onTile.GetComponent<Renderer>().bounds.size.z;

                transform.position = Vector3.Lerp(startPosition, endPosition, fracDistance);
            }
            else
            {
                if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
                {
                    onTile = hit.collider.gameObject;
                }

                moving = false;
            }
        }
        else if (rotating)
        {
            if (Math.Round(transform.eulerAngles.y) != Math.Round(endPosition.y))
            {
                float distCovered = (Time.time - startTime) * speed * 15;
                float fracDistance = distCovered / 90.0F;

                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.LerpAngle(startPosition.y, endPosition.y, fracDistance), transform.eulerAngles.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, (float)Math.Round(transform.eulerAngles.y), 0);
                rotating = false;
            }
        }
    }

    public void StepForward()
    {
        if(!moving && !rotating)
        {
            RaycastHit hit = new RaycastHit();

            if (!Physics.Raycast(transform.position, transform.forward, out hit, onTile.GetComponent<Renderer>().bounds.size.z))
            {
                startPosition = transform.position;
                endPosition = transform.position + transform.forward * onTile.GetComponent<Renderer>().bounds.size.z;
                startTime = Time.time;

                moving = true;
            }
        }
    }

    public void StepBack()
    {
        if (!moving && !rotating)
        {
            RaycastHit hit = new RaycastHit();

            if (!Physics.Raycast(transform.position, -transform.forward, out hit, onTile.GetComponent<Renderer>().bounds.size.z))
            {
                startPosition = transform.position;
                endPosition = transform.position - transform.forward * onTile.GetComponent<Renderer>().bounds.size.z;
                startTime = Time.time;

                moving = true;
            }
        }
    }

    public void StepLeft()
    {
        if (!moving && !rotating)
        {
            RaycastHit hit = new RaycastHit();

            if (!Physics.Raycast(transform.position, -transform.right, out hit, onTile.GetComponent<Renderer>().bounds.size.x))
            {
                startPosition = transform.position;
                endPosition = transform.position - transform.right * onTile.GetComponent<Renderer>().bounds.size.x;
                startTime = Time.time;

                moving = true;
            }
        }
    }

    public void StepRight()
    {
        if (!moving && !rotating)
        {
            RaycastHit hit = new RaycastHit();

            if (!Physics.Raycast(transform.position, transform.right, out hit, onTile.GetComponent<Renderer>().bounds.size.x))
            {
                startPosition = transform.position;
                endPosition = transform.position + transform.right * onTile.GetComponent<Renderer>().bounds.size.x;
                startTime = Time.time;

                moving = true;
            }
        }
    }

     public void TurnLeft()
    {
        if (!moving && !rotating)
        {
            startPosition = transform.eulerAngles;
            if (transform.eulerAngles.y == 0)
            {
                endPosition = new Vector3(0, transform.eulerAngles.y - 90.0F + 360.0F, 0);
            }
            else
            {
                endPosition = new Vector3(0, transform.eulerAngles.y - 90.0F, 0);
            }
            startTime = Time.time;

            rotating = true;
        }
    }

    public void TurnRight()
    {
        if (!moving && !rotating)
        {
            startPosition = transform.eulerAngles;
            if (transform.eulerAngles.y == 270)
            {
                endPosition = new Vector3(0, transform.eulerAngles.y + 90.0F - 360.0F, 0);
            }
            else
            {
                endPosition = new Vector3(0, transform.eulerAngles.y + 90.0F, 0);
            }
            startTime = Time.time;

            rotating = true;
        }
    }
}
