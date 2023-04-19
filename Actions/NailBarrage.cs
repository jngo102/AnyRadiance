using System;
using System.Collections;
using System.Collections.Generic;
using AnyRadiance.Radiance;
using UnityEngine;
using Action = HKAIFramework.Action;

namespace AnyRadiance.Radiance.Actions
{
    internal class NailBarrage : Action
    {
        private const float Radius = 5;
        private const float AnticTime = 0.5f;
        private const float InitialFlySpeed = 10;
        private const float FlyTime = 1;

        public override IEnumerator Execute()
        {
            Transform transform = Radiance.Self.transform;
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
                //nail.GetComponent<tk2dSpriteAnimator>().Play("Fly");
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
