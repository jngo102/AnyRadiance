using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

namespace AnyRadiance
{
    internal static class Extensions
    {
        public static IEnumerator PlayUntilFinished(this tk2dSpriteAnimator animator, string animName)
        {
            animator.Play(animName);
            yield return new WaitUntil(() => animator.CurrentFrame >= animator.CurrentClip.frames.Length - 1);
        }

        private static T Copy<T>(this T comp, T other) where T : Component
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anyt$$anonymous$$ng specific.
                }
            }
            FieldInfo[] finfos = type.GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp;
        }

        public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
        {
            return go.AddComponent<T>().Copy(toAdd);
        }

        public static void PlayOneShot(this AudioClip clip, Vector3 location)
        {
            GameObject audioPlayer = AnyRadiance.Instance.GameObjects["Audio Player"].Spawn(location);
            var audioSource = audioPlayer.GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}