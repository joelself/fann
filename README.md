# `Fann C#`
#### *FANNCSharp v0.1.3 [x86](https://www.nuget.org/packages/FANNCSharp-x86) and [x64](https://www.nuget.org/packages/FANNCSharp-x64) are now on [nuget.org](http://nuget.org)*!
## FANN

**Fast Artificial Neural Network (FANN) Library** is a free open source neural network library, which implements multilayer artificial neural networks in C with support for both fully connected and sparsely connected networks.

Cross-platform execution in both fixed and floating point are supported. It includes a framework for easy handling of training data sets. It is easy to use, versatile, well documented, and fast.

## `Fann C#`
**`Fann C#`** is a wapper around FANN that lets you use the FANN libraries from C# on Windows. Currently all methods of the neural_net and training_data classes have been implemented. Additionally the new FANN parallel methods have been added as part of the NeuralNet classes.

[![Build status](https://ci.appveyor.com/api/projects/status/3dciresihh30envw/branch/master?svg=true)](https://ci.appveyor.com/project/joelself/fanncsharp/branch/master)
 [![FannCSharp](https://ghit.me/badge.svg?repo=joelself/FannCSharp)](https://ghit.me/repo/joelself/FannCSharp) [![GitHub release](https://img.shields.io/github/release/joelself/FannCSharp.svg)](https://github.com/joelself/FannCSharp/releases/latest)

 
## Current Progress
All of the FANN neural_net and training_data C++ wrapper functionality is available along with the FANN parallel functions (for fannfloat and fanndouble).
### [LATEST RELEASE](https://github.com/joelself/FannCSharp/releases/latest)

## To Install

#### From Binaries

I've changed from providing the dll's and zip's in the repository to providing them in github releases. You'll have to go to the page of the realese you want ([here is the latest release](https://github.com/joelself/FannCSharp/releases/latest)) and find and download the zip file with the dll's you want.
You have 4 options (note that debug FANN dlls are named fannfloatd.dll, fanndoubled.dll, and fannfixedd.dll):

#####For a network that supports float neural networks:
 
- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.Float.dll, fannfloat.dll|FANNCSharp.Float.dll, fannfloat.dll|
|zip file with files|[FANNCSharp.FloatDebugx86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharp.FloatDebugx64.zip](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.Float.dll, fannfloat.dll|FANNCSharp.Float.dll, fannfloat.dll|
|zip file with files|[FANNCSharp.FloatReleasex86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharp.FloatReleasex64.zip](https://github.com/joelself/FannCSharp/releases/latest)|

 - In your project add a reference to FANNCSharp.Float.dll and make sure fannfloat.dll is in the same directory or is findable through your $PATH

#####For a network that supports double neural networks:

- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.Double.dll, fanndouble.dll|FANNCSharp.Double.dll, fanndouble.dll|
|zip file with files|[FANNCSharp.DoubleDebugx86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharp.DoubleDebugx64.zip](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.Double.dll, fanndouble.dll|FANNCSharp.Double.dll, fanndouble.dll|
|zip file with files|[FANNCSharp.DoubleReleasex86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharp.DoubleReleasex64.zip](https://github.com/joelself/FannCSharp/releases/latest)|

 - In your project add a reference to FANNCSharp.Double.dll and make sure fanndouble.dll is in the same directory or is findable through your $PATH

#####For a network that supports fixed neural networks:

- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.Fixed.dll, fannfixed.dll|FANNCSharp.Fixed.dll, fannfixed.dll|
|zip file with files|[FANNCSharp.FixedDebugx86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharp.FixedDebugx64.zip](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.Fixed.dll, fannfixed.dll|FANNCSharp.Fixed.dll, fannfixed.dll|
|zip file with files|[FANNCSharp.FixedReleasex86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharp.FixedReleasex64.zip](https://github.com/joelself/FannCSharp/releases/latest)|

 - In your project add a reference to FANNCSharp.Fixed.dll and make sure fannfixed.dll is in the same directory or is findable through your $PATH

#####For a dll that supports all 3 types of neural networks for easy switching:

- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.dll, fannfloat.dll, fanndouble.dll, fannfixed.dll|FANNCSharp.dll, fannfloat.dll, fanndouble.dll, fannfixed.dll|
|zip file with files|[FANNCSharp.FixedDebugx86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharp.FixedDebugx64.zip](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|FANNCSharp.dll, fannfloat.dll, fanndouble.dll, fannfixed.dll|FANNCSharp.dll, fannfloat.dll, fanndouble.dll, fannfixed.dll|
|zip file with files|[FANNCSharpReleasex86.zip](https://github.com/joelself/FannCSharp/releases/latest)|[FANNCSharpReleasex64.zip](https://github.com/joelself/FannCSharp/releases/latest)|

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

From here you have 4 options (remember debug FANN dlls are named fannfloatd.dll, fanndoubled.dll, and fannfixedd.dll):

1. Build a dll that supports float neural networks:
  - To do this build the FANNCSharp.Float project
  - The dlls will be in .\fann\bin\\(Configuration)\\(Platform)\
  - You will need FANNCSharp.Float.dll as well as fannfloat.dll
  - In your project add a reference to FANNCSharp.Float.dll and make sure fannfloat.dll is in the same directory or is findable through your $PATH
2. Build a dll that supports double neural networks:
  - To do this build the FANNCSharp.Double project
  - The dlls will be in .\fann\bin\\(Configuration)\\(Platform)\
  - You will need FANNCSharp.Double.dll as well as fanndouble.dll
  - In your project add a reference to FANNCSharp.Double.dll and make sure fanndouble.dll is in the same directory or is findable through your $PATH
3. Build a dll that supports fixed neural networks:
  - To do this build the FANNCSharp.Fixed project
  - The dlls will be in .\fann\bin\\(Configuration)\\(Platform)\
  - You will need FANNCSharp.Fixed.dll as well as fannfixed.dll
  - In your project add a reference to FANNCSharp.Fixed.dll and make sure fannfixed.dll is in the same directory or is findable through your $PATH
4. Build a dll that supports all 3 types of neural networks for easy switching:
  - To do this build the FANNCSharp project
  - The dlls will be in .\fann\bin\\(Configuration)\\(Platform)\
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
