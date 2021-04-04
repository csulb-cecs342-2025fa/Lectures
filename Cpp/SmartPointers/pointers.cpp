// A VERY brief demo of ownership semantics in C++.
#include <iostream>
#include <memory>

using namespace std;

void Leak();
void Unique();
void Share();


int main() {
   Leak();
   Unique();
   Share();
}

void Leak() {
   int *pi = new int(10);
   // pi is on the stack, pointing to a value on the heap. when Leak terminates,
   // its locals on the stack, including pi, are popped. There is now no pointer
   // that remembers where this integer value was allocated, and so it will 
   // never be freed. This is a memory leak.
}

void Unique() {
   // Unique ownership creates a stack variable that will automatically destroy
   // a heap value that it owns, when the stack variable is popped.
   std::unique_ptr<int> u = std::make_unique<int>(5);
   cout << *u << endl; // prints 5.

   // Unique pointers cannot be copied.
   // std::unique_ptr<int> u2 = u; // ERROR: cannot copy a unique_ptr.

   if (true) {
      // but they CAN be *moved* to another unique_ptr.
      std::unique_ptr<int> u3 = std::move(u);

      // u is now null; dereferencing it will give a null pointer exception.

      // u3 goes out of scope on the next line. At that point, the heap value it owns
      // will be freed automatically.
   }
   
   // When u goes out of scope at the end of this function, it will get popped;
   // but because it no longer owns a heap value, nothing else happens.
}

void Share() {
   // A shared_ptr uses reference counting to delete the heap value when all
   // references to it are lost.

   std::shared_ptr<int> x = std::make_shared<int>(100);

   if (true) {
      // Shared ownership can be copied to create a new share to the original value.
      std::shared_ptr<int> y = x;
      // now there are two references to the int 100.
      // when y goes out of scope, the reference count to the int is reduced.
      // but because there is still one reference, it is not freed.
   }

   cout << *x << endl; // not a null pointer; the 100 is still there.

   // When x goes out of scope next, the reference count on the heap int is reduced
   // to 0, and then it is automatically destroyed.
}