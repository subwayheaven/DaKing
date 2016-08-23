﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MoodDisplayScript : MonoBehaviour {

    private IMoodEffect[] arrMoodEffects = new IMoodEffect[4];

    static MoodDisplayScript instance;
    void Awake()
    {
        if (MoodDisplayScript.instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            this.init();
            MoodDisplayScript.instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static MoodDisplayScript getInstance()
    {
        return instance;
    }

    private void init()
    {
        arrMoodEffects[0] = new MoodEffectSaturation();
        arrMoodEffects[1] = new MoodEffectVignette();
        arrMoodEffects[2] = new MoodEffectBloomIntensity();
        arrMoodEffects[3] = new MoodEffectBloomBlurSize();
        Debug.Log("Instantiated");
    }

    public void handleMood(int newMood)
    {
        newMood += ResourceManager.instance.getPlayerAttributes().depression;
        float moodPercent = newMood / 100f;
        Debug.Log("moodPercent = "+moodPercent);
        for (int i = 0; i < arrMoodEffects.Length; ++i)
        {
            StartCoroutine(arrMoodEffects[i].updateEffect(moodPercent));
        }
    }
}
