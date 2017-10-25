using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour {
    private static PlayerUpgradeManager mManager;

    [SerializeField]
    GameObject gun;
    [SerializeField]
    GameObject weaponTypeGameObject;

    private int mSelectedWeapon;
    private List<GameObject> mWeaponTypes;
    private AllienGun allienGun;

    public static PlayerUpgradeManager Instance{
        get
        {
            return mManager;
        }
    }
    void Awake()
    {
        if (mManager == null)
        {
            mManager = this;
            DontDestroyOnLoad(this);
        }else if(mManager != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        allienGun = gun.GetComponent<AllienGun>();
        mWeaponTypes = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.layer == 10).ToList();
        Debug.Log(mWeaponTypes.Count);
        foreach(GameObject g in mWeaponTypes)
        {
            Debug.Log(g.name);
        }
        SelectWeapon(mWeaponTypes[0]);
        mSelectedWeapon = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SelectWeapon(GameObject go)
    {
        GunUpgrade bullet = go.GetComponent<GunUpgrade>();
        allienGun.changeBulletType(go);
        allienGun.SwapColor(bullet.GetGunColor());
        weaponTypeGameObject.GetComponent<SpriteRenderer>().sprite = bullet.GetWeaponIcon();
    }

    public void ChangeWeaponRight()
    {
        mSelectedWeapon += 1;
        if (mSelectedWeapon >= mWeaponTypes.Count)
            mSelectedWeapon = 0;
        SelectWeapon(mWeaponTypes[mSelectedWeapon]);
    }

    public void ChangeWeaponLeft()
    {
        mSelectedWeapon -= 1;
        if (mSelectedWeapon < 0)
            mSelectedWeapon = mWeaponTypes.Count - 1;
        SelectWeapon(mWeaponTypes[mSelectedWeapon]);
    }
}
