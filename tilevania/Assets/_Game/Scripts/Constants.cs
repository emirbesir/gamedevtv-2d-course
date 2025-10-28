using UnityEngine;

public static class Constants
{
    public static class Layers
    {
        public static readonly int Ground = 1 << LayerMask.NameToLayer("Ground");
        public static readonly int Climbing = 1 << LayerMask.NameToLayer("Climbing");
        public static readonly int Enemy = 1 << LayerMask.NameToLayer("Enemy");
    }

    public static class PlayerAnimations
    {
        public const string IsWalking = "IsWalking";
        public const string IsClimbing = "IsClimbing";
        public const string Dying = "Dying";
    }

    public static class Enemy
    {
        public const float RayLength = 0.6f;
    }
}
