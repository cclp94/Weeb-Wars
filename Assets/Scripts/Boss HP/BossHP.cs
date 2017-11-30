using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour {

    public GameObject hpPointPrefab;

    private float distanceBetweenHPPoints = 0.12f;
    private int healthVisiblePointer;
	// Use this for initialization
	void Start () {
        Init();
	}

    void Init()
    {
        float distance = 0f;
        healthVisiblePointer = 68;
        for (int i = 0; i< 69; i++)
        {
            GameObject go = Instantiate(hpPointPrefab, transform);
            go.transform.position = new Vector3(transform.position.x -4 + (distance), transform.position.y, transform.position.z);
            distance += distanceBetweenHPPoints;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateHealth(float currentHP, float initialHP)
    {
        int newHealthVisiblePointer = (int) Mathf.Round((currentHP * 68) / initialHP);
        print("old health: " + healthVisiblePointer + ", new: " + newHealthVisiblePointer);
        int counter = 0;
        foreach(SpriteRenderer go in GetComponentsInChildren<SpriteRenderer>())
            go.enabled = (counter++ <= newHealthVisiblePointer);
        healthVisiblePointer = newHealthVisiblePointer;

    }
}
