using GameData;


namespace Unit.Behavior
{
    public class UnitBehaviorStateMachine
    {
        public UnitBehaviorStateType CurrentStateType { get; private set; }
        private IState CurrentState { get { return unitBehaviorStates[(int)CurrentStateType]; } }
        private UnitBehaviorState[] unitBehaviorStates;

        public UnitBehaviorStateMachine(Unit unit)
        {
            int endIndex = (int)GameData.UnitBehaviorStateType.End;
            unitBehaviorStates = new UnitBehaviorState[endIndex];
            for (int i = 0; i < endIndex; i++)
            {
                switch(i)
                {
                    case (int)GameData.UnitBehaviorStateType.Idle:
                        unitBehaviorStates[i] = new IdleState(unit);
                        break;
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
                        unitBehaviorStates[i] = new IdleState(unit);
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
                CurrentStateType = startingStateType;
                startingState.Enter();
            }
        }

        private IState StateTypeToState(GameData.UnitBehaviorStateType stateType, Unit unit = null)
        {
            if ((int)stateType < 0 || (int)stateType >= unitBehaviorStates.Length)
            {
                UnityEngine.Debug.LogError($"{typeof(UnitBehaviorStateMachine).Name}: " +
                    $"{stateType}의 인덱스가 맞지 않습니다.");
                return null;
            }

            return unitBehaviorStates[(int)stateType];
        }

        public void TransitionTo(GameData.UnitBehaviorStateType nextStateType)
        {
            IState nextState =  StateTypeToState(nextStateType);

            if (nextState != null && nextState != CurrentState)
            {
                CurrentState.Exit();
                CurrentStateType = nextStateType;
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