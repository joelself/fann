using System;
using FANNCSharp;
#if FANN_FIXED
using NeuralNet = FANNCSharp.NeuralNetFixed;
using TrainingData = FANNCSharp.TrainingDataFixed;
using DataType = System.Int32;
#elif FANN_DOUBLE
using NeuralNet = FANNCSharp.NeuralNetDouble;
using TrainingData = FANNCSharp.TrainingDataDouble;
using DataType = System.Double;
#else
using NeuralNet = FANNCSharp.NeuralNetFloat;
using TrainingData = FANNCSharp.TrainingDataFloat;
using DataType = System.Single;
#endif
namespace Example
{
    class SteepnessTrain
    {
        private static void TrainOnSteepnessFile(NeuralNet net, string filename,
                             uint max_epochs, uint epochs_between_reports,
                             float desired_error, float steepness_start,
                             float steepness_step, float steepness_end)
        {
            float error;
            using (TrainingData data = new TrainingData())
            {
                data.ReadTrainFromFile(filename);

                if (epochs_between_reports != 0)
                {
                    Console.WriteLine("Max epochs {0}. Desired error: {1}", max_epochs.ToString("00000000"), desired_error.ToString("0.0000000000"));
                }

                net.ActivationSteepnessHidden = steepness_start;
                net.ActivationSteepnessOutput = steepness_start;
                for (int i = 0; i <= max_epochs; i++)
                {
                    error = net.TrainEpoch(data);

                    if(epochs_between_reports != 0 && i % epochs_between_reports == 0 || i == max_epochs || i == 1 || error < desired_error)
                    {
                        Console.WriteLine("Epochs     {0}. Current error: {1}", i.ToString("00000000"), error.ToString("0.0000000000"));
                    }

                    if(error < desired_error)
                    {
                        steepness_start += steepness_end;
                        if(steepness_start <= steepness_end)
                        {
                            Console.WriteLine("Steepness: {0}", steepness_start);
                            net.ActivationSteepnessHidden = steepness_start;
                            net.ActivationSteepnessOutput = steepness_start;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        static void Main()
        {
            const uint num_input = 2;
            const uint num_output = 1;
            const uint num_layers = 3;
            const uint num_neurons_hidden = 3;
            const float desired_error = 0.00001F;
            const uint max_epochs = 500000;
            const uint epochs_between_reports = 1000;
            DataType[] calc_out;

            using (TrainingData data = new TrainingData())
            {
                using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, num_input, num_neurons_hidden, num_output))
                {
                    data.ReadTrainFromFile("..\\..\\examples\\xor.data");

                    net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
                    net.ActivationFunctionOutput = ActivationFunction.SIGMOID_SYMMETRIC;

                    net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_QUICKPROP;

                    TrainOnSteepnessFile(net, "..\\..\\examples\\xor.data", max_epochs, epochs_between_reports, desired_error, 1.0F, 0.1F, 20.0F);

                    net.ActivationFunctionHidden = ActivationFunction.THRESHOLD_SYMMETRIC;
                    net.ActivationFunctionOutput = ActivationFunction.THRESHOLD_SYMMETRIC;

                    for(uint i = 0; i < data.TrainDataLength; i++)
                    {
                        calc_out = net.Run(data.Input[i]);
                        Console.WriteLine("XOR test ({0}, {1}) -> {2}, should be {3}, difference={4}",
                                           data.GetTrainInput(i)[0], data.GetTrainInput(i)[1], calc_out[0], data.GetTrainOutput(i)[0],
                                           FannAbs(calc_out[0] - data.GetTrainOutput(i)[0]));
                    }

                    net.Save("..\\..\\examples\\xor_float.net");

                    Console.ReadKey();
                }
            }
        }

        static float FannAbs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }
}
