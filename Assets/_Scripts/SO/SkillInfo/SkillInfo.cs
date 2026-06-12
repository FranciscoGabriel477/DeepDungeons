using System;
using UnityEngine;

public class SkillInfo : ScriptableObject
{
    public string skillName;
    public float staminaCost;
    public float cooldown;
    [Serializable]
    public struct UIInfo
    {
        public Sprite skillSprite;
        public string skillDescription;
    }
    
    public UIInfo UIinfo;
}
