using UnityEngine;
using UnityEngine.Analytics;

public class PlayerState
{

    protected PlayerStateMachine stateMachine;
    protected Player player;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;

    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    //��Ԃ��X�V?
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }


    public virtual void Enter()
    {
        //�w�肳�ꂽ�A�j���[�V������true��
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.time;

        //�㉺�A���E���͂��󂯎��
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        //leap


        //�ړ�
        //20-150�x�ō��ړ�
        if (20 <= player.zRotation && player.zRotation <= 90)
        {
            xInput = -1;
        }
        //200-340�x�ŉE�ړ�
        else if (300 < player.zRotation && player.zRotation < 340)
        {
            xInput = 1;
        }

        //�W�����v
        if (player.yVelocity > 1f)
        {
            if (player.IsGroundDetected())
            {
                yInput = 1;
                player.leapJump = true;
            }
            else
            {
                player.leapJump = false;
            }
        }

        //leap



        //�W�����v�̏㏸�A���~�A�j���[�V�����̑J��
        player.anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
