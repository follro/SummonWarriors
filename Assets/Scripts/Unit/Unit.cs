using System.Collections;
using System.Collections.Generic;
using Unit.Behavior;
using UnityEngine;

namespace Unit
{
    public class Unit : MonoBehaviour, IProduct
    {
        public UnitData UnitStatus { get; set; } //ÀÓ½Ã À¯´Ö ½ºÅÝ
        public Unit TargetedUnit { get; private set; }
        /*
         * À¯´Ö ÀÌµ¿ °ü·Ã 
         * À¯´Ö ¾Ö´Ï¸ÞÀÌ¼Ç °ü·Ã
         * À¯´Ö Å¸°ÙÆÃ °ü·Ã
         */

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
