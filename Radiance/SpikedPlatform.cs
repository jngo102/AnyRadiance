using System.Collections.Generic;
using GlobalEnums;
using UnityEngine;

namespace AnyRadiance.Radiance
{
    [RequireComponent(typeof(Renderer))]
    internal class SpikedPlatform : MonoBehaviour
    {
        private List<PlayMakerFSM> _spikeFSMs = new();

        private void Awake()
        {
            var renderer = GetComponent<Renderer>();

            GameObject spikePrefab = AnyRadiance.Instance.GameObjects["Spike"];
            Bounds bounds = renderer.bounds;
            for (float i = bounds.min.x; i < bounds.max.x; i += (bounds.max.x - bounds.min.x) / 4)
            {
                var spike = spikePrefab.Spawn(transform);
                spike.name = "Plat Spike " + i;
                spike.transform.localPosition = new Vector2(i - bounds.center.x + 0.25f, bounds.max.y - bounds.center.y + 1);
                Destroy(spike.LocateMyFSM("Hero Saver"));
                spike.GetComponent<DamageHero>().damageDealt = 2;
                var spikeFSM = spike.LocateMyFSM("Control");
                _spikeFSMs.Add(spikeFSM);
            }
        }

        /// <summary>
        /// Activate platform spikes slowly.
        /// </summary>
        public void Up()
        {
            foreach (var fsm in _spikeFSMs)
            {
                fsm.SendEvent("UP");
            }
        }

        /// <summary>
        /// Activate platform spikes instantly.
        /// </summary>
        public void UpInstant()
        {
            foreach (var fsm in _spikeFSMs)
            {
                fsm.transform.Find("Floor Antic").GetComponent<MeshRenderer>().enabled = true;
                fsm.SetState("Spike Up");
            }
        }

        /// <summary>
        /// Deactivate platform spikes slowly.
        /// </summary>
        public void Down()
        {
            foreach (var fsm in _spikeFSMs)
            {
                fsm.SendEvent("DOWN");
            }
        }

        /// <summary>
        /// Deactivate platform spikes instantly.
        /// </summary>
        public void DownInstant()
        {
            foreach (var fsm in _spikeFSMs)
            {
                fsm.SetState("Down");
            }
        }
    }
}
