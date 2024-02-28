using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{ 
    public List<Fakemon> fakemons;
    public List<Sprite> pixelArts;
    public List<GameObject> models;
    public GameObject modelInstance;
    public GameObject stage3d;
    public TMP_FontAsset typography;

    public int actualSort;

    public static GameManager instance;


    void Awake()
    {
        fakemons = SQLConn.GetFakemon();
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    private void ChangeTypo()
    {
        if (typography != null)
        {
            foreach (var item in FindObjectsOfType<TextMeshProUGUI>())
            {
                item.font = typography;
                item.fontStyle = FontStyles.Normal;
            }
        }
    }

    void Update()
    {
        ChangeTypo();
        /*
        if (Input.GetKeyDown("1"))
            this.Sort(0);
        if (Input.GetKeyDown("2"))
            this.Sort(1);
        if (Input.GetKeyDown("3"))
            this.Sort(2);
        if (Input.GetKeyDown("4"))
            this.Sort(3);
        */
    }
    public void SetModel(string name)
    {
        var result = models.Where(item => item.name.Contains(name)).ToList();

        if (modelInstance != null) Destroy(modelInstance);

        modelInstance = Instantiate(result.Count > 0 ? result[0] : models[0], stage3d.transform);
    }
    public List<Fakemon> Sort(int method)
    {
        var sortedList = new List<Fakemon>();

        switch (method)
        {
            case 0:
                sortedList = fakemons.OrderBy(x => x.id).ToList();
                break;
            case 1:
                sortedList = fakemons.OrderBy(x => x.fakename).ToList();
                break;
            case 2:
                sortedList = fakemons.OrderBy(x => x.season).ToList();
                break;
            case 3:
                sortedList = fakemons.OrderBy(x => x.type).ToList();
                break;
        }

        if (actualSort == method)
        {
            sortedList.Reverse();
            actualSort = 20;
        } else actualSort = method;

        return sortedList;
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
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
