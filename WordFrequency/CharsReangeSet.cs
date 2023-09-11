namespace WordFrequency
{
    public class CharsReangeSet
    {
        List<CharsRange> charsRanges { get; set; }

        public CharsReangeSet()
        {
            charsRanges = new List<CharsRange>();
        }

        public void AddRange(CharsRange range)
        {
            charsRanges.Add(range);
        }

        public bool CharInRanges(char c)
        {
            foreach (var charsRange in charsRanges)
            {
                if (charsRange.CharInRange(c))
                    return true;
            }
            return false;
        }
    }
}
