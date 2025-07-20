using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class UnitFinder
    {
        private const int DefaultTargetsNum = 100;
        private Collider[] hitColliders;

        public UnitFinder(int targetsNum = DefaultTargetsNum)
        {
            hitColliders = new Collider[targetsNum];
        }

        private Unit GetValidUnit(Collider collider)
        {
            Unit unit = collider.GetComponent<Unit>();
            if (unit == null || unit.CurrentUnitState == GameData.UnitBehaviorStateType.Dead) return null;
            return unit;
        }

        public List<Unit> FindAllUnit(Vector3 pos, float radius, LayerMask layerMask, Transform excludeSelfTransform = null) 
        {
            List<Unit> foundTargets = new List<Unit>();
            int numColliders = Physics.OverlapSphereNonAlloc(pos, radius, hitColliders, layerMask);

            for (int i = 0; i < numColliders; i++)
            {
                Collider currentCollider = hitColliders[i];
                Unit currentHitTarget = null;

                if (currentCollider == null) continue;

                if (excludeSelfTransform != null && excludeSelfTransform == currentCollider.transform) continue;

                currentHitTarget = GetValidUnit(currentCollider);

                if (currentHitTarget == null) continue;

                foundTargets.Add(currentHitTarget);

            }

            return foundTargets;
        }

        public Unit FindNearUnit(Vector3 pos, float radius, LayerMask layerMask, Transform excludeSelfTransform = null) 
        {
            float minSqrtDis = radius * radius;
            Unit target = null;

            int numColliders = Physics.OverlapSphereNonAlloc(pos, radius, hitColliders, layerMask);

            for (int i = 0; i < numColliders; i++)
            {
                Collider currentCollider = hitColliders[i];
                Unit currentHitTarget = null;

                if (excludeSelfTransform != null && excludeSelfTransform == currentCollider.transform) continue;

                currentHitTarget = GetValidUnit(currentCollider);

                if (currentHitTarget == null) continue;

                float currentSqrtDis = (currentCollider.transform.position - pos).sqrMagnitude;

                if (minSqrtDis > currentSqrtDis)
                {
                    minSqrtDis = currentSqrtDis;
                    target = currentHitTarget;
                }
            }

            return target; //null이 넘어가면 탐색을 못한 것
        }

    }
}