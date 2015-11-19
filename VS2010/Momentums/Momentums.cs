using System;
using System.Collections.Generic;
using System.IO;
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
                trainData.SetTrainData(getInput("..\\..\\datasets\\robot.train"), getOutput("..\\..\\datasets\\robot.train"));
                trainData.SetTrainData(getInput("..\\..\\datasets\\robot.test"), getOutput("..\\..\\datasets\\robot.test"));

                for (float momentum = 0.0F; momentum < 0.7F; momentum += 0.1F)
                {
                    Console.WriteLine("============= momentum = {0} =============\n", momentum);
                    using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, trainData.InputCount, num_neurons_hidden, trainData.OutputCount))
                    {                        
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

        private static Dictionary<string, DataType[][][]> dataInputs = new Dictionary<string, DataType[][][]>();
        private static void getAllInput(string file)
        {
            if (!dataInputs.ContainsKey(file))
            {
                string line;
                string[] tokens;
                StreamReader trainFile = new StreamReader(file);
                tokens = trainFile.ReadLine().Split(new char[] { ' ' });
                int lengthData = Int32.Parse(tokens[0]);
                int inputCount = Int32.Parse(tokens[1]);
                int outputCount = Int32.Parse(tokens[2]);
                bool inputLine = true;
                DataType[][][] allData = new DataType[2][][];
                allData[0] = new DataType[lengthData/2][];
                allData[1] = new DataType[lengthData/2][];
                int count = 0;
                while ((line = trainFile.ReadLine()) != null && count < lengthData)
                {
                    DataType[] input;
                    if (inputLine)
                    {
                        input = new DataType[inputCount];
                    }
                    else
                    {
                        input = new DataType[outputCount];
                    }
                    tokens = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        input[i] = DataType.Parse(tokens[i]);
                    }
                    allData[inputLine ? 0 : 1][count/2] = input;
                    inputLine = !inputLine;
                    count++;
                }
                dataInputs.Add(file, allData);
            }
        }
        private static DataType[][] getInput(string file)
        {
            getAllInput(file);
            return dataInputs[file][0];
        }
        private static DataType[][] getOutput(string file)
        {
            getAllInput(file);
            return dataInputs[file][1];
        }
    }
}
