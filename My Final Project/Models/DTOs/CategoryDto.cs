namespace My_Final_Project.Models.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateCategoryRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

