using System;
using System.Web.Helpers;
using System.Xml.Linq;
namespace MSRecipes.Domain
{
	public class Recipe
	{
        public int Id { get; set; }
        public string Code { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public RecipeStatus Status { get; set; }


        public void Update(int id, string code, int patientId, string description,
                       DateTime createdDate, DateTime expiryDate, RecipeStatus status)
        {
            Id = id;
            Code = code;
            PatientId = patientId;
            Description = description;
            CreatedDate = createdDate;
            ExpiryDate = expiryDate;
            Status = status;
        }
    }
}