using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Fakemon> fakemons;
    void Awake()
    {
        fakemons = SQLConn.GetFakemon();
    }
    public void CloseGame()
    {
        if(Application.isEditor) EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
