using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    //player's health
    public double health;
    //max health
    public double maxHealth;

    //UI variables for heart sprites
    public Sprite emptyHeart;
    public Sprite halfHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public Metrics metrics;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        hearts[0] = GameObject.Find("Heart").GetComponent<Image>();
        hearts[1] = GameObject.Find("Heart(1)").GetComponent<Image>();
        hearts[2] = GameObject.Find("Heart(2)").GetComponent<Image>();
        hearts[3] = GameObject.Find("Heart(3)").GetComponent<Image>();
        metrics = GameObject.FindGameObjectWithTag("Metrics").GetComponent<Metrics>();
    }

    // Update is called once per frame
    void Update()
    {
        //loop through heart array
        for (int i = 0; i < hearts.Length; i++)
        {
            //if half heart
            if ((double)i == (health - 0.5))
            {
                hearts[i].sprite = halfHeart;
            }
            //if full heart
            else if ((double)i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            //if empty heart
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            //if i is less than max, hearts are enabled
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void PlayerDamage(float amount)
    {
        health -= amount;
        
        //checks if player is dead
        if (health <= 0)
        {
            Update();
            metrics.playerDied();
            Destroy(gameObject);//destroys player object
        }
    }
}
