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
namespace XorTrain
{
    class XorTrain
    {

        static int PrintCallback(NeuralNet net, TrainingData train, uint max_epochs, uint epochs_between_reports, float desired_error, uint epochs, Object user_data)
        {
            Console.WriteLine("Epochs     {0:8}. Current Error: {1:-8}", epochs, net.MSE);
            return 0;
        }

        static void XorTest()
        {
            Console.WriteLine("\nXOR test started.");
            
            const float learning_rate = 0.7f;
            const uint num_layers = 3;
            const uint num_input = 2;
            const uint num_hidden = 3;
            const uint num_output = 1;
            const float desired_error = 0.001f;
            const uint max_iterations = 300000;
            const uint iterations_between_reports = 1000;

            Console.WriteLine("\nCreating network.");

            using (NeuralNet net = new NeuralNet(num_layers, num_input, num_hidden, num_output))
            {
                net.LearningRate = learning_rate;

                net.ActivationSteepnessHidden = 1.0F;
                net.ActivationSteepnessOutput = 1.0F;

                net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC_STEPWISE;
                net.ActivationFunctionOutput = ActivationFunction.SIGMOID_SYMMETRIC_STEPWISE;

                // Output network type and parameters
                Console.Write("\nNetworkType                         :  ");
                switch (net.NetworkType)
                {
                    case NetworkType.LAYER:
                        Console.WriteLine("LAYER");
                        break;
                    case NetworkType.SHORTCUT:
                        Console.WriteLine("SHORTCUT");
                        break;
                    default:
                        Console.WriteLine("UNKNOWN");
                        break;
                }
                net.PrintParameters();

                Console.WriteLine("\nTraining network.");

                using (TrainingData data = new TrainingData())
                {
                    if (data.ReadTrainFromFile("..\\..\\examples\\xor.data"))
                    {
                        // Initialize and train the network with the data
                        net.InitWeights(data);

                        Console.WriteLine("Max Epochs {0:8}. Desired Error: {1:-8}", max_iterations, desired_error);
                        net.SetCallback(PrintCallback, null);
                        net.TrainOnData(data, max_iterations, iterations_between_reports, desired_error);

                        Console.WriteLine("\nTesting network.");

                        for (uint i = 0; i < data.TrainDataLength; i++)
                        {
                            // Run the network on the test data
                            DataType[] calc_out = net.Run(data.Input[i]);

                            Console.WriteLine("XOR test ({0}, {1}) -> {2}, should be {3}, difference = {4}",
                                data.GetTrainInput(i)[0].ToString("+#;-#"),
                                data.GetTrainInput(i)[1].ToString("+#;-#"),
                                calc_out[0] == 0 ? 0.ToString() : calc_out[0].ToString("+#;-#"),
                                data.GetTrainOutput(i)[0].ToString("+#;-#"),
                                FannAbs(calc_out[0] - data.GetTrainOutput(i)[0]));
                        }

                        Console.WriteLine("\nSaving network.");

                        // Save the network in floating point and fixed point
                        net.Save("..\\..\\examples\\xor_float.net");
                        uint decimal_point = (uint)net.SaveToFixed("..\\..\\examples\\xor_fixed.net");
                        data.SaveTrainToFixed("..\\..\\examples\\xor_fixed.data", decimal_point);

                        Console.WriteLine("\nXOR test completed.");
                    }
                }
            }
        }
        static int Main(string[] args)
        {
            try
            {
                XorTest();
            }
            catch
            {
                Console.Error.WriteLine("\nAbnormal exception.");
            }
            Console.ReadKey();
            return 0;
        }

        static DataType FannAbs(DataType value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }
}
