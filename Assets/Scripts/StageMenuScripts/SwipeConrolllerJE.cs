using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeConrolllerJE : MonoBehaviour , IEndDragHandler
{   

    [SerializeField] int maxPage = 4;
    int currentPage;
    Vector3 startPos;
    Vector3 targetPos;
    bool isStartPosSaved = false;

    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime = 0.3f;
    [SerializeField] LeanTweenType tweenType;

    float dragThreshold ;
    [SerializeField]  Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;
    [SerializeField] private Button PreviousBtn, NextBtn;

    private LTDescr tween;

    private void Awake()
    {
        currentPage = 1;
        if (levelPagesRect != null)
        {
            startPos = levelPagesRect.localPosition;
            isStartPosSaved = true;
        }
        targetPos = startPos;
        dragThreshold = Screen.width /15f ;
        UpdateBar();
        UpdateArrowBtn();
    }

    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
        }
        MovePage();
    }

    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
        }
        MovePage();
    }

    public void SetPage(int page)
    {
        currentPage = Mathf.Clamp(page, 1, maxPage);
        MovePage();
    }

    void MovePage()
    {
        if (!isStartPosSaved && levelPagesRect != null)
        {
            startPos = levelPagesRect.localPosition;
            isStartPosSaved = true;
        }

        targetPos = startPos + (currentPage - 1) * pageStep;

        if (levelPagesRect != null)
        {
            if (tween != null)
            {
                tween.reset();
                LeanTween.cancel(levelPagesRect.gameObject);
                tween = null;
            }
            tween = levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType).setIgnoreTimeScale(true);
        }

        UpdateBar();
        UpdateArrowBtn();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x) PreviousPage();
            else NextPage();
        }
        else
        {
            MovePage();
        }
    }

    private void OnDisable()
    {
        if (tween != null)
        {
            tween.reset();
            tween = null;
        }
    }

    
    void UpdateBar()
    {
        if (barImage == null) return;
        for (int i = 0; i < barImage.Length; i++)
        {
            if (barImage[i] != null)
            {
                barImage[i].sprite = (i == currentPage - 1) ? barOpen : barClosed;
            }
        }
    }

    void UpdateArrowBtn()
    {
        if (PreviousBtn != null) PreviousBtn.interactable = (currentPage > 1);
        if (NextBtn != null) NextBtn.interactable = (currentPage < maxPage);
    }
}


