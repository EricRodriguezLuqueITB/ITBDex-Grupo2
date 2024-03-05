using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelInteraction : MonoBehaviour
{
    private Touch touch;
    private Vector2 startPosition;
    private string idleSoundName;

    private void Start()
    {
        List<Sound> sounds = AudioManager.instance.sounds.Where(item => item.name.Contains(name.Split('(')[0]) && item.name.ToLower().Contains("idle")).ToList();

        if (sounds.Count > 0) idleSoundName = sounds[0].name;
    }
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
        if(idleSoundName != null && GameObject.Find("Profile") != null && !AudioManager.instance.sounds.Where(item => item.name == idleSoundName).ToList()[0].source.isPlaying) AudioManager.instance.ChoosePlay(idleSoundName, 0);
    }

    private void Interact()
    {
        List<Sound> sounds = AudioManager.instance.sounds.Where(item => item.name.Contains(name.Split('(')[0])).ToList();

        switch (sounds.Count)
        {
            case 1:
                AudioManager.instance.ChoosePlay(sounds[0].name, 0);
                break;
            case > 1:

                List<Sound> soundsF = sounds.Where(item => item.name.ToLower().Contains("attack")).ToList();
                if(soundsF.Count == 1) AudioManager.instance.ChoosePlay(soundsF[0].name, 0);
                break;
        }
    }
}
