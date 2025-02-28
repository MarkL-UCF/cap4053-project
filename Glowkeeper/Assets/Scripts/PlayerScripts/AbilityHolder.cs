using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AbilityHolder : MonoBehaviour
{
    public PlayerAbility ability;
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
                    cooldownTime = ability.cooldownTime;
                    lastTime = Time.time;
                }
                break;
            case AbilityState.cooldown:
                if(Time.time > lastTime + cooldownTime)
                {
                    state = AbilityState.ready;
                }
                break;

        }
    }
}


