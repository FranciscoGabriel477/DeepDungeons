using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    public List<LevelGroup> levels;

    [Serializable]
    public struct LevelInfo
    {
        public string levelName;
        public Vector3 levelStart;
    }

    [Serializable]
    public struct LevelGroup
    {
        public string groupName;
        public List<LevelInfo> levelList;
    }
}