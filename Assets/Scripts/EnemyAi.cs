using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public static EnemyAi instance;

    private Transform player;
    public float speed = 3f;
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    
    public float shootDistance = 3f;

    public float startTimeBtwShots;
    private float timeBtwShots;
    bool isReached = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        shootDistance = Random.Range(7, 13);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.instance.isPlayerDead)
        {
            Vector3 lookDirection = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            if (!isReached)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, player.position) <= shootDistance)
            {
                Shoot();
                isReached = true;
            }
            else
                isReached = false;
        }
    }
    void Shoot()
	{
        if(timeBtwShots <= 0)
		{
            firePoint.GetChild(0).GetComponent<ParticleSystem>().Play();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            timeBtwShots = startTimeBtwShots;
		}
		else
		{
            timeBtwShots -= Time.deltaTime;
		}
	}
}
