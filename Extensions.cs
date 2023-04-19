using System.Collections;
using UnityEngine;

namespace AnyRadiance
{
    internal static class Extensions
    {
        /// <summary>
        /// Play a tk2dSpriteAnimator until it finishes.
        /// </summary>
        /// <param name="animator">The tk2dSpriteAnimator to play an animation on</param>
        /// <param name="animName">The name of the animation to completely play</param>
        /// <returns>An IEnumerator that spans the duration of the played animation</returns>
        public static IEnumerator PlayUntilFinished(this tk2dSpriteAnimator animator, string animName)
        {
            animator.Play(animName);
            yield return new WaitUntil(() => animator.CurrentFrame >= animator.CurrentClip.frames.Length - 1);
        }

        /// <summary>
        /// Spawn an audio player and play a one shot audio clip on it, then recycle the audio player.
        /// </summary>
        /// <param name="clip">The one shot clip to play</param>
        /// <param name="location">The location to spawn the audio player at</param>
        public static void PlayOneShot(this AudioClip clip, Vector3 location)
        {
            IEnumerator PlayAndRecycle()
            {
                GameObject audioPlayer = AnyRadiance.Instance.GameObjects["Audio Player"].Spawn(location);
                var audioSource = audioPlayer.GetComponent<AudioSource>();
                audioSource.clip = clip;
                audioSource.Play();
                yield return new WaitForSeconds(clip.length);
                audioSource.clip = null;
            }
            
            GameManager.instance.StartCoroutine(PlayAndRecycle());
        }
    }
}