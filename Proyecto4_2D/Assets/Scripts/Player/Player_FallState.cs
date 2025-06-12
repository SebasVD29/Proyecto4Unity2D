using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FallState : Player_AiredState
{
    public Player_FallState(Player player, StateMachine stateMachine, string _animBoolName) : base(player, stateMachine, _animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }


}
