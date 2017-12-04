using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;

public class PlayerUpgradeManager : MonoBehaviour {
    private static PlayerUpgradeManager mManager;

    [SerializeField]
    GameObject defaultGunUpgrade;
    [SerializeField]
    LevelManager mLevelManager;
    private GameObject gun;
    private GameObject weaponTypeGameObject;

    private int mSelectedWeapon;
    private List<GameObject> mWeaponTypes;
    private AllienGun allienGun;
    private bool[] mWeaponsAvailable;

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

    void Start()
    {
        InitManager();
    }

    // Use this for initialization
    public void InitManager() {
        InitComponents();
        int indexDefault = mWeaponTypes.IndexOf(defaultGunUpgrade);
        if (mWeaponsAvailable == null)
        {
            mWeaponsAvailable = new bool[mWeaponTypes.Count];
            mWeaponsAvailable[indexDefault] = true;
        }
        foreach(GameObject g in mWeaponTypes)
        {
            print(g.name);
        }
        SelectWeapon(mWeaponTypes[indexDefault]);
        mSelectedWeapon = indexDefault;
    }
	
	// Update is called once per frame
	void Update () {
        if(startBackIn5 != -1){
            if(Time.time - startBackIn5 >= 5){
                SceneManager.LoadScene("Choose a Level");
            }
        }
	}

    private void InitComponents()
    {
        startBackIn5 = -1;
        gun = GameObject.FindGameObjectWithTag("Gun");
        weaponTypeGameObject = GameObject.FindGameObjectWithTag("WeaponTypeUI");
        allienGun = gun.GetComponent<AllienGun>();
        mWeaponTypes = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.layer == 10).ToList();
    }

    void SelectWeapon(GameObject go)
    {
        if (allienGun == null || weaponTypeGameObject == null)
            InitComponents();
        GunUpgrade bullet = go.GetComponent<GunUpgrade>();
        allienGun.changeBulletType(go);
        //print(bullet.GetGunColor());
        allienGun.SwapColor(bullet.GetGunColor());
        GameObject.FindGameObjectWithTag("WeaponTypeUI").GetComponent<SpriteRenderer>().sprite = bullet.GetWeaponIcon();
    }

    public void ChangeWeaponRight()
    {
        int newSelection = (mSelectedWeapon + 1);
        if (newSelection >= mWeaponTypes.Count)
            newSelection = 0;
        if (mWeaponsAvailable[newSelection])
        {
            mSelectedWeapon = newSelection;
            SelectWeapon(mWeaponTypes[mSelectedWeapon]);
        }
    }

    public void ChangeWeaponLeft()
    {
        int newSelection = (mSelectedWeapon - 1);
        if (newSelection < 0)
            newSelection = mWeaponTypes.Count - 1;
        if (mWeaponsAvailable[newSelection])
        {
            mSelectedWeapon = newSelection;
            SelectWeapon(mWeaponTypes[mSelectedWeapon]);
        }
    }

    public void unlockWeapon(string weapon)
    {
        int indexOfWeapon = mWeaponTypes.FindIndex(x => x.name == weapon);
		if(indexOfWeapon != -1)
        	mWeaponsAvailable[indexOfWeapon] = true;
    }
    float startBackIn5;
    public void goBackToLevelInFiveSeconds(){
        startBackIn5 = Time.time;
    }
}
