using UnityEngine;

namespace DungeonLife.BossFlower
{
    public class BossContext
    {
        public Transform boss;
        public Rigidbody2D bossRb;

        public Transform player;
        public Rigidbody2D playerRb;

        public Transform arenaCenter;
        public HazardSpawner spawner;

        public Vector2 PlayerPos => player ? (Vector2)player.position : Vector2.zero;
        public Vector2 BossPos => boss ? (Vector2)boss.position : Vector2.zero;
        public Vector2 CenterPos => arenaCenter ? (Vector2)arenaCenter.position : BossPos;
        public Vector2 PlayerVel => playerRb ? playerRb.velocity : Vector2.zero;
        public Vector2 DirToPlayer => (PlayerPos - BossPos).sqrMagnitude < 0.0001f ? Vector2.zero : (PlayerPos - BossPos).normalized;
    }
}