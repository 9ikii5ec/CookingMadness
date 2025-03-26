using UnityEngine;
using UnityEngine.UI;

public class RecipeChecker : MonoBehaviour
{
    [Header("CheckMarcks")]
    [SerializeField] private GameObject checkMark1;
    [SerializeField] private GameObject checkMark2;
    [SerializeField] private GameObject checkMark3;

    [Header("Sliders")]
    [SerializeField] private Slider slider1;
    [SerializeField] private Slider slider2;
    [SerializeField] private Slider slider3;

    public void CheckMarkChecker()
    {
        UpdateSlider(checkMark1, slider1);
        UpdateSlider(checkMark2, slider2);
        UpdateSlider(checkMark3, slider3);
    }

    private void UpdateSlider(GameObject checkMark, Slider slider)
    {
        if (checkMark.activeInHierarchy)
        {
            slider.gameObject.SetActive(true);
            slider.value = Mathf.Clamp(1f, slider.minValue, slider.maxValue);
        }
    }
}
