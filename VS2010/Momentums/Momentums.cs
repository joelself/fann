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
    class Robot
    {
        static void Main()
        {
            const uint num_layers = 3;
	        const uint num_neurons_hidden = 96;
	        const float desired_error = 0.001F;

            
            using (TrainingData trainData = new TrainingData())
            using (TrainingData testData = new TrainingData())
            {
                trainData.ReadTrainFromFile("..\\..\\datasets\\robot.train");
                testData.ReadTrainFromFile("..\\..\\datasets\\robot.test");

                for (float momentum = 0.0F; momentum < 0.7F; momentum += 0.1F)
                {
                    Console.WriteLine("============= momentum = {0} =============\n", momentum);
                    using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, trainData.InputCount, num_neurons_hidden, trainData.OutputCount))
                    {
                        // Just testing the callback
                        //net.SetCallback(TrainingCallback, "Hello!");
                        
                        net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_INCREMENTAL;

                        net.LearningMomentum = momentum;

                        net.TrainOnData(trainData, 20000, 5000, desired_error);

                        Console.WriteLine("MSE error on train data: {0}", net.TestData(trainData));
                        Console.WriteLine("MSE error on test data: {0}", net.TestData(testData));
                    }

                }
            }
            Console.ReadKey();
        }

        static int TrainingCallback(NeuralNetFloat net, TrainingDataFloat data, uint maxEpochs, uint epochsBetweenReports, float desiredError, uint epochs, object userData) {
            Console.WriteLine("Callback: {0}, {1}, {2}, {2}, {3}, {4}, {5}, {6}", net.InputCount, data.Input[0][0], maxEpochs, epochsBetweenReports, desiredError, epochs, userData);
            return 1;
        }
    }
}
