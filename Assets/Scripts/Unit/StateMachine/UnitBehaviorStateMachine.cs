using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit.Behavior
{
    public class UnitBehaviorStateMachine
    {
        public IState CurrentState { get; private set; }
        private UnitBehaviorState[] unitBehaviorStates;

        public UnitBehaviorStateMachine(Unit unit)
        {
            int endIndex = (int)GameData.UnitBehaviorStateType.End;
            unitBehaviorStates = new UnitBehaviorState[endIndex];
            for (int i = 0; i < endIndex; i++)
            {
                switch(i)
                {
                    case (int)GameData.UnitBehaviorStateType.Move:
                        unitBehaviorStates[i] = new MovementState(unit);
                        break;
                    case (int)GameData.UnitBehaviorStateType.Attack:
                        unitBehaviorStates[i] = new AttackState(unit);
                        break;
                    case (int)GameData.UnitBehaviorStateType.Dead:
                        unitBehaviorStates[i] = new DeadState(unit); 
                        break;
                    default:
                        unitBehaviorStates[i] = null;
                        break;
                }
            }

            Initialize(GameData.UnitBehaviorStateType.Move);
        }
        public void Initialize(GameData.UnitBehaviorStateType startingStateType)
        {
            IState startingState = StateTypeToState(startingStateType);

            if (startingState != null)
            {
                CurrentState = startingState;
                startingState.Enter();
            }
        }

        private IState StateTypeToState(GameData.UnitBehaviorStateType stateType, Unit unit = null)
        {
            IState state = null;
            if (unitBehaviorStates[(int)stateType] != null)
                state = unitBehaviorStates[(int)stateType];

            return state;
        }

        public void TransitionTo(GameData.UnitBehaviorStateType nextStateType)
        {
            IState nextState =  StateTypeToState(nextStateType);

            if (nextState != null && nextState != CurrentState)
            {
                CurrentState.Exit();
                CurrentState = nextState;
                nextState.Enter();
            }
        }
        public void Update()
        {
            if(CurrentState != null) 
                CurrentState.Update();  
        }
        public void FixedUpdate()
        {
            if (CurrentState != null)
                CurrentState.FixedUpdate();
        }
    }
}