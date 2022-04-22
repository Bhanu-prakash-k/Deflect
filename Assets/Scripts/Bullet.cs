using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;

    public float bulletSpeed = 20f;
    public GameObject bulletCollisionParticles;
	public GameObject tankDeathParticles;
    
    bool shieldCollided = false;

	private void Awake()
	{
        if (instance == null)
            instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
           
        shieldCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (!shieldCollided)
		{
            transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        }
		else
		{
            transform.position += transform.forward * -bulletSpeed * Time.deltaTime;
        }
    }
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Shield"))
		{
            shieldCollided = true;
		}
		if (other.gameObject.CompareTag("Player"))
		{
            Debug.Log("Player");
			Instantiate(bulletCollisionParticles, transform.position, transform.rotation);
			PlayerController.instance.isPlayerDead = true;
			Destroy(gameObject);
		}
		if (other.gameObject.CompareTag("Wall"))
		{
			Instantiate(bulletCollisionParticles, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		if (other.gameObject.CompareTag("Tank"))
		{
            if (shieldCollided)
            {
				Instantiate(bulletCollisionParticles, transform.position, transform.rotation);
				Instantiate(tankDeathParticles, other.transform.position, other.transform.rotation);
				Destroy(other.gameObject);
				GameManager.instance.totalEnemies--;
				if(GameManager.instance.totalEnemies == 0)
                {
					PlayerController.instance.isLevelFinished = true;
					GameManager.instance.StartCoroutine(GameManager.instance.LevelFinished());
				}
				Destroy(gameObject);
			}
		}
	}
	
}
