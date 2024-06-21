
using Assts.Scripts.Signals;

namespace Assets.Scripts.Signals
{
    class OnClientPatienceRanOut : ISignal
    {
        public readonly ClientController client;

        public OnClientPatienceRanOut(ClientController client)
        {
            this.client = client;
        }
    }
}
