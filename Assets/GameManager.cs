using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Android;
using System.Text;

public class GameManager : MonoBehaviour
{ 
    public List<Fakemon> fakemons;
    public List<Fakemon> fakemonsFiltered;

    public List<Sprite> pixelArts;
    public List<GameObject> models;
    public List<Sprite> seasonIcons;

    public GameObject modelInstance;
    public GameObject stage3d;
    public TMP_FontAsset font;

    public static GameManager instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        fakemons = SQLConn.GetFakemon();
        fakemonsFiltered = fakemons;
    }
    private void ChangeTypo()
    {
        if (instance.font != null)
        {
            foreach (var item in FindObjectsOfType<TextMeshProUGUI>())
            {
                item.font = instance.font;
                item.fontStyle = FontStyles.Normal;
            }
        }
    }

    void Update()
    {
        ChangeTypo();
    }
    public void SetModel(string name)
    {
        var result = models.Where(item => item.name.Contains(name)).ToList();

        if (modelInstance != null) Destroy(modelInstance);

        modelInstance = Instantiate(result.Count > 0 ? result[0] : models[0], stage3d.transform);
    }
    public void CloseGame()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
