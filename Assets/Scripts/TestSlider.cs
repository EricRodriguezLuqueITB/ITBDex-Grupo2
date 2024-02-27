using UnityEngine;
using UnityEngine.UI;

public class TestSlider : MonoBehaviour
{
    public Slider musicSlider;
    public Slider efectSlider;
    public AudioManager audioManager;
    public static TestSlider instance;
    // Start is called before the first frame update
    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();//Donde pone Test poner el nombre del Objeto con el componente AudioManager
        Slider[] sliders = GetComponentsInChildren<Slider>();
        musicSlider = sliders[0];
        musicSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(musicSlider.value, 1); });
        efectSlider = sliders[1];
        efectSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(efectSlider.value, 0); });
        instance = this;
    }

    private void Update()
    {
    }

    // Update is called once per frame
    void ValueChangeCheck(float value, int id)
    {
        if (audioManager != null)
        {

            audioManager.ChangeVolumen(value, id);
        }
    }
}