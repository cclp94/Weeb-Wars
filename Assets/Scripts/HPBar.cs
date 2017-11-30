using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    int mMaxHealth;
    int mIndex;

    SpriteRenderer[] mFilled;
    [SerializeField]
    public WeebPlayer mWeeb;

    void Start ()
    {
        mFilled = transform.Find("HP").GetComponentsInChildren<SpriteRenderer>();
        mMaxHealth = mFilled.Length;
        mIndex = mFilled.Length - 1;
    }

    public void AddHealth(int x)
    {
        for(int i = 0; i < x; i++)
        {
            mFilled[mIndex].enabled = true;
            if(mIndex == mMaxHealth)
            {
                break;
            }
            mIndex++;
        }
    }

    public void HPChange(int currentHP, int initialHP)
    {
        mIndex = (currentHP * mMaxHealth) / initialHP;
        print(mIndex);
        if(mIndex <= 0)
        {
            mWeeb.Die();
            return;
        }
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        for(int i =0; i < mMaxHealth; i++)
        {
            mFilled[i].enabled = (i <= mIndex) ? true : false;
        }
    }
}
