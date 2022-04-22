using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float speed = 3f;
    public float shieldTimer = 5f;
    public VariableJoystick variableJoystick;
    public GameObject shield;

    [HideInInspector]
    public bool isLevelFinished = false;
    [HideInInspector]
    public bool isPlayerDead = false;

    public Slider shieldSlider;
    public TMP_Text shieldTimerText;

    Rigidbody rb;
    Animator anim;
    public Animator blinkAnimator;
    //Material shieldMaterial;

    private void Awake()
	{
        if (instance == null)
            instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        shield.SetActive(true);
        shieldSlider.maxValue = shieldTimer;
        shieldSlider.value = shieldTimer;
        shieldTimerText.text = shieldSlider.value.ToString();
        //shieldMaterial = shield.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLevelFinished)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetBool("Run", true);
                //shield.SetActive(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                anim.SetBool("Run", false);
            }
            shieldTimer -= 10 * Time.deltaTime;
            shieldSlider.value = shieldTimer;
            shieldTimerText.text = shieldSlider.value.ToString();
            if (shieldTimer <= 30f)
            {
                blinkAnimator.SetBool("Blink", true);
            }
            else if (shieldTimer >= 35f)
            {
                blinkAnimator.SetBool("Blink", false);
            }
            if (shieldTimer <= 0f)
            {
                shieldTimer = 0f;
                shield.SetActive(false);
            }
            if(shieldTimer >= 100f)
            {
                shieldTimer = 100f;
            }
        }
        else
        {
            LevelFinish();
        }
    }
	private void FixedUpdate()
	{
        if (!isLevelFinished)
        {
            rb.velocity = new Vector3(variableJoystick.Horizontal * speed, rb.velocity.y, variableJoystick.Vertical * speed);
            if (variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
                //transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            }
        }
        else
        {
            LevelFinish();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickupShield"))
        {
            ShieldSpawner.instance.canSpawnShield = true;
            shieldTimer += 50f;
            shield.SetActive(true);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (isPlayerDead)
            {
                //rb.velocity = Vector3.zero;
                isLevelFinished = true;
                anim.SetTrigger("Death");
                StartCoroutine(LoadSameLevel());
            }
        }
    }
    public void LevelFinish()
    {
        rb.velocity = Vector3.zero;
        anim.SetBool("Run", false);
    }
    IEnumerator LoadSameLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
