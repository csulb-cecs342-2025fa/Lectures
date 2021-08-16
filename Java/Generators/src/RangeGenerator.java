import java.util.Iterator;

public class RangeGenerator implements Iterable<Integer> {
   private int mStart, mEnd, mIncrement;
   
   public RangeGenerator(int start, int end, int increment) {
      mStart = start;
      mEnd = end;
      mIncrement = increment;
   }
   
   public Iterator<Integer> iterator() {
      return new RangeIterator();
   }

   // An inner class in Java has access to the fields of the outer class.
   // Whichever instance of RangeGenerator creates a RangeIterator, that RangeGenerator's
   // fields are accessible to the RangeIterator.
   private class RangeIterator implements Iterator<Integer> { // this iterator produces Integers.
      private int mCurrent; // the next value in the range to be returned.

      public RangeIterator() {
         mCurrent = mStart;
      }

      // There are more values in the range if we have not reached the end value.
      public boolean hasNext() {
         return mCurrent < mEnd;
      }
   
      public Integer next() {
         System.out.println("Generating " + mCurrent); // for demonstration.
         int c = mCurrent;
         mCurrent += mIncrement;
         return c;
      }
   }
   
   // Demo using a RangeGenerator.
   public static void main(String[] args) {
      RangeGenerator range = new RangeGenerator(0, 5000000, 5);
     
      // Reminder: the above compiles to this:
      Iterator<Integer> itr = range.iterator();
      while (itr.hasNext()) {
         Integer i = itr.next();
         System.out.println(i);
         break;
      }
   }
}