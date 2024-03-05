using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TestSlider : MonoBehaviour
{
    public Slider musicSlider;
    public Slider efectSlider;

    void Awake()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        musicSlider = sliders[0];
        musicSlider.onValueChanged.AddListener(delegate { AudioManager.instance.ChangeVolumen(musicSlider.value, 1); });
        efectSlider = sliders[1];
        efectSlider.onValueChanged.AddListener(delegate { AudioManager.instance.ChangeVolumen(efectSlider.value, 0); });
    }
}