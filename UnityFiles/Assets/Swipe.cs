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
    [SerializeField] private TMP_Text swipeFeedbackText;

    private int pageNumber = 0;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 defaultPosition;
    private Vector2 offscreenPosition;
    private bool isDragging = false;
    private CardData previousCardData; // Önceki kartı tutmak için

    void Start()
    {
        defaultPosition = pageImage.transform.localPosition;
        offscreenPosition = new Vector2(defaultPosition.x, defaultPosition.y - 800f);
        pageImage.transform.localPosition = offscreenPosition;
        swipeFeedbackText.text = ""; // Başlangıçta swipe feedback boş
        UpdatePage();
    }

    void Update()
    {
        if (Input.touchCount > 0)
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

        pageImage.transform.localPosition = endPosition;
        previousCardData = cardManager.GetCurrentCardData(); // Mevcut kartı önceki kart olarak ayarla
        cardManager.ShowNextCard(leftSwipe: true);
        UpdatePage();
        StartCoroutine(DisplaySwipeFeedback(true)); // Sol kaydırma sonucu göster
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

        pageImage.transform.localPosition = endPosition;
        previousCardData = cardManager.GetCurrentCardData(); // Mevcut kartı önceki kart olarak ayarla
        cardManager.ShowNextCard(leftSwipe: false);
        UpdatePage();
        StartCoroutine(DisplaySwipeFeedback(false)); // Sağ kaydırma sonucu göster
    }

    private void UpdatePage()
    {
        pageImage.sprite = pageSprite;
        pageImage.transform.localPosition = defaultPosition;
        StartCoroutine(SlideInPage());

        var cardData = cardManager.GetCurrentCardData();
        cardManager.UpdateCardData(cardData);
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
    }

    private IEnumerator DisplaySwipeFeedback(bool isLeftSwipe)
    {
        if (previousCardData != null)
        {
            if (isLeftSwipe)
            {
                swipeFeedbackText.text = previousCardData.leftConclusion;
            }
            else
            {
                swipeFeedbackText.text = previousCardData.rightConclusion;
            }

            swipeFeedbackText.CrossFadeAlpha(1, 1f, false); // Yavaşça göster
            yield return new WaitForSeconds(3f); // 3 saniye bekle
            swipeFeedbackText.CrossFadeAlpha(0, 1f, false); // Yavaşça kaybol
        }
    }
}
