using UnityEngine;

public class GameClearManager : MonoBehaviour
{
    public bool gameClear = false;

    Player player;

    [Header("UI Elements")]
    public GameObject canvasUI;

    public void GameClearUI_Start()
    {
        gameClear = true;
        canvasUI.SetActive(true);
        Time.timeScale = 0;
    }
}
