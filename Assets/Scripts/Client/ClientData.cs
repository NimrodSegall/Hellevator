using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Client
{
    [Serializable]
    public class ClientData
    {
        public readonly int arrivesOnFloor;
        public readonly int departsOnFloor;
        public readonly float arrivalTime;
        public readonly int id;
        public readonly float clientPatianceTime;

        public ClientData(int arrivesOnFloor, int departsOnFloor,
            float arrivalTime, int id, float clientPatianceTime)
        {
            this.arrivesOnFloor = arrivesOnFloor;
            this.departsOnFloor = departsOnFloor;
            this.arrivalTime = arrivalTime;
            this.id = id;
            this.clientPatianceTime = clientPatianceTime;
        }
    }
}