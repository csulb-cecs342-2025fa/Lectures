// How do these features actually work?

import java.util.*;

public class Main {
   public static void main(String[] args) {
      System.out.println("Enter a number");
      Scanner s = new Scanner(System.in);
      int selection = s.nextInt();
      // Why don't we use "new" with this int variable?
      
      List<Integer> myList;
      
      
      // Where are these objects placed in memory? Why?
      if (selection % 2 == 0)
         myList = new ArrayList<Integer>(); 
      else
         myList = new LinkedList<Integer>();
      
      
      // How does the program decide which version of this List method to call
      // at runtime? 
      myList.add(100);
      System.out.println(myList.get(0));
   }
}
