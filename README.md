# `Fann C#`
## FANN	

*Check out the current (as of 30 Dec. 2015) [tagged release](https://github.com/joelself/FannCSharp/tree/v0.1.2) that now has debug binaries.* Or check out the [releases page](https://github.com/joelself/FannCSharp/releases/) which always has the latest release.

**Fast Artificial Neural Network (FANN) Library** is a free open source neural network library, which implements multilayer artificial neural networks in C with support for both fully connected and sparsely connected networks.

Cross-platform execution in both fixed and floating point are supported. It includes a framework for easy handling of training data sets. It is easy to use, versatile, well documented, and fast. 

## `Fann C#`
**`Fann C#`** is a wapper around FANN that lets you use the FANN libraries from C# on Windows. Currently all methods of the neural_net and training_data classes have been implemented. Additionally the new FANN parallel methods have been added as part of the NeuralNet classes.

## Current Progress
All of the FANN neural_net and training_data C++ wrapper functionality is available along with the FANN parallel functions (for fannfloat and fanndouble).

## To Install

#### From Binaries

You have 4 options:

1. For a network that supports float neural networks:
  - For x64 download [FANNCSharp.Float.dll and fannfloat.dll](bin/Release/x64/FANNCSharp.FloatReleasex64.zip?raw=true)
  - For x86 download [FANNCSharp.Float.dll and fannfloat.dll](bin/Release/x86/FANNCSharp.FloatReleasex86.zip?raw=true)
  - In your project add a reference to FANNCSharp.Float.dll and make sure fannfloat.dll is in the same directory or is findable through your $PATH
2. For a network that supports double neural networks:
  - For x64 download [FANNCSharp.Double.dll and fanndouble.dll](bin/Release/x64/FANNCSharp.DoubleReleasex64.zip?raw=true)
  - For x86 download [FANNCSharp.Double.dll and fanndouble.dll](bin/Release/x86/FANNCSharp.DoubleReleasex86.zip?raw=true)
  - In your project add a reference to FANNCSharp.Double.dll and make sure fanndouble.dll is in the same directory or is findable through your $PATH
3. For a network that supports fixed neural networks:
  - For x64 download [FANNCSharp.Fixed.dll and fannfixed.dll](bin/Release/x64/FANNCSharp.FixedReleasex64.zip?raw=true)
  - For x86 download [FANNCSharp.Fixed.dll and fannfixed.dll](bin/Release/x86/FANNCSharp.FixedReleasex86.zip?raw=true)
  - In your project add a reference to FANNCSharp.Fixed.dll and make sure fannfixed.dll is in the same directory or is findable through your $PATH
4. For a dll that supports all 3 types of neural networks for easy switching:
  - For x64 download [FANNCSharpx64.zip](/bin/Release/x64/FANNCSharpReleasex64.zip?raw=true)
  - For x86 download [FANNCSharpx86.zip](/bin/Release/x86/FANNCSharpReleasex86.zip?raw=truee)
  - Extract the zip files in your project or wherever you want them to be
  - In your project add a reference to FANNCSharp.dll and make sure fannfloat.dll, fanndouble.dll and fannfixed.dll are in the same directory or are findable through your $PATH
  - To easily switch between the different types of networks do what the example projects do and add the following code to the top of your file:
```
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
```
  - Then add FANN_FIXED, FANN_DOUBLE, or FANN_FLOAT to your conditional compilation symbols (Project -> Properties -> Build -> Conditional compilation symbols)
  - If you write your code using ```DataType``` in place of the ```float```, ```double``` or ```int``` keywords you would normally use then you can easily switch network types by changing the compilation symbol and recompiling (Note there are methods and properties that some network types support, but others don't, see the documentation for a full list of each type's supported functions).

#### From Source

First you'll want to clone the repository:

`git clone https://github.com/joelself/FannCSharp.git`

Once that's finished, navigate to the VS2010 directory. In this case it would be .\fann\VS2010:

`cd VS2010`

Open the solution fann.sln

From here you have 4 options:

1. Build a dll that supports float neural networks:
  - To do this build the FANNCSharp.Float project
  - The dlls will be in .\fann\bin\(Platform)\
  - You will need FANNCSharp.Float.dll as well as fannfloat.dll
  - In your project add a reference to FANNCSharp.Float.dll and make sure fannfloat.dll is in the same directory or is findable through your $PATH
2. Build a dll that supports double neural networks:
  - To do this build the FANNCSharp.Double project
  - The dlls will be in .\fann\bin\(Platform)\
  - You will need FANNCSharp.Double.dll as well as fanndouble.dll
  - In your project add a reference to FANNCSharp.Double.dll and make sure fanndouble.dll is in the same directory or is findable through your $PATH
3. Build a dll that supports fixed neural networks:
  - To do this build the FANNCSharp.Fixed project
  - The dlls will be in .\fann\bin\(Platform)\
  - You will need FANNCSharp.Fixed.dll as well as fannfixed.dll
  - In your project add a reference to FANNCSharp.Fixed.dll and make sure fannfixed.dll is in the same directory or is findable through your $PATH
4. Build a dll that supports all 3 types of neural networks for easy switching:
  - To do this build the FANNCSharp project
  - The dlls will be in .\fann\bin\(Platform)\
  - You will need FANNCSharp.dll as well as fannfloat.dll, fanndouble.dll and fannfixed.dll
  - In your project add a reference to FANNCSharp.dll and make sure fannfloat.dll, fanndouble.dll and fannfixed.dll are in the same directory or are findable through your $PATH
  - To easily switch between the different types of networks follow the directions above in the **From Binaries** section

#### Using FANNCSharp

The main types, NeuralNet and TrainingData, are in the FANNCSharp.Float, FANNCSharp.Double, and FANNCSharp.Fixed namespaces. The types common to all types of NeuralNets are in the FANNCSharp namespace. For more detail on each of the types [see the documentation](http://joelself.github.io/FannCSharp/).

## Documentation

This wrapper's documentation can be found [here](http://joelself.github.io/FannCSharp/). While the documentation for FANN itself can be found [here](http://libfann.github.io/fann/docs/files/fann-h.html).

## To Learn More About FANN

To get started with FANN, go to the [FANN help site](http://leenissen.dk/fann/wp/help/), which will include links to all the available resources. FannCSharp closely mirrors the C++ interfaces: [neural_net](http://libfann.github.io/fann/docs/files/fann_cpp-h.html) and [training_data](http://libfann.github.io/fann/docs/files/fann_training_data_cpp-h.html).

For more information about FANN, please refer to the [FANN website](http://leenissen.dk/fann/wp/)
