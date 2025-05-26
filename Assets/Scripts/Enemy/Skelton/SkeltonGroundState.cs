using UnityEngine;

public class SkeltonGroundState : EnemyState
{
    protected Enemy_Skelton enemy;
    protected Transform player;

    public SkeltonGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName ,Enemy_Skelton _enemy) : base(_enemyBase, _stateMachiine, _animBooolName)
    {
        this.enemy =  _enemy;
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

        if (enemy.IsplayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 0.6)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
