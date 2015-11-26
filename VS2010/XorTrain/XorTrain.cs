using System;
using FANNCSharp;
#if FANN_FIXED
using FANNCSharp.Fixed;
using DataType = System.Int32;
#elif FANN_DOUBLE
using FANNCSharp.Double;
using DataType = System.Double;
#else
using FANNCSharp.Float;
using DataType = System.Single;
#endif
namespace XorTrain
{
    class XorTrain
    {
        static void Main(string[] args)
        {
            DataType[] calc_out;
            const uint num_input = 2;
            const uint num_output = 1;
            const uint num_layers = 3;
            const uint num_neurons_hidden = 3;
            const float desired_error =  0;
            const uint max_epochs = 1000;
            const uint epochs_between_reports = 10;

            int decimal_point;

            Console.WriteLine("Creating network.");
            using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, num_input, num_neurons_hidden, num_output))
            using (TrainingData data = new TrainingData("..\\..\\..\\examples\\xor.data"))
            {
                net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
                net.ActivationFunctionOutput = ActivationFunction.SIGMOID_SYMMETRIC;

                net.TrainStopFunction = StopFunction.STOPFUNC_BIT;
                net.BitFailLimit = 0.01F;

                net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_RPROP;

                net.InitWeights(data);

                Console.WriteLine("Training network.");
                net.TrainOnData(data, max_epochs, epochs_between_reports, desired_error);

                Console.WriteLine("Testing network");
                // Keep a copy of the inputs and outputs so that we don't call TrainingData.Input
                // and TrainingData.Output multiple times causing a copy of all the data on each
                // call. An alternative is to use the Input/OutputAccessors which are fast with 
                // repeated calls to get data and can be cast to arrays with the Array property
                DataType[][] input = data.Input;
                DataType[][] output = data.Output;
                for (int i = 0; i < data.TrainDataLength; i++)
                {
                    calc_out = net.Run(input[i]);
                    Console.WriteLine("XOR test ({0},{1}) -> {2}, should be {3}, difference={4}",
                                        input[i][0], input[i][1], calc_out[0], output[i][0],
                                        FannAbs(calc_out[0] - output[i][0]));
                }

                Console.WriteLine("Saving network.\n");

                net.Save("..\\..\\..\\examples\\xor_float.net");

                decimal_point = net.SaveToFixed("..\\..\\..\\examples\\xor_fixed.net");
                data.SaveTrainToFixed("..\\..\\..\\examples\\xor_fixed.data", (uint)decimal_point);

                Console.ReadKey();
            }
        }

        static float FannAbs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }
}
