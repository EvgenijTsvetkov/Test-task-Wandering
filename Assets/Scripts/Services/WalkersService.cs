using System.Collections.Generic;
using System.Linq;
using Entities;
using Logic;
using Logic.InteractDoor;
using UnityEngine;
using Zenject;

namespace Services
{
    public class WalkersService
    {
        private readonly List<Walker> walkers = new List<Walker>();

        private WalkerInteractDoor walkerInteractDoor;

        [Inject]
        private void Construct(WalkerInteractDoor walkerInteractDoor)
        {
            this.walkerInteractDoor = walkerInteractDoor;
            walkerInteractDoor.OpenDoor += OnOpenDoor;
        }

        private void OnOpenDoor(Door openedDoor, Room from, Room to)
        {
            var walkersInRooms =
                walkers
                    .Where(walker => walker.CurrentRoom == from || walker.CurrentRoom == to)
                    .ToList();

            if (walkersInRooms.Count == 0)
                return;

            var walkerWantingMoveToRoom = walkersInRooms[Random.Range(0, walkersInRooms.Count)];
            walkerWantingMoveToRoom.CurrentRoom = walkerWantingMoveToRoom.CurrentRoom == from ? to : from;

            walkerWantingMoveToRoom.MoveToRoom(openedDoor);
        }

        public void RegisterWalker(Walker walker)
        {
            if (walker as WalkerInteractDoor)
                return;

            walkers.Add(walker);
        }
    }
}