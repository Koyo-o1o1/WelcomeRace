using UnityEngine;

public class SkeltonIdleState : SkeltonGroundState
{
    public SkeltonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_Skelton _enemy) : base(_enemyBase, _stateMachiine, _animBooolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
