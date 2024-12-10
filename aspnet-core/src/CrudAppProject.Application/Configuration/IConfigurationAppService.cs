using System.Threading.Tasks;
using CrudAppProject.Configuration.Dto;

namespace CrudAppProject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
