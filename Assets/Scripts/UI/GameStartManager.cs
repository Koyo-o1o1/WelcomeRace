using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject startUI; // Start�{�^��������UI

    public TitleUIManager titleUIManager;

    public Player player;

    public static float realGameStartTime;


    [Header("BGM Settings")]
    public AudioSource bgmSource;        // AudioSource���A�^�b�`
    public AudioClip bgmClip;            // MP3�t�@�C���iAudioClip�j



    //�Q�[���J�n���ɍU��������Ȃ��悤�ɂ��邽�߂̕ϐ�
    public static float gameStartTime = -1f; // �� �ÓI�ɋL�^

    void Start()
    {
        realGameStartTime = Time.realtimeSinceStartup;
        Time.timeScale = 0f;  // �Q�[���̓������~�߂�
    }

    void Update()
    {
        // ���łɃQ�[�����n�܂��Ă����牽�����Ȃ�
        if (gameStartTime > 0f) return;


        // �e�w�݂̂������Ă�����X�^�[�g
        if (player != null && player.isStartPose)
        {
            OnStartButtonClicked(); // �X�^�[�g�������s
        }
    }

    //[start]�N���b�N���̏���
    public void OnStartButtonClicked()
    {

        // UI���\��
        titleUIManager.canvasUI.SetActive(false);
        titleUIManager.background.SetActive(false);
        startUI.SetActive(false);

        gameStartTime = Time.time;


        PlayBGM();

        // 1�b��ɃQ�[�����ĊJ
        StartCoroutine(StartGameAfterDelay(1f));
    }

    // [start]�N���b�N���b���Ă���Q�[���X�^�[�g
    IEnumerator StartGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;

        
    }



    //gameBGM
    public void PlayBGM()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

}
