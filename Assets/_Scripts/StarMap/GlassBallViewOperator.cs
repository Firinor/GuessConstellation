using UnityEngine;
using UnityEngine.UI;

public class GlassBallViewOperator : MonoBehaviour
{
    [SerializeField]
    private float scrollStep = 0.15f;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private StarMapInGlassBallOperator starMapInGlassBallOperator;
    [SerializeField]
    private StarMapScrollRect starMapScrollRect;
    [SerializeField]
    private StarMapManager starMapOperator;

    [HideInInspector]
    public bool ActivePuzzle;
    private Vector2 mousePoint;
    [SerializeField]
    private int maxMouseDisplacement;

    void Awake()
    {
        starMapScrollRect.SetGlassBallViewOperator(this);
    }

    public float ZoomScroll(Vector2 mouseScrollDelta)
    {
        slider.value += scrollStep * (mouseScrollDelta.y > 0 ? 1 : -1);
        slider.value = Mathf.Clamp(slider.value, slider.minValue, slider.maxValue);
        return slider.value;
    }

    public void SliderZoom()
    {
        starMapInGlassBallOperator.SetImageScale(slider.value);
    }

    public RectTransform GetRectTransform()
    {
        return starMapInGlassBallOperator.GetRectTransform();
    }

    public void SetCursorPosition(Vector2 localPoint)
    {
        starMapInGlassBallOperator.SetCursorPosition(localPoint);
        if(ActivePuzzle)
            starMapOperator.SetButtonActivity();
    }

    public void StartClick()
    {
        mousePoint = Input.mousePosition;
    }
    public bool EndClickOnStartClick()
    {
        Vector2 currentMousePosition = Input.mousePosition;
        bool result = Mathf.Abs(mousePoint.x - currentMousePosition.x) < maxMouseDisplacement
            && Mathf.Abs(mousePoint.y - currentMousePosition.y) < maxMouseDisplacement;

        ResetClick();

        return result;
    }
    public void ResetClick()
    {
        mousePoint = new Vector2(-2000, -2000);
    }
}