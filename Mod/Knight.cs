using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using System.Reflection;
using UnityEngine;
using Vasi;

namespace AnyRadiance
{
    internal class Knight : MonoBehaviour
    {
        private const float MaxDashTime = 0.35f;

        private float _dashTimer;

        private HeroController _heroController;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _heroController = HeroController.instance;
            _rigidbody = GetComponent<Rigidbody2D>();

            PlayMakerFSM spellCtrl = gameObject.LocateMyFSM("Spell Control");
            FsmState q2Land = spellCtrl.GetState("Q2 Land");
            q2Land.RemoveAction<SetVelocity2d>();
            q2Land.InsertMethod(7, () => _heroController.AffectedByGravity(true));

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
                if (_dashTimer >= MaxDashTime)
                {
                    typeof(HeroController).GetMethod("ResetAttacks", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.Invoke(self, null);
                }
                _heroController.gameObject.LocateMyFSM("Nail Arts").SendEvent("DASH END");
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
