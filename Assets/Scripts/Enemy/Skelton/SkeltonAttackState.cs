using UnityEngine;

public class SkeltonAttackState : EnemyState
{
    private Enemy_Skelton enemy;

    public SkeltonAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_Skelton _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttack=Time.deltaTime;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
