using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dex : MonoBehaviour
{
    [SerializeField] private GameObject prefabList;
    private Transform container;

    [SerializeField] private GameObject pixelArtCage;
    public int actualSort;
    private string textSearch = "";
    private bool reversed;
    private string actualSeason = "Clear";

    private void OnEnable()
    {
        reversed = false;
        container = transform.GetComponentsInChildren<Transform>().Where(item => item.name == "container").ToList()[0];
        CreateList(GameManager.instance.fakemons);
    }
    public void CreateList(List<Fakemon> fks)
    {
        ClearList();
        foreach (Fakemon f in fks)
        {
            GameObject obj = Instantiate(prefabList, container.transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = $"{f.id}º - {f.fakename} ({f.type})  / {f.season}";

            Image img = obj.GetComponent<Image>();

            switch (f.type)
            {
                case "Origin":
                    img.color = Color.white;
                    break;
                case "Fire":
                    img.color = new Color(0.97f, 0.41f, 0.41f);
                    break;
                case "Water":
                    img.color = new Color(0.2f, 0.4f, 1);
                    break;
                case "Ground":
                    img.color = new Color(0.6f, 0.4f, 0.2f);
                    break;
                case "Wind":
                    img.color = new Color(0.8f, 1, 1);
                    break;
                case "Electric":
                    img.color = new Color(1, 1, 0.6f);
                    break;
            }

            foreach (var item in obj.GetComponentsInChildren<TextMeshProUGUI>())
            {
                switch (item.name)
                {
                    case "TextID":
                        item.text = f.id.ToString();
                        break;
                    case "TextName":
                        item.text = f.fakename;
                        break;
                    case "TextType":
                        item.text = f.type;
                        break;
                }
            }

            obj.GetComponentsInChildren<Image>().Where(item => item.name == "SeasonIcon").ToArray()[0].sprite = GameManager.instance.seasonIcons.Where(item => item.name == f.season).ToArray()[0];

            ProfileButton pb = obj.GetComponent<ProfileButton>();

            pb.fk = f;
            pb.GetComponent<Button>().onClick.AddListener(pb.OnClick);
        }
    }
    public void SetPixelArt(string name)
    {
        List<Sprite> result = GameManager.instance.pixelArts.Where(item => item.name.ToLower().Contains(name.ToLower())).ToList();

        pixelArtCage.GetComponent<Image>().sprite = result.Count > 0 ? result[0] : GameManager.instance.pixelArts[0];
        pixelArtCage.GetComponentInChildren<TextMeshProUGUI>().text = "";

        pixelArtCage.transform.Find("Name").GetComponentInChildren<TextMeshProUGUI>().text = name;
    }
    public void Search(string text)
    {
        GameManager.instance.fakemonsFiltered = GameManager.instance.fakemons.Where(item => item.fakename.ToLower().Contains(text.ToLower())).ToList();
        if (actualSeason != "Clear") GameManager.instance.fakemonsFiltered = GameManager.instance.fakemonsFiltered.Where(item => item.season == actualSeason).ToList();
        CreateList(SortFakemon(actualSort, false));
        textSearch = text;
    }
    public void Search(int sort)
    {
        CreateList(SortFakemon(sort, true));
    }
    public void FilterSeason(string season)
    {
        actualSeason = season;
        Search(textSearch);
    }
    public List<Fakemon> SortFakemon(int method, bool revers)
    {
        var sortedList = GameManager.instance.fakemonsFiltered;

        switch (method)
        {
            case 0:
                sortedList = sortedList.OrderBy(x => x.id).ToList();
                break;
            case 1:
                sortedList = sortedList.OrderBy(x => x.fakename).ToList();
                break;
            case 2:
                sortedList = sortedList.OrderBy(x => x.season).ToList();
                break;
            case 3:
                sortedList = sortedList.OrderBy(x => x.type).ToList();
                break;
        }

        if (revers && method == actualSort) reversed = !reversed;
        else if (revers && method != actualSort) reversed = false;

        if (reversed) sortedList.Reverse();

        actualSort = method;

        return sortedList;
    }
    private void ClearList()
    {
        List<Transform> childs = container.GetComponentsInChildren<Transform>().ToList();
        int qty = childs.Count();

        for (int i = qty - 1; i > 0; i--)
        {
            Destroy(childs[i].gameObject);
        }
    }
    private void OnDisable()
    {
        ClearList();
    }
}
