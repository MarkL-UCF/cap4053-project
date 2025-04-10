using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Boolean newAbilityPickup;
    public PlayerAbility newAbility;
    public PlayerAbility ability;
    public Image CoolDownBar;
    public Image CoolLabel;
    float cooldownTime;
    float activeTime;
    float timer;

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public KeyCode key;

    private void Start()
    {
        CoolDownBar = GameObject.Find("CoolDownBar").GetComponent<Image>();
        CoolLabel = GameObject.Find("CoolDown Label").GetComponent<Image>();
        CoolDownBar.enabled = false;
        CoolLabel.enabled = false;
        newAbilityPickup = false;
        timer = 0;
    }
    void Update()
    {
        if (!PauseController.IsGamePaused)
        {
            if (newAbilityPickup && ability == null)
            {
                ability = newAbility;
                newAbilityPickup = false;
            }
            switch (state)
            {
                case AbilityState.ready:
                    if (newAbilityPickup == true)
                    {
                        ability = newAbility;
                        newAbilityPickup = false;
                    }
                    if (Input.GetKeyDown(key) && timer <= 0 && !PauseController.IsGamePaused)
                    {
                        ability.Activate(gameObject);
                        state = AbilityState.active;
                        activeTime = ability.activeTime;
                    }
                    break;
                case AbilityState.active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        ability.BeginCooldown(gameObject);
                        state = AbilityState.cooldown;
                        CoolDownBar.enabled = true;
                        CoolLabel.enabled = true;
                        cooldownTime = ability.cooldownTime;
                        timer = cooldownTime;

                    }
                    break;
                case AbilityState.cooldown:

                    CoolDownBar.fillAmount = Mathf.Clamp((timer / cooldownTime), 0, 1);
                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        state = AbilityState.ready;
                        CoolDownBar.enabled = false;
                        CoolLabel.enabled = false;

                    }
                    break;

            }
        }
    }
}


