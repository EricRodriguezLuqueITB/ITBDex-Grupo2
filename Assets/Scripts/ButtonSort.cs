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

        GetComponent<Button>().onClick.AddListener(name.Contains("Filter") ? FilterSeason : SortNum);

    }
    private void SortNum()
    {
        dex.Search(sortNum);
    }
    private void FilterSeason()
    {
        dex.FilterSeason(name[6..]);
    }
}
