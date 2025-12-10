using FirMath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetOperator : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private StarMapManager starMapOperator;
    [SerializeField]
    private Image keyImage;
    [SerializeField]
    private TextMeshProUGUI keyText;

    public void CheckAnswer()
    {
        SetButtonActivity(false);
        starMapOperator.CheckAnswer();
    }

    public void SetButtonActivity(bool v)
    {
        button.gameObject.SetActive(v);
    }

    public void SetAnswerKey(Sprite sprite)
    {
        keyImage.sprite = sprite;
        GameImage.SetImageWidth(keyImage, keyImage.rectTransform.sizeDelta.x);
        keyText.text = sprite.name;
    }
}