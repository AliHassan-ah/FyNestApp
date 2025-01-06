using CrudAppProject.EntityFrameworkCore.Seed.Email;

namespace CrudAppProject.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly CrudAppProjectDbContext _context;

        public InitialHostDbBuilder(CrudAppProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new EmailTemplateBuilder(_context, 1).Create();


            _context.SaveChanges();
        }
    }
}
