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
                //일단은 MovementState만 넣음 생성관련 함수 만들어줘야됨
                unitBehaviorStates[i] = new MovementState(unit);    
            }
        }
        
        private IState StateTypeToState(GameData.UnitBehaviorStateType stateType)
        {
            IState state = CurrentState;

            //Enum에 따른 State 업데이트 넣어줘야됨
            switch (stateType)
            {
                case GameData.UnitBehaviorStateType.Move:
                    if (unitBehaviorStates[(int)stateType] == null)
                        state = new MovementState(); // 중간에 하다맘
                    break;
                case GameData.UnitBehaviorStateType.Attack:
                    break;
                case GameData.UnitBehaviorStateType.Dead:
                    break;
                default:
                    state = null; //일단은  NULL 나중에 기본 상태 넣어줘야됨
                    break;
            }
            return state;
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