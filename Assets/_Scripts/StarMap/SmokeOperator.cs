using UnityEngine;

public class SmokeOperator : MonoBehaviour
{
    public void OnAmimationFinish()
    {
        gameObject.SetActive(false);
    }
}
