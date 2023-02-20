using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnyRadiance
{
    internal partial class Radiance
    {
        private IEnumerator NailWalls()
        {
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
                        nail.Recycle();
                    }
                }
            }

            StartCoroutine(AccelerateNails(1.01f));

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
                        nail.GetComponent<tk2dSpriteAnimator>().Play("Fly");
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

        private const float Radius = 5;
        private const float AnticTime = 0.5f;
        private const float InitialFlySpeed = 10;
        private const float FlyTime = 1;
        private IEnumerator NailBarrage()
        {
            var nails = new List<GameObject>();
            for (int angle = 60; angle <= 360; angle += 60)
            {
                GameObject nail = AnyRadiance.Instance.GameObjects["Nail"].Spawn(new Vector3(
                    transform.position.x + Radius * Mathf.Cos(angle * Mathf.Deg2Rad),
                    transform.position.y + Radius * Mathf.Sin(angle * Mathf.Deg2Rad),
                    0),
                    Quaternion.Euler(0, 0, 180));
                nails.Add(nail);
            }
            AnyRadiance.Instance.AudioClips["Tele"].PlayOneShot(transform.position);

            float currentTime = 0;
            while (currentTime < AnticTime)
            {
                Vector3 heroPos = HeroController.instance.transform.position;
                foreach (var nail in nails)
                {
                    Vector2 vector = (heroPos - nail.transform.position).normalized;
                    float faceAngle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
                    // Offset by 90 degrees because at 0 degrees the nail sprite is pointing up
                    nail.transform.SetRotationZ(faceAngle - 90);
                }
                currentTime += Time.deltaTime;
                yield return null;
            }

            foreach (var nail in nails)
            {
                nail.GetComponent<Rigidbody2D>().velocity = new Vector2(
                    Mathf.Cos((nail.transform.eulerAngles.z + 90) * Mathf.Deg2Rad),
                    Mathf.Sin((nail.transform.eulerAngles.z + 90) * Mathf.Deg2Rad)).normalized * InitialFlySpeed;
                nail.GetComponent<tk2dSpriteAnimator>().Play("Fly");
            }

            currentTime = 0;
            while (currentTime < FlyTime)
            {
                foreach (var nail in nails)
                {
                    nail.GetComponent<Rigidbody2D>().velocity *= 1.01f;
                }
                currentTime += Time.deltaTime;
                yield return null;
            }

            foreach (var nail in nails)
            {
                nail.Recycle();
            }
        }
    }
}