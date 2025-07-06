using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Behavior
{
    public abstract class UnitBehaviorState : IState
    {
        protected Unit unit;
        public UnitBehaviorState(Unit unit)
        {
            this.unit = unit;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
