using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject canvasUI;
    public GameObject background;
    public GameObject startButton;
    public GameObject continueButton;

    void Start()
    {
        // 開始時：StartButtonを表示、Continueを非表示
        if(canvasUI!=null)
            canvasUI.SetActive(true);

        if(background!=null)
            background.SetActive(true);
        
        if (startButton != null)
            startButton.SetActive(true);

        if (continueButton != null)
            continueButton.SetActive(false);
    }
}
