using UnityEngine;

public class SkeltonMoveState : SkeltonGroundState
{
    public SkeltonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachiine, string _animBooolName, Enemy_Skelton _enemy) : base(_enemyBase, _stateMachiine, _animBooolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //���x��^����

        enemy.SetVelocity(enemy.moveSpeed*enemy.facingDirection,rb.linearVelocity.y);

        //�ǂ̌��oor�n�ʂ��Ȃ�
        if (enemy.isWallDetcted() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
            
        }
    }
}
