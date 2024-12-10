using System.ComponentModel.DataAnnotations;

namespace CrudAppProject.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}