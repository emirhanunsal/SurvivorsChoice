using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Button myButton;
    [SerializeField] private Sprite newButtonImage;
    [SerializeField] private string nextSceneName;

    private void Start()
    {
        
        myButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        
        if (myButton != null && newButtonImage != null)
        {
            myButton.image.sprite = newButtonImage;
        }

        
        SceneManager.LoadScene(nextSceneName);
    }
}
