using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Fakemon> fakemons;
    void Awake()
    {
        fakemons = SQLConn.GetFakemon();
        foreach (var item in fakemons)
        {
            Debug.Log(item.fakename);
        }
    }
}
