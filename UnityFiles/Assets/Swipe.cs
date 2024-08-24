using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Swipe : MonoBehaviour
{
    [SerializeField] private Image pageImage;
    [SerializeField] private float dragSpeed = 0.05f;
    [SerializeField] private float slideDuration = 0.5f;
    [SerializeField] private float slideOutDuration = 1f;
    [SerializeField] private float distance = 800f;
    [SerializeField] private Sprite pageSprite;
    [SerializeField] private CardManager cardManager;
    [SerializeField] public TMP_Text swipeFeedbackText;
    [SerializeField] public TMP_Text dayCounterText; 
    [SerializeField] private Aging Aging;

    
    
    private int dayCounter = 0; 
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 defaultPosition;
    private Vector2 offscreenPosition;
    private bool isDragging = false;
    private CardData previousCardData;

    void Start()
    {
        defaultPosition = pageImage.transform.localPosition;
        offscreenPosition = new Vector2(defaultPosition.x, defaultPosition.y - 800f);
        pageImage.transform.localPosition = offscreenPosition;
        swipeFeedbackText.text = ""; 
        dayCounterText.text = "Day: 0"; 
        UpdatePage();
    }

    void Update()
    {
        if (Input.touchCount > 0 && cardManager.canSwipe && !cardManager.isGameEnd)
        {
            Touch touch = Input.GetTouch(0);
        
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                currentTouchPosition = touch.position;
                float distanceMoved = (currentTouchPosition.x - startTouchPosition.x) * dragSpeed;
                pageImage.transform.position = new Vector3(pageImage.transform.position.x + distanceMoved, pageImage.transform.position.y, pageImage.transform.position.z);
                startTouchPosition = currentTouchPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                isDragging = false;

                float screenWidth = Screen.width;
                float threshold = screenWidth / 4;
                cardManager.canSwipe = false;

                if (endTouchPosition.x < screenWidth / 2 - threshold)
                {
                    StartCoroutine(SlideOutAndPreviousPage());
                }
                else if (endTouchPosition.x > screenWidth / 2 + threshold)
                {
                    StartCoroutine(SlideOutAndNextPage());
                }
                else
                {
                    StartCoroutine(ReturnToCenter());
                }
            }
        }
    }

    private IEnumerator SlideOutAndPreviousPage()
    {
        Vector3 startPosition = defaultPosition;
        Vector3 endPosition = new Vector3(defaultPosition.x - Screen.width, defaultPosition.y);

        float elapsedTime = 0f;
        while (elapsedTime < slideOutDuration)
        {
            pageImage.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / slideOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Aging.IncrementDayCounter();
        pageImage.transform.localPosition = endPosition;
        previousCardData = cardManager.GetCurrentCardData(); 
        cardManager.ShowNextCard(leftSwipe: true);
        UpdatePage();
        cardManager.canSwipe = true;
        Debug.Log("Went to left");
        StartCoroutine(DisplaySwipeFeedback(true)); 
    }

    private IEnumerator SlideOutAndNextPage()
    {
        Vector3 startPosition = defaultPosition;
        Vector3 endPosition = new Vector3(defaultPosition.x + Screen.width, defaultPosition.y);

        float elapsedTime = 0f;
        while (elapsedTime < slideOutDuration)
        {
            pageImage.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / slideOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Aging.IncrementDayCounter();
        pageImage.transform.localPosition = endPosition;
        previousCardData = cardManager.GetCurrentCardData();
        cardManager.ShowNextCard(leftSwipe: false);
        UpdatePage();
        cardManager.canSwipe = true;
        Debug.Log("Went to right");

        StartCoroutine(DisplaySwipeFeedback(false)); 
    }

    private void UpdatePage()
    {
        if (!cardManager.isGameEnd)
        {
            pageImage.sprite = pageSprite;
            pageImage.transform.localPosition = defaultPosition;
            StartCoroutine(SlideInPage());

            var cardData = cardManager.GetCurrentCardData();
            cardManager.UpdateCardData(cardData);

            
            dayCounter++;
            dayCounterText.text = "Day: " + dayCounter.ToString();
        }
        
    }
        

    private IEnumerator SlideInPage()
    {
        Vector3 startPosition = defaultPosition + new Vector2(0f, distance);
        Vector3 endPosition = defaultPosition;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            pageImage.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pageImage.transform.localPosition = endPosition;
    }

    private IEnumerator ReturnToCenter()
    {
        Vector3 startPosition = pageImage.transform.localPosition;
        Vector3 endPosition = defaultPosition;

        float elapsedTime = 0f;
        while (elapsedTime < slideDuration)
        {
            pageImage.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pageImage.transform.localPosition = endPosition;
        cardManager.canSwipe = true;
    }

    private IEnumerator DisplaySwipeFeedback(bool isLeftSwipe)
    {
        
        if (previousCardData != null && !cardManager.isGameEnd)
        {
            if (isLeftSwipe)
            {
                swipeFeedbackText.text = previousCardData.leftConclusion;
            }
            else
            {
                swipeFeedbackText.text = previousCardData.rightConclusion;
            }

            swipeFeedbackText.CrossFadeAlpha(1, 1f, false); 
            yield return new WaitForSeconds(3f); 
            swipeFeedbackText.CrossFadeAlpha(0, 1f, false); 
        }
    }

}
