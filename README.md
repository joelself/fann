# `FANN C#`
#### Interested in FANN C# for .NET Core? Show your support in [this issue](https://github.com/joelself/FANNCSharpCore/issues/1) on the new FANN C# Core repository.
#### *`FANN C#` v0.1.7 [x86](https://www.nuget.org/packages/FANNCSharp-x86) and [x64](https://www.nuget.org/packages/FANNCSharp-x64) are now on [nuget.org](http://nuget.org)*!
## FANN

**Fast Artificial Neural Network (FANN) Library** is a free open source neural network library, which implements multilayer artificial neural networks in C with support for both fully connected and sparsely connected networks.

Cross-platform execution in both fixed and floating point are supported. It includes a framework for easy handling of training data sets. It is easy to use, versatile, well documented, and fast.

## `FANN C#`
**FANN C#** is a wapper around FANN that lets you use the FANN libraries from C# on Windows.

[![AppVeyor branch](https://img.shields.io/appveyor/ci/gruntjs/grunt/master.svg)](https://ci.appveyor.com/project/joelself/fanncsharp/branch/master)
 [![FannCSharp](https://ghit.me/badge.svg?repo=joelself/FannCSharp)](https://ghit.me/repo/joelself/FannCSharp) [![GitHub release](https://img.shields.io/github/release/joelself/FannCSharp.svg)](https://github.com/joelself/FannCSharp/releases/latest) [![NuGet](https://img.shields.io/nuget/dt/FANNCSharp-x64.svg)](https://www.nuget.org/packages/FANNCSharp-x64) [![NuGet](https://img.shields.io/nuget/dt/FANNCSharp-x86.svg)](https://www.nuget.org/packages/FANNCSharp-x86)

 
## Current Progress
All of the FANN `neural_net` and `training_data` C++ wrapper functionality is available along with the FANN parallel functions (for `fannfloat` and `fanndouble`).
### [LATEST RELEASE](https://github.com/joelself/FannCSharp/releases/latest)

## To Install

#### From Binaries

I've changed from providing the dll's and zip's in the repository to providing them in GitHub releases. You'll have to go to the page of the realese you want ([here is the latest release](https://github.com/joelself/FannCSharp/releases/latest)) and find and download the zip file with the dll's you want.
You have 4 options (note that debug FANN dlls are named `fannfloatd.dll`, `fanndoubled.dll`, and `fannfixedd.dll`):

#####For a network that supports float neural networks:
 
- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.Float.dll`, `fannfloatd.dll`|`FANNCSharp.Float.dll`, `fannfloatd.dll`|
|zip file with files|[`FANNCSharp.FloatDebugx86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharp.FloatDebugx64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.Float.dll`, `fannfloat.dll`|`FANNCSharp.Float.dll`, `fannfloat.dll`|
|zip file with files|[`FANNCSharp.FloatReleasex86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharp.FloatReleasex64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|

 - In your project add a reference to `FANNCSharp.Float.dll` and make sure `fannfloat[d].dll` is in the same directory or is findable through your `$PATH`

#####For a network that supports double neural networks:

- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.Double.dll`, `fanndoubled.dll`|`FANNCSharp.Double.dll`, `fanndoubled.dll`|
|zip file with files|[`FANNCSharp.DoubleDebugx86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharp.DoubleDebugx64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.Double.dll`, `fanndouble.dll`|`FANNCSharp.Double.dll`, `fanndouble.dll`|
|zip file with files|[`FANNCSharp.DoubleReleasex86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharp.DoubleReleasex64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|

 - In your project add a reference to `FANNCSharp.Double.dll` and make sure `fanndouble[d].dll` is in the same directory or is findable through your `$PATH`

#####For a network that supports fixed neural networks:

- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.Fixed.dll`, `fannfixedd.dll`|`FANNCSharp.Fixed.dll`, `fannfixedd.dll`|
|zip file with files|[`FANNCSharp.FixedDebugx86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharp.FixedDebugx64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.Fixed.dll`, `fannfixed.dll`|`FANNCSharp.Fixed.dll`, `fannfixed.dll`|
|zip file with files|[`FANNCSharp.FixedReleasex86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharp.FixedReleasex64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|

 - In your project add a reference to `FANNCSharp.Fixed.dll` and make sure `fannfixed[d].dll` is in the same directory or is findable through your `$PATH`

#####For a dll that supports all 3 types of neural networks for easy switching:

- Debug:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.dll`, `fannfloatd.dll`, `fanndoubled.dll`, `fannfixedd.dll`|`FANNCSharp.dll`, `fannfloatd.dll`, `fanndoubled.dll`, `fannfixedd.dll`|
|zip file with files|[`FANNCSharp.FixedDebugx86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharp.FixedDebugx64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|
 
- Release:

| |x86|x64|
|:-:|:-:|:-:|
|Files you need|`FANNCSharp.dll`, `fannfloat.dll`, `fanndouble.dll`, `fannfixed.dll`|`FANNCSharp.dll`, `fannfloat.dll`, `fanndouble.dll`, `fannfixed.dll`|
|zip file with files|[`FANNCSharpReleasex86.zip`](https://github.com/joelself/FannCSharp/releases/latest)|[`FANNCSharpReleasex64.zip`](https://github.com/joelself/FannCSharp/releases/latest)|

  - Extract the zip files in your project or wherever you want them to be
  - In your project add a reference to `FANNCSharp.dll` and make sure `fannfloat[d].dll`, `fanndouble[d].dll` and `fannfixed[d].dll` are in the same directory or are findable through your `$PATH`
  - To easily switch between the different types of networks do [what the example projects do](https://github.com/joelself/FannCSharp/blob/master/VS2010/MomentumsCallback/MomentumsCallback.cs) and add the following code to the top of your file:
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
  - Then add `FANN_FIXED`, `FANN_DOUBLE`, or `FANN_FLOAT` to your conditional compilation symbols (Project -> Properties -> Build -> Conditional compilation symbols)
  - If you write your code using `DataType` in place of the `float`, `double` or `int` keywords you would normally use then you can easily switch network types by changing the compilation symbol and recompiling (Note there are methods and properties that some network types support, but others don't, [see the documentation](http://joelself.github.io/FannCSharp/) for a full list of each type's supported functions).

#### From Source
##### Tools you'll need:
- swig 3
- (Optional) 7zip command line

##### Steps:

1. First you'll want to clone the repository:

 `git clone https://github.com/joelself/FannCSharp.git`

2. Once that's finished, navigate to the VS2010 directory. In this case it would be `.\fann\VS2010`:

 `cd VS2010`

3. Open the solution fann.sln

4. From here you have 4 options (remember debug FANN dlls are named `fannfloatd.dll`, `fanndoubled.dll`, and `fannfixedd.dll`):

  1. Build a dll that supports float neural networks:
    1. To do this build the `FANNCSharp.Float` project
    2. The dlls will be in `.\fann\bin\(Configuration)\(Platform)\`
    3. You will need `FANNCSharp.Float.dll` as well as `fannfloat[d].dll`
    4. In your project add a reference to `FANNCSharp.Float.dll` and make sure `fannfloat[d].dll` is in the same directory or is findable through your `$PATH`
  2. Build a dll that supports double neural networks:
    1. To do this build the `FANNCSharp.Double` project
    2. The dlls will be in `.\fann\bin\(Configuration)\(Platform)\`
    3. You will need `FANNCSharp.Double.dll` as well as `fanndouble[d].dll`
    4. In your project add a reference to `FANNCSharp.Double.dll` and make sure `fanndouble[d].dll` is in the same directory or is findable through your `$PATH`
 3. Build a dll that supports fixed neural networks:
    1. To do this build the `FANNCSharp.Fixed` project
    2. The dlls will be in `.\fann\bin\(Configuration)\(Platform)\`
    3. You will need `FANNCSharp.Fixed.dll` as well as `fannfixed[d].dll`
    4. In your project add a reference to `FANNCSharp.Fixed.dll` and make sure `fannfixed[d].dll` is in the same directory or is findable through your `$PATH`
 4. Build a dll that supports all 3 types of neural networks for easy switching:
    1. To do this build the `FANNCSharp` project
    2. The dlls will be in `.\fann\bin\(Configuration)\(Platform)\`
    3. You will need `FANNCSharp.dll` as well as `fannfloat[d].dll`, `fanndouble[d].dll` and `fannfixed[d].dll`
    4. In your project add a reference to `FANNCSharp.dll` and make sure `fannfloat[d].dll`, `fanndouble[d].dll` and fannfixed[d].dll are in the same directory or are findable through your `$PATH`
    5. To easily switch between the different types of networks follow the directions above in the **From Binaries** section

#### Using `FANN C#`

The main types, `NeuralNet` and `TrainingData`, are in the `FANNCSharp.Float`, `FANNCSharp.Double`, and `FANNCSharp.Fixed` namespaces. The types common to all types of NeuralNets are in the `FANNCSharp` namespace. For more detail on each of the types [see the documentation](http://joelself.github.io/FannCSharp/).

## Documentation

FANN C#'s documentation can be found [here](http://joelself.github.io/FannCSharp/). While the documentation for FANN itself can be found [here](http://libfann.github.io/fann/docs/files/fann-h.html).

## To Learn More About FANN

To get started with FANN, go to the [FANN help site](http://leenissen.dk/fann/wp/help/), which will include links to all the available resources. FANN C# closely mirrors the C++ interfaces: [neural_net](http://libfann.github.io/fann/docs/files/fann_cpp-h.html) and [training_data](http://libfann.github.io/fann/docs/files/fann_training_data_cpp-h.html).

For more information about FANN, please refer to the [FANN website](http://leenissen.dk/fann/wp/)
