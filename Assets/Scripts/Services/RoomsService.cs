using System.Collections.Generic;
using Logic;
using UnityEngine;

namespace Services
{
    public class RoomsService
    {
        private readonly List<Room> rooms = new List<Room>();

        public void RegisterRoom(Room door) =>
            rooms.Add(door);
        
        public Room FindNearestRoom(Vector3 finderPosition, Room currentRoom)
        {
            if (rooms.Count == 0)
                return null;

            Room foundRoom = rooms[0];
            float minDistance = Vector3.Distance(foundRoom.Position(), finderPosition);
            
            if (foundRoom == currentRoom) 
                minDistance += currentRoom.Length + currentRoom.Width;
            
            foreach (var room in rooms)
            {
                if (room == currentRoom)
                    continue;
                
                float distance = Vector3.Distance(room.Position(), finderPosition);

                if (!(distance < minDistance))
                    continue;

                minDistance = distance;
                foundRoom = room;
            }

            return foundRoom;
        }
    }
}