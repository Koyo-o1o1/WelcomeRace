using System.Collections;
using UnityEngine;

public class ContinueUIManager : MonoBehaviour
{
    private Enemy enemy;
    private Player player;
    private Character_Stats character_Stats;

    //continue時か判定する変数
    public bool isContinue=false;

    bool canPlayDeadSound = true;


    [Header("UI Elements")]
    public GameObject canvasUI;
    public GameObject background;
    public GameObject continueUI;

    [Header("BGM")]
    public AudioSource normalBgmSource;    // 通常BGM（ゲーム開始時のBGM）
    public AudioSource bossBgmSource;
    [SerializeField] private BossAudioManager bossAudioManager;
    public AudioSource deadAudioSource;
    public AudioClip deadClip;


    [SerializeField] private GameStartManager gameStartManager;

    #region

    [SerializeField] public PotionManager potionManager;

    #endregion

    //ゲーム開始時に攻撃がされないようにするための変数
    public static float gameContinueTime = -1f; // ← 静的に記録

    void Start()
    {
        //プレイヤー取得
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        character_Stats = player.GetComponent<Character_Stats>();

        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

    }

    private void Update()
    {
        // 親指のみが立っていたらスタート
        if (player != null && player.isContinue)
        {
            OnContinueButtonClicked(); // スタート処理実行
        }
    }

    public void Continue_UI_Start()
    {
        // 通常BGMを停止
        if (normalBgmSource != null && normalBgmSource.isPlaying)
        {
            normalBgmSource.Stop();
        }

        // BossBGMを停止
        if (bossBgmSource != null && bossBgmSource.isPlaying)
        {
            bossAudioManager.hasTriggered=false;
            bossBgmSource.Stop();
        }

        isContinue = true;

        if (HasSavePoint())
        {
            if (canvasUI != null)
                canvasUI.SetActive(true);

            if (background != null)
                background.SetActive(true);

            if (continueUI != null)
                continueUI.SetActive(true);
        }

        //キャラの色を戻す
        player.fx.ResetMaterial();


        PlayDeadSound();

        //deadAudioSource.clip = deadClip;
        //deadAudioSource.Play();




    }

    // セーブポイントが登録されているかチェック
    private bool HasSavePoint()
    {
        if (GameManager.Instance == null) return false;

        return GameManager.Instance.GetLastSavePoint() != Vector3.zero;
    }

    //クリック押されたとき
    public void OnContinueButtonClicked()
    {
        gameContinueTime = Time.time;

        // 1秒後にゲームを再開
        StartCoroutine(ContinueGameAfterDelay(1f));

        //セーブポイント更新
        player.transform.position = GameManager.Instance.GetLastSavePoint();
        //HP回復
        character_Stats.currentHealth=character_Stats.GetMaxHealthValue();

        // HPバー反映(?はnull check)
        character_Stats.onHealthChanged?.Invoke();

        //キャラの色を戻す
        player.fx.ResetMaterial();

        player.stateMachine.ChangeState(player.idleState);

        // 敵の初期位置とHPをリセット
        enemy.ResetEnemies();

        //bgmかけなおす
        gameStartManager.PlayBGM();

        player.isContinue = false;
    }

    

    //[start]クリック後一秒してからゲームスタート
    IEnumerator ContinueGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        

        potionManager.currentPotions = potionManager.maxPotions;
        potionManager.UpdatePotionUI();


        //プレイヤーを右向きへ
        player.FaceRight();

        // UIを非表示
        canvasUI.SetActive(false);
        background.SetActive(false);
        continueUI.SetActive(false);

        isContinue = false;

        Time.timeScale = 1f;
    }



    void PlayDeadSound()
    {
        if (canPlayDeadSound)
        {
            deadAudioSource.clip = deadClip;
            deadAudioSource.volume = 0.4f;
            deadAudioSource.Play();
            StartCoroutine(ResetDeadSoundCooldown());
            canPlayDeadSound = false;
        }
    }

    IEnumerator ResetDeadSoundCooldown()
    {
        yield return new WaitForSeconds(5f); // 2秒待機
        canPlayDeadSound = true;
    }
}
