using FannWrapperDouble;
namespace FANNCSharp
{
    /* Class: ConnectionDouble

        Describes a connection between two neurons and its weight

        FromNeuron - Unique number used to identify source neuron
        ToNeuron - Unique number used to identify destination neuron
        Weight - The numerical value of the weight

        See Also:
            <NeuralNetDouble.ConnectionArray>, <NeuralNetDouble.WeightArray>

       This structure appears in FANN >= 2.1.0
    */
    public class ConnectionDouble
    {
        internal ConnectionDouble(Connection other)
        {
            connection = other;
        }

        /* Unique number used to identify source neuron */
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

        /* Unique number used to identify destination neuron */
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

        /* The numerical value of the weight */
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

        internal Connection connection { get; set; }
    }

}
