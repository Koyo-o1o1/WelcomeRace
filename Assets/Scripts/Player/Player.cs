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

    //����
    //����̉�]
    public float zRotation;
    //����̏�������x
    public float yVelocity;
    public bool leapJump = false;
    public bool isLeftHand = false;

    //�E��
    //�E��̑O�������x
    public float forwardSpeed;
    public float grabStrengh;
    public bool leapAttack = false;
    

    [Header("Attack info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    //�R���{�������肷��ϐ�
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



    //�N���X�^���̍U��
    [Header("Crystal info")]
    public GameObject crystalPrefab; // �N���X�^���̃v���n�u
    public Transform firePoint; // �N���X�^�����ˈʒu
    private float lastShootTime = 0f; // �Ō�Ɍ���������
    public float shootCooldown = 2f; // 2�b�̃N�[���_�E��
    private bool canShootCrystal = true; // �N���X�^�����ˉ\��
    private float lastGrabTime = 0f; // **��������Ă����Ō�̎���**
    private float firstReleaseTime = 0f; // **����J�����ŏ��̎���**
    private float releaseWindow = 1f; // **1�b�ȓ��ɊJ������U��**
    private bool wasGrabbing = false; // **�O�t���[���̈����Ԃ��L�^**

    //�񕜂ɗp����
    private List<bool> fingerBool = new List<bool>();
    [SerializeField] private PotionManager potionManager;




    //���
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

        // �܂��Q�[���X�^�[�g����1�b�ȓ��Ȃ�U�����𖳌���
        if (Time.realtimeSinceStartup - GameStartManager.realGameStartTime < 1.1f)
            return;

        


        //leap

        //����擾
        Frame frame = controller.Frame();

        //�肪���Ȃ��Ƃ��Е����o���ꂽ�Ƃ�
        if (frame != null)
        {
            foreach (Hand hand in frame.Hands)
            {
                //���肩�m�F
                if (hand.IsLeft)
                {
                    // continue���͈ړ��ł��Ȃ��悤��
                    if (!continueUIManager.isContinue && !gameClearManager.gameClear)
                    {

                        //�ړ�

                        //����̎�̂Ђ�̊p�x�̊m�F
                        Quaternion handRotation = hand.Rotation;
                        Vector3 eulerAngles = handRotation.eulerAngles;
                        zRotation = eulerAngles.z;


                        //�W�����v

                        //����̎�̂Ђ�̑��x���擾
                        Vector3 palmVelocity = hand.PalmVelocity;
                        yVelocity = palmVelocity.y;

                        isLeftHand = true;
                    }
                }
                else
                {
                    isLeftHand = false;
                }


                //�E�肩�m�F
                if (hand.IsRight)
                {
                    fingerBool.Clear(); // ���t���[���X�V���邽�߁A�ŏ��Ƀ��X�g���N���A
                    int cnt = 0; //�����Ă���{��

                    //���ׂĂ̂�т��m�F
                    for (int i = 0; i < 5; i++)
                    {
                        Finger finger = hand.fingers[i];
                        // �w�������Ă��遁�L�тĂ���Ɣ���i�L�ы��0.8�ȏ�ȂǂŔ��f�j
                        bool isExtended = finger.IsExtended;
                        if (isExtended)
                        {
                            cnt++;
                        }
                        fingerBool.Add(isExtended);
                    }


                    // continue���͈ړ��ł��Ȃ��悤��
                    if (continueUIManager.isContinue || gameClearManager.gameClear)
                    { 
                        

                        //�R���e�j���[����
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

                        //�U��
                        Vector3 palmVelocity = hand.PalmVelocity;
                        Vector3 palmNormal = hand.PalmNormal;

                        //�O�������x
                        forwardSpeed = palmVelocity.z;
                        //����x����
                        grabStrengh = hand.GrabStrength;

                        //�����Ă���Ƃ�
                        if (grabStrengh > 0.6)
                        {
                            //0.8�ȏ�̃X�s�[�h������Ƃ�
                            if (forwardSpeed > 0.8)
                            {
                                //����AttackState�łȂ��Ƃ�
                                if (stateMachine.currentState != primaryAttack)
                                {
                                    leapAttack = true;
                                }
                            }
                        }


                        //�N���X�^��
                        bool canAttack = (stateMachine.currentState == moveState || stateMachine.currentState == idleState);


                        // ��ԍŌ�Ɏ�������Ă�������
                        if (grabStrengh > 0.6)
                        {
                            lastGrabTime = Time.time; // **����������Ō�̎��Ԃ��L�^**
                            wasGrabbing = true;
                        }





                        // ����J�����u�Ԃ��L�^
                        if (grabStrengh < 0.3f && wasGrabbing)
                        {
                            firstReleaseTime = Time.time; // **����J�����ŏ��̎��Ԃ��L�^**
                            wasGrabbing = false;

                            // 1�b�ȓ��ɊJ�����ꍇ�̂ݍU��
                            if (firstReleaseTime - lastGrabTime <= releaseWindow && canShootCrystal && canAttack)
                            {
                                //���ׂĂ̎w�������Ă���Ƃ�
                                if (cnt == 5)
                                {
                                    lastShootTime = Time.time;
                                    FireCrystal();
                                    canShootCrystal = false;
                                    StartCoroutine(ResetCrystalCooldown());
                                }
                            }
                        }

                        //�X�^�[�g����
                        // �E��̎w�̏�Ԃ��擾���镔���̑����ɒǉ�
                        if (cnt == 1 && fingerBool[0] == true)
                        {
                            isStartPose = true;
                        }
                        else
                        {
                            isStartPose = false;
                        }

                        //�񕜏���
                        if (cnt == 1 && fingerBool[4] == true)
                        {
                            potionManager.UsePotion();
                        }
                    }

                }
            }


            // �m�F�p(keypad and mause)

            // start
            if (Input.GetKeyDown(KeyCode.S))
            {
                isStartPose = true;
            }

            // continue(�蓮)
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

    //�R���{����؂�ւ���R���[�`��
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    //�A�j���[�V�����I����������֐�
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();





    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }




    //�N���X�^��
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
        canShootCrystal = true; // 2�b��ɍĔ��ˉ\
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
