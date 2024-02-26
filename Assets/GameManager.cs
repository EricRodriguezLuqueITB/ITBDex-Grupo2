using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public List<Fakemon> fakemons;
    public List<Sprite> pixelArts;
    void Awake()
    {
        fakemons = SQLConn.GetFakemon();
        this.Show(1);
    }

    void Update()
    {
        if(Input.GetKeyDown("1"))
            this.Sort(0);
        if (Input.GetKeyDown("2"))
            this.Sort(1);
        if (Input.GetKeyDown("3"))
            this.Sort(2);
        if (Input.GetKeyDown("4"))
            this.Sort(3);
    }
    public void Sort(int method)
    {
        var sortedList = new List<Fakemon>();
        switch (method)
        {
            case 0:
                sortedList = fakemons.OrderBy(x => x.id).ToList();
                break;
            case 1:
                sortedList =fakemons.OrderBy(x => x.fakename).ToList();
                break;
            case 2:
                sortedList = fakemons.OrderBy(x => x.season).ToList();
                break;
            case 3:
                sortedList = fakemons.OrderBy(x => x.type).ToList();
                break;
        }

        fakemons = sortedList;
        this.Show(method);
    }

    public void Show(int method)
    {
        foreach (var item in fakemons)
        {
            switch (method)
            {
                case 0:
                    Debug.Log(item.fakename);
                    Debug.Log(item.id);
                    break;
                case 1:
                    Debug.Log(item.fakename);
                    Debug.Log(item.info);
                    break;
                case 2:
                    Debug.Log(item.fakename);
                    Debug.Log(item.season);
                    break;
                case 3:
                    Debug.Log(item.fakename);
                    Debug.Log(item.type);
                    break;
            }

        }
    }
    public void CloseGame()
    {
        if(Application.isEditor) EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
