using System.Collections.Generic;
using Logic.InteractDoor;
using UnityEngine;

namespace Services
{
    public class InteractDoorsService
    {
        private readonly List<Door> doors = new List<Door>();

        public void RegisterDoor(Door door) =>
            doors.Add(door);

        public Door FindNearestDoor(Vector3 finderPosition)
        {
            if (doors.Count == 0)
                return null;

            Door foundDoor = doors[0];
            float minDistance = Vector3.Distance(foundDoor.Position(), finderPosition);

            foreach (var door in doors)
            {
                float distance = Vector3.Distance(door.Position(), finderPosition);

                if (!(distance < minDistance))
                    continue;

                minDistance = distance;
                foundDoor = door;
            }

            return foundDoor;
        }
    }
}