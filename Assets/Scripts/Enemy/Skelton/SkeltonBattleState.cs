using UnityEngine;

public class SkeltonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skelton enemy;
    private int moveDirection;

    public SkeltonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName,Enemy_Skelton _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //追加
        //壁の検出or地面がない
        if (enemy.isWallDetcted() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);

        }

        //プレイヤー検出
        if (enemy.IsplayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsplayerDetected().distance < enemy.attacckDistance)
            {
                if(canAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        //バトル状態中に見失うorバトル状態の時間を過ぎた場合
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.transform.position,enemy.transform.position) > 15)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

        //プレイヤーの方が右にいる場合
        if (player.position.x > enemy.transform.position.x)
        {
            moveDirection = 1;
        }
        //プレイヤーのほうが左にいる場合
        else
        {
            moveDirection = -1;
        }

        //速度を与える
        enemy.SetVelocity(enemy.moveSpeed * moveDirection, rb.linearVelocity.y);
    }

    //攻撃の間隔を制御
    private bool canAttack()
    {
        if (Time.time >= enemy.lastTimeAttack + enemy.attackCooldown)
        {
            enemy.lastTimeAttack = Time.time;
            return true;
        }

        return false;
    }
}
