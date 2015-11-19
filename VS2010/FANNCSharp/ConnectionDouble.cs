using FannWrapperDouble;
using System;
using BaseConnection = FannWrapperDouble.Connection;
/*
 * Title: FANN C# Connection double
 */
namespace FANNCSharp
{
    /* Class: Connection

        Describes a connection between two neurons and its weight

        FromNeuron - Unique number used to identify source neuron
        ToNeuron - Unique number used to identify destination neuron
        Weight - The numerical value of the weight

        See Also:
            <FANNCSharp.Double::NeuralNet::Connections>, <FANNCSharp.Double::NeuralNet::Weights>

       This structure appears in FANN >= 2.1.0
    */
    public class Connection : IDisposable
    {
        internal Connection(BaseConnection other)
        {
            connection = other;
        }
        /* Constructor: Connection
            Creates a connection with the specified parameters
            Parameters:
                fromNeuron - Unique number used to identify source neuron
                toNeuron - Unique number used to identify destination neuron
                weight - The numerical value of the weight
            Example:
              >Connection connection(2, 7, 0.5);
         */
        public Connection(uint fromNeuron, uint toNeuron, double weight)
        {
            FromNeuron = fromNeuron;
            ToNeuron = toNeuron;
            Weight = weight;
        }
        /* Method: Dispose
        
            Destructs the connection. Must be called manually.
        */
        public void Dispose()
        {
            connection.Dispose();
        }
        /* Property: FromNeuron
         Unique number used to identify source neuron
       */
        public uint FromNeuron
        {
            get
            {
                return connection.from_neuron;
            }
            set
            {
                connection.from_neuron = value;
            }
        }

        /* Property: ToNeuron
           Unique number used to identify destination neuron
         */
        public uint ToNeuron
        {
            get
            {
                return connection.to_neuron;
            }
            set
            {
                connection.to_neuron = value;
            }
        }

        /* Property: Weight
           The numerical value of the weight
         */
        public double Weight
        {
            get
            {
                return connection.weight;
            }
            set
            {
                connection.weight = value;
            }
        }

        internal BaseConnection connection { get; set; }
    }

}
