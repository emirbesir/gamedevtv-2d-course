using UnityEngine;

public static class Constants
{
    public static class LayerIndices
    {
        public static readonly int GroundIndex = LayerMask.NameToLayer("Ground");
        public static readonly int ClimbingIndex = LayerMask.NameToLayer("Climbing");
        public static readonly int EnemyIndex = LayerMask.NameToLayer("Enemy");
        public static readonly int HazardsIndex = LayerMask.NameToLayer("Hazards");
    }

    public static class LayerMasks
    {
        public static readonly int Ground = 1 << LayerIndices.GroundIndex;
        public static readonly int Climbing = 1 << LayerIndices.ClimbingIndex;
        public static readonly int Enemy = 1 << LayerIndices.EnemyIndex;
        public static readonly int HazardsLayerMask = 1 << LayerIndices.HazardsIndex;
    }

    public static class PlayerAnimations
    {
        public const string IsWalking = "IsWalking";
        public const string IsClimbing = "IsClimbing";
        public const string Dying = "Dying";
        public const string Attacking = "Attacking";
        public const string Jumping = "Jumping";
    }

    public static class Enemy
    {
        public const float RayLength = 0.6f;
    }

    public const float DefaultPlayerGravityScale = 2f;
}
