using System.Collections;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject startUI; // StartボタンがあるUI

    public TitleUIManager titleUIManager;

    public Player player;

    public static float realGameStartTime;


    [Header("BGM Settings")]
    public AudioSource bgmSource;        // AudioSourceをアタッチ
    public AudioClip bgmClip;            // MP3ファイル（AudioClip）



    //ゲーム開始時に攻撃がされないようにするための変数
    public static float gameStartTime = -1f; // ← 静的に記録

    void Start()
    {
        realGameStartTime = Time.realtimeSinceStartup;
        Time.timeScale = 0f;  // ゲームの動きを止める
    }

    void Update()
    {
        // すでにゲームが始まっていたら何もしない
        if (gameStartTime > 0f) return;


        // 親指のみが立っていたらスタート
        if (player != null && player.isStartPose)
        {
            OnStartButtonClicked(); // スタート処理実行
        }
    }

    //[start]クリック時の処理
    public void OnStartButtonClicked()
    {

        // UIを非表示
        titleUIManager.canvasUI.SetActive(false);
        titleUIManager.background.SetActive(false);
        startUI.SetActive(false);

        gameStartTime = Time.time;


        PlayBGM();

        // 1秒後にゲームを再開
        StartCoroutine(StartGameAfterDelay(1f));
    }

    // [start]クリック後一秒してからゲームスタート
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
