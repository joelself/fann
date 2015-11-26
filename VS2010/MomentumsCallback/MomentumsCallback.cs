/*************************************************************************************************************/
// Same as the Momentums project, but uses callbacks for populating the training data and during
// training to demonstrate how to use the callbacks. And adds a test for the Accessor classes

using System;
using FANNCSharp;
using System.IO;
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
    class MomentumsCallback
    {
        static void Main()
        {
            const uint num_layers = 3;
	        const uint num_neurons_hidden = 96;
	        const float desired_error = 0.00007F;

            
            using (TrainingData trainData = new TrainingData())
            using (TrainingData testData = new TrainingData())
            {
                trainData.CreateTrainFromCallback(374, 48, 3, TrainingDataCallback);
                testData.CreateTrainFromCallback(594, 48, 3, TestDataCallback);

                // Test Accessor classes
                for (int i = 0; i < trainData.TrainDataLength; i++)
                {
                    Console.Write("Input {0}: ", i);
                    for (int j = 0; j < trainData.InputCount; j++)
                    {
                        Console.Write("{0}, ", trainData.InputAccessor[i][j]);
                    }
                    Console.Write("\nOutput {0}: ", i);
                    for (int j = 0; j < trainData.OutputCount; j++)
                    {
                        Console.Write("{0}, ", trainData.OutputAccessor[i][j]);
                    }
                    Console.WriteLine("");
                }

                for (float momentum = 0.0F; momentum < 0.7F; momentum += 0.1F)
                {
                    Console.WriteLine("============= momentum = {0} =============\n", momentum);
                    using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, trainData.InputCount, num_neurons_hidden, trainData.OutputCount))
                    {
                        net.SetCallback(TrainingCallback, "Hello!");

                        net.TrainingAlgorithm = TrainingAlgorithm.TRAIN_INCREMENTAL;

                        net.LearningMomentum = momentum;

                        net.TrainOnData(trainData, 20000, 500, desired_error);

                        Console.WriteLine("MSE error on train data: {0}", net.TestData(trainData));
                        Console.WriteLine("MSE error on test data: {0}", net.TestData(testData));
                    }

                }
            }
            Console.ReadKey();
        }

        static StreamReader trainingFile = null;
        static StreamReader testFile = null;
        static void TrainingDataCallback(uint number, uint inputCount, uint outputCount, DataType[] input, DataType[] output)
        {
            if (trainingFile == null)
            {
                trainingFile = new StreamReader("..\\..\\..\\datasets\\robot.train");
                trainingFile.ReadLine(); // The info on the first line is provided by the callee
            }
            if (number % 100 == 99)
            {
                System.GC.Collect(); // Make sure nothing's getting garbage-collected prematurely
                GC.WaitForPendingFinalizers();
            }
            GetDataFromStream(trainingFile, inputCount, input);
            GetDataFromStream(trainingFile, outputCount, output);
        }

        static void TestDataCallback(uint number, uint inputCount, uint outputCount, DataType[] input, DataType[] output)
        {
            if (testFile == null)
            {
                testFile = new StreamReader("..\\..\\..\\datasets\\robot.test");
                testFile.ReadLine(); // The info on the first line is provided by the callee
            }
            if (number % 100 == 99)
            {
                System.GC.Collect(); // Make sure nothing's getting garbage-collected prematurely
                GC.WaitForPendingFinalizers();
            }
            GetDataFromStream(testFile, inputCount, input);
            GetDataFromStream(testFile, outputCount, output);
        }

        static void GetDataFromStream(StreamReader file, uint count, DataType[] output)
        {
            string[] tokens = file.ReadLine().Split(new char[] { ' ' });
            for (int i = 0; i < count; i++)
            {
                output[i] = DataType.Parse(tokens[i]);
            }
        }

        static int TrainingCallback(NeuralNet net, TrainingData data, uint maxEpochs, uint epochsBetweenReports, float desiredError, uint epochs, object userData) {
            System.GC.Collect(); // Make sure nothing's getting garbage-collected prematurely
            GC.WaitForPendingFinalizers();
            Console.WriteLine("Callback: Last neuron weight: {0}, Last data input: {1}, Max epochs: {2}\nEpochs between reports: {3}, Desired error: {4}, Current epoch: {5}\nGreeting: \"{6}\"",
                                net.ConnectionArray[net.TotalConnections - 1].Weight, data.InputAccessor.Get((int)data.TrainDataLength - 1, (int)data.InputCount - 1),
                                maxEpochs, epochsBetweenReports, desiredError, epochs, userData);
            return 1;
        }
    }
}
