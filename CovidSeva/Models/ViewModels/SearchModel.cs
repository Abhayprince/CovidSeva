namespace CovidSeva.Models.ViewModels
{
    public struct SearchModel
    {
        public ServiceType? ServiceType { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string Freetext { get; set; }
    }
}