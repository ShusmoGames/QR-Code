using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderUpdateTime : MonoBehaviour
{
    [SerializeField] private TMP_Text time;
    [SerializeField] private Slider slider;

    private void Start() => ShowTime();
    public void ShowTime() => time.text = slider.value.ToString();

}
