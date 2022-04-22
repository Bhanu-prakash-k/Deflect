using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalEnemies;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Tank").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator LevelFinished()
    {
        yield return new WaitForSeconds(0.1f);
        Camera.main.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Camera.main.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        Camera.main.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
