using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HP = 100;
    private float currentAlpha = 0f; 
    private float effectDuration = 2f; 
    private Coroutine bloodyScreenCoroutine; 


    public TextMeshProUGUI playerHealthUI;
    public GameObject bloodyScreen,gameOverUI;

    public bool isDead;

    private void Start()
    {
        playerHealthUI.text = $"Health: {HP}";
    }
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("Player Dead");
            PlayerDead();
            isDead = true;

        }
        else
        {
            print("PlayerHit");
            StartCoroutine(BloodyScreenEffect());
            playerHealthUI.text = $"Health: {HP}";
            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);
        }
    }

    private void PlayerDead()
    {
        SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDie);

        SoundManager.Instance.playerChannel.clip = SoundManager.Instance.gameOverMusic;
        SoundManager.Instance.playerChannel.PlayDelayed(2f);

        GetComponent<MouseMovement>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;

        // Dying Animation 
        GetComponentInChildren<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenBlackout>().StartFade();

        StartCoroutine(ShowGameOverUI());
    }
    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
    }
    private IEnumerator BloodyScreenEffect()
    {
        if (!bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(true);
        }
        var image = bloodyScreen.GetComponentInChildren<RawImage>();

        currentAlpha = Mathf.Min(currentAlpha + 0.2f, 1.5f);
        effectDuration = Mathf.Min(effectDuration + 1f, 5f); 

        image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);

        float elapsedTime = 0f;

        while (elapsedTime < effectDuration)
        {
          
            if (elapsedTime == 0)
                yield return null;

            float newAlpha = Mathf.Lerp(currentAlpha, 0f, elapsedTime / effectDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (currentAlpha <= 0.05f)
        {
            currentAlpha = 0f;
            effectDuration = 2f;
            bloodyScreen.SetActive(false);
        }
    }
    private void StartBloodyScreenEffect()
    {
        if (bloodyScreenCoroutine != null)
        {
            StopCoroutine(bloodyScreenCoroutine);
        }
        bloodyScreenCoroutine = StartCoroutine(BloodyScreenEffect());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RobotHit"))
        {
            if (!isDead)
            {
                TakeDamage(other.gameObject.GetComponent<RobotHit>().damage);
            }
        }
    }
}
