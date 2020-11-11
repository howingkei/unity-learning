using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskData : MonoBehaviour
{
    float speedX;

    public void setSpeedX(float speedX_)
    {
        speedX = speedX_;
    }

    public float getSpeedX() { return speedX; }
    public Vector3 StartPoint
    {  
        get
        {
            return gameObject.transform.position;
        }
        set
        {
            gameObject.transform.position = value;
        }
    }

    public Color color
    {
        get
        {
            return gameObject.GetComponent<Renderer>().material.color;
        }
        set
        {
            gameObject.GetComponent<Renderer>().material.color = value;
        }
    }

    public Vector3 Speed { get; set; }
    public Vector3 Direction
    {
        get
        {
            return Direction;
        }
        set
        {
            gameObject.transform.Rotate(value);
        }
    }
}
