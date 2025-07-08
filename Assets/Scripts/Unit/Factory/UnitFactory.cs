using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit.Spawning
{
    public class UnitFactory : Factory
    {
        [SerializeField] private Unit[] units;
        public override IProduct GetProduct(Vector3 pos, int id = 0)
        {
            
        }

        public void SpawnUnit()
        {

        }
    }
}
