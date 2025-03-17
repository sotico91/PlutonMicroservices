using System;

namespace MSRecipes.Application.DTOs
{
	public class CreateRecipeDto
	{

        public string Code { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}