/*SwigFannFixed.i*/
%module fannfixed
%include "fixedfann.h"
%include "SwigFann.i"
%inline %{
	typedef int* int_ptr;
    typedef int fann_type;
%}
%array_class(int_ptr, intArrayArray);
%array_class(int, intArray);


