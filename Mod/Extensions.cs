namespace AnyRadiance
{
    internal static class Extensions
    {
        public static bool IsFinished(this tk2dSpriteAnimator animator)
        {
            return animator.CurrentFrame >= animator.CurrentClip.frames.Length - 1;
        }
    }
}