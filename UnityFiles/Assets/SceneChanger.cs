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
        // Butonun tıklama olayını dinle
        myButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Butonun resmini değiştir
        if (myButton != null && newButtonImage != null)
        {
            myButton.image.sprite = newButtonImage;
        }

        // Sahneyi değiştir
        SceneManager.LoadScene(nextSceneName);
    }
}
