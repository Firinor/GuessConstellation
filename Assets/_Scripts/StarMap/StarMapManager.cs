using UnityEngine;
using UnityEngine.UI;

public enum Hemisphere { Northern, Southern, Winter, Spring, Summer, Autumn}

public class StarMapManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject targetConstellation;
    [SerializeField]
    private GameObject starMap;
    [SerializeField]
    private GameObject nebula;
    [SerializeField]
    private GameObject helpMap;
    [SerializeField]
    private TargetOperator targetOperator;
    [SerializeField]
    private Image targetImage;
    [SerializeField]
    private GlassBallViewOperator glassBallViewOperator;
    [SerializeField]
    private StarMapInGlassBallOperator starMapInGlassBallOperator;
    [SerializeField]
    private StarMapInformator starMapInformator;

    [SerializeField, Range(0, 2)]
    private int difficulty = 1;
    #endregion

    protected void OnEnable()
    {
        ClearPuzzle();
        
        /*Observable.EveryUpdate()
            .Where(_ => theTimerIsRunning && leftTime > 0)
            .Subscribe(_ => TimerTick())
            .AddTo(disposables);*/

        StartPuzzle();
    }
    public void LosePuzzle()
    {
        DeactivatePuzzle();
        //FailButton.SetActive(true);
    }
    public void ClearPuzzle()
    {
        ResetPointerAndButton();
    }
    public void CheckAnswer()
    {
        glassBallViewOperator.ActivePuzzle = false;
        targetImage.enabled = true;
        Vector2Int point = starMapInGlassBallOperator.GetCursorPoint();
        Color answer = targetImage.sprite.texture.GetPixel(point.x, point.y);
        if(answer.a > 0.4f)
        {
            SuccessfullySolvePuzzle();
        }
        else
        {
            LosePuzzle();
        }
    }
    private void ResetPointerAndButton()
    {
        targetOperator.SetButtonActivity(false);
        starMapInGlassBallOperator.SetTargetActivity(false);
        starMapInGlassBallOperator.SetCursorActivity(false);
    }
    public void SetButtonActivity()
    {
        targetOperator.SetButtonActivity(true);
    }
    public void SetPuzzleInformationPackage(StarMapPackage spatMapPackage)
    {
        //leftTime = spatMapPackage.AllottedTime;
        difficulty = spatMapPackage.Difficulty;
        SetNewConstellation();
        //SetVictoryEvent(spatMapPackage.successPuzzleAction);
        //SetFailEvent(spatMapPackage.failedPuzzleAction);
        ///SetBackground(spatMapPackage.PuzzleBackground);
    }

    private void SetNewConstellation()
    {
        Hemisphere hemisphere = starMapInformator.ChoseHemisphere();
        var answerSprite = starMapInformator.ChoseAnswerSprite(hemisphere);
        SetStarMapPuzzleContent(hemisphere);
        SetStarMapAnswerSprite(answerSprite.sprite);
        SetStarMapKeySprite(answerSprite.constellation);
    }

    private void SetStarMapKeySprite(Constellation constellation)
    {
        Sprite sprite = starMapInformator[constellation];
        targetOperator.SetAnswerKey(sprite);
    }

    private void SetStarMapAnswerSprite(Sprite sprite)
    {
        starMapInGlassBallOperator.SetAnswerSprite(sprite);
    }

    private void SetStarMapPuzzleContent(Hemisphere hemisphere)
    {
        Sprite sprite = starMapInformator[hemisphere].HemispherePuzzleSprite[difficulty];

        bool isEasy = difficulty == 0;
        starMapInGlassBallOperator.SetPuzzleSprite(sprite, isEasy);

        helpMap.GetComponent<HelpMapOperator>().SetListOfPagesActive(isEasy, hemisphere);
    }

    public void StartPuzzle()
    {
        //keyOperator.CreateHint();
        //theTimerIsRunning = leftTime > 0;
        glassBallViewOperator.ActivePuzzle = true;
    }
    public void SuccessfullySolvePuzzle()
    {
        DeactivatePuzzle();
        //VictoryButton.SetActive(true);
    }
    protected void DeactivatePuzzle()
    {
        //theTimerIsRunning = false;
    }

    public void PuzzleExit()
    {
        OpenStarMap();
        //background.enabled = false;
        gameObject.SetActive(false);
    }

    public void OpenHelpBook()
    {
        if (!helpMap.activeSelf)
        {
            EnabledPuzzle(false);
            EnabledHelpMap(true);
        }
    }
    public void OpenStarMap()
    {
        EnabledHelpMap(false);
        EnabledPuzzle(true);
    }
    private void EnabledHelpMap(bool v)
    {
        helpMap.SetActive(v);
    }

    private void EnabledPuzzle(bool v)
    {
        targetConstellation.SetActive(v);
        starMap.SetActive(v);
        nebula.SetActive(v);
    }
}

public class StarMapPackage
{
    [SerializeField]
    [Range(0, 1024)]
    private float allottedTime = 0;

    public float AllottedTime { get => allottedTime; }

    [SerializeField]
    [Range(0, 2)]
    private int difficulty = 0;

    public int Difficulty { get => difficulty; }
}