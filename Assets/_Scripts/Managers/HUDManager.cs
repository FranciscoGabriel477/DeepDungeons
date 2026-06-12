using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;


public class HUDManager: MonoBehaviour
{
    public static HUDManager instance{get; private set;}
    public Image lifeBar;
    public Image StaminaBar;
    //public Image ExperienceBar;
    public Image skillSlot1;
    public Image skillSlot2;
    public Image skillSlot1Cooldown;
    public Image skillSlot2Cooldown;
    /*public GameObject skillChoices;
    public Image skillChoice1Image;
    public Image skillChoice2Image;
    public TextMeshProUGUI skillChoice1Text;
    public TextMeshProUGUI skillChoice2Text;*/
    public Image ScreenFade;
    public Image ScreenGameOverFade;
    public GameObject gameOverButton;
    //public int slotSelected=0;
    //int skill1Index;
    //int skill2Index;
    //public List<SkillInfo> skillInfos;
    //public EventHandler<SkillInfoSelected> OnSkillSelected;
    public class SkillInfoSelected
    {
        public SkillInfo skillinfo;
        public int slot;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        //PlayerLevelUp();
    }
    public void PlayerLifeChange(object sender, PlayerStats.LifeInfo lifeInfo)
    {
        lifeBar.fillAmount=lifeInfo.currentLife/lifeInfo.currentMaxLife;
    }
    public void PlayerStaminaChange(object sender, PlayerStats.StaminaInfo staminaInfo)
    {
        StaminaBar.fillAmount=staminaInfo.currentStamina/staminaInfo.currentMaxStamina;
    }
    /*public void PlayerExperienceChange(object sender, PlayerStats.ExperienceInfo experienceInfo)
    {
        ExperienceBar.fillAmount=experienceInfo.currentExperience/experienceInfo.experienceToUp;
    }

    public void PlayerLevelUp()
    {
        skillChoices.SetActive(true);
        skill1Index = Random.Range(0,skillInfos.Count);
        do
        {
            skill2Index=Random.Range(0,skillInfos.Count);
        }while (skill1Index == skill2Index);
        skillChoice1Image.sprite=skillInfos[skill1Index].UIinfo.skillSprite;
        skillChoice1Text.text=skillInfos[skill1Index].UIinfo.skillDescription;
        skillChoice2Image.sprite=skillInfos[skill2Index].UIinfo.skillSprite;
        skillChoice2Text.text=skillInfos[skill2Index].UIinfo.skillDescription;
    }
    public void SkillSelected(int choice)
    {
        if (choice == 1)
        {
            OnSkillSelected?.Invoke(this, new SkillInfoSelected{skillinfo=skillInfos[skill1Index],slot=slotSelected});
        }
        if (choice == 2)
        {
            OnSkillSelected?.Invoke(this, new SkillInfoSelected{skillinfo=skillInfos[skill2Index],slot=slotSelected});
        }
        slotSelected=0;
        skillChoices.SetActive(false);
    }*/

    public void ChangeScreenFade(float fade)
    {
        Color temp=ScreenFade.color;
        temp.a=fade;
        ScreenFade.color=temp;
    }
    public void ChangeScreenGameOverFade(float fade)
    {
        Color temp=ScreenGameOverFade.color;
        temp.a=fade;
        ScreenGameOverFade.color=temp;
    }

    public void PlayAgainButtonPressed()
    {
        Debug.Log("A");
        GameManager.instance.PlayAgain();
    }
    
}