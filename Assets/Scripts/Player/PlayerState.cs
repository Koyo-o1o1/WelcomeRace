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

    //状態を更新?
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }


    public virtual void Enter()
    {
        //指定されたアニメーションをtrueへ
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.time;

        //上下、左右入力を受け取る
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        //leap


        //移動
        //20-150度で左移動
        if (20 <= player.zRotation && player.zRotation <= 90)
        {
            xInput = -1;
        }
        //200-340度で右移動
        else if (300 < player.zRotation && player.zRotation < 340)
        {
            xInput = 1;
        }

        //ジャンプ
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



        //ジャンプの上昇、下降アニメーションの遷移
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
