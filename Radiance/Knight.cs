using GlobalEnums;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Modding;
using System.Reflection;
using UnityEngine;
using Vasi;
using InvokeMethod = HutongGames.PlayMaker.Actions.InvokeMethod;

namespace AnyRadiance.Radiance
{
    internal class Knight : MonoBehaviour
    {
        private const float MaxDashTime = 0.35f;

        private float _dashTimer;
        private FsmState _q2Land;

        private HeroController _heroController;

        private void Awake()
        {
            _heroController = HeroController.instance;

            // Allow player to be affected by gravity so they don't hover on moving platforms
            PlayMakerFSM spellCtrl = gameObject.LocateMyFSM("Spell Control");
            _q2Land = spellCtrl.GetState("Q2 Land");
            _q2Land.RemoveAction<SetVelocity2d>();
            _q2Land.InsertMethod(7, () => _heroController.AffectedByGravity(true));

            On.HeroController.CanDash += AlwaysEnableDash;
            On.HeroController.LookForInput += CustomInput;
        }

        // Custom dash input that allows dash to be held for up to `MaxDashTime` seconds to extend
        // dash and release it to cancel.
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

        // Allow player to dash midair without needing to touch the ground first.
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

            _q2Land.RemoveAction<InvokeMethod>();
            _q2Land.InsertAction(7, new SetVelocity2d { x = 0, y = 0 });
        }
    }
}
