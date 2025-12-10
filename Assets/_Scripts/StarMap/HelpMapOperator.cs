using UnityEngine;
using UnityEngine.UI;

public class HelpMapOperator : MonoBehaviour
{
    [SerializeField]
    private Sprite[] maps;
    [SerializeField]
    private Image image;
    [SerializeField]
    private RectTransform imageRectTransform;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float scrollStep = 0.15f;
    [SerializeField]
    private HelpMapScrollRect helpMapScrollRect;
    [SerializeField]
    private GameObject listOfPages;

    void Awake()
    {
        helpMapScrollRect.SetGlassBallViewOperator(this);
    }

    public void SwitchMap(int i)
    {
        if (maps != null && maps.Length > i)
        {
            image.sprite = maps[i];
        }
    }

    public void SetListOfPagesActive(bool isEasy, Hemisphere hemisphere)
    {
        listOfPages.SetActive(!isEasy);
        if(isEasy)
            SwitchMap((int)hemisphere);
    }

    public float ZoomScroll(Vector2 mouseScrollDelta)
    {
        slider.value += scrollStep * (mouseScrollDelta.y > 0 ? 1 : -1);
        slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
        return slider.value;
    }

    public void SliderZoom()
    {
        imageRectTransform.localScale = Vector3.one * slider.value;
    }
}

