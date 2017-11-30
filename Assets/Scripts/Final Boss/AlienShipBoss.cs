using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShipBoss : Enemy {

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
	// Use this for initialization
	void Start () {
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
    }
    int shotCounter = 0;
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
            lastShotTime = Time.time;
        }

        // Movements
        if (!isCrazyMode)
        {

        }
        else
        {

        }
        UpdateAnimator();
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
}
