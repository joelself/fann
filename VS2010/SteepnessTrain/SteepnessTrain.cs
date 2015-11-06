using System;
using FANNCSharp;
using FannWrapper;
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

                net.SetActivationSteepnessHidden(steepness_start);
                net.SetActivationSteepnessOutput(steepness_start);
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
                            net.SetActivationSteepnessHidden(steepness_start);
                            net.SetActivationSteepnessOutput(steepness_start);
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
            const float desired_error = 0.001F;
            const uint max_epochs = 500000;
            const uint epochs_between_reports = 1000;
            DataType[] calc_out;

            using (TrainingData data = new TrainingData())
            {
                using (NeuralNet net = new NeuralNet())
                {
                    net.Create(num_layers, num_input, num_neurons_hidden, num_output);

                    data.ReadTrainFromFile("..\\..\\examples\\xor.data");

                    net.SetActivationFunctionHidden(activation_function_enum.SIGMOID_SYMMETRIC);
                    net.SetActivationFunctionOutput(activation_function_enum.SIGMOID_SYMMETRIC);

                    net.SetTrainingAlgorithm(training_algorithm_enum.TRAIN_QUICKPROP);

                    TrainOnSteepnessFile(net, "..\\..\\examples\\xor.data", max_epochs, epochs_between_reports, desired_error, 1.0F, 0.1F, 20.0F);

                    net.SetActivationFunctionHidden(activation_function_enum.THRESHOLD_SYMMETRIC);
                    net.SetActivationFunctionOutput(activation_function_enum.THRESHOLD_SYMMETRIC);

                    DataType[][] input = data.GetInput();
                    DataType[][] output = data.GetOutput();
                    for(int i = 0; i < data.LengthTrainData(); i++)
                    {
                        calc_out = net.Run(input[i]);
                        Console.WriteLine("XOR test ({0}, {1}) -> {2}, should be {3}, difference={4}",
                                           input[i][0], input[i][1], calc_out[0], output[i][0],
                                           FannAbs(calc_out[0] - output[i][0]));
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
