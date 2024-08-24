using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Aging : MonoBehaviour
{
    [SerializeField] private Image characterImage; 
    [SerializeField] private Sprite[] agingSprites; 
    [SerializeField] private TMP_Text dayCounterText;
    private int dayCounter = 0;
    private int currentAgeIndex = 0;

    void Start()
    {
        UpdateCharacterSprite();
        UpdateDayCounterText();
    }

    
    public void IncrementDayCounter()
    {
        dayCounter++;
        UpdateDayCounterText();

        if (dayCounter % 7 == 0 && currentAgeIndex < agingSprites.Length - 1)
        {
            currentAgeIndex++;
            UpdateCharacterSprite();
        }
    }

    
    private void UpdateCharacterSprite()
    {
        if (agingSprites.Length > 0 && characterImage != null)
        {
            characterImage.sprite = agingSprites[currentAgeIndex];
        }
    }

    
    private void UpdateDayCounterText()
    {
        if (dayCounterText != null)
        {
            dayCounterText.text = "Day: " + dayCounter.ToString();
        }
    }
}