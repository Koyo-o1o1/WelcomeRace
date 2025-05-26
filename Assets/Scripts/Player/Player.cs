using System.Collections;
using System.Collections.Generic;




//leap
using Leap;
using NUnit.Framework;
using UnityEngine;

public class Player : Entity
{

    [Header("BGM Settings")]
    public AudioSource landing_bgmSource;
    public AudioClip landing_bgmClip;

    //leap
    public Controller controller;
    public bool isStartPose = false;
    public bool isContinue = false;
    [SerializeField] ContinueUIManager continueUIManager;
    [SerializeField] GameClearManager gameClearManager;

    //左手
    //左手の回転
    public float zRotation;
    //左手の上向き速度
    public float yVelocity;
    public bool leapJump = false;
    public bool isLeftHand = false;

    //右手
    //右手の前向き速度
    public float forwardSpeed;
    public float grabStrengh;
    public bool leapAttack = false;
    

    [Header("Attack info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    //コンボ中か判定する変数
    public bool isBusy { get; private set; }

    [Header("Move info")]
    public float moveSpeed = 8f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsaagTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }



    //クリスタルの攻撃
    [Header("Crystal info")]
    public GameObject crystalPrefab; // クリスタルのプレハブ
    public Transform firePoint; // クリスタル発射位置
    private float lastShootTime = 0f; // 最後に撃った時間
    public float shootCooldown = 2f; // 2秒のクールダウン
    private bool canShootCrystal = true; // クリスタル発射可能か
    private float lastGrabTime = 0f; // **手を握っていた最後の時間**
    private float firstReleaseTime = 0f; // **手を開いた最初の時間**
    private float releaseWindow = 1f; // **1秒以内に開いたら攻撃**
    private bool wasGrabbing = false; // **前フレームの握り状態を記録**

    //回復に用いる
    private List<bool> fingerBool = new List<bool>();
    [SerializeField] private PotionManager potionManager;




    //状態
    #region States

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();

        //leap
        controller = new Controller();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");

        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        // まだゲームスタート直後1秒以内なら攻撃等を無効化
        if (Time.realtimeSinceStartup - GameStartManager.realGameStartTime < 1.1f)
            return;

        


        //leap

        //手を取得
        Frame frame = controller.Frame();

        //手が少なくとも片方検出されたとき
        if (frame != null)
        {
            foreach (Hand hand in frame.Hands)
            {
                //左手か確認
                if (hand.IsLeft)
                {
                    // continue中は移動できないように
                    if (!continueUIManager.isContinue && !gameClearManager.gameClear)
                    {

                        //移動

                        //左手の手のひらの角度の確認
                        Quaternion handRotation = hand.Rotation;
                        Vector3 eulerAngles = handRotation.eulerAngles;
                        zRotation = eulerAngles.z;


                        //ジャンプ

                        //左手の手のひらの速度を取得
                        Vector3 palmVelocity = hand.PalmVelocity;
                        yVelocity = palmVelocity.y;

                        isLeftHand = true;
                    }
                }
                else
                {
                    isLeftHand = false;
                }


                //右手か確認
                if (hand.IsRight)
                {
                    fingerBool.Clear(); // 毎フレーム更新するため、最初にリストをクリア
                    int cnt = 0; //立っている本数

                    //すべてのゆびを確認
                    for (int i = 0; i < 5; i++)
                    {
                        Finger finger = hand.fingers[i];
                        // 指が立っている＝伸びていると判定（伸び具合が0.8以上などで判断）
                        bool isExtended = finger.IsExtended;
                        if (isExtended)
                        {
                            cnt++;
                        }
                        fingerBool.Add(isExtended);
                    }


                    // continue中は移動できないように
                    if (continueUIManager.isContinue || gameClearManager.gameClear)
                    { 
                        

                        //コンテニュー処理
                        if (cnt == 2 && fingerBool[1] && fingerBool[2] && continueUIManager.isContinue)
                        {
                            isContinue = true;
                        }
                        else
                        {
                            isContinue = false;
                        }

                        return;
                    }

                    if (!continueUIManager.isContinue)
                    {

                        //攻撃
                        Vector3 palmVelocity = hand.PalmVelocity;
                        Vector3 palmNormal = hand.PalmNormal;

                        //前向き速度
                        forwardSpeed = palmVelocity.z;
                        //握り度合い
                        grabStrengh = hand.GrabStrength;

                        //握っているとき
                        if (grabStrengh > 0.6)
                        {
                            //0.8以上のスピードがあるとき
                            if (forwardSpeed > 0.8)
                            {
                                //現在AttackStateでないとき
                                if (stateMachine.currentState != primaryAttack)
                                {
                                    leapAttack = true;
                                }
                            }
                        }


                        //クリスタル
                        bool canAttack = (stateMachine.currentState == moveState || stateMachine.currentState == idleState);


                        // 一番最後に手を握っていた時間
                        if (grabStrengh > 0.6)
                        {
                            lastGrabTime = Time.time; // **手を握った最後の時間を記録**
                            wasGrabbing = true;
                        }





                        // 手を開いた瞬間を記録
                        if (grabStrengh < 0.3f && wasGrabbing)
                        {
                            firstReleaseTime = Time.time; // **手を開いた最初の時間を記録**
                            wasGrabbing = false;

                            // 1秒以内に開いた場合のみ攻撃
                            if (firstReleaseTime - lastGrabTime <= releaseWindow && canShootCrystal && canAttack)
                            {
                                //すべての指が立っているとき
                                if (cnt == 5)
                                {
                                    lastShootTime = Time.time;
                                    FireCrystal();
                                    canShootCrystal = false;
                                    StartCoroutine(ResetCrystalCooldown());
                                }
                            }
                        }

                        //スタート判定
                        // 右手の指の状態を取得する部分の続きに追加
                        if (cnt == 1 && fingerBool[0] == true)
                        {
                            isStartPose = true;
                        }
                        else
                        {
                            isStartPose = false;
                        }

                        //回復処理
                        if (cnt == 1 && fingerBool[4] == true)
                        {
                            potionManager.UsePotion();
                        }
                    }

                }
            }


            // 確認用(keypad and mause)

            // start
            if (Input.GetKeyDown(KeyCode.S))
            {
                isStartPose = true;
            }

            // continue(手動)
            if (Input.GetKeyDown(KeyCode.C))
            {
                isContinue = true;
            }

            // attack
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                leapAttack = true;
            }

            // clystal
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                lastShootTime = Time.time;
                FireCrystal();
                canShootCrystal = false;
                StartCoroutine(ResetCrystalCooldown());

            }

            // potion
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                potionManager.UsePotion();
            }




        }
        //leap

        stateMachine.currentState.Update();

    }

    //コンボ中を切り替えるコルーチン
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    //アニメーション終了をさせる関数
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();





    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }




    //クリスタル
    void FireCrystal()
    {
        if (crystalPrefab != null && firePoint != null)
        {
            Instantiate(crystalPrefab, firePoint.position, firePoint.rotation);
        }
    }


    private IEnumerator ResetCrystalCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShootCrystal = true; // 2秒後に再発射可能
    }


    //gameBGM
    public void PlayBGM_Landing()
    {

        if (landing_bgmSource != null && landing_bgmClip != null)
        {
            landing_bgmSource.clip = landing_bgmClip;
            landing_bgmSource.Play();
        }
    }
}
