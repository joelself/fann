/*FannWrapper.i*/
%module FannWrapper
%{
#include "fann.h"
#include "fann_cpp.h"
#include "fann_error.h"
#include "fann_data.h"
#include "fann_data_cpp.h"
#include "fann_training_data_cpp.h"
%}
%include typemaps.i
%include "carrays.i"
%include "cpointer.i"
%include "std_string.i"
%include "fann.h"
%include "fann_error.h"
%include "fann_data.h"
%include "fann_cpp.h"
%include "fann_data_cpp.h"
%include "fann_training_data_cpp.h"


%pointer_class(fann_type, fann_type_p)
%pointer_class(float, float_p)
%array_functions(float*, float_p_array)
%array_functions(float, float_array)

%inline %{
    typedef float fann_type;
%}
