using UnityEngine;
using Vasi;

namespace AnyRadiance
{
    internal class MovingPlatform : MonoBehaviour
    {
        private HeroController _heroController;
        private void Awake()
        {
            _heroController = HeroController.instance;
        }
        

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.name == "Knight")
            {
                foreach (var contact in other.contacts)
                {
                    if (contact.normal.y < -1)
                    {
                        return;
                    }
                }

                _heroController.SetConveyorSpeed(GetComponent<Rigidbody2D>().velocity.x);
                _heroController.SetConveyorSpeedV(GetComponent<Rigidbody2D>().velocity.y);
                _heroController.SetCState("onConveyor", true);
                _heroController.SetCState("onConveyorV", true);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.name == "Knight")
            {
                _heroController.SetConveyorSpeed(GetComponent<Rigidbody2D>().velocity.x);
                _heroController.SetConveyorSpeedV(GetComponent<Rigidbody2D>().velocity.y);
                _heroController.SetCState("onConveyor", true);
                _heroController.SetCState("onConveyorV", true);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.name == "Knight")
            {
                _heroController.SetCState("onConveyor", false);
                _heroController.SetCState("onConveyorV", false);
            }
        }
    }
}
