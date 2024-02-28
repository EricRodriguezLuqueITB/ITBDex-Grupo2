using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ButtonSort : MonoBehaviour
{
    [SerializeField] private GameObject dexObj;
    private Dex dex;
    [SerializeField] private int sortNum;
    void Start()
    {
        dex = dexObj.GetComponent<Dex>();
        GetComponent<Button>().onClick.AddListener(Action);
    }
    private void Action()
    {
        dex.CreateList(GameManager.instance.Sort(sortNum));
    }
}
