using GlobalEnums;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    [RequireComponent(typeof(PlayMakerFSM))]
    internal class EtherealPlatform : MonoBehaviour
    {
        private PlayMakerFSM _platFSM;
        private BoxCollider2D _trigger;
        public bool Appeared { get; private set; }
        
        private void Awake()
        {
            _platFSM = gameObject.LocateMyFSM("radiant_plat");

            _trigger = gameObject.AddComponent<BoxCollider2D>();
            _trigger.isTrigger = true;
            _trigger.size = new Vector2(100, 6);
            _trigger.offset = Vector2.up * 3;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Appeared) return;
            if (other.gameObject.layer != (int)PhysLayers.PLAYER) return;
            
            Appear();
            var abyssPit = AnyRadiance.Instance.GameObjects["Abyss Pit"];
            if (abyssPit.transform.position.y >= transform.position.y - 2) return;
            PlayerData.instance.SetHazardRespawn(transform.position + Vector3.up * 1.5f, true);
            iTween.MoveTo(abyssPit, new Vector2(abyssPit.transform.position.x, transform.position.y - 2), 2);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != (int)PhysLayers.PLAYER) return;
            Disappear();
        }

        public void Appear()
        {
            Appeared = true;
            _platFSM.SendEvent("APPEAR");
            GetComponent<SpikedPlatform>()?.UpInstant();
            float diff = 100 - _trigger.size.y;
            _trigger.size += Vector2.up * diff;
            _trigger.offset += Vector2.down * diff / 2;
        }

        public void Disappear()
        {
            _platFSM.SendEvent("DISAPPEAR");
            GetComponent<SpikedPlatform>()?.DownInstant();
        }
    }
}
