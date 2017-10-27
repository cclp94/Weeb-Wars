using UnityEngine;
using System.Collections;

public class AllienGun : MonoBehaviour
{
    Animator mAnimator;
    bool mShooting;

    Vector2 bDirection;

    float kShootDuration = 0.25f;
    float mTime;

    [SerializeField]
    GameObject mBulletPrefab;
    WeebPlayer mWeeb;

    AudioSource mBusterSound;
    SpriteRenderer playerSpriteRenderer;

    Texture2D mColorSwapTex;

    public void InitColorSwapTex()
    {
        mColorSwapTex = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);
        mColorSwapTex.filterMode = FilterMode.Point;
        mColorSwapTex.SetPixel(0, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        mColorSwapTex.Apply();
        playerSpriteRenderer.material.SetTexture("_SwapTex", mColorSwapTex);
    }

    public void SwapColor(Color c)
    {
        mColorSwapTex.SetPixel(0, 0, c);
        mColorSwapTex.Apply();
    }

    void Awake()
    {
        mAnimator = transform.parent.GetComponent<Animator>();
        mWeeb = transform.parent.GetComponent<WeebPlayer>();
        playerSpriteRenderer = mWeeb.GetComponent<SpriteRenderer>();
        mBusterSound = GetComponent<AudioSource>();
        InitColorSwapTex();

    }

    void Update ()
    {
        if(Input.GetButtonDown ("Fire") && !mWeeb.IsStunned())
        {
            // Shoot bullet
            GameObject bulletObject = Instantiate (mBulletPrefab, transform.position, Quaternion.identity) as GameObject;
            GunUpgrade bullet = bulletObject.GetComponent<GunUpgrade>();
            Vector2 facingDirection = mWeeb.GetFacingDirection();

            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bDirection = cursorPos - (Vector2)transform.position;
            bDirection.Normalize();
            Debug.Log(bDirection.x + ", " + bDirection.y);
            Debug.Log(facingDirection.x + ", " + facingDirection.y);
            if (bDirection.x < 0 && facingDirection == Vector2.right)
            {
                if (bDirection.y > 0.0f) bDirection = Vector2.up;
                else bDirection = Vector2.down;
            }
            else if (bDirection.x > 0 && facingDirection == Vector2.left)
            {
                if (bDirection.y > 0.0f) bDirection = Vector2.up;
                else bDirection = Vector2.down;
            }

            // Set direction of bullet
            bullet.SetDirection(bDirection);

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

    public void changeBulletType(GameObject bullet)
    {
        mBulletPrefab = bullet;
    }
}
