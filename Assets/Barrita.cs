using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Barrita : MonoBehaviour
{
    public string volumeName;
    
    void Awake()
    {
        GetComponent<Slider>().value = PlayerPrefs.HasKey(volumeName) ? PlayerPrefs.GetFloat(volumeName) : 1;
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(volumeName, GetComponent<Slider>().value);
    }
}
