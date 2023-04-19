using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnyRadiance.Radiance;
using UnityEngine;
using Action = HKAIFramework.Action;
using Random = UnityEngine.Random;

namespace AnyRadiance.Radiance.Actions
{
    internal class NailWalls : Action
    {
        public override IEnumerator Execute()
        {
            Transform transform = Radiance.Self.transform;
            const int numWalls = 2;
            var nails = new Dictionary<int, List<GameObject>>(numWalls);
            for (int i = 0; i < numWalls; i++)
            {
                nails[i] = new List<GameObject>();
            }

            IEnumerator AccelerateNails(float accelerationRate)
            {
                float currentTime = 0;
                const float attackTime = 4;
                while (currentTime < attackTime)
                {
                    for (int i = 0; i < nails.Count; i++)
                    {
                        foreach (var nail in nails[i])
                        {
                            if (nail == null) continue;
                            nail.GetComponent<Rigidbody2D>().velocity *= accelerationRate;
                        }
                    }
                    yield return null;
                    currentTime += Time.deltaTime;
                }

                for (int i = 0; i < nails.Count; i++)
                {
                    foreach (GameObject nail in nails[i])
                    {
                        if (nail == null) continue;
                        nail.Recycle();
                    }
                }
            }

            GameManager.instance.StartCoroutine(AccelerateNails(1.01f));

            var wallPos = new[] { ArenaInfo.CurrentLeft, ArenaInfo.CurrentRight }[Random.Range(0, 2)];
            for (int i = 0; i < numWalls; i++)
            {
                var safeSpot = Random.Range((int)ArenaInfo.CurrentBottom + 4, (int)ArenaInfo.CurrentTop - 21);

                for (int y = (int)ArenaInfo.CurrentBottom; y < (int)ArenaInfo.CurrentTop; y++)
                {
                    if (y == safeSpot) continue;
                    GameObject nail = AnyRadiance.Instance.GameObjects["Nail"].Spawn(new Vector3(wallPos, y, 0),
                        Quaternion.Euler(0, 0, wallPos < ArenaInfo.CurrentCenterX ? -90 : 90));
                    nail.GetComponent<tk2dSpriteAnimator>().Play("Nail Antic");
                    nail.GetComponent<DamageHero>().shadowDashHazard = true;
                    nails[i].Add(nail);
                }

                AnyRadiance.Instance.AudioClips["Sword Create"].PlayOneShot(transform.position);

                yield return new WaitForSeconds(0.35f);

                for (int j = 0; j < nails.Count; j++)
                {
                    foreach (GameObject nail in nails[j])
                    {
                        if (nail == null) continue;
                        //nail.GetComponent<tk2dSpriteAnimator>().Play("Fly");
                        nail.GetComponent<Rigidbody2D>().velocity = (nail.transform.rotation.z < 0 ? Vector2.right : Vector2.left) * 10;
                    }
                }

                AnyRadiance.Instance.AudioClips["Sword Shoot"].PlayOneShot(transform.position);

                yield return new WaitForSeconds(0.5f);

                if (wallPos < ArenaInfo.CurrentCenterX)
                {
                    wallPos = ArenaInfo.CurrentRight;
                }
                else
                {
                    wallPos = ArenaInfo.CurrentLeft;
                }
            }
        }
    }
}
