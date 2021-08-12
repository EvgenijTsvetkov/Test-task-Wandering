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
            var walkersInFromRoom = walkers.Where(walker => walker.CurrentRoom == from).ToList();
            var walkersInFromTo = walkers.Where(walker => walker.CurrentRoom == to).ToList();

            if (walkersInFromRoom.Count == 0 && walkersInFromTo.Count == 0)
                return;

            var allWalkers = new List<Walker>(walkersInFromRoom);
            allWalkers.AddRange(walkersInFromTo);

            var walkerWantingMoveToRoom = allWalkers[Random.Range(0, allWalkers.Count)];
            walkerWantingMoveToRoom.CurrentRoom = walkersInFromRoom.Contains(walkerWantingMoveToRoom) ? to : from;

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