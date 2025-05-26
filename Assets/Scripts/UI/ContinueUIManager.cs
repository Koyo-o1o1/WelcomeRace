using System.Collections;
using UnityEngine;

public class ContinueUIManager : MonoBehaviour
{
    private Enemy enemy;
    private Player player;
    private Character_Stats character_Stats;

    //continue�������肷��ϐ�
    public bool isContinue=false;

    bool canPlayDeadSound = true;


    [Header("UI Elements")]
    public GameObject canvasUI;
    public GameObject background;
    public GameObject continueUI;

    [Header("BGM")]
    public AudioSource normalBgmSource;    // �ʏ�BGM�i�Q�[���J�n����BGM�j
    public AudioSource bossBgmSource;
    [SerializeField] private BossAudioManager bossAudioManager;
    public AudioSource deadAudioSource;
    public AudioClip deadClip;


    [SerializeField] private GameStartManager gameStartManager;

    #region

    [SerializeField] public PotionManager potionManager;

    #endregion

    //�Q�[���J�n���ɍU��������Ȃ��悤�ɂ��邽�߂̕ϐ�
    public static float gameContinueTime = -1f; // �� �ÓI�ɋL�^

    void Start()
    {
        //�v���C���[�擾
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        character_Stats = player.GetComponent<Character_Stats>();

        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

    }

    private void Update()
    {
        // �e�w�݂̂������Ă�����X�^�[�g
        if (player != null && player.isContinue)
        {
            OnContinueButtonClicked(); // �X�^�[�g�������s
        }
    }

    public void Continue_UI_Start()
    {
        // �ʏ�BGM���~
        if (normalBgmSource != null && normalBgmSource.isPlaying)
        {
            normalBgmSource.Stop();
        }

        // BossBGM���~
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

        //�L�����̐F��߂�
        player.fx.ResetMaterial();


        PlayDeadSound();

        //deadAudioSource.clip = deadClip;
        //deadAudioSource.Play();




    }

    // �Z�[�u�|�C���g���o�^����Ă��邩�`�F�b�N
    private bool HasSavePoint()
    {
        if (GameManager.Instance == null) return false;

        return GameManager.Instance.GetLastSavePoint() != Vector3.zero;
    }

    //�N���b�N�����ꂽ�Ƃ�
    public void OnContinueButtonClicked()
    {
        gameContinueTime = Time.time;

        // 1�b��ɃQ�[�����ĊJ
        StartCoroutine(ContinueGameAfterDelay(1f));

        //�Z�[�u�|�C���g�X�V
        player.transform.position = GameManager.Instance.GetLastSavePoint();
        //HP��
        character_Stats.currentHealth=character_Stats.GetMaxHealthValue();

        // HP�o�[���f(?��null check)
        character_Stats.onHealthChanged?.Invoke();

        //�L�����̐F��߂�
        player.fx.ResetMaterial();

        player.stateMachine.ChangeState(player.idleState);

        // �G�̏����ʒu��HP�����Z�b�g
        enemy.ResetEnemies();

        //bgm�����Ȃ���
        gameStartManager.PlayBGM();

        player.isContinue = false;
    }

    

    //[start]�N���b�N���b���Ă���Q�[���X�^�[�g
    IEnumerator ContinueGameAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        

        potionManager.currentPotions = potionManager.maxPotions;
        potionManager.UpdatePotionUI();


        //�v���C���[���E������
        player.FaceRight();

        // UI���\��
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
        yield return new WaitForSeconds(5f); // 2�b�ҋ@
        canPlayDeadSound = true;
    }
}
