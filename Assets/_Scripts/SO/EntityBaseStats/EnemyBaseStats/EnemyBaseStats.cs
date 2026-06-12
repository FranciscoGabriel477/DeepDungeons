using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
public abstract class EnemyBaseStats : EntityBaseStats
{
    public float rangeOfVision;
    public float patrolRange;
    public float minWardTime;
    public float maxWardTime;
    public float chaseRange;
    public float coolingTime;
    public float stance;
    [Serializable]
    public struct AttackData
    {
        public string attackName;
        public float attackRange;
        public float attackTime;
        public float cooldown;
    }

    public List<AttackData> attackDatas;
    public float experienceDroped;
}
