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
        // �J�n���FStartButton��\���AContinue���\��
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
