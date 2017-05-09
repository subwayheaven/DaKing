﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerAttributes : MonoBehaviour
{
    //Money
    public static int newDayMoney;
    public int money;
    public Text moneyText;

    //Military
    public static int newDayMilitary;
    public int military;
    public Text militaryText;

    //Depression
    public static int newDayDepression;
    public int depression;

    //Depression sprites
    public Sprite noDepressionKing;
    public Sprite quarterPercentDepressionKing;
    public Sprite halfPercentDepressionKing;
    public Sprite threeQuarterPercentDepressionKing;

    //Whats the highest value for depression?
    public int maxDepression = 100;

    public int noDepressionValue;
    public int threeQuarterPercentDepressionValue;
    public int halfPercentDepressionValue;
    public int quarterPercentDepressionValue;

    //Flash text contorller
    public FlashTextController flashTextController;

    private Animator animator;

    private bool hasAlreadyChangedMood = false;

    public bool HasAlreadyChangedMood
    {
        get
        {
            return hasAlreadyChangedMood;
        }

        set
        {
            hasAlreadyChangedMood = value;
        }
    }

    void Start()
    {
        //Make note of starting stats
        newDayMoney = money;
        newDayMilitary = military;
        newDayDepression = depression;

        moneyText.text = money.ToString();
        militaryText.text = military.ToString();

        animator = GetComponent<Animator>();
    }

    public void setMoney(int newVal)
    {
        money = newVal;
        moneyText.text = money.ToString();
        // Debug.Log("PlayerAttribute.money: " + money);
    }

    public void setMilitary(int newVal)
    {
        military = newVal;
        militaryText.text = military.ToString();
    }

    public void setMood(int newVal)
    {
        depression = newVal;
        GlobalReferencesBehaviour.instance.SceneData.controller.GetComponent<MoodDisplayScript>().handleMood(depression);
    }

    public void flashTextValues(int moneyAmount, int militaryAmount, int depressionAmount)
    {
        flashTextController.flash(moneyAmount, militaryAmount, depressionAmount);
    }

    public void setNewDayValues()
    {
        //Make note of starting stats
        newDayMoney = money;
        newDayMilitary = military;
        newDayDepression = depression;
    }

    public void setMoodState(int mood)
    {
        if (hasAlreadyChangedMood) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetInteger("mood", mood);



    }

}
