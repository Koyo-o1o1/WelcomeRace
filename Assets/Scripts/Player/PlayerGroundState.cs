using UnityEngine;
using UnityEngine.InputSystem.LowLevel;


public class PlayerGroundState : PlayerState
{

    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
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

        // ‚Ü‚¾ƒQ[ƒ€ƒXƒ^[ƒg’¼Œã1•bˆÈ“à‚È‚çUŒ‚‚ğ–³Œø‰»
        if (Time.time - GameStartManager.gameStartTime < 1.1f)
            return;


        //ƒ}ƒEƒX‚ª‰Ÿ‚³‚ê‚½‚Æ‚« //leap
        if (Input.GetKeyDown(KeyCode.Mouse0) || player.leapAttack)
        {
            //leap
            player.leapAttack = false;

            //UŒ‚
            stateMachine.ChangeState(player.primaryAttack);

            
        }

        //°‚ÆÚG‚µ‚Ä‚¢‚È‚¢‚Æ‚«
        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }

        //ƒWƒƒƒ“ƒv‚Ö‘JˆÚ(°‚ÆÚG‚µ‚Ä‚¢‚é‚Ì‚İ) //leap
        if ((Input.GetKey(KeyCode.UpArrow) || player.leapJump) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);

            //leap
            yInput = 0;
            player.leapJump = false;
        }

        
    }

}
