using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeConrolllerJE : MonoBehaviour , IEndDragHandler
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshold ;
    [SerializeField]  Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;
    [SerializeField] private Button PreviousBtn, NextBtn;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshold = Screen.width /15f ;
        UpdateBar();
        UpdateArrowBtn();
    }

    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
           
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowBtn();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.delta.x - eventData.position.x) > dragThreshold)
        {
            if (eventData.delta.x > eventData.pressPosition.x) PreviousPage();
            else NextPage();
        }
        else
        {
            MovePage();
        }
    }

    
    void UpdateBar()
    {
        foreach(var item in barImage)
        {
            item.sprite = barClosed;
            
        }
        barImage[currentPage - 1].sprite = barOpen;
    }

    void UpdateArrowBtn()
    {
        PreviousBtn.interactable = true;
        NextBtn.interactable = true;

        if (currentPage == 1) PreviousBtn.interactable = false;
        else if (currentPage == maxPage) NextBtn.interactable = false;
    }
}
