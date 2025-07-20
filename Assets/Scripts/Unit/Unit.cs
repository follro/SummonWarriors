using System.Collections;
using System.Collections.Generic;
using Unit.Behavior;
using UnityEngine;

namespace Unit
{
    public class Unit : MonoBehaviour, IProduct
    {
        public UnitData UnitStatus { get; set; } //¿”Ω√ ¿Ø¥÷ Ω∫≈›
        public GameData.UnitBehaviorStateType CurrentUnitState { get { return stateMachine.CurrentStateType; } }
        private UnitBehaviorStateMachine stateMachine;

        #region Unity Lifecycle Methods
        private void Awake()
        {
            Initialize();
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            stateMachine.Update();  
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        #endregion
        
        public void Initialize()
        {
            stateMachine = new UnitBehaviorStateMachine(this);
        }

        
    }
}
