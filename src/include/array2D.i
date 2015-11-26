%define %arrayarray_accessor(TYPE,TYPE2,NAME)
%inline %{
typedef struct _##NAME{
private:
    TYPE **array;
public:
    _##TYPE2* Get(int index) {
      return _##TYPE2::FromPointer(array[index]);
    }
    TYPE Get(int x, int y) {
      return array[x][y];
    }
    static _##NAME *FromPointer(TYPE **t) {
    #ifdef __cplusplus
      _##NAME * newStruct = new _##NAME();
    #else
      _##NAME * newStruct = (_##NAME *) calloc(1, sizeof(_##NAME));
    #endif
      newStruct->array = t;
      return newStruct;
    }
    TYPE** Cast() {
        return array;
    }
} NAME;

%}
%enddef

%define %array_accessor(TYPE,TYPE2,NAME)
%inline %{
struct _##TYPE2;
typedef struct _##NAME{
    friend _##TYPE2;
private:
    TYPE *array;
public:
    TYPE Get(int index) {
      return array[index];
    }
    static _##NAME *FromPointer(TYPE *t) {
    #ifdef __cplusplus
      _##NAME * newStruct = new _##NAME();
    #else
      _##NAME * newStruct = (_##NAME *) calloc(1, sizeof(_##NAME));
    #endif
      newStruct->array = t;
      return newStruct;
    }
    TYPE* Cast() {
        return array;
    }
} NAME;

%}

%enddef
