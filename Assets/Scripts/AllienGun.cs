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
    [SerializeField]
    SpriteRenderer playerSpriteRenderer;

    Texture2D mColorSwapTex;

    public void InitColorSwapTex()
    {
        mColorSwapTex = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);
        mColorSwapTex.filterMode = FilterMode.Point;
        //mColorSwapTex.SetPixel(0, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        mColorSwapTex.Apply();
        playerSpriteRenderer.material.SetTexture("_SwapTex", mColorSwapTex);
    }

    public void SwapColor(Color c)
    {
        if (mColorSwapTex == null)
            InitColorSwapTex();
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
        PlayerUpgradeManager.Instance.InitManager();
    }

    void Update ()
    {
        if (Time.timeScale == 0)
            return;
        transform.rotation.Set(0.0f, 0.0f, 0.0f, transform.rotation.w);
        if (Input.GetButtonDown ("Fire") && !mWeeb.IsStunned() && mBulletPrefab.GetComponent<GunUpgrade>().canInstatiateNewBullet())
        {
            // Shoot bullet

            GameObject bulletObject = Instantiate (mBulletPrefab, transform.position, Quaternion.identity) as GameObject;
            GunUpgrade bullet = bulletObject.GetComponent<GunUpgrade>();
            Vector2 facingDirection = mWeeb.GetFacingDirection();

            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bDirection = cursorPos - (Vector2)transform.position;
            bDirection.Normalize();
            if (bDirection.x < 0 && facingDirection == Vector2.right || (bDirection.x > 0 && facingDirection == Vector2.left))
            {
                bDirection = facingDirection;
            }
            /*else if (bDirection.x > 0 && facingDirection == Vector2.left)
            {
                if (bDirection.y > 0.0f) bDirection = Rotate(Vector2.up, 30);
                else bDirection = Rotate(Vector2.down, -30);
            }*/

            // Set direction of bullet
            bullet.SetDirection(bDirection);
            /*Debug.Log(Vector2.Angle(cursorPos, (Vector2)transform.position));
            if(bDirection.y > 0)
            {
                transform.Rotate(Vector3.forward, Vector2.Angle((Vector2)transform.position, cursorPos) * 2 * 3.14f);
            }
            else
            {
                transform.Rotate(Vector3.forward, -Vector2.Angle((Vector2)transform.position, cursorPos)*2*3.14f);
            }*/

            // Set animation paramsd 
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
	public Vector2 Rotate(Vector2 v, float degrees)
	{
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}
}
