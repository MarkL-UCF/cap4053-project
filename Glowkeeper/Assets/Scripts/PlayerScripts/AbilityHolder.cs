using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public PlayerAbility ability;
    public Image CoolDownBar;
    public Image CoolLabel;
    float cooldownTime;
    float activeTime;
    float lastTime = 0f;

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
        CoolDownBar.enabled = false;
        CoolLabel.enabled = false;
    }
    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key) && Time.time > lastTime + cooldownTime)
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
                    CoolLabel.enabled= true;
                    cooldownTime = ability.cooldownTime;
                    lastTime = Time.time;
                }
                break;
            case AbilityState.cooldown:
                
                CoolDownBar.fillAmount = Mathf.Clamp(1 - (Time.time/(lastTime + cooldownTime)), 0, 1);
                if (Time.time > lastTime + cooldownTime)
                {
                    state = AbilityState.ready;
                    CoolDownBar.enabled = false;
                    CoolLabel.enabled= false;
                }
                break;

        }
    }
}


