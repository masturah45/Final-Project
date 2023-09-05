namespace My_Final_Project.Models.Entities
{
    public class Issue : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Therapist> Therapists { get; set; }
    }
}
