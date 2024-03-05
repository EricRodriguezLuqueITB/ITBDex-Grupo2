using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    private Touch touch;
    private Vector2 startPosition;
    private void Update()
    {
        if (Input.touchCount > 0 && GameManager.instance.CheckZoom())
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    if (startPosition == touch.position) Interact();
                    break;
            }
        }
    }

    private void Interact()
    {
        //Debug.Log(AudioManager.instance.sounds.Where(item => item.name.Contains(name.Split('(')[0])).ToList().Count);
        AudioManager.instance.ChoosePlay(AudioManager.instance.sounds.Where(item => item.name.Contains(name.Split('(')[0])).ToList()[0].name, 0);
    }
}
