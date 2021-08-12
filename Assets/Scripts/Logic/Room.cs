using Extensions;
using Services;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Vector2 volume = Vector2.one;

        public float Width => volume.x;
        public float Length => volume.y;

        [Inject]
        private void Construct(RoomsService roomsService)
        {
            roomsService.RegisterRoom(this);
        }

        public Vector3 Position() =>
            transform.position;

        public Vector3 GetRandomPoint() => 
            transform.position.RandomPointOnSquare(Width, Length);
    }
}