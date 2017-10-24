using UnityEngine;
using System.Collections;

public class BusterGun : MonoBehaviour
{
    Animator mAnimator;
    bool mShooting;

    float kShootDuration = 0.25f;
    float mTime;

    [SerializeField]
    GameObject mBulletPrefab;
    MegaMan mMegaManRef;

    AudioSource mBusterSound;
    SpriteRenderer playerSpriteRenderer;

    Texture2D mColorSwapTex;

    public void InitColorSwapTex()
    {
        mColorSwapTex = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);
        mColorSwapTex.filterMode = FilterMode.Point;
        mColorSwapTex.SetPixel(0, 0, new Color(1.0f, 0.0f, 0.0f, 0.0f));
        mColorSwapTex.Apply();
        playerSpriteRenderer.material.SetTexture("_SwapTex", mColorSwapTex);
    }

    void SwapColor(Color c)
    {
        mColorSwapTex.SetPixel(0, 0, c);
        mColorSwapTex.Apply();
    }

    void Start ()
    {
        mAnimator = transform.parent.GetComponent<Animator>();
        mMegaManRef = transform.parent.GetComponent<MegaMan>();
        playerSpriteRenderer = mMegaManRef.GetComponent<SpriteRenderer>();
        mBusterSound = GetComponent<AudioSource>();
        InitColorSwapTex();
    }

    void Update ()
    {
        if(Input.GetButtonDown ("Fire"))
        {
            // Shoot bullet
            GameObject bulletObject = Instantiate (mBulletPrefab, transform.position, Quaternion.identity) as GameObject;
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            // Set direction of bullet
            bullet.SetDirection(mMegaManRef.GetFacingDirection());

            // Set animation params
            mShooting = true;
            mTime = 0.0f;

            // Play sound
            mBusterSound.Play ();
        }

        if(mShooting)
        {
            mTime += Time.deltaTime;
            if(mTime > kShootDuration)
            {
                mShooting = false;
            }
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool ("isShooting", mShooting);
    }
}
