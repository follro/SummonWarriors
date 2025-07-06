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
                //�ϴ��� MovementState�� ���� �������� �Լ� �������ߵ�
                unitBehaviorStates[i] = new MovementState(unit);    
            }
        }
        
        private IState StateTypeToState(GameData.UnitBehaviorStateType stateType)
        {
            IState state = CurrentState;

            //Enum�� ���� State ������Ʈ �־���ߵ�
            switch (stateType)
            {
                case GameData.UnitBehaviorStateType.Move:
                    if (unitBehaviorStates[(int)stateType] == null)
                        state = new MovementState(); // �߰��� �ϴٸ�
                    break;
                case GameData.UnitBehaviorStateType.Attack:
                    break;
                case GameData.UnitBehaviorStateType.Dead:
                    break;
                default:
                    state = null; //�ϴ���  NULL ���߿� �⺻ ���� �־���ߵ�
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