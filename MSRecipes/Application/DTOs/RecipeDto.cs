using System;

namespace MSRecipes.Application.DTOs
{
	public class RecipeDto
	{
        public int Id { get; set; }
        public string Code { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; }
    }
}