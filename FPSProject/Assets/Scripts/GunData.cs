using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject {

    ////Gun Stats
    public int bulletInventory;
	public int magBulletInventory;
    public int bulletLoaded;
    public int gunRange;
    public float bulletDamage;
    public float reloadtime;
    public float timeBetweenShoot;

    //recoil
    public float xMax;
    public float yMax;
	public float zMax;


}
