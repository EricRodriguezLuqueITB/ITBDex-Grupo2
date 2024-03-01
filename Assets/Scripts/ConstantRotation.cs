using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotateSpeedModifier = 0.4f;
    private bool zoom = true;

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
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(
                    0f,
                    -touch.deltaPosition.x * rotateSpeedModifier,
                    0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }

    }
    public void changeZoom()
    {
        zoom = !zoom;
    }
}
