using System.Collections;
using System.Collections.Generic;
using AnyRadiance.Radiance;
using UnityEngine;
using Action = HKAIFramework.Action;
using Random = UnityEngine.Random;

namespace AnyRadiance.Radiance.Actions
{
    internal class LaserColumns : Action
    {
        /// <inheritdoc />
        public override IEnumerator Execute()
        {
            Transform transform = Radiance.Self.transform;
            var hc = HeroController.instance;
            var pd = PlayerData.instance;
            
            var beams = new List<GameObject>();
            int safeSpot = Random.Range((int)ArenaInfo.CurrentLeft, (int)ArenaInfo.CurrentRight);
            for (int x = 20; x < 100; x += 1)
            {
                if (x == safeSpot) continue;
                GameObject beam = AnyRadiance.Instance.GameObjects["Eye Beam"].Spawn(new Vector3(x, 50, 0), Quaternion.Euler(0, 0, -90));
                beam.GetComponent<DamageHero>().shadowDashHazard = true;
                beam.LocateMyFSM("Control").SendEvent("ANTIC");
                beams.Add(beam);
            }

            AnyRadiance.Instance.AudioClips["Beam Prepare"].PlayOneShot(transform.position);

            float dist = Mathf.Abs(hc.transform.position.x - safeSpot);
            float runSpeed = hc.RUN_SPEED;
            float dashSpeed = pd.equippedCharm_16 ? hc.DASH_SPEED_SHARP : hc.DASH_SPEED;
            float dashCooldown = pd.equippedCharm_31 ? hc.DASH_COOLDOWN_CH : hc.DASH_COOLDOWN;
            const float reactionTimeMax = 0.35f;
            float anticTime = 0;
            float playerDist = 0;

            // Give the player just enough time to make it to the safe gap
            while (playerDist < dist)
            {
                if (playerDist + dashSpeed * hc.DASH_TIME > dist)
                {
                    float runTime = (dist - playerDist) / dashSpeed;
                    playerDist += runSpeed * runTime;
                    anticTime += runTime;
                }
                else
                {
                    playerDist += dashSpeed * hc.DASH_TIME;
                    anticTime += hc.DASH_TIME;
                }

                if (playerDist + runSpeed * dashCooldown > dist)
                {
                    float runTime = (dist - playerDist) / runSpeed;
                    playerDist += runSpeed * runTime;
                    anticTime += runTime;
                }
                else
                {
                    playerDist += runSpeed * dashCooldown;
                    anticTime += dashCooldown;
                }
            }

            anticTime += reactionTimeMax;

            yield return new WaitForSeconds(anticTime);

            foreach (var beam in beams)
            {
                beam.LocateMyFSM("Control").SendEvent("FIRE");
            }

            AnyRadiance.Instance.AudioClips["Beam Burst"].PlayOneShot(transform.position);

            yield return new WaitForSeconds(0.5f);

            foreach (var beam in beams)
            {
                beam.Recycle();
            }
        }
    }
}
