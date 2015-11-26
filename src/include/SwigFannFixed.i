/*SwigFannFixed.i*/
%module fannfixed
%include "fixedfann.h"
%include "SwigFann.i"
%apply int INPUT[]  { int* input }
%apply int INPUT[]  { int* output }
%apply int INPUT[]  { int* desired_output }
%include "fann_data.h"
%include "fann_training_data_cpp.h"
%include "fann_data_cpp.h"
%include "fann_cpp.h"
%inline %{
	typedef int* int_ptr;
    typedef int fann_type;
%}
%array_class(int, intArray);
%array_class(int_ptr, intArrayArray);
%array_accessor(int, IntArrayAccessor,IntAccessor)
%arrayarray_accessor(int, IntAccessor, IntArrayAccessor);


