%module SwigFann
%rename(ActivationFunction) FANN::activation_function_enum;
%rename(ActivationFunction) activation_function_enum;
%rename(ErrorFunction) error_function_enum;
%rename(NetworkType) network_type_enum;
%rename(StopFunction) stop_function_enum;
%rename(TrainingAlgorithm) training_algorithm_enum;
%rename(Connection) fann_connection;
%{
#include "fann.h"
#include "fann_cpp.h"
#include "fann_data.h"
#include "fann_train.h"
#include "fann_data_cpp.h"
#include "fann_training_data_cpp.h"
#include "stdio.h"
%}
%rename(to_fann) operator struct fann*();
%rename(to_fann_train_data) operator struct fann_train_data*();
%csconstvalue(0) LINEAR;
%csconstvalue(0) ERRORFUNC_LINEAR;
%csconstvalue(0) LAYER;
%csconstvalue(0) STOPFUNC_MSE;
%csconstvalue(0) TRAIN_INCREMENTAL;
%ignore fann_neuron;
%ignore fann_layer;
%ignore fann_error;
%ignore fann;
%ignore fann_train_enum;
%ignore fann_activationfunc_enum;
%ignore fann_errorfunc_enum;
%ignore fann_stopfunc_enum;
%ignore fann_nettype_enum;
%ignore FANN_NETTYPE_NAMES;
%ignore FANN_STOPFUNC_NAMES;
%ignore FANN_ERRORFUNC_NAMES;
%ignore FANN_ACTIVATIONFUNC_NAMES;
%ignore FANN_TRAIN_NAMES;
%typemap(in) void* user_data %{ $1 = (void*)$input;%}
%typemap(imtype, out="global::System.IntPtr") void* user_data "global::System.IntPtr" 
%typemap(cstype, out="global::System.IntPtr") void* user_data "global::System.IntPtr"
%typemap(csin) void* user_data "$csinput"
%typemap(ctype) int (*)(FANN::neural_net&, FANN::training_data&, unsigned int, unsigned int, float, unsigned int, void*) "int (*)(FANN::neural_net&, FANN::training_data&, unsigned int, unsigned int, float, unsigned int, void*)" 
%typemap(in) int (*)(FANN::neural_net&, FANN::training_data&, unsigned int, unsigned int, float, unsigned int, void*) %{ $1 = (int (*)(FANN::neural_net&, FANN::training_data&, unsigned int, unsigned int, float, unsigned int, void*))$input;%} 
%typemap(imtype, out="global::System.IntPtr") int (*)(FANN::neural_net&, training_data&, unsigned int, unsigned int, float, unsigned int, void*) "global::System.IntPtr" 
%typemap(cstype, out="global::System.IntPtr") int (*)(FANN::neural_net&, FANN::training_data&, unsigned int, unsigned int, float, unsigned int, void*) "global::System.IntPtr"
%typemap(csin) int (*)(FANN::neural_net&, FANN::training_data&, unsigned int, unsigned int, float, unsigned int, void*) "$csinput"
%typemap(ctype) FANN::callback_type "FANN::callback_type" 
%typemap(in) FANN::callback_type %{ $1 = (FANN::callback_type)$input;%} 
%typemap(imtype, out="global::System.IntPtr") FANN::callback_type "global::System.IntPtr" 
%typemap(cstype, out="global::System.IntPtr") FANN::callback_type "global::System.IntPtr"
%typemap(csin) FANN::callback_type "$csinput"
%typemap(ctype) void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "void *" 
%typemap(in) void (*)(unsigned int, unsigned int, unsigned int, float *, float *) %{ $1 = (void (*)(unsigned int, unsigned int, unsigned int, float *, float *))$input;%} 
%typemap(imtype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "global::System.IntPtr" 
%typemap(cstype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "global::System.IntPtr"
%typemap(csin) void (*)(unsigned int, unsigned int, unsigned int, float *, float *) "$csinput"
%typemap(ctype) void (*)(unsigned int, unsigned int, unsigned int, double *, double *) "void *" 
%typemap(in) void (*)(unsigned int, unsigned int, unsigned int, double *, double *) %{ $1 = (void (*)(unsigned int, unsigned int, unsigned int, double *, double *))$input;%} 
%typemap(imtype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, double *, double *) "global::System.IntPtr" 
%typemap(cstype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, double *, double *) "global::System.IntPtr"
%typemap(csin) void (*)(unsigned int, unsigned int, unsigned int, double *, double *) "$csinput"
%typemap(ctype) void (*)(unsigned int, unsigned int, unsigned int, int *, int *) "void *"
%typemap(in) void (*)(unsigned int, unsigned int, unsigned int, int *, int *) %{ $1 = (void (*)(unsigned int, unsigned int, unsigned int, int *, int *))$input;%} 
%typemap(imtype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, int *, int *) "global::System.IntPtr" 
%typemap(cstype, out="global::System.IntPtr") void (*)(unsigned int, unsigned int, unsigned int, int *, int *) "global::System.IntPtr"
%typemap(csin) void (*)(unsigned int, unsigned int, unsigned int, int *, int *) "$csinput"
#define FANN_EXTERNAL /**/
#define FANN_API /**/
%include "typemaps.i"
%include "enums.swg"
%include "carrays.i"
%include "arrays_csharp.i"
%include "array2D.i"
%include "cpointer.i"
%include "std_string.i"
FILE *fopen(const char *filename, const char *mode);
int fclose ( FILE * stream );
%inline %{
typedef enum FANN::activation_function_enum ActivationFunction;
typedef enum error_function_enum ErrorFunction;
typedef enum network_type_enum NetworkType;
typedef enum stop_function_enum StopFunction;
typedef enum training_algorithm_enum TrainingAlgorithm;
%}
%array_class(unsigned int, uintArray);
%array_class(ActivationFunction, ActivationFunctionArray);
%array_class(fann_connection, ConnectionArray);
