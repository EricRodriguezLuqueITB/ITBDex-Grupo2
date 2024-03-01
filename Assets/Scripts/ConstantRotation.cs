using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
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
            transform.Rotate(new Vector3(0, 0.3f, 0), Space.World);
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
                    rotationY = RotateY(rotationY.y * 0.5f);

                    if (rotationY.y < 0.0002f && rotationY.y > -0.0002f)
                    {
                        rotationY = Vector3.zero;
                    }
                    clock = Time.time;
                }
            }

            if (touch.phase == TouchPhase.Moved)
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
    private Vector3 RotateY(float y)
    {
        return new Vector3(0, y, 0);
    }
}
