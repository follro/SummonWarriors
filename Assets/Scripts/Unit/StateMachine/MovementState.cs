using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Behavior
{
    public class MovementState : UnitBehaviorState
    {
        public MovementState(Unit unit) : base(unit)
        {
        }

        public override void Enter()
        {
            Debug.Log("MoveMentState OnEnter");
        }

        public override void Exit()
        {
            Debug.Log("MoveMentState OnExit");
        }

        public override void FixedUpdate()
        {
            Debug.Log("MovementState OnFixedUpdate");
        }

        public override void Update()
        {
            Debug.Log("MovementState OnUpdate");
        }   
    }
}