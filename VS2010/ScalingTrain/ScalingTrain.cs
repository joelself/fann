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
namespace Example
{
    class ScalingTrain
    {
        static void Main()
        {
            const uint num_input = 3;
            const uint num_output = 1;
            const uint num_layers = 4;
            const uint num_neurons_hidden = 5;
            const float desired_error = 0.0001F;
            const uint max_epochs = 5000;
            const uint epochs_between_reports = 1000;
            using(NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, num_input, num_neurons_hidden, num_neurons_hidden, num_output))
            {
                net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
                net.ActivationFunctionOutput = ActivationFunction.LINEAR;
                net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_RPROP;
                using (TrainingData data = new TrainingData("..\\..\\..\\datasets\\scaling.data"))
                {
                    net.SetScalingParams(data, -1, 1, -1, 1);
                    net.ScaleTrain(data);

                    net.TrainOnData(data, max_epochs, epochs_between_reports, desired_error);
                    net.Save("..\\..\\..\\datasets\\scaling.net");

                    Console.ReadKey();
                }
            }
        }
        
    }
}
