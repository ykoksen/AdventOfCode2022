namespace Logic.Day15
{
    public class Section
    {
        public int Lower { get; set; }
        public int Upper { get; set; }

        public Section(int lower, int upper)
        {
            Lower = lower;
            Upper = upper;
        }

        public bool TryAdd(Section other)
        {
            if (other.Lower > Upper || other.Upper < Lower)
                return false;

            if (other.Lower >= Lower && other.Lower <= Upper)
            {
                if (other.Upper > Upper)
                    Upper = other.Upper;

                return true;
            }

            if (other.Upper >= Lower && other.Upper <= Upper)
            {
                if (other.Lower < Lower)
                    Lower = other.Lower;

                return true;
            }

            if (other.Upper >= Upper && other.Lower <= Lower)
            {
                Lower = other.Lower;
                Upper = other.Upper;

                return true;
            }

            throw new Exception("This should not happen");
        }



        public int Count => Upper - Lower + 1;
    }

    public class SectionList
    {
        public HashSet<Section> Sections = new HashSet<Section>();

        public void Add(Section section)
        {
            var addedTo = Sections.FirstOrDefault(x => x.TryAdd(section));

            if (addedTo != null)
            {
                Sections.Remove(addedTo);
                Add(addedTo);
            }
            else
            {
                Sections.Add(section);
            }
        }

        public int TotalCount() => Sections.Sum(x => x.Count);       
            
    }
}
