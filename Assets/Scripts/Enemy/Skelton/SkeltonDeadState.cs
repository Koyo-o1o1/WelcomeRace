public class SkeltonDeadState : EnemyState
{
    private Enemy_Skelton enemy;

    public SkeltonDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_Skelton _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        if (stateMachine.currentState != enemy.deadState)
        {
            enemy.SetZeroVelocity();
            stateMachine.ChangeState(enemy.deadState);
        }

        enemy.Invoke("DestroyEnemy", 1f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();
    }



}
