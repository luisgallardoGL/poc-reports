using StarWars.Characters;

namespace StarWars.Reports
{
    public class Association : ISearchResult
    {

        public int id { get; set; }
        public string name { get; set; }
        public string legalName { get; set; }
        public string liveReason { get; set; }

    }
}
