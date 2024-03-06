using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Android;
using System.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    public List<Fakemon> fakemons;
    public List<Fakemon> fakemonsFiltered;

    public List<Sprite> pixelArts;
    public List<GameObject> models;
    public List<Sprite> seasonIcons;
    public List<Sprite> seasonFrames;

    public GameObject modelInstance;
    public GameObject stage3d;
    public GameObject stageFrame;
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
    public bool CheckZoom()
    {
        GameObject rotCon = GameObject.Find("Stage3D");

        if (rotCon.TryGetComponent(out RotationController rc)) return !rc.zoom;
        return true;
    }

    void Update()
    {
        ChangeTypo();
    }
    public void SetModel(string name)
    {
        var result = models.Where(item => item.name.ToLower().Contains(name.ToLower())).ToList();

        if (modelInstance != null) Destroy(modelInstance);

        modelInstance = Instantiate(result.Count > 0 ? result[0] : models[0], stage3d.transform);

        modelInstance.AddComponent<ModelInteraction>();

        string season = fakemons.Where(item => item.fakename.Contains(name)).ToList()[0].season;

        if (seasonFrames.Where(item => item.name.Contains(season)).ToList().Count > 0)
        {
            stageFrame.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            stageFrame.GetComponent<Image>().sprite = seasonFrames.Where(item => item.name.Contains(season)).ToList()[0];
        }
        else stageFrame.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }
    public void CloseGame()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
