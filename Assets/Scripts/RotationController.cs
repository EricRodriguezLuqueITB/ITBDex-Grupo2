using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    private Touch touch;
    private Vector3 rotationY;
    private float rotateSpeedModifier = 0.4f;
    public bool zoom = true;
    private float clock;

    private void FixedUpdate()
    {
        if (zoom)
        {
            transform.Rotate(new Vector3(0, 1, 0), Space.World);
        }
        else
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
            }
            else
            {
                if (rotationY.y != 0 && Time.time - clock > 0.1f)
                {
                    rotationY *= 0.9f;

                    if (rotationY.y < 0.8 && rotationY.y > -0.8)
                    {
                        rotationY = Vector3.zero;
                    }
                    clock = Time.time;
                }
            }

            if (touch.phase == TouchPhase.Moved && Input.touchCount > 0)
            {
                rotationY = RotateY(-touch.deltaPosition.x * rotateSpeedModifier);
            }
            transform.Rotate(rotationY);
        }
    }
    public void changeZoom()
    {
        zoom = !zoom;
    }
    public void ResetRotation()
    {
        rotationY *= 0;
        transform.rotation = Quaternion.Euler(RotateY(200));
    }
    private Vector3 RotateY(float y)
    {
        return new Vector3(0, y, 0);
    }
}
