namespace WordFrequency
{
    public class CharsRange
    {
        public CharsRange(char minIdx, char maxIdx)
        {
            minIndex = minIdx;
            maxIndex = maxIdx;
        }
        public char minIndex { get; private set; }
        public char maxIndex { get; private set; }

        public bool CharInRange(char c)
        {
            if (c >= minIndex && c <= maxIndex)
                return true;
            return false;
        }
    }
}
