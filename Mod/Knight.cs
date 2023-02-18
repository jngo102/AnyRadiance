using GlobalEnums;
using Modding;
using System.Reflection;
using UnityEngine;

namespace AnyRadiance
{
    internal class Knight : MonoBehaviour
    {
        private const float MaxDashTime = 0.35f;

        private float _dashTimer;

        private void Awake()
        {
            On.HeroController.CanDash += AlwaysEnableDash;
            On.HeroController.LookForInput += CustomInput;
        }

        private void CustomInput(On.HeroController.orig_LookForInput orig, HeroController self)
        {
            ReflectionHelper.SetField<HeroController, float>(self, "dash_timer", 0);

            orig(self);

            var dash = InputHandler.Instance.inputActions.dash;

            if (dash.IsPressed)
            {
                _dashTimer += Time.deltaTime;
            }

            if (dash.WasReleased || _dashTimer >= MaxDashTime)
            {
                _dashTimer = 0;
                typeof(HeroController).GetMethod("CancelDash", BindingFlags.NonPublic | BindingFlags.Instance)?
                    .Invoke(self, null);
            }
        }

        private bool AlwaysEnableDash(On.HeroController.orig_CanDash orig, HeroController self)
        {
            if (self.hero_state != ActorStates.no_input &&
                self.hero_state != ActorStates.hard_landing && 
                self.hero_state != ActorStates.dash_landing &&
                ReflectionHelper.GetField<HeroController, float>(self, "dashCooldownTimer") <= 0 &&
                !self.cState.dashing && 
                !self.cState.backDashing && 
                (!self.cState.attacking || !(ReflectionHelper.GetField<HeroController, float>(self, "attack_time") < 
                                             ReflectionHelper.GetField<HeroController, float>(self, "ATTACK_RECOVERY_TIME"))) && 
                !self.cState.preventDash && 
                !self.cState.hazardDeath && 
                PlayerData.instance.GetBool("canDash"))
            {
                return true;
            }
            
            return false;
        }

        private void OnDestroy()
        {
            On.HeroController.CanDash -= AlwaysEnableDash;
            On.HeroController.LookForInput -= CustomInput;
        }
    }
}
