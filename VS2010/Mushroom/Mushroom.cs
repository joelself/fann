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
    class Robot
    {
        static void Main()
        {
	        const uint num_layers = 3;
	        const uint num_neurons_hidden = 32;
	        const float desired_error = 0.0001F;
	        const uint max_epochs = 300;
	        const uint epochs_between_reports = 10;


            Console.WriteLine("Creating network.");
            using (TrainingData data = new TrainingData("..\\..\\datasets\\mushroom.train"))
            using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, data.InputCount, num_neurons_hidden, data.OutputCount))
            {
                Console.WriteLine("Training network.");

                net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
                net.ActivationFunctionOutput = ActivationFunction.SIGMOID;

                net.TrainOnData(data, max_epochs, epochs_between_reports, desired_error);

                Console.WriteLine("Testing network.");

                using (TrainingData testData = new TrainingData())
                {
                    testData.ReadTrainFromFile("..\\..\\datasets\\mushroom.test");
                    net.ResetMSE();
                    for (int i = 0; i < testData.TrainDataLength; i++)
                    {
                        // The difference between calling GetTrain[Input|Output] and calling
                        // the Input and Output properties is huge in terms of speed
                        net.Test(testData.GetTrainInput((uint)i), testData.GetTrainOutput((uint)i));
                    }

                    Console.WriteLine("MSE error on test data {0}", net.MSE);

                    Console.WriteLine("Saving network.");

                    net.Save("..\\..\\examples\\mushroom_float.net");

                    Console.ReadKey();
                }

            }
        }
    }
}
