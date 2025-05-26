using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = .8f;

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;

        //コンボ終了orコンボ受け付け時間超過
        if (comboCounter > 1 || Time.time >= lastTimeAttacked + comboWindow)
        {
            //コンボカウンターリセット
            comboCounter = 0;
        }

        //その時のコンボカウンターに応じたアニメーション
        player.anim.SetInteger("ComboCounter",comboCounter);

        float attackDir = player.facingDirection;

        if (xInput != 0)
        {
            //コンボ中に攻撃方向を変えられるようにする
            attackDir = xInput;
        }

        //コンボ中の移動
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        //コンボ中を切り替えるコルーチンスタート
        player.StartCoroutine("BusyFor", .1f);

        //コンボカウンターを進める
        comboCounter++;
        //最後の攻撃時間を記憶
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
