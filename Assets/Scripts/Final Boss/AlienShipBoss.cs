using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum MovementState
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    CENTER,
}

public class AlienShipBoss : BossEnemy {

    public float mTurnTime;
    public float mShootTime;
    public GameObject weakPointPrefab;
    public GameObject bulletPrefab;
    
    private CircleCollider2D mCollider;
    private bool isCrazyMode;
    private bool damagable;
    private float lastTurnTime;
    private float lastShotTime;
    private int visibleIndex;
    private List<ShipWeakSpot> weakSpots;
    private Animator mAnimator;
    private MovementState movementState;

    // Center Position
    public Transform mostLeft;
    public Transform mostRight;
    public Transform mostUp;
    public Transform mostDown;

	// Use this for initialization
	void Start () {
        movementState = MovementState.CENTER;
        mAnimator = GetComponent<Animator>();
        lastTurnTime = Time.time;
        lastShotTime = Time.time;
        damagable = false;
        weakSpots = new List<ShipWeakSpot>();
        mCollider = GetComponent<CircleCollider2D>();
        // Create weak spots
        CreateWeakSpots();
        visibleIndex = 0;
        SetVisibleWeakSpots();
        Activate();
    }
    int shotCounter = 0;
    bool mActive;
	// Update is called once per frame
	void Update () {
        // Check if all weak spots destroyed
        if(weakSpots.FindIndex(x => !x.IsDestroyed()) == -1)
        {
            damagable = true;
        }
        // Check turn
		if(Time.time - lastTurnTime >= mTurnTime)
        {
            Turn();
            lastTurnTime = Time.time;
        }
        // CHeck shot
        if (Time.time - lastShotTime >= mShootTime)
        {
            Shoot();
            mActive = true;
            shotCounter++;
            lastShotTime = Time.time;
        }
        if (!damagable && mActive)
        {
            if (!isCrazyMode)
            {
                Move();
            }
            else
            {

            }
        }
        
        UpdateAnimator();
	}

    void Move()
    {
        Vector3 move = Vector3.zero;
        float movementRadius = (mCollider.radius + 1);
        switch (movementState)
        {
            case MovementState.LEFT:
                if (transform.position.x - movementRadius <= mostLeft.position.x)
                {
                    print("Moving Down");
                    movementState = MovementState.DOWN;
                }
                move.Set(mostLeft.position.x - transform.position.x, 0f, 0f);
                break;
            case MovementState.RIGHT:
                if (transform.position.x + movementRadius >= mostRight.position.x)
                {
                    print("Moving Up");
                    movementState = MovementState.UP;
                }
                move.Set(mostRight.position.x - transform.position.x, 0f, 0f);
                break;
            case MovementState.UP:
                if (transform.position.y + movementRadius >= mostUp.position.y)
                {
                    print("Moving Left");
                    movementState = MovementState.LEFT;
                }
                move.Set(0f, mostUp.position.y - transform.position.y, 0f);
                break;
            case MovementState.DOWN:
                if (transform.position.y - movementRadius <= mostDown.position.y)
                {
                    print("Moving Center");
                    movementState = MovementState.CENTER;
                }
                move.Set(0f, mostDown.position.y - transform.position.y, 0f);
                break;
            case MovementState.CENTER:
                float centerX = mostLeft.position.x + (mostRight.position.x - mostLeft.position.x) / 2.0f;
                float centerY = mostDown.position.y + (mostUp.position.y - mostDown.position.y) / 2.0f;
                if (Mathf.Round(transform.position.x) == Mathf.Round(centerX) || Mathf.Round(transform.position.y) == Mathf.Round(centerY))
                {
                    print("Moving Right");
                    movementState = MovementState.RIGHT;
                }
                move.Set(centerX - transform.position.x, centerY - transform.position.y, 0f);
                break;
        }
        transform.position = transform.position + (move.normalized * Time.deltaTime * 4);
    }

    void UpdateAnimator()
    {
        mAnimator.SetBool("isOpen", this.damagable);
    }

    void FinishedDamagablePeriod()
    {
        damagable = false;
        foreach(ShipWeakSpot weak in weakSpots)
        {
            Destroy(weak.gameObject);
        }
        weakSpots = new List<ShipWeakSpot>();
        CreateWeakSpots();
        visibleIndex = 0;
        SetVisibleWeakSpots();
    }

    void Shoot()
    {
        float radius = mCollider.radius + 0.5f;
        int degrees = shotCounter%2==1 ? 0 : 25;
        for (int i = 0; i < 8; i++, degrees += 45)
        {
            float x = radius * Mathf.Cos(Mathf.Deg2Rad * degrees);
            float y = radius * Mathf.Sin(Mathf.Deg2Rad * degrees);
            Vector2 direction = new Vector2(x, y);
            Vector3 pos = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            GameObject go = Instantiate(bulletPrefab);
            go.transform.position = pos;
            go.GetComponent<AlienBossBullet>().SetDirection(direction.normalized);
        }
    }

    void Turn()
    {
        visibleIndex = (visibleIndex + 2) % 8;
        SetVisibleWeakSpots();
    }

    void CreateWeakSpots()
    {
        float radius = mCollider.radius;
        Vector3 currentPosition = transform.position;
        for(int i = 0; i < 4; i++)
        {
            
            // Get random weak spot position for top part
            int randDegree = Random.Range(20, 160);
            float randPos = Random.Range(radius * 0.2f, radius * 0.9f);
            float x = randPos * Mathf.Cos(Mathf.Deg2Rad * randDegree);
            randPos = Random.Range(radius * 0.2f, radius * 0.9f);
            float y = randPos * Mathf.Sin(Mathf.Deg2Rad * randDegree);
            Vector3 weakPointPosition = new Vector3(currentPosition.x + x, currentPosition.y + y, currentPosition.z);
            GameObject go = Instantiate(weakPointPrefab, transform);
            go.transform.position = weakPointPosition;
            weakSpots.Add(go.GetComponent<ShipWeakSpot>());
            // Get random weak spot position for bottom part
            randDegree = Random.Range(200, 340);
            randPos = Random.Range(radius * 0.2f, radius * 0.9f);
            x = randPos * Mathf.Cos(Mathf.Deg2Rad * randDegree);
            randPos = Random.Range(radius * 0.2f, radius * 0.9f);
            y = randPos * Mathf.Sin(Mathf.Deg2Rad * randDegree);
            weakPointPosition = new Vector3(currentPosition.x + x, currentPosition.y + y, currentPosition.z);
            go = Instantiate(weakPointPrefab, transform);
            go.transform.position = weakPointPosition;
            weakSpots.Add(go.GetComponent<ShipWeakSpot>());
        }
    }

    void SetVisibleWeakSpots()
    {
        for(int i = 0; i < weakSpots.Count; i++)
            weakSpots[i].SetIsVisible((i == visibleIndex || visibleIndex + 1 == i));
    }

    override public void OnTriggerEnter2D(Collider2D col)
    {
        if (damagable && col.gameObject.tag == "DamagingBullet")
        {
            col.GetComponent<GunUpgrade>().Collide(this.gameObject);
        }else if(col.gameObject.tag == "EnemyBullet")
        {
            Destroy(col.gameObject);
        }
    }
    bool hasDied = false;

	override public void Die()
	{   
        print("Roll credits");
		Destroy(gameObject);
		Instantiate(deathPrefab, transform.position, Quaternion.identity);
        hasDied = true;
        GameObject.Find("EndDialog").GetComponent<DialogueTrigger>().TriggerDialogue();
        SceneManager.LoadScene("Credits");
	}
}
