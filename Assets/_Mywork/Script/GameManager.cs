using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [Header("Bullet")]
    [SerializeField]
    private Transform bulletPoint;
    [SerializeField]
    private GameObject bulletObj;
    [SerializeField]
    private float maxShootDelay = 0.2f;
    [SerializeField]
    private float currentShootDelay = 0.2f;
    [SerializeField]
    private Text bulletText;
    private int maxMag = 30;
    private int currentMag = 0;

    [Header("FX")]
    [SerializeField]
    private GameObject weaponFlashFX;
    [SerializeField]
    private Transform bulletDropPoint;
    [SerializeField]
    private GameObject bulletDropFX;
    [SerializeField]
    private Transform weaponClipPoint;
    [SerializeField]
    private GameObject weaponClipFX;

    [Header("Enemy")]
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject[] spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        currentShootDelay = 0;
        initBullet();

        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        bulletText.text = currentMag + " / " + maxMag;
    }

    public void Shooting(Vector3 targetPosition, Enemy enemy, AudioSource weaponSound, AudioClip shootingSound)
    {
        currentShootDelay += Time.deltaTime;

        if (currentShootDelay < maxShootDelay || currentMag <= 0)
        {
            return;
        }

        currentMag -= 1;
        currentShootDelay = 0;

        weaponSound.clip = shootingSound;
        weaponSound.Play();

        Vector3 aim = (targetPosition - bulletPoint.position).normalized;

        //Instantiate(weaponFlashFX, bulletPoint);
        GameObject flashFX = PoolManager.Instance.ActivateObj(1);
        SetObjPosition(flashFX, bulletPoint);
        flashFX.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);


        //Instantiate(bulletDropFX, bulletDropPoint);
        GameObject magFX = PoolManager.Instance.ActivateObj(2);
        SetObjPosition(magFX, bulletDropPoint);


        //Instantiate(bulletObj, bulletPoint.position, Quaternion.LookRotation(aim, Vector3.up));
        GameObject prefabToSpawn = PoolManager.Instance.ActivateObj(0);
        SetObjPosition(prefabToSpawn, bulletPoint);
        prefabToSpawn.transform.rotation = Quaternion.LookRotation(aim, Vector3.up);
        

        /*if (enemy != null && enemy.enemyCurrentHP > 0) 
        {
            enemy.enemyCurrentHP -= 1;
            Debug.Log("enemy HP : " +  enemy.enemyCurrentHP);
        }*/

    }

    public void ReloadClip()
    {
        //Instantiate(weaponClipFX, weaponClipPoint);
        GameObject clipFX = PoolManager.Instance.ActivateObj(3);
        SetObjPosition(clipFX, weaponClipPoint);
        initBullet();
    }

    private void initBullet()
    {
        currentMag = maxMag;
    }

    private void SetObjPosition(GameObject obj, Transform targetTransform)
    {
        obj.transform.position = targetTransform.position;
    }

    IEnumerator EnemySpawn()
    {
        Instantiate(enemy, spawnPoint[Random.Range(0,spawnPoint.Length)].transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemySpawn());
    }
}
