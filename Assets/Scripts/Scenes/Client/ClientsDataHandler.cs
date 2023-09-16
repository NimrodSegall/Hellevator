using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ClientsDataHandler
{
    private List<ClientData> _clientsData = new List<ClientData>();
    public ReadOnlyCollection<ClientData> ClientsData => _clientsData.AsReadOnly();

    public void AddClientData(int arrivesOnFloor, int departsOnFloor, float arrivalTime)
    {
        var data = new ClientData(arrivesOnFloor, departsOnFloor, arrivalTime, GetNewId());
        _clientsData.Add(data);
    }

    public ClientData GetClientById(int id)
    {
        return _clientsData[id];
    }

    private int GetNewId()
    {
        return _clientsData.Count;
    }
}
